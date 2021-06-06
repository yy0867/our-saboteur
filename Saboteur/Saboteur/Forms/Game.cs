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
        private const int MAX_PLAYER = 7;

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
        int selectedIndex = 0;                  // hand index
        List<Card> hands = new List<Card>();
        List<PlayerState> playerStates;
        Stack<Card> usedCard = new Stack<Card>();
        Dictionary<int, List<PictureBox>> playersInfo = new Dictionary<int, List<PictureBox>>();

        // Graphics Instances
        Graphics g = null;
        List<PictureBox> allocatedImages = new List<PictureBox>();
        List<PictureBox> playerIcons = new List<PictureBox>();
        Dictionary<Tool, List<PictureBox>> toolIcons = new Dictionary<Tool, List<PictureBox>>();



        // Network Variables
        bool isFirstPacket = true;
        int clientID = 0;

        #region Test
        private PlayerState mockedPlayerStates(bool canUsePicaxe, bool canUseLantern, bool canUseCart)
        {
            var state = new PlayerState();
            state.isDestroyedPickaxe = canUsePicaxe;
            state.isDestroyedLantern = canUseLantern;
            state.isDestroyedCart = canUseCart;
            return state;
        }
        private List<PlayerState> mockedPlayerStates()
        {
            var states = new List<PlayerState>();
            //states.Add(mockedPlayerStates(true, true, true));
            //states.Add(mockedPlayerStates(true, true, false));
            //states.Add(mockedPlayerStates(true, false, false));
            //states.Add(mockedPlayerStates(false, false, false));
            //states.Add(mockedPlayerStates(false, true, true));
            //states.Add(mockedPlayerStates(false, false, true));
            return states;
        }
        private void MockSendPacket()
        {
            GameInfo mock = new GameInfo();

            MapLibrary.Point point;
            CaveCard card;

            mock.fields.MapInit();
            mock.playersState = mockedPlayerStates();
            mock.isSaboteur = false;
            mock.clientID = 1;
            
            #region(MockingTest_Hand)
            mock.holdingCards.Add(new CaveCard(Dir.RIGHTLEFT, true));
            mock.holdingCards.Add(new CaveCard(Dir.RIGHTLEFT, true));
            mock.holdingCards.Add(new CaveCard(Dir.RIGHTLEFT, true));
            mock.holdingCards.Add(new CaveCard(Dir.RIGHTLEFT, true));
            mock.holdingCards.Add(new CaveCard(Dir.RIGHTLEFT, true));
            mock.holdingCards.Add(new CaveCard(Dir.RIGHTLEFT, true));
            mock.holdingCards.Add(new CaveCard(Dir.RIGHTLEFT, true));
            mock.holdingCards.Add(new CaveCard(Dir.LEFTDOWN, true));
            mock.holdingCards.Add(new RockDownCard());
            #endregion


            updateInfo(mock);
        }
        #endregion

        public Game()
        {
            InitializeComponent();
            InitializeIcons();

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
                string message = info.isSaboteur ? "당신은 사보타지입니다!" : "당신은 광부입니다!";
                MessageBox.Show(message);
            }

            this.clientID = info.clientID;
            this.playerNum = info.playersState.Count;
            this.isSaboteur = info.isSaboteur;
            this.usedCard = info.usedCards;
            info.fields.CopyTo(this.field);

            DrawCardOnField();

            if (this.hands.Contains(null) || this.isFirstPacket)
            {
                this.hands = info.holdingCards;
                DeleteHands();
                DrawHands(hands);
            }

            int usedCardCount = info.usedCards.Count;
            this.Invoke((MethodInvoker)(() =>
            {
                this.lblUsedCardNum.Text = usedCardCount.ToString();
                this.lblDeckNum.Text = info.restCardNum.ToString();
                if (usedCard.Count > 0)
                    this.picUsedCard.Image = GetCardImage(usedCard.Peek());
            }));

            this.playerStates = info.playersState;
            setEquipmentIcon(info.playersState);

            this.isFirstPacket = false;
        }

        private void picCard_MouseDown(object sender, MouseEventArgs e)
        {
            this.selectedPic = null;
            this.selectedCard = null;

            this.selectedPic = (PictureBox)sender;

            this.selectedIndex = GetHandIndexByLocation(this.selectedPic.Left + cardWidth / 2, this.selectedPic.Top / 2);
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
            this.field.CopyTo(packet.fields);
            packet.holdingCards = this.hands;

            packet.isSaboteur = this.isSaboteur;
            packet.playersState = this.playerStates; // 현재 플레이어의 상태
            //packet.usedCards.Push(this.selectedCard);
            packet.usedCards = this.usedCard;

            Network.Send(packet);
        }

        private bool IsArrived(Point gridPoint)
        {
            MapLibrary.Point coords = ConvertLocationToCoords(gridPoint);
            int[] dir = { 0, -1, 0, 1, 0 };

            for (int i = 0; i < dir.Length - 1; i++)
            {
                int r = coords.R + dir[i], c = coords.C + dir[i + 1];

                if (field.GetCard(r, c) is DestCard)
                    return true;
            }

            return false;
        }

        // Release on Grid
        private void ProcessGrid(Point gridPoint)
        {
            // is CaveCard
            if (this.selectedCard is CaveCard)
            {
                Attach(gridPoint, (CaveCard)this.selectedCard);
                RemoveFromHands();

                // if arrived at destcard
                if (IsArrived(gridPoint))
                {
                    // Logical check
                }
            }

            // is RockDownCard
            else if (this.selectedCard is RockDownCard)
            {
                MapLibrary.Point coords = ConvertLocationToCoords(gridPoint);
                if (field.isValidated(coords) || field.GetCard(coords) is StartCard || field.GetCard(coords) is DestCard)
                {
                    MoveToStartPosition(this.selectedPic);
                }
                else if (field.GetCard(coords) is CaveCard)
                {
                    if (((CaveCard)field.GetCard(coords)).getDir() != Dir.NONE)
                    {
                        field.RockDown(coords);
                        this.selectedCard.face = CardFace.FRONT;
                        this.usedCard.Push(this.selectedCard);
                        DeleteImage(coords.R, coords.C);
                        DeleteImage(this.selectedPic);
                        RemoveFromHands();
                    }
                    else
                    {
                        MoveToStartPosition(selectedPic);
                    }
                }
            }

            // is MapCard
            else if (this.selectedCard is MapCard)
            {
                MapLibrary.Point coords = ConvertLocationToCoords(gridPoint);
                CaveCard card = field.GetCard(coords);
                if (card is DestCard)
                {
                    string message = ((DestCard)card).getIsGoldCave() ? "금 카드입니다!" : "금 카드가 아닙니다!";

                    MessageBox.Show(message);
                    this.selectedCard.face = CardFace.FRONT;
                    this.usedCard.Push(this.selectedCard);
                    DeleteImage(this.selectedPic);
                    RemoveFromHands();
                }
                else
                {
                    MoveToStartPosition(this.selectedPic);
                }
            }
        }

        // Release on Player
        private void ProcessEquipment(int playerID, EquipmentCard equipment)
        {
            //Grapical
            equipment = applayEquipmentIcon(playerID, equipment);

            this.selectedCard.face = CardFace.FRONT;
            this.usedCard.Push(this.selectedCard);

            //Logical
            setPlayerState(playerID, equipment);
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

        private void RemoveFromHands()
        {
            this.hands[this.selectedIndex] = null;

            selectedPic.MouseUp -= picCard_MouseUp;
            selectedPic.MouseDown -= picCard_MouseDown;
            selectedPic.MouseMove -= picCard_MouseMove;
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

                // Release on Map
                if (releasePoint == Field.MAP) // is in map
                {
                    Point? gridPoint = GetGridPoint(mouseLocation.X, mouseLocation.Y); // Mouse Pointer Position

                    if (gridPoint.HasValue) // grid point is valid
                    {
                        if ((this.selectedCard is EquipmentCard) || (this.selectedCard is CaveCard && !field.IsValidPosition(ConvertLocationToCoords(mouseLocation), (CaveCard)selectedCard)))
                        {
                            MoveToStartPosition(selectedPic);
                            return;
                        }
                        else
                        {
                            //if (this.playerStates[this.clientID].hasDestroyed() && this.selectedCard is CaveCard)
                            //{
                            //    MessageBox.Show("장비가 파괴되어 길을 놓을 수 없습니다.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    MoveToStartPosition(this.selectedPic);
                            //    return;
                            //}
                            ProcessGrid((Point)gridPoint);
                            
                            Send();
                        }
                    }
                    else
                    {
                        MoveToStartPosition(selectedPic);
                        return;
                    }
                }

                // Release on Player: Use Equipment(Repair, Destruction) Card
                else if (releasePoint == Field.PLAYER)
                {
                    EquipmentCard selectedEquipment = selectedCard as EquipmentCard;
                    if (selectedEquipment == null)
                        return;

                    int index = GetPlayerIndex(mouseLocation);

                    if (index >= playerNum)
                    {
                        MoveToStartPosition(selectedPic);
                    }
                    else
                    {
                        //this.selectedCard.face = CardFace.FRONT;
                        //this.usedCard.Push(this.selectedCard);
                        ProcessEquipment(index, selectedEquipment);

                        DeleteImage(selectedPic);
                        RemoveFromHands();
                        Send();
                    }
                }

                // Release on Deck: Discard the Card    => Show Card Back
                else if (releasePoint == Field.DECK)
                {
                    this.selectedCard.face = CardFace.BACK;
                    this.usedCard.Push(this.selectedCard);
                    //this.picUsedCard.Image = GetCardImage(this.usedCard.Peek());

                    DeleteImage(selectedPic);
                    RemoveFromHands();
                    Send();
                }

                // ##################### ADD UP ########################
                else
                {
                    MoveToStartPosition(selectedPic);
                }
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
            for (int i = 0; i < this.allocatedImages.Count; i++)
            {
                if (this.allocatedImages[i].Top < fieldSize.Height + fieldTopPadding * 2) continue;
                else if (this.allocatedImages[i].Tag.ToString() != "Card") continue;

                if (this.allocatedImages[i].Left < location.X &&
                    location.X < this.allocatedImages[i].Left + cardWidth)
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
                        cardImage = imgCards.Images[39];
                        Rotate(cardImage);
                        break;

                    case Dir.NODOWN:
                        cardImage = imgCards.Images[39];
                        break;

                    case Dir.NOLEFT:
                        cardImage = imgCards.Images[38];
                        break;

                    case Dir.NORIGHT:
                        cardImage = imgCards.Images[38];
                        Rotate(cardImage);
                        break;

                    case Dir.ALL:
                        cardImage = imgCards.Images[37];
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
            // Card 뒷면
            if (card.face == CardFace.BACK)
                return imgCards.Images[22];

            // Card 앞면
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

            allocatedImages.Add(pic);

            if (isMoveable)
            {
                pic.MouseDown += new MouseEventHandler(this.picCard_MouseDown);
                pic.MouseMove += new MouseEventHandler(this.picCard_MouseMove);
                pic.MouseUp += new MouseEventHandler(this.picCard_MouseUp);
            }

        }

        private PictureBox FindPictureboxByLocation(Point location)
        {
            for (int i = 0; i < this.allocatedImages.Count; i++)
            {
                if (this.allocatedImages[i].Left < location.X && location.X < this.allocatedImages[i].Left + this.allocatedImages[i].Width &&
                    this.allocatedImages[i].Top < location.Y && location.Y < this.allocatedImages[i].Top + this.allocatedImages[i].Height)
                {
                    return this.allocatedImages[i];
                }
            }
            return null;
        }

        private PictureBox FindPictureboxByLocation(int x, int y)
        {
            return FindPictureboxByLocation(new Point(x, y));
        }

        private void DeleteImage(PictureBox victim)
        {
            if (victim != null)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    this.Controls.Remove(victim);
                    this.allocatedImages.Remove(victim);
                }));
            }
        }

        private void DeleteImage(int row, int col)
        {
            Point point = ConvertCoordsToLocation(row, col);
            point.X += cardWidth / 2;
            point.Y += cardHeight / 2;

            PictureBox victim = FindPictureboxByLocation(point);

            DeleteImage(victim);
        }

        private void DeleteHands()
        {
            for (int i = 0; i < this.hands.Count(); i++)
            {
                Point p = new Point(handPadding * (i + 1) + cardWidth * i + cardWidth / 2, fieldSize.Height + cardHeight / 2);
                DeleteImage(FindPictureboxByLocation(p));
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
                        DestCard dest = (DestCard)curCard;
                        Image image;
                        if (dest.face == CardFace.FRONT)
                        {
                            if (dest.getIsGoldCave())
                                image = imgCards.Images["goal_gold.png"];

                            else
                            {
                                image = imgCards.Images["goal_stone_down_left.png"];
                            }
                        }
                        else
                            image = imgCards.Images["goal_back.png"];

                        AddImage(location, image);
                    }

                    // Draw Cave Card
                    else
                    {
                        if (!curCard.isEmpty() && prevMap[i, j].isEmpty())
                        {
                            Image curImage = GetCardImage(curCard);

                            if (curImage != null)
                                AddImage(location, curImage);
                        }
                        else if (curCard.isEmpty() && !prevMap[i, j].isEmpty())
                        {
                            DeleteImage(i, j);
                        }
                    }

                    prevMap[i, j].setDir(curCard.getDir());
                    prevMap[i, j].setIsConnected(curCard.getIsConnected());
                    prevMap[i, j].setType(curCard.getType());
                    prevMap[i, j].face = curCard.face;
                }
            }
        }
        // ###### Draw Card Methods - End ######
        #endregion

        #region Icon Methods
        private void InitializeIcons()
        {
            for(var i = Tool.PICKAXE; i <= Tool.CART; i++)
                this.toolIcons.Add(i, new List<PictureBox>());

            for (int i = 0; i < MAX_PLAYER; i++)
            {
                string player = "player_" + i;
                this.playerIcons.Add((PictureBox)Controls.Find(player + "_icon", true)[0]);
                this.toolIcons[Tool.CART].Add((PictureBox)Controls.Find(player + "_cart", true)[0]);
                this.toolIcons[Tool.PICKAXE].Add((PictureBox)Controls.Find(player + "_pickaxe", true)[0]);
                this.toolIcons[Tool.LATTERN].Add((PictureBox)Controls.Find(player + "_lantern", true)[0]);

                this.toolIcons[Tool.CART][i].Visible = false;
                this.toolIcons[Tool.PICKAXE][i].Visible = false;
                this.toolIcons[Tool.LATTERN][i].Visible = false;
            }
        }

        private void setPlayerIcon(int index, bool isTurnOn)
        {
            Image on = Properties.Resources.player_on;
            Image off = Properties.Resources.player_off;

            this.Invoke((MethodInvoker)(() =>
            {
                if (isTurnOn)
                {
                    this.playerIcons[index].BackgroundImage = on;
                } else
                {
                    this.playerIcons[index].BackgroundImage = off;
                }
            }));
        }
        private bool hasMultiEffects(Tool tool)
        {
            return tool >= Tool.PICKLATTERN;
        }
        private bool hasMultiEffects(EquipmentCard equipment)
        {
            return hasMultiEffects(equipment.tool);
        }
        private EquipmentCard selectEffect(EquipmentCard equipment)
        {
            QueryForm query = new QueryForm(equipment.tool, (selectedTool) => {
                equipment.tool = selectedTool;
            });
            query.ShowDialog();
            query.BringToFront();
            query.Focus();
            return equipment;
        }
        private EquipmentCard applayEquipmentIcon(int playerID, EquipmentCard equipment)
        {
            if (hasMultiEffects(equipment))
                equipment = selectEffect(equipment);
            setEquipmentIcon(playerID, equipment);
            return equipment;
        }

        private void setEquipmentIcon(int index, EquipmentCard equipment)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                if (equipment.getType() == CType.EQ_REPAIR)
                {
                    this.toolIcons[equipment.tool][index].Visible = false;
                } else
                {
                    this.toolIcons[equipment.tool][index].Visible = true;
                }
            }));
        }

        private void setEquipmentIcon(int index, PlayerState state)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                this.toolIcons[Tool.PICKAXE][index].Visible = state.isDestroyedPickaxe;
                this.toolIcons[Tool.LATTERN][index].Visible = state.isDestroyedLantern;
                this.toolIcons[Tool.CART][index].Visible = state.isDestroyedCart;
            }));
        }

        private void setEquipmentIcon(List<PlayerState> states)
        {
            int i = 0;
            foreach (var state in states)
                setEquipmentIcon(i++, state);
        }

        private void setPlayerState(int index, EquipmentCard equipment)
        {
            switch (equipment.tool)
            {
                case Tool.CART:
                    if (equipment.getType() == CType.EQ_REPAIR)
                        this.playerStates[index].isDestroyedCart = false;
                    else
                        this.playerStates[index].isDestroyedCart = true;
                    break;
                case Tool.LATTERN:
                    if (equipment.getType() == CType.EQ_REPAIR)
                        this.playerStates[index].isDestroyedLantern = false;
                    else
                        this.playerStates[index].isDestroyedLantern = true;
                    break;
                case Tool.PICKAXE:
                    if (equipment.getType() == CType.EQ_REPAIR)
                        this.playerStates[index].isDestroyedPickaxe = false;
                    else
                        this.playerStates[index].isDestroyedPickaxe = true;
                    break;
            }
            
        }

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

            DeleteImage(this.selectedPic);

            //this.selectedPic.Left = X;
            //this.selectedPic.Top = Y;
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
