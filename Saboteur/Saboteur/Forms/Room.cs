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
            
        }

        private void lanternImageToggle(PictureBox pictureBox)
        {
            if (pictureBox.Image == Properties.Resources.light_off)
                pictureBox.Image = Properties.Resources.light_on;
            else
                pictureBox.Image = Properties.Resources.light_off;
        }

        private void InitializeComponent()
        {
            this.chatResultBox = new System.Windows.Forms.TextBox();
            this.playerLantern0 = new System.Windows.Forms.PictureBox();
            this.chatInputBox = new System.Windows.Forms.TextBox();
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
            // chatInputBox
            // 
            this.chatInputBox.Location = new System.Drawing.Point(471, 533);
            this.chatInputBox.Name = "chatInputBox";
            this.chatInputBox.Size = new System.Drawing.Size(756, 35);
            this.chatInputBox.TabIndex = 3;
            // 
            // Room
            // 
            this.Controls.Add(this.chatInputBox);
            this.Controls.Add(this.playerLantern0);
            this.Controls.Add(this.chatResultBox);
            this.Name = "Room";
            this.Size = new System.Drawing.Size(1264, 681);
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
