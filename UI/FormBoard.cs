using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backend;

namespace UI
{
    public partial class FormBoard : Form
    {
        private GameManager m_Game;
        private BoardButton[,] m_buttonBoardMatrix;
        private FormSettings m_SettingsForm = new FormSettings();
        private Player m_CurrentPlayer, m_OtherPlayer;

        internal class BoardButton : Button
        {
            public int Row { get; set; }
            public int Col { get; set; }

            public BoardButton(int i_Row, int i_Col) : base()
            {
                Row = i_Row;
                Col = i_Col;
            }
        }

        public FormBoard()
        {
            InitializeComponent();
            
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            m_SettingsForm.ShowDialog();
            m_buttonBoardMatrix = new BoardButton[m_SettingsForm.m_UserChoiceBoardSize, m_SettingsForm.m_UserChoiceBoardSize];
            setBoard();
            m_Game = m_SettingsForm.m_Game;
            m_CurrentPlayer = m_Game.m_player1;
            m_OtherPlayer = m_Game.m_player2;
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {
            
        }

        private void setBoard()
        {
            for (int i = 0; i < m_SettingsForm.m_UserChoiceBoardSize; i++)
            {
                for (int j = 0; j < m_SettingsForm.m_UserChoiceBoardSize; j++)
                {
                    BoardButton button= new BoardButton(i, j);
                    button.Enabled = true;
                    m_buttonBoardMatrix[i, j] = button;
                    boardLayOut.Controls.Add(button);
                    button.Click += matrixButton_Click;
                }
            }
        }

        private void matrixButton_Click(object sender, EventArgs e)
        {
            //switch caser by sender wich number of button
            BoardButton button = sender as BoardButton;
            button.Enabled = false;
            button.Text = m_CurrentPlayer.m_Sign.ToString();    // X or O

            m_Game.MakeMove(button.Row, button.Col, m_CurrentPlayer.m_Sign);

            if (m_Game.CheckForSequence(m_CurrentPlayer.m_Sign))
            {
                m_Game.SetPlayerWon(m_OtherPlayer);
                int currPoints;
                switch (m_CurrentPlayer.m_Sign)
                {
                    case Player.e_Sign.X:   // "Player1"
                        currPoints = int.Parse(score1.Text);
                        score1.Text = (++currPoints).ToString();
                        break;
                    case Player.e_Sign.O:   // "Player2"
                        currPoints = int.Parse(score2.Text);
                        score2.Text = (++currPoints).ToString();
                        break;
                }
                     

                //if (m_Game.m_board.IsFull())
                    //Console.WriteLine("Full board! It's a TIE.");
                    // SHOW BOX DIALOG WITH RESULT AND BUTTON TO STOP AND BUTTON TO CONTINUE
                //Console.WriteLine(i_Game.GetScore());
                //PromptOptionsDueToRoundEndAndGetChoice(i_Game, ref io_NoFinalQuit);
            }

            //else if(m_Game.m_board.IsFull())
            //  same but without a win

            GameManager.SwapTurns(ref m_CurrentPlayer, ref m_OtherPlayer);

        }
    }
}
