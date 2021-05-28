using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saboteur.Forms
{
    public partial class Room : UserControl
    {
        public Room()
        {
            InitializeComponent();
        }

        private void Room_Load(object sender, EventArgs e)
        {
            // 쓰레드로 실행
            // info = receive();
            // ui update
        }

        private void lanternImageToggle(PictureBox pictureBox)
        {
            Image lanternOn = new Bitmap("resources/icons/light_on.png");
            Image lanternOff = new Bitmap("resources/icons/light_off.png");

            if (pictureBox.Image == lanternOff)
                pictureBox.Image = lanternOn;
            else
                pictureBox.Image = lanternOff;
        }

        private void InitializeComponent()
        {
            this.chatResultBox = new System.Windows.Forms.TextBox();
            this.chatInputBox = new System.Windows.Forms.TextBox();
            this.playerLantern0 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern0)).BeginInit();
            this.SuspendLayout();
            // 
            // chatResultBox
            // 
            this.chatResultBox.Location = new System.Drawing.Point(471, 18);
            this.chatResultBox.Multiline = true;
            this.chatResultBox.Name = "chatResultBox";
            this.chatResultBox.Size = new System.Drawing.Size(756, 475);
            this.chatResultBox.TabIndex = 0;
            // 
            // chatInputBox
            // 
            this.chatInputBox.Location = new System.Drawing.Point(471, 533);
            this.chatInputBox.Name = "chatInputBox";
            this.chatInputBox.Size = new System.Drawing.Size(756, 35);
            this.chatInputBox.TabIndex = 3;
            // 
            // playerLantern0
            // 
            this.playerLantern0.Image = global::Saboteur.Properties.Resources.light_on;
            this.playerLantern0.Location = new System.Drawing.Point(65, 39);
            this.playerLantern0.Name = "playerLantern0";
            this.playerLantern0.Size = new System.Drawing.Size(48, 68);
            this.playerLantern0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern0.TabIndex = 1;
            this.playerLantern0.TabStop = false;
            // 
            // Room
            // 
            this.Controls.Add(this.chatResultBox);
            this.Controls.Add(this.playerLantern0);
            this.Controls.Add(this.chatInputBox);
            this.Name = "Room";
            this.Size = new System.Drawing.Size(1264, 681);
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void update(PacketLibrary.Packet packet)
        {
            bool[] lanterns = new bool[7];

            PacketLibrary.RoomInfo info = (PacketLibrary.RoomInfo)packet;
            // 랜턴
            // 메시지
            Network.Receive();
        }
    }
}
