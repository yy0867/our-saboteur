
namespace Saboteur.Forms
{
    partial class Room
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.chatResultBox = new System.Windows.Forms.TextBox();
            this.chatInputBox = new System.Windows.Forms.TextBox();
            this.playerLantern0 = new System.Windows.Forms.PictureBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern0)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.playerLantern0);
            this.mainPanel.Controls.Add(this.chatInputBox);
            this.mainPanel.Controls.Add(this.chatResultBox);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(2347, 1362);
            this.mainPanel.TabIndex = 1;
            // 
            // chatResultBox
            // 
            this.chatResultBox.Location = new System.Drawing.Point(918, 38);
            this.chatResultBox.Multiline = true;
            this.chatResultBox.Name = "chatResultBox";
            this.chatResultBox.Size = new System.Drawing.Size(1396, 889);
            this.chatResultBox.TabIndex = 0;
            // 
            // chatInputBox
            // 
            this.chatInputBox.Location = new System.Drawing.Point(918, 976);
            this.chatInputBox.Name = "chatInputBox";
            this.chatInputBox.Size = new System.Drawing.Size(1395, 35);
            this.chatInputBox.TabIndex = 2;
            // 
            // playerLantern0
            // 
            this.playerLantern0.Image = global::Saboteur.Properties.Resources.light_on;
            this.playerLantern0.Location = new System.Drawing.Point(261, 288);
            this.playerLantern0.Name = "playerLantern0";
            this.playerLantern0.Size = new System.Drawing.Size(189, 270);
            this.playerLantern0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern0.TabIndex = 3;
            this.playerLantern0.TabStop = false;
            this.playerLantern0.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Room";
            this.Size = new System.Drawing.Size(1264, 681);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TextBox chatResultBox;
        private System.Windows.Forms.TextBox chatInputBox;
        private System.Windows.Forms.PictureBox playerLantern0;
    }
}
