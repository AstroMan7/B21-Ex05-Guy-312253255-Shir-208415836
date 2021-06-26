using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class FormSettings : Form
    {
        //public delegate void  


        public FormSettings()
        {
            InitializeComponent();
            
        }

        private void textBoxPlayer2CheckChanged(object sender, EventArgs e) 
        {

        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (sender == null)
                throw new InvalidOperationException("error");

            Button button = (Button)sender; 
            //start program


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

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
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


    }
}
