
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
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.panJoinRoom = new System.Windows.Forms.Panel();
            this.lblInputPassword = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.panJoinRoom.SuspendLayout();
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
            this.btnCreateRoom.Location = new System.Drawing.Point(486, 389);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(286, 51);
            this.btnCreateRoom.TabIndex = 0;
            this.btnCreateRoom.Text = "방 만들기";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            // 
            // picBackground
            // 
            this.picBackground.Image = ((System.Drawing.Image)(resources.GetObject("picBackground.Image")));
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(1264, 681);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackground.TabIndex = 1;
            this.picBackground.TabStop = false;
            // 
            // panJoinRoom
            // 
            this.panJoinRoom.Controls.Add(this.textBox1);
            this.panJoinRoom.Controls.Add(this.lblInputPassword);
            this.panJoinRoom.Location = new System.Drawing.Point(486, 532);
            this.panJoinRoom.Name = "panJoinRoom";
            this.panJoinRoom.Size = new System.Drawing.Size(286, 51);
            this.panJoinRoom.TabIndex = 2;
            // 
            // lblInputPassword
            // 
            this.lblInputPassword.AutoSize = true;
            this.lblInputPassword.Location = new System.Drawing.Point(14, 20);
            this.lblInputPassword.Name = "lblInputPassword";
            this.lblInputPassword.Size = new System.Drawing.Size(57, 12);
            this.lblInputPassword.TabIndex = 0;
            this.lblInputPassword.Text = "코드 입력";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(81, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(185, 21);
            this.textBox1.TabIndex = 1;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panJoinRoom);
            this.Controls.Add(this.btnCreateRoom);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.picBackground);
            this.Name = "MainMenu";
            this.Size = new System.Drawing.Size(1264, 681);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.panJoinRoom.ResumeLayout(false);
            this.panJoinRoom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.Panel panJoinRoom;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblInputPassword;
    }
}
