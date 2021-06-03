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

        private const int START_CARD_INDEX = 31;
        private const int DEST_GOLD = 4;
        private const int DEST_DOWN_LEFT_INDEX = 5;
        private const int DEST_DOWN_RIGHT_INDEX = 6;

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
            DrawCardOnField();
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

        // ###### Draw Card Methods - Start ######
        private void Rotate(Image image)
        {
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        private Image DirToImage(Dir dir, bool isConnected = true)
        {
            Image cardImage = null;
            if (isConnected)
            {
                switch (dir)
                {
                    case Dir.ALL: // Four way
                        cardImage = imgCards.Images[36];
                        break;

                    case Dir.DOWN: // Only Down
                        cardImage = imgCards.Images[32];
                        Rotate(cardImage);
                        break;

                    case Dir.DOWNUP: // |
                        cardImage = imgCards.Images[33];
                        break;

                    case Dir.UP: // Only Up
                        cardImage = imgCards.Images[32];
                        break;

                    case Dir.LEFT: // Only Left
                        cardImage = imgCards.Images[30];
                        Rotate(cardImage);
                        break;

                    case Dir.LEFTDOWN: // ㄱ
                        cardImage = imgCards.Images[23];
                        break;

                    case Dir.LEFTUP: // _|
                        cardImage = imgCards.Images[25];
                        Rotate(cardImage);
                        break;

                    case Dir.RIGHTLEFT: // -
                        cardImage = imgCards.Images[27];
                        break;

                    case Dir.RIGHT:
                        cardImage = imgCards.Images[30];
                        break;

                    case Dir.RIGHTDOWN:
                        cardImage = imgCards.Images[25];
                        break;

                    case Dir.RIGHTUP:
                        cardImage = imgCards.Images[23];
                        Rotate(cardImage);
                        break;

                    case Dir.NODOWN:
                        cardImage = imgCards.Images[29];
                        Rotate(cardImage);
                        break;

                    case Dir.NOLEFT:
                        cardImage = imgCards.Images[35];
                        Rotate(cardImage);
                        break;

                    case Dir.NORIGHT:
                        cardImage = imgCards.Images[35];
                        break;

                    case Dir.NOUP:
                        cardImage = imgCards.Images[29];
                        break;

                    case Dir.NONE:
                        cardImage = null;
                        break;
                }
            }
            else // not connected
            {
                switch (dir)
                {
                    case Dir.LEFTDOWN:
                        cardImage = imgCards.Images[24];
                        break;

                    case Dir.RIGHTDOWN:
                        cardImage = imgCards.Images[26];
                        break;

                    case Dir.RIGHTLEFT:
                        cardImage = imgCards.Images[28];
                        break;

                    case Dir.RIGHT:
                        cardImage = imgCards.Images[30];
                        break;

                    case Dir.UP:
                        cardImage = imgCards.Images[32];
                        break;

                    case Dir.DOWNUP:
                        cardImage = imgCards.Images[34];
                        break;

                    case Dir.NOUP:
                        cardImage = imgCards.Images[37];
                        break;

                    case Dir.NODOWN:
                        cardImage = imgCards.Images[38];
                        break;

                    case Dir.NONE:
                        cardImage = null;
                        break;
                }
            }

            return cardImage;
        }

        private Image GetCardImage(Card card)
        {
            if (card is CaveCard)
            {
                CaveCard c = (CaveCard)card;

                return DirToImage(c.getDir(), c.getIsConnected());
            }
            else if (card is ActionCard)
            {

            }
            return null;
        }

        private void AddImage(Point location, Image cardImage)
        {
            PictureBox pic = new PictureBox();

            pic.Image = cardImage;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Name = location.X.ToString() + ", " + location.Y.ToString();
            pic.Location = new System.Drawing.Point(location.X, location.Y);
            pic.Size = new Size(cardWidth, cardHeight);
            //pic.BringToFront();

            this.Controls.Add(pic);
        }

        private void DrawCardOnField()
        {
            CaveCard curCard = null;
            Point location = new Point();

            for (int i = 0; i < CONST.MAP_ROW; i++)
            {
                for (int j = 0; j < CONST.MAP_COL; j++)
                {
                    curCard = field.GetCard(i, j);
                    location = ConvertCoordsToLocation(i, j);

                    // Draw Start Card
                    if (curCard is StartCard)
                    {
                        AddImage(location, imgCards.Images[START_CARD_INDEX]);
                    } 

                    // Draw Dest Card
                    else if (curCard is DestCard)
                    {

                    } 

                    // Draw Cave Card
                    else
                    {

                    }
                }
            }
        }
        // ###### Draw Card Methods - End ######

        private void MoveToStartPosition(PictureBox card)
        {
            card.Left = mouseDragStart.X;
            card.Top = mouseDragStart.Y;
        }

        // ###### Grid Methods - Start ######
        private Point ConvertCoordsToLocation(int row, int col)
        {
            Point point = new Point();

            point.X = fieldLeftPadding + col * cardWidth;
            point.Y = fieldTopPadding + row * cardHeight;

            return point;
        }

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
