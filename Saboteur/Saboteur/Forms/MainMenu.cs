using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using PacketLibrary;

namespace Saboteur.Forms
{
    public partial class MainMenu : UserControl
    {
        private const int MIN_CODE_LEN = 4;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            panJoinRoom.Visible = false;
            btnCreateRoom.Enabled = false;
            btnJoinRoom.Enabled = false;
        }



        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            panJoinRoom.Visible = true;     // set panel visible which hide in bottom picturebox

            // panel up animation
            for (int i = panJoinRoom.Top; i >= btnJoinRoom.Top; i--)
            {
                panJoinRoom.Top--;
                Thread.Sleep(5);
            }

            btnJoinRequest.Enabled = false;
            txtRoomCode.Focus(); // set focus to code input textbox
        }

        private void txtRoomCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // room code textbox allows numbers & backspace only
            if (char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)) return;
            e.Handled = true;
        }

        private void txtRoomCode_TextChanged(object sender, EventArgs e)
        {
            // join request button status depands on input code's length
            btnJoinRequest.Enabled = txtRoomCode.TextLength >= MIN_CODE_LEN;
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            if (!Network.isConnected)
                return;

            // Request Create Room
            RoomInfo info = new RoomInfo();
            Network.Send(info);

            ViewController.SwitchScreen(Screen.Room);
        }

        private void btnJoinRequest_Click(object sender, EventArgs e)
        {
            if (!Network.isConnected)
                return;

            /// [TODO] send txtRoomCode.Text [x]
            RoomInfo info = new RoomInfo();
            info.roomCode = int.Parse(txtRoomCode.Text);
            Network.Send(info);

            // else (failed)
            //MessageBox.Show("일치하는 방이 없습니다. \r\n다시 입력해주세요!", "No Room", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //txtRoomCode.Clear();
            //txtRoomCode.Focus();

            /// [IMPORTANT] TEST CODE
            /// ******** DELETE REQUIRED ********
            ViewController.SwitchScreen(Screen.Room);
        }

        private void btnConnectServer_Click(object sender, EventArgs e)
        {
            Network.setServerIP = txtServerIP.Text;
            if (Network.Connect())
            {
                MessageBox.Show("서버와 정상적으로 연결되었습니다.", "Connected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Network의 Receive Thread 시작
                Thread receiveThread = new Thread(new ThreadStart(Network.Receive));
                receiveThread.Start();

                // 버튼 활성화
                btnCreateRoom.Enabled = true;
                btnJoinRoom.Enabled = true;
            }
            else
            {
                MessageBox.Show("서버와 연결되지 않았습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
