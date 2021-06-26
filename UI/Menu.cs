using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{

    class Menu
    {
        private readonly List<string> r_MenuList = new List<string>();

        //Main menu
        public void SetStartMenu()
        {
            r_MenuList.Add("To Play Against the computer        press 1");
            r_MenuList.Add("To Play Against another player      press 2");
            r_MenuList.Add("To exit program, any time you wish, press q");
 
        }

        public void ShowMenu()
        {
            foreach (string massage in r_MenuList)
            {
                Console.WriteLine(massage);
            }
        }
    }
}
