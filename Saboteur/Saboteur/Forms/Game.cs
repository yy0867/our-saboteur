using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketLibrary;

namespace Saboteur.Forms
{
    struct Point
    {
        public int X, Y;
        public void SetPosition(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public partial class Game : UserControl
    {
        private bool isMouseDown = false;
        Point mouseDragPrev = new Point();
        Point mouseDragStart = new Point();

        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
        }

        public void updateInfo(Packet packet)
        {

        }

        private void picCard_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox card = (PictureBox)sender;

            isMouseDown = true;
            mouseDragPrev.SetPosition(e.X, e.Y);
            mouseDragStart.SetPosition(card.Left, card.Top);
        }

        private void picCard_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox card = (PictureBox)sender;

            if (isMouseDown)
            {
                card.Left += (e.X - mouseDragPrev.X);
                card.Top += (e.Y - mouseDragPrev.Y);
            }
        }

        private void picCard_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox card = (PictureBox)sender;

            if (isMouseDown)
            {
                MoveToStartPosition(card);
                isMouseDown = false;
            }
        }

        private void MoveToStartPosition(PictureBox card)
        {
            card.Left = mouseDragStart.X;
            card.Top = mouseDragStart.Y;
        }
    }
}
