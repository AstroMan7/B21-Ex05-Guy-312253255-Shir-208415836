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
            Visible = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //m_SettingsForm.Visible = true;
            m_SettingsForm.ShowDialog();
            Visible = true;
            m_buttonBoardMatrix = new BoardButton[m_SettingsForm.m_UserChoiceBoardSize, m_SettingsForm.m_UserChoiceBoardSize];
            setBoard();
            m_Game = m_SettingsForm.m_Game;
            labelPlayer1.Text = m_Game.m_player1.m_Name;
            labelPlayer1.Text += ":";
            labelPlayer2.Text = m_Game.m_player2.m_Name;
            labelPlayer2.Text += ":";
            m_CurrentPlayer = m_Game.m_player1;
            m_OtherPlayer = m_Game.m_player2;
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {
            
        }

        private void setBoard()
        {
            int rootSize = m_SettingsForm.m_UserChoiceBoardSize;

            for (int i = 0; i < rootSize; i++)
            {
                for (int j = 0; j < rootSize; j++)
                {
                    BoardButton button= new BoardButton(i, j);
                    m_buttonBoardMatrix[i, j] = button;
                    button.Width = (boardLayOut.Width - rootSize*boardLayOut.Margin.Horizontal) / rootSize;
                    button.Height = (boardLayOut.Height - rootSize * boardLayOut.Margin.Vertical) / rootSize;
                    button.Enabled = true;
                    button.Click += matrixButton_Click;
                    boardLayOut.Controls.Add(button);
                }
            }
        }

        private void matrixButton_Click(object sender, EventArgs e)
        {
            //switch caser by sender wich number of button
            BoardButton button = sender as BoardButton;
            int row=button.Row, col=button.Col;
            button.Enabled = false;
            button.Text = m_CurrentPlayer.m_Sign.ToString();    // X or O

            m_Game.MakeMove(row, col, m_CurrentPlayer.m_Sign);

            responseToMove();

            GameManager.SwapTurns(ref m_CurrentPlayer, ref m_OtherPlayer);

            if (m_CurrentPlayer.IsComputer())
            {
                m_Game.AIChooseComputerCordinates(out row, out col, m_SettingsForm.m_UserChoiceBoardSize, m_Game.m_board);
                m_Game.MakeMove(row, col, m_CurrentPlayer.m_Sign);
                button = m_buttonBoardMatrix[row, col];
                button.Enabled = false;
                button.Text = m_CurrentPlayer.m_Sign.ToString();    // X or O

                responseToMove();

                GameManager.SwapTurns(ref m_CurrentPlayer, ref m_OtherPlayer);
            }
        }

        private void responseToMove()
        {
            DialogResult dialogRes;
            string question = "Would you like to play another round?";
            StringBuilder dialogText;

            if (m_Game.CheckForSequence(m_CurrentPlayer.m_Sign))
            {
                m_Game.SetPlayerWon(m_OtherPlayer);
                int currPoints;
                switch (m_OtherPlayer.m_Sign)
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

                if(!m_Game.m_board.IsFull()) // Win, no TIE
                {
                    dialogText = new StringBuilder("The winner is ");
                    dialogText.AppendLine(m_OtherPlayer.m_Name);
                    dialogText.Append(question);
                    dialogRes = MessageBox.Show(dialogText.ToString(), "A Win!", MessageBoxButtons.YesNo);
                    if (dialogRes == DialogResult.Yes)  // --> New round
                    {
                        m_Game.NewRound();
                        clearGameBoardMatrix();
                    }
                    if (dialogRes == DialogResult.No)   // --> End game.
                    {
                        MessageBox.Show("Thank you for playing!");
                        Close();
                    }
                }
            }

            if (m_Game.m_board.IsFull())    // with or without a WIN - T I E
            {
                dialogText = new StringBuilder("Tie!");
                dialogText.AppendLine();
                dialogText.Append(question);
                dialogRes = MessageBox.Show(dialogText.ToString(), "A Tie!", MessageBoxButtons.YesNo);
                if (dialogRes == DialogResult.Yes)  // --> New round
                {
                    m_Game.NewRound();
                    clearGameBoardMatrix();
                }
                if (dialogRes == DialogResult.No)   // --> End game.
                {
                    MessageBox.Show("Thank you for playing!");
                    Close();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void clearGameBoardMatrix()
        {
            BoardButton currButton;
            for (int i = 0; i < m_SettingsForm.m_UserChoiceBoardSize; i++)
            {
                for (int j = 0; j < m_SettingsForm.m_UserChoiceBoardSize; j++)
                {
                    currButton = m_buttonBoardMatrix[i, j];
                    currButton.Enabled = true;
                    currButton.Text = "";
                }
            }
        }
    }
}
