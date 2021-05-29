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
using System.Drawing;

namespace Saboteur.Forms
{
    public partial class Room : UserControl
    {
        const int MAX_PLAYER = 7;
        List<PictureBox> playerLanterns = new List<PictureBox>();
        bool[] isPlayer = new bool[MAX_PLAYER];
        int playerID = -1;
        
        private string serverIP = "127.0.0.1";

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

            updateInfo(mockPacket(3, ""));
            //updateInfo(mockPacket(2, "number 2 msg"));
            //updateInfo(mockPacket(3, "my msg"));
        }

        private void initializeScroll()
        {
            this.myChatResultBox.Select(this.myChatResultBox.Text.Length, 0);
            this.myChatResultBox.ScrollToCaret();
            this.otherChatResultBox.Select(this.otherChatResultBox.Text.Length, 0);
            this.otherChatResultBox.ScrollToCaret();
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

            if (isPlayer[index])
                playerLanterns[index].Image = lanternOn;
            else
                playerLanterns[index].Image = lanternOff;
            isPlayer[index] = !isPlayer[index];
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
            if (this.playerID == -1)
                this.playerID = this.receivedRoomInfo.clientID;

            updateChattingLog(this.receivedRoomInfo.message, this.receivedRoomInfo.clientID);
        }

        private void updateChattingLog(string[] newLog) {
            this.myChatResultBox.Lines = newLog;
        }

        private string convertMessage(string msg)
        {
            return "\r\n" + msg + " : [ " + this.playerID + " ] ";
        }

        private string convertMessage(string msg, int otherID)
        {
            return "\r\n" +" [ "+ otherID + " ] : " + msg;
        }
        private void updateChattingLog(string newMessage, int receivedID)
        {
            if (!newMessage.Equals(""))
            {
                if(receivedID == this.playerID)
                {
                    this.myChatResultBox.AppendText(convertMessage(newMessage));
                    this.otherChatResultBox.AppendText("\r\n");
                } else
                {
                    this.myChatResultBox.AppendText("\r\n");
                    this.otherChatResultBox.AppendText(convertMessage(newMessage, receivedID));
                }
            }
        }


        private void chatInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var newChat = this.chatInputBox.Text;
                Network.Send(getMessagePacket(newChat));
                
                this.chatInputBox.ResetText();

                //test
                //updateInfo((mockPacket(3, newChat))); //send mock
                //updateInfo((mockPacket(5, "responed"))); // receive mock
            }
        }

        private RoomInfo getMessagePacket(string msg)
        {
            RoomInfo packet = new RoomInfo();
            packet.message = msg;
            packet.clientID = this.playerID;
            packet.roomCode = this.receivedRoomInfo.roomCode;

            return packet;
        }

    }
}
