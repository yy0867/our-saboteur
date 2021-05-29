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
        List<PictureBox> playerLanterns = new List<PictureBox>();
        bool[] isPlayer = new bool[MAX_PLAYER];
        
        private string serverIP = "127.0.0.1";

        RoomInfo receivedRoomInfo;

        private Packet mockPacket()
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.clientID = 0;
            roomInfo.roomCode = 1234;
            roomInfo.players = new bool[] { true, true, true, true, false, false, false };
            roomInfo.message = new string[] { "1st line", "second line" };
            return roomInfo;
        }

        public Room()
        {
            InitializeComponent();
            initializeLantern();

            updateInfo(mockPacket());
        }

        private void Room_Load(object sender, EventArgs e)
        {
           

        }

        private void initializeLantern()
        {
            for (int i = 0; i < MAX_PLAYER; i++)
            {
                playerLanterns.Add((PictureBox)Controls.Find("playerLantern" + i, true)[0]);
            }
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
            updateChattingLog(this.receivedRoomInfo.message);
        }

        private void updateChattingLog(string[] newLog) {
            this.chatResultBox.Lines = newLog;
        }


        private void chatInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //sendMessageToServer
            }
        }
    }
}
