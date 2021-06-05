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

    enum Field
    {
        MAP = 0,
        PLAYER,
        DECK,
        HAND
    }

    public partial class Game : UserControl
    {
        // Constants
        private const int cardWidth = 77;
        private const int cardHeight = 125;
        private const int fieldLeftPadding = 28;
        private const int fieldTopPadding = 3;
        private const int handPadding = 30;
        private const int playerCardHeight = 126;
        private Size fieldSize = new Size(CONST.MAP_COL * cardWidth, CONST.MAP_ROW * cardHeight);

        private const int START_CARD_INDEX = 31;
        private const int DEST_GOLD = 4;
        private const int DEST_DOWN_LEFT_INDEX = 5;
        private const int DEST_DOWN_RIGHT_INDEX = 6;

        private Color Grid_Possible = Color.FromArgb(70, 65, 195, 0);
        private Color Grid_Impossible = Color.FromArgb(70, 225, 57, 53);

        // Mouse Activities
        private bool isMouseDown = false;       // is Mouse Pressing?
        Point mouseDragPrev = new Point();      // for calculate translate
        Point mouseDragStart = new Point();     // for know start position
        Point rectPrev = new Point();           // for grid rect

        // Game Instances
        int playerNum = 0;
        bool isSaboteur = false;
        CaveCard[,] prevMap = new CaveCard[CONST.MAP_ROW, CONST.MAP_COL];
        Map field = new Map();
        Card selectedCard = null;               // which card is selected
        PictureBox selectedPic = null;          // selectedCard's Image
        List<Card> hands = new List<Card>();
        Stack<Card> frontUsedCard = new Stack<Card>();
        Stack<Card> backUsedCard = new Stack<Card>();
        Dictionary<int, List<PictureBox>> playersInfo = new Dictionary<int, List<PictureBox>>(); 

        // Graphics Instances
        Graphics g = null;
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        // Network Variables
        bool isFirstPacket = true;
        int clientID = 0;

        #region Test
        private void MockSendPacket()
        {
            GameInfo mock = new GameInfo();

            MapLibrary.Point point;
            CaveCard card;

            mock.fields.MapInit();
            mock.isSaboteur = false;

            #region(MockingTest_Hand)
            mock.holdingCards.Add(new CaveCard(Dir.ALL, true));
            mock.holdingCards.Add(new CaveCard(Dir.LEFTDOWN, true));
            mock.holdingCards.Add(new CaveCard(Dir.UP, false));
            mock.holdingCards.Add(new EquipmentCard(CType.EQ_REPAIR, Tool.PICKLATTERN));
            mock.holdingCards.Add(new EquipmentCard(CType.EQ_DESTRUCTION, Tool.CART));
            #endregion

            updateInfo(mock);
        }
        #endregion

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

            DrawCardOnField();

            #region Test
            //MockSendPacket();
            #endregion
        }

        public void updateInfo(Packet packet)
        {
            GameInfo info = (GameInfo)packet;

            if (this.isFirstPacket)
            {
                this.isFirstPacket = false;

                string message = info.isSaboteur ? "당신은 사보타지입니다!" : "당신은 광부입니다!";
                MessageBox.Show(message);
            }

            this.clientID = info.clientID;
            this.playerNum = info.playersState.Count;
            this.isSaboteur = info.isSaboteur;

            this.field = info.fields;
            DrawCardOnField();

            this.hands = info.holdingCards;

            int usedCardCount = info.backUsedCards.Count + info.frontUsedCards.Count;
            this.Invoke((MethodInvoker)(() =>
            {
                this.lblUsedCardNum.Text = usedCardCount.ToString();
                this.lblDeckNum.Text = info.deckCards.Count.ToString();
            }));

            DrawHands(hands);
        }

        private void picCard_MouseDown(object sender, MouseEventArgs e)
        {
            this.selectedPic = null;
            this.selectedCard = null;

            this.selectedPic = (PictureBox)sender;

            int selectedIndex = GetHandIndexByLocation(this.selectedPic.Left + cardWidth / 2, this.selectedPic.Top / 2);
            if (selectedIndex != -1)
                this.selectedCard = hands[selectedIndex];

            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseDragPrev.SetPosition(e.X, e.Y);
                mouseDragStart.SetPosition(this.selectedPic.Left, this.selectedPic.Top);

                ShowGrid();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (this.selectedCard is CaveCard)
                {
                    ((CaveCard)this.selectedCard).rotate();
                    this.selectedPic.Image = GetCardImage(this.selectedCard);

                    this.selectedCard = null;
                    this.selectedPic = null;
                }
            }
        }

        private void picCard_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && this.selectedPic != null)
            {
                this.selectedPic.Left += (e.X - mouseDragPrev.X);
                this.selectedPic.Top += (e.Y - mouseDragPrev.Y);

                if (this.selectedCard is CaveCard)
                    SetPredictionRect(this.selectedPic.Left + e.X, this.selectedPic.Top + e.Y);

                //else if (this.selectedCard is ActionCard) 

            }
        }

        // Send Packet
        private void Send()
        {
            GameInfo packet = new GameInfo();

            packet.clientID = this.clientID;
            packet.fields = this.field;
            packet.holdingCards = this.hands;

            packet.isSaboteur = this.isSaboteur;
            //packet.playersState = this.playerState; // 현재 플레이어의 상태

            Network.Send(packet);
        }

        // Release on Grid
        private void ProcessGrid(Point gridPoint)
        {
            // is CaveCard
            if (this.selectedCard is CaveCard)
            {
                Attach(gridPoint, (CaveCard)this.selectedCard);
            }

            // is RockDownCard
            else if (this.selectedCard is ActionCard)
            {

            }

            // is MapCard
            else if (this.selectedCard is MapCard)
            {

            }
        }

        // Release on Player
        private void ProcessEquipment(int playerID)
        {

        }

        private Field GetReleaseField(int X, int Y)
        {
            if (picFieldBackground.Left <= X && X <= fieldSize.Width &&
                picFieldBackground.Top <= Y && Y <= fieldSize.Height)
                return Field.MAP;

            else if (fieldSize.Width < X && X <= this.Width &&
                this.Top <= Y && Y <= fieldSize.Height)
                return Field.PLAYER;

            else if (this.Left <= X && X <= picDeck.Left - handPadding &&
                fieldSize.Height < Y && Y <= this.Height)
                return Field.HAND;

            else if (picDeck.Left - handPadding < X && X <= this.Width &&
                fieldSize.Height < Y && Y <= this.Height)
                return Field.DECK;

            return Field.HAND;
        }

        private int GetPlayerIndex(Point point)
        {
            int Y = point.Y;
            
            if (Y < 10) return 0;

            return (Y - 10) / playerCardHeight;
        }

        private void picCard_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.isMouseDown && this.selectedPic != null)
            {
                this.isMouseDown = false;
                EraseGraphics();

                Point mouseLocation = new Point(e.X + selectedPic.Left, e.Y + selectedPic.Top);

                Field releasePoint = GetReleaseField(mouseLocation.X, mouseLocation.Y);
                // ##################### ADD DOWN BY USING METHOD ########################
                // Release on Grid

                if (releasePoint == Field.MAP) // is in map
                {
                    Point? gridPoint = GetGridPoint(mouseLocation.X, mouseLocation.Y); // Mouse Pointer Position

                    if (gridPoint.HasValue) // grid point is valid
                    {
                        if (!(this.selectedCard is CaveCard) || !field.IsValidPosition(ConvertLocationToCoords(mouseLocation), (CaveCard)selectedCard))
                        {
                            MoveToStartPosition(selectedPic);
                            return;
                        }
                        else
                        {
                            ProcessGrid((Point)gridPoint);

                            selectedPic.MouseUp -= picCard_MouseUp;
                            selectedPic.MouseDown -= picCard_MouseDown;
                            selectedPic.MouseMove -= picCard_MouseMove;
                        }
                    }
                    else
                    {
                        MoveToStartPosition(selectedPic);
                        return;
                    }
                }

                // Release on Player
                else if (releasePoint == Field.PLAYER)
                {
                    if (!(selectedCard is EquipmentCard))
                        return;

                    int index = GetPlayerIndex(mouseLocation);

                    if (index >= playerNum)
                    {
                        MoveToStartPosition(selectedPic);
                    }
                    else
                    {
                        ProcessEquipment(index);

                        selectedPic.MouseUp -= picCard_MouseUp;
                        selectedPic.MouseDown -= picCard_MouseDown;
                        selectedPic.MouseMove -= picCard_MouseMove;
                    }
                }

                // Release on Deck
                else if (releasePoint == Field.DECK)
                {

                }

                // ##################### ADD UP ########################
                else
                {
                    MoveToStartPosition(selectedPic);
                }

                Send();
            }
        }

        #region Draw Hand Methods
        private void DrawHands(List<Card> holdingCards)
        {
            Point location = new Point(handPadding, 897);
            bool isConnected;

            foreach (Card card in holdingCards) {
                isConnected = true;
                if (card is CaveCard)
                    isConnected = ((CaveCard)card).getIsConnected();
                AddImage(location, GetCardImage(card), true);
                location.X += (handPadding + cardWidth);
            }
        }

        // return index when mouse down event on Hand
        private int GetHandIndexByLocation(Point location)
        {
            for (int i = 0; i < this.pictureBoxes.Count; i++)
            {
                if (this.pictureBoxes[i].Top < fieldSize.Height + fieldTopPadding * 2) continue;
                else if (this.pictureBoxes[i].Tag.ToString() != "Card") continue;

                if (this.pictureBoxes[i].Left < location.X &&
                    location.X < this.pictureBoxes[i].Left + cardWidth)
                {
                    return (location.X - handPadding) / (handPadding + cardWidth);
                }
            }
            return -1;
        }

        private int GetHandIndexByLocation(int x, int y)
        {
            return GetHandIndexByLocation(new Point(x, y));
        }
        #endregion

        #region Draw Card Methods
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
                }
            }
            else // not connected
            {
                switch (dir)
                {
                    case Dir.LEFTDOWN:
                        cardImage = imgCards.Images[24];
                        break;

                    case Dir.RIGHTUP:
                        cardImage = imgCards.Images[24];
                        Rotate(cardImage);
                        break;

                    case Dir.RIGHTDOWN:
                        cardImage = imgCards.Images[26];
                        break;

                    case Dir.LEFTUP:
                        cardImage = imgCards.Images[26];
                        Rotate(cardImage);
                        break;

                    case Dir.RIGHTLEFT:
                        cardImage = imgCards.Images[28];
                        break;

                    case Dir.RIGHT:
                        cardImage = imgCards.Images[30];
                        break;

                    case Dir.LEFT:
                        cardImage = imgCards.Images[30];
                        Rotate(cardImage);
                        break;

                    case Dir.UP:
                        cardImage = imgCards.Images[32];
                        break;

                    case Dir.DOWN:
                        cardImage = imgCards.Images[32];
                        Rotate(cardImage);
                        break;

                    case Dir.DOWNUP:
                        cardImage = imgCards.Images[34];
                        break;

                    case Dir.NOUP:
                        cardImage = imgCards.Images[37];
                        break;

                    case Dir.NODOWN:
                        cardImage = imgCards.Images[37];
                        Rotate(cardImage);
                        break;

                    case Dir.ALL:
                        cardImage = imgCards.Images[38];
                        break;

                    default:
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
                if (card.getType() == CType.MAP)
                    return imgCards.Images[15];
                
                else if (card.getType() == CType.ROCK_DOWN)
                    return imgCards.Images[14];
                
                else if (card.getType() == CType.EQ_DESTRUCTION)
                {
                    switch (((EquipmentCard)card).tool)
                    {
                        case Tool.CART:
                            return imgCards.Images[11];
                        case Tool.LATTERN:
                            return imgCards.Images[12];
                        case Tool.PICKAXE:
                            return imgCards.Images[13];
                    }
                }

                else if (card.getType() == CType.EQ_REPAIR)
                {
                    switch (((EquipmentCard)card).tool)
                    {
                        case Tool.CART:
                            return imgCards.Images[16];
                        case Tool.LATTERN:
                            return imgCards.Images[17];
                        case Tool.LATTERNCART:
                            return imgCards.Images[18];
                        case Tool.PICKAXE:
                            return imgCards.Images[19];
                        case Tool.PICKCART:
                            return imgCards.Images[20];
                        case Tool.PICKLATTERN:
                            return imgCards.Images[21];
                    }
                }
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
            //pic.Parent = picFieldBackground;
            pic.Tag = "Card";

            this.Invoke((MethodInvoker)(() => {
                this.Controls.Add(pic);
                pic.BringToFront();
            }));

            pictureBoxes.Add(pic);

            if (isMoveable)
            {
                pic.MouseDown += new MouseEventHandler(this.picCard_MouseDown);
                pic.MouseMove += new MouseEventHandler(this.picCard_MouseMove);
                pic.MouseUp += new MouseEventHandler(this.picCard_MouseUp);
            }

        }

        private PictureBox FindPictureboxByLocation(Point location)
        {
            for (int i = 0; i < this.pictureBoxes.Count; i++)
            {
                if (this.pictureBoxes[i].Left < location.X && location.X < this.pictureBoxes[i].Left + this.pictureBoxes[i].Width &&
                    this.pictureBoxes[i].Top < location.Y && location.Y < this.pictureBoxes[i].Top + this.pictureBoxes[i].Height)
                {
                    return this.pictureBoxes[i];
                }
            }
            return null;
        }

        private PictureBox FindPictureboxByLocation(int x, int y)
        {
            return FindPictureboxByLocation(new Point(x, y));
        }

        private void DeleteImage(int row, int col)
        {
            Point point = ConvertCoordsToLocation(row, col);
            point.X += cardWidth / 2;
            point.Y += cardHeight / 2;

            PictureBox victim = FindPictureboxByLocation(point);

            if (victim != null)
            {
                this.Controls.Remove(victim);
                this.pictureBoxes.Remove(victim);
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
        #endregion

        #region Draw Player Methods
        // ###### Draw Player Methods - Start ######
        private void DrawPlayers()
        {
            if (playersInfo.Count == 0 || playersInfo == null)
                return;


        }
        // ###### Draw Player Methods - End ######
        #endregion

        private void MoveToStartPosition(PictureBox card)
        {
            card.Left = mouseDragStart.X;
            card.Top = mouseDragStart.Y;
        }

        // ###### Grid Methods - Start ######
        private MapLibrary.Point ConvertLocationToCoords(Point location)
        {
            return new MapLibrary.Point((location.Y - fieldTopPadding) / cardHeight, (location.X - fieldLeftPadding) / cardWidth);
        }
        private MapLibrary.Point ConvertLocationToCoords(int X, int Y)
        {
            return ConvertLocationToCoords(new Point(X, Y));
        }

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
            if (this.selectedCard == null) return;

            Point? gridPoint = GetGridPoint(X, Y);

            if (gridPoint.HasValue)
            {
                Point point = (Point)gridPoint;
                if (rectPrev.X != point.X || rectPrev.Y != point.Y)
                {
                    EraseGraphics();
                    ShowGrid();

                    MapLibrary.Point coords = ConvertLocationToCoords(point);

                    rectPrev.X = point.X; rectPrev.Y = point.Y;
                    Rectangle rect = new Rectangle(point.X, point.Y, cardWidth, cardHeight);

                    Brush brush = new SolidBrush(Grid_Impossible);

                    if (field.IsValidPosition(coords, (CaveCard)this.selectedCard))
                    {
                        brush = new SolidBrush(Grid_Possible);
                    }

                    g.FillRectangle(brush, rect);
                }
            }
        }

        private void Attach(int X, int Y, CaveCard cave)
        {
            int row = Y / cardHeight;
            int col = X / cardWidth;

            field.MapAdd(new MapLibrary.Point(row, col), cave);

            this.selectedPic.Left = X;
            this.selectedPic.Top = Y;
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
