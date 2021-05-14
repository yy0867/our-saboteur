using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Saboteur.Forms
{
    public partial class MainMenu : UserControl
    {

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            panJoinRoom.Visible = false;
        }

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            panJoinRoom.Visible = true;

            for (int i = panJoinRoom.Top; i >= btnJoinRoom.Top; i--)
            {
                panJoinRoom.Top--;
                Thread.Sleep(1);
            }
        }
    }
}
