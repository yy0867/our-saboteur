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
    public partial class Room : UserControl
    {
        public Room()
        {
            InitializeComponent();
        }

        private void Room_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (playerLantern0.Image == Properties.Resources.light_off)
                playerLantern0.Image = Properties.Resources.light_on;
            else
                playerLantern0.Image = Properties.Resources.light_off;
        }
    }
}
