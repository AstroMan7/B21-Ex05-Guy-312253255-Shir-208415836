using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UI
{
    public class Program
    {
        public static void Main()
        {
            //FormSettings form = new FormSettings();
            // Application.EnableVisualStyles();
            //UserInterface game = new UserInterface();
            //game.RunProgram();

            FormBoard gameForm = new FormBoard();
            gameForm.ShowDialog();
        }
    }
}
