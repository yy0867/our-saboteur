using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketLibrary;
using MapLibrary;

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
        private const int cardWidth = 77;
        private const int cardHeight = 125;
        private const int fieldLeftPadding = 28;
        private const int fieldTopPadding = 3;
        private Size fieldSize = new Size(CONST.MAP_COL * cardWidth, CONST.MAP_ROW * cardHeight);

        private bool isMouseDown = false;
        Point mouseDragPrev = new Point();
        Point mouseDragStart = new Point();

        Map field = new Map();

        Graphics g = null;

        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            g = picFieldBackground.CreateGraphics();
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

            ShowGrid();
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

                HideGrid();
            }
        }

        private void MoveToStartPosition(PictureBox card)
        {
            card.Left = mouseDragStart.X;
            card.Top = mouseDragStart.Y;
        }

        // ###### Grid Methods - Start ######
        private void ShowGrid()
        {
            Pen pen = new Pen(Color.White);
            pen.DashStyle = DashStyle.Dash;
            pen.DashPattern = new float[] { 5, 7 };

            for (int i = fieldLeftPadding; i <= fieldLeftPadding + fieldSize.Width; i += cardWidth)
                g.DrawLine(pen, i, fieldTopPadding, i, fieldTopPadding + fieldSize.Height);

            for (int i = fieldTopPadding; i <= fieldTopPadding + fieldSize.Height; i += cardHeight)
                g.DrawLine(pen, fieldLeftPadding, i, fieldLeftPadding + fieldSize.Width, i);
        }

        private void HideGrid()
        {
            g.Dispose();
            picFieldBackground.Image = Properties.Resources.game_background;
            g = picFieldBackground.CreateGraphics();
        }
    }
}
