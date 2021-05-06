using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saboteur
{
    public enum Screen
    {
        MainMenu,
        Room,
        Game
    }
    class ViewController
    {
        private Forms.MainMenu MainMenu;
        private Forms.Game Game;
        private Forms.Room Room;

        public ViewController()
        {
            MainMenu = new Forms.MainMenu();
            Game = new Forms.Game();
            Room = new Forms.Room();
        }

        public void switchScreen(Screen screen)
        {
            switch (screen)
            {
                case Screen.MainMenu:
                    MainForm.mainForm.mainPanel.Controls.Add(MainMenu);
                    break;
                case Screen.Game:
                    MainForm.mainForm.mainPanel.Controls.Add(Game);
                    break;
                case Screen.Room:
                    MainForm.mainForm.mainPanel.Controls.Add(Room);
                    break;
            }
        }
    }
}
