using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        public void switchScreen(Screen screen)
        {

        }
    }
}
