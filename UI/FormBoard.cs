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
    public partial class FormBoard : Form
    {
        
        public FormBoard()
        {
            InitializeComponent();
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            //switch caser by sender wich number of button
            Button button = (Button)sender;

            button.Enabled = false;
        }
    }
}
