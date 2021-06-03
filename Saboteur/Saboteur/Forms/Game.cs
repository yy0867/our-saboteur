using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PacketLibrary;
using MapLibrary;
using CardLibrary;

namespace Saboteur.Forms
{
    struct Point
    {
        public int X, Y;

        public Point(int X = 0, int Y = 0)
        {
            this.X = X;
            this.Y = Y;
        }

        public void SetPosition(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public partial class Game : UserControl
    {
        // Constants
        private const int cardWidth = 77;
        private const int cardHeight = 125;
        private const int fieldLeftPadding = 28;
        private const int fieldTopPadding = 3;
        private Size fieldSize = new Size(CONST.MAP_COL * cardWidth, CONST.MAP_ROW * cardHeight);

        // Mouse Activities
        private bool isMouseDown = false;       // is Mouse Pressing?
        Point mouseDragPrev = new Point();      // for calculate translate
        Point mouseDragStart = new Point();     // for know start position
        Point rectPrev = new Point();           // for grid rect

        // Game Instances
        Map field = new Map();
        Card selectedCard = null;               // which card is selected
        PictureBox selectedPic = null;          // selectedCard's Image

        // Graphics Instances
        Graphics g = null;

        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            g = picFieldBackground.CreateGraphics();
            field.MapInit();
        }

        public void updateInfo(Packet packet)
        {
        }

        private void picCard_MouseDown(object sender, MouseEventArgs e)
        {
            selectedPic = (PictureBox)sender;

            isMouseDown = true;
            mouseDragPrev.SetPosition(e.X, e.Y);
            mouseDragStart.SetPosition(selectedPic.Left, selectedPic.Top);

            // ************ SET selectedCard ************** // 
            // selectedCard = ~~~~; // 

            ShowGrid();
        }

        private void picCard_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                selectedPic.Left += (e.X - mouseDragPrev.X);
                selectedPic.Top += (e.Y - mouseDragPrev.Y);

                SetPredictionRect(selectedPic.Left + e.X, selectedPic.Top + e.Y);
            }
        }

        // Release on Grid
        private void ProcessGrid(Point gridPoint)
        {
            // TEST
            selectedCard = new CaveCard(Dir.ALL, true);
            // TEST

            // is CaveCard
            if (selectedCard is CaveCard)
            {
                Attach(gridPoint, (CaveCard)selectedCard);
            }

            // is RockDownCard
            else if (selectedCard is ActionCard)
            {

            }

            // is MapCard
            else if (selectedCard is MapCard)
            {

            }
        }

        private void picCard_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                //MoveToStartPosition(card);
                isMouseDown = false;
                EraseGraphics();

                // ##################### ADD DOWN BY USING METHOD ########################
                // Release on Grid
                Point? gridPoint = GetGridPoint(selectedPic.Left + e.X, selectedPic.Top + e.Y); // Mouse Pointer Position

                if (gridPoint.HasValue) // is in grid
                {
                    ProcessGrid((Point)gridPoint);
                }

                //// Release on Player
                // else if ()
                // {

                // }

                //// Release on Deck
                // else if ()
                // {

                // }

                // ..?

                // ##################### ADD UP ########################
                else
                {
                    MoveToStartPosition(selectedPic);
                }

                selectedPic = null;
                selectedCard = null;
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

        private void EraseGraphics()
        {
            picFieldBackground.Refresh();
        }

        private void SetPredictionRect(int X, int Y)
        {
            Point? gridPoint = GetGridPoint(X, Y);

            if (gridPoint.HasValue)
            {
                Point point = (Point)gridPoint;
                if (rectPrev.X != point.X || rectPrev.Y != point.Y)
                {
                    EraseGraphics();
                    ShowGrid();

                    rectPrev.X = point.X; rectPrev.Y = point.Y;
                    Rectangle rect = new Rectangle(point.X, point.Y, cardWidth, cardHeight);
                    Brush brush = new SolidBrush(Color.GreenYellow);
                    g.FillRectangle(brush, rect);
                }
            }
        }

        private void Attach(int X, int Y, CaveCard cave)
        {
            int row = Y / cardHeight;
            int col = X / cardWidth;

            field.MapAdd(new MapLibrary.Point(row, col), cave);

            selectedPic.Left = X;
            selectedPic.Top = Y;
        }

        // override Attach()
        private void Attach(Point point, CaveCard cave)
        {
            Attach(point.X, point.Y, cave);
        }

        private Point? GetGridPoint(int X, int Y)
        {
            if (X < fieldLeftPadding || X > fieldLeftPadding + fieldSize.Width ||
                   Y < fieldTopPadding || Y > fieldTopPadding + fieldSize.Height) return null; // (X, Y) is not in grid

            int left = fieldLeftPadding + (X - fieldLeftPadding) / cardWidth * cardWidth;
            int top = fieldTopPadding + (Y - fieldTopPadding) / cardHeight * cardHeight;

            return new Point(left, top);
        }
        // ###### Grid Methods - End ######
    }
}
