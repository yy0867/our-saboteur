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
using System.Threading;
using PacketLibrary;

namespace Saboteur.Forms
{
    public partial class Room : UserControl
    {
        const int MAX_PLAYER = 7;
        const int SERVER_ID = -1;
        List<PictureBox> playerLanterns = new List<PictureBox>();
        bool[] isPlayer = new bool[MAX_PLAYER];
        int playerID = -1;
        
        private string serverIP = "172.30.1.37";

        RoomInfo receivedRoomInfo;

        private Packet mockPacket(int id, string message)
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.clientID = id;
            roomInfo.roomCode = 1234;
            roomInfo.players = new bool[] { true, true, true, true, false, false, false };
            roomInfo.message = message;
            return roomInfo;
        }
        
        public Room()
        {
            InitializeComponent();
            InitializeLantern();

            //updateInfo(mockPacket(3, ""));
            //updateInfo(mockPacket(2, "number 2 msg"));
            //updateInfo(mockPacket(3, "my msg"));
        }


        private void InitializeLantern()
        {
            for (int i = 0; i < MAX_PLAYER; i++)
            {
                playerLanterns.Add((PictureBox)Controls.Find("playerLantern" + i, true)[0]);
            }
        }

        private void Room_Load(object sender, EventArgs e)
        {
           

        }
        private void lanternImageToggle(int index)
        {
            Image lanternOn = Properties.Resources.light_on;
            Image lanternOff = Properties.Resources.light_off;

            this.Invoke((MethodInvoker)(() => {
                if (isPlayer[index])
                    playerLanterns[index].Image = lanternOn;
                else
                    playerLanterns[index].Image = lanternOff;
                isPlayer[index] = !isPlayer[index];
            }));
        }

        private void lanternImageToggle()
        {
            int playerSize = receivedRoomInfo.players.Length;
            for(int i = 0; i < playerSize; i++)
            {
                lanternImageToggle(i);
            }
       
        }

        public void updateInfo(Packet packet)
        {
            this.receivedRoomInfo = (RoomInfo)packet;
            this.isPlayer = this.receivedRoomInfo.players;
            lanternImageToggle();
            if (this.playerID == SERVER_ID)
                this.playerID = this.receivedRoomInfo.clientID;
            if (this.playerID == 0)
            {
                this.Invoke((MethodInvoker)(()=>{ this.btn_start.Visible = true; }));
                
            }   

            updateChattingLog(this.receivedRoomInfo.message, this.receivedRoomInfo.clientID);
        }

        private string convertMessage(string msg, int ID)
        {
            if (ID == this.playerID)
                return msg + " : [ " + this.playerID + " ] \r\n";
            else if (ID == SERVER_ID)
                return "******** " + msg + " ********\r\n";
            return "[ "+ ID + " ] : " + msg + "\r\n";
        }
        private void updateChattingLog(string newMessage, int receivedID)
        {
            this.Invoke((MethodInvoker)(() => {
                if (!newMessage.Equals(""))
                {
                    var convertedMessage = convertMessage(newMessage, receivedID);
                    this.chatResultBox.AppendText(convertedMessage);
                    int endPosition = convertedMessage.Length;
                    int startPosition = this.chatResultBox.Text.Length - endPosition + 1;
                    this.chatResultBox.Select(startPosition, endPosition);
                    if (receivedID == this.playerID)
                    {
                        this.chatResultBox.SelectionAlignment = HorizontalAlignment.Right;
                        this.chatResultBox.SelectionColor = Color.Goldenrod;
                        this.chatResultBox.SelectionFont = new Font(this.chatResultBox.Font, FontStyle.Bold | FontStyle.Underline);
                    } else if (receivedID == SERVER_ID)
                    {
                        this.chatResultBox.SelectionAlignment = HorizontalAlignment.Center;
                        this.chatResultBox.SelectionColor = Color.Green;
                        this.chatResultBox.SelectionFont = new Font("돋움", 15, FontStyle.Italic | FontStyle.Bold);
                    }
                }
                this.chatResultBox.Select(this.chatResultBox.Text.Length, 0);
                this.chatResultBox.ScrollToCaret();
            }));
        }


        private void chatInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var newChat = this.chatInputBox.Text;
                Task task = Task.Run(() =>
                {
                    Network.Send(getMessagePacket(newChat));
                });
                
                
                this.chatInputBox.ResetText();

                //test
                //updateInfo((mockPacket(3, newChat))); //send mock
                //updateInfo((mockPacket(5, "responed"))); // receive mock
                //updateInfo((mockPacket(SERVER_ID, "server"))); // receive mock
            }
        }

        private RoomInfo getMessagePacket(string msg)
        {
            RoomInfo packet = new RoomInfo();
            packet.message = msg;
            packet.roomCode = this.receivedRoomInfo.roomCode;
            packet.clientID = this.playerID;
            packet.roomCode = this.receivedRoomInfo.roomCode;

            return packet;
        }

        private GameInfo getGameStartPacket()
        {
            GameInfo packet = new GameInfo();
            packet.message = "";
            packet.clientID = this.playerID;

            return packet;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Task task = Task.Run(() =>
            {
                Network.Send(getGameStartPacket());
            });

            ViewController.SwitchScreen(Screen.Game);
        }
    }
}
