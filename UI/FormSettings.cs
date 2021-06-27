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
    public partial class FormSettings : Form
    {
        //public delegate void  
        public const int k_MaxSizeOfBoard = 9;
        public const int k_MinSizeOfBoard = 3;
        public GameManager m_Game;
        public Player m_Player1, m_Player2 = null;
        public int m_UserChoiceBoardSize = 3; // default
        private GameManager.eGameType m_GameType = GameManager.eGameType.P2C; // default


        public FormSettings()
        {
            InitializeComponent();
            // WHAT ELSE?
        }


        private void textBoxPlayer2CheckChanged(object sender, EventArgs e) 
        {
            string player2name = (sender as TextBox).Text;
            m_Player2 = new Player(Player.e_Sign.O, Player.e_InitialTurn.Second, false, player2name);

        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (sender == null)
                throw new InvalidOperationException("error");
            Visible = true;
            if (m_Player1 == null || (m_Player2 != null && m_Player2.m_Name == ""))
            {
                DialogResult dialog;
                StringBuilder ErrorToUser;
                ErrorToUser = new StringBuilder();
                ErrorToUser.AppendLine("please enter a valid player name");
                dialog = MessageBox.Show(ErrorToUser.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Board gameBoard = new Board(m_UserChoiceBoardSize);
                m_Game = new GameManager(m_GameType, m_UserChoiceBoardSize, gameBoard, m_Player1, m_Player2);
                Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string player1name = (sender as TextBox).Text;
            m_Player1 = new Player(Player.e_Sign.X, Player.e_InitialTurn.First, false, player1name);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {// add second player (not computer)
            CheckBox box = sender as CheckBox;
            m_GameType = box.Checked ? GameManager.eGameType.P2P : GameManager.eGameType.P2C;
            textBoxNamePlayer2.Enabled = box.Checked ? true : false;
            textBoxNamePlayer2.ReadOnly = box.Checked ? false : true;
            textBoxNamePlayer2.Text = box.Checked ? "" : "[Computer]";
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string player2name = (sender as TextBox).Text;
            if(player2name != null)
            {
                m_Player2 = new Player(Player.e_Sign.O, Player.e_InitialTurn.Second, false, player2name);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown cols = sender as NumericUpDown;
            m_UserChoiceBoardSize = (int)cols.Value;
            numericUpDownRows.Value = cols.Value;
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown rows = sender as NumericUpDown;
            m_UserChoiceBoardSize = (int)rows.Value;
            numericUpDownCols.Value = rows.Value;
        }
    }
}
