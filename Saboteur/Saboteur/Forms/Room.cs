using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Saboteur.Forms
{
    public partial class Room : UserControl
    {
        const int MAX_PLAYER = 7;
        List<PictureBox> playerLanterns = new List<PictureBox>();
        bool[] isPlayer = new bool[MAX_PLAYER];
        public Room()
        {
            InitializeComponent();
        }

        private void initializeLantern()
        {
            for (int i = 0; i < MAX_PLAYER; i++)
            {
                playerLanterns.Add((PictureBox)Controls.Find("playerLantern" + i, true)[0]);
            }
        }

        private void Room_Load(object sender, EventArgs e)
        {
            initializeLantern();
            
        }

        private void lanternImageToggle(int index)
        {
            Image lanternOn = Properties.Resources.light_on;
            Image lanternOff = Properties.Resources.light_off;

            if (!isPlayer[index])
                playerLanterns[index].Image = lanternOn;
            else
                playerLanterns[index].Image = lanternOff;
            isPlayer[index] = !isPlayer[index];
        }

        private void chatResultBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
