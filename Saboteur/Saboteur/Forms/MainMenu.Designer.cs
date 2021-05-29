
namespace Saboteur.Forms
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.button1 = new System.Windows.Forms.Button();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.panJoinRoom = new System.Windows.Forms.Panel();
            this.btnJoinRequest = new System.Windows.Forms.Button();
            this.txtRoomCode = new System.Windows.Forms.TextBox();
            this.lblInputPassword = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.btnConnectServer = new System.Windows.Forms.Button();
            this.panJoinRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.BackColor = System.Drawing.SystemColors.Control;
            this.btnJoinRoom.Font = new System.Drawing.Font("예스체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnJoinRoom.Location = new System.Drawing.Point(486, 475);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(286, 51);
            this.btnJoinRoom.TabIndex = 0;
            this.btnJoinRoom.Text = "방 참가하기";
            this.btnJoinRoom.UseVisualStyleBackColor = false;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.Font = new System.Drawing.Font("예스체", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCreateRoom.Location = new System.Drawing.Point(486, 389);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(286, 51);
            this.btnCreateRoom.TabIndex = 0;
            this.btnCreateRoom.Text = "방 만들기";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // panJoinRoom
            // 
            this.panJoinRoom.Controls.Add(this.btnJoinRequest);
            this.panJoinRoom.Controls.Add(this.txtRoomCode);
            this.panJoinRoom.Controls.Add(this.lblInputPassword);
            this.panJoinRoom.Location = new System.Drawing.Point(486, 532);
            this.panJoinRoom.Name = "panJoinRoom";
            this.panJoinRoom.Size = new System.Drawing.Size(286, 51);
            this.panJoinRoom.TabIndex = 2;
            // 
            // btnJoinRequest
            // 
            this.btnJoinRequest.Image = ((System.Drawing.Image)(resources.GetObject("btnJoinRequest.Image")));
            this.btnJoinRequest.Location = new System.Drawing.Point(227, 8);
            this.btnJoinRequest.Name = "btnJoinRequest";
            this.btnJoinRequest.Size = new System.Drawing.Size(48, 33);
            this.btnJoinRequest.TabIndex = 2;
            this.btnJoinRequest.UseVisualStyleBackColor = true;
            this.btnJoinRequest.Click += new System.EventHandler(this.btnJoinRequest_Click);
            // 
            // txtRoomCode
            // 
            this.txtRoomCode.Font = new System.Drawing.Font("예스체", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtRoomCode.Location = new System.Drawing.Point(81, 13);
            this.txtRoomCode.Name = "txtRoomCode";
            this.txtRoomCode.Size = new System.Drawing.Size(128, 25);
            this.txtRoomCode.TabIndex = 1;
            this.txtRoomCode.TextChanged += new System.EventHandler(this.txtRoomCode_TextChanged);
            this.txtRoomCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoomCode_KeyPress);
            // 
            // lblInputPassword
            // 
            this.lblInputPassword.AutoSize = true;
            this.lblInputPassword.Font = new System.Drawing.Font("예스체", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInputPassword.Location = new System.Drawing.Point(14, 18);
            this.lblInputPassword.Name = "lblInputPassword";
            this.lblInputPassword.Size = new System.Drawing.Size(54, 15);
            this.lblInputPassword.TabIndex = 0;
            this.lblInputPassword.Text = "코드 입력";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 525);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1264, 156);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // picBackground
            // 
            this.picBackground.Image = ((System.Drawing.Image)(resources.GetObject("picBackground.Image")));
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(1264, 526);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackground.TabIndex = 1;
            this.picBackground.TabStop = false;
            // 
            // txtServerIP
            // 
            this.txtServerIP.Font = new System.Drawing.Font("예스체", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtServerIP.Location = new System.Drawing.Point(486, 309);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(209, 33);
            this.txtServerIP.TabIndex = 4;
            // 
            // btnConnectServer
            // 
            this.btnConnectServer.Font = new System.Drawing.Font("예스체", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConnectServer.Location = new System.Drawing.Point(713, 309);
            this.btnConnectServer.Name = "btnConnectServer";
            this.btnConnectServer.Size = new System.Drawing.Size(59, 33);
            this.btnConnectServer.TabIndex = 5;
            this.btnConnectServer.Text = "연결";
            this.btnConnectServer.UseVisualStyleBackColor = true;
            this.btnConnectServer.Click += new System.EventHandler(this.btnConnectServer_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConnectServer);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panJoinRoom);
            this.Controls.Add(this.btnCreateRoom);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.picBackground);
            this.Name = "MainMenu";
            this.Size = new System.Drawing.Size(1264, 681);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.panJoinRoom.ResumeLayout(false);
            this.panJoinRoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.Panel panJoinRoom;
        private System.Windows.Forms.TextBox txtRoomCode;
        private System.Windows.Forms.Label lblInputPassword;
        private System.Windows.Forms.Button btnJoinRequest;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Button btnConnectServer;
    }
}
