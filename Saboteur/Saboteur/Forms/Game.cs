﻿using System;
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
        private const int handPadding = 30;
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
        CaveCard[,] prevMap = new CaveCard[CONST.MAP_ROW, CONST.MAP_COL];
        Map field = new Map();
        Card selectedCard = null;               // which card is selected
        PictureBox selectedPic = null;          // selectedCard's Image

        // Graphics Instances
        Graphics g = null;
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        // TEST
        private void MockSendPacket()
        {
            GameInfo mock = new GameInfo();

            MapLibrary.Point point;
            CaveCard card;

            mock.fields.MapInit();

            #region(MockingTest_Field)
            for (int i = 0; i < 4; i++)
            {
                point = new MapLibrary.Point(3, 5 + i);
                card = new CaveCard(Dir.RIGHTLEFT, true);

                mock.fields.MapAdd(point, card);
            }

            point = new MapLibrary.Point(3, 9);
            card = new CaveCard(Dir.LEFTUP, true);
            mock.fields.MapAdd(point, card);

            for (int i = 0; i < 2; i++)
            {
                point = new MapLibrary.Point(2 - i, 9);
                card = new CaveCard(Dir.DOWNUP, true);

                mock.fields.MapAdd(point, card);
            }

            point = new MapLibrary.Point(0, 9);
            card = new CaveCard(Dir.DOWNUP, false);

            mock.fields.MapAdd(point, card);
            #endregion

            #region(MockingTest_Hand)
            mock.holdingCards.Add(new CaveCard(Dir.ALL, true));
            mock.holdingCards.Add(new CaveCard(Dir.LEFTDOWN, true));
            mock.holdingCards.Add(new CaveCard(Dir.UP, false));
            mock.holdingCards.Add(new CaveCard(Dir.LEFT, true));
            mock.holdingCards.Add(new CaveCard(Dir.NOLEFT, true));
            #endregion

            updateInfo(mock);
        }
        // TEST

        public Game()
        {
            InitializeComponent();

            for (int i = 0; i < CONST.MAP_ROW; i++)
                for (int j = 0; j < CONST.MAP_COL; j++)
                    prevMap[i, j] = new CaveCard();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            g = picFieldBackground.CreateGraphics();
            field.MapInit();

            // TEST
            MapLibrary.Point point;
            CaveCard card;

            for (int i = 0; i < 4; i++)
            {
                point = new MapLibrary.Point(3, 5 + i);
                card = new CaveCard(Dir.RIGHTLEFT, true);

                field.MapAdd(point, card);
            }

            point = new MapLibrary.Point(3, 9);
            card = new CaveCard(Dir.LEFTUP, true);
            field.MapAdd(point, card);

            for (int i = 0; i < 2; i++)
            {
                point = new MapLibrary.Point(2 - i, 9);
                card = new CaveCard(Dir.DOWNUP, true);

                field.MapAdd(point, card);
            }
            // TEST

            DrawCardOnField();

            // TEST 
            MockSendPacket();
            // TEST 
        }

        public void updateInfo(Packet packet)
        {
            GameInfo info = (GameInfo)packet;

            field = info.fields;
            DrawCardOnField();
            DrawHands(info.holdingCards);
        }

        private void picCard_MouseDown(object sender, MouseEventArgs e)
        {
            selectedPic = (PictureBox)sender;

            isMouseDown = true;
            mouseDragPrev.SetPosition(e.X, e.Y);
            mouseDragStart.SetPosition(selectedPic.Left, selectedPic.Top);

            // ************ SET selectedCard ************** // 
            // selectedCard = ~~~~; // 
            DeleteImage(3, 7);

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
            if (this.isMouseDown)
            {
                //MoveToStartPosition(card);
                this.isMouseDown = false;
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
                    MoveToStartPosition(this.selectedPic);
                }

                this.selectedPic = null;
                this.selectedCard = null;
            }
        }

        // ###### Draw Hands Methods - Start ######
        private void DrawHands(List<Card> holdingCards)
        {
            Point location = new Point(handPadding, 897);

            foreach (Card card in holdingCards) { 
                AddImage(location, GetCardImage(card), true);
                location.X += (handPadding + cardWidth);
            }
        }
        // ###### Draw Hands Methods - End ######

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

        private void AddImage(Point location, Image cardImage, bool isMoveable = false)
        {
            PictureBox pic = new PictureBox();

            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Name = location.X + "," + location.Y;
            pic.Location = new System.Drawing.Point(location.X, location.Y);
            pic.Size = new Size(cardWidth, cardHeight);
            pic.Image = cardImage;
            pic.BackColor = Color.Black;
            pic.Parent = picFieldBackground;

            this.Controls.Add(pic);
            pic.BringToFront();

            pictureBoxes.Add(pic);

            if (isMoveable)
            {
                pic.MouseDown += new MouseEventHandler(this.picCard_MouseDown);
                pic.MouseMove += new MouseEventHandler(this.picCard_MouseMove);
                pic.MouseUp += new MouseEventHandler(this.picCard_MouseUp);
            }
        }

        private void DeleteImage(int row, int col)
        {
            Point point = ConvertCoordsToLocation(row, col);
            point.X += cardWidth / 2;
            point.Y += cardHeight / 2;

            for (int i = 0; i < this.pictureBoxes.Count; i++)
            {
                if (this.pictureBoxes[i].Left < point.X && point.X < this.pictureBoxes[i].Left + this.pictureBoxes[i].Width &&
                    this.pictureBoxes[i].Top < point.Y && point.Y < this.pictureBoxes[i].Top + this.pictureBoxes[i].Height)
                {
                    this.Controls.Remove(this.pictureBoxes[i]);
                    this.pictureBoxes.RemoveAt(i);
                    return;
                }
            }
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

                    if (prevMap[i, j] == curCard)
                        continue;

                    location = ConvertCoordsToLocation(i, j);

                    // Draw Start Card
                    if (curCard is StartCard)
                    {
                        AddImage(location, imgCards.Images[START_CARD_INDEX]);
                    } 

                    // Draw Dest Card
                    else if (curCard is DestCard)
                    {
                        AddImage(location, imgCards.Images["goal_back.png"]);
                    } 

                    // Draw Cave Card
                    else
                    {
                        Image curImage = GetCardImage(curCard);

                        if (curImage != null)
                            AddImage(location, curImage);
                    }

                    prevMap[i, j] = curCard;
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
