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
        public static Forms.MainMenu MainMenu = new Forms.MainMenu();
        public static Forms.Game Game = new Forms.Game();
        public static Forms.Room Room = new Forms.Room();

        public static void SwitchScreen(Screen screen)
        {
            MainForm.mainForm.mainPanel.Controls.Clear();
            switch (screen)
            {
                case Screen.MainMenu:
                    MainForm.mainForm.mainPanel.Controls.Add(ViewController.MainMenu);
                    break;
                case Screen.Game:
                    MainForm.mainForm.mainPanel.Controls.Add(ViewController.Game);
                    break;
                case Screen.Room:
                    MainForm.mainForm.mainPanel.Controls.Add(ViewController.Room);
                    break;
            }
        }
    }
}
