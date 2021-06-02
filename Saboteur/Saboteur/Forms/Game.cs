using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saboteur.Forms
{
    public partial class Game : UserControl
    {
        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            panDeck.BackColor = Color.FromArgb(0, 0, 0, 0);
        }
    }
}
