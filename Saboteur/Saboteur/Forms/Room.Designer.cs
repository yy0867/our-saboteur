using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Saboteur.Forms
{
    public partial class Room : UserControl
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


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatInputBox = new System.Windows.Forms.TextBox();
            this.playerLantern0 = new System.Windows.Forms.PictureBox();
            this.playerLantern1 = new System.Windows.Forms.PictureBox();
            this.playerLantern2 = new System.Windows.Forms.PictureBox();
            this.playerLantern6 = new System.Windows.Forms.PictureBox();
            this.playerLantern3 = new System.Windows.Forms.PictureBox();
            this.playerLantern4 = new System.Windows.Forms.PictureBox();
            this.playerLantern5 = new System.Windows.Forms.PictureBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.chatResultBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern5)).BeginInit();
            this.SuspendLayout();
            // 
            // chatInputBox
            // 
            this.chatInputBox.Location = new System.Drawing.Point(567, 595);
            this.chatInputBox.Name = "chatInputBox";
            this.chatInputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatInputBox.Size = new System.Drawing.Size(632, 21);
            this.chatInputBox.TabIndex = 3;
            this.chatInputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chatInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatInputBox_KeyDown);
            // 
            // playerLantern0
            // 
            this.playerLantern0.BackColor = System.Drawing.Color.Transparent;
            this.playerLantern0.Image = global::Saboteur.Properties.Resources.light_off;
            this.playerLantern0.Location = new System.Drawing.Point(97, -19);
            this.playerLantern0.Name = "playerLantern0";
            this.playerLantern0.Size = new System.Drawing.Size(130, 130);
            this.playerLantern0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern0.TabIndex = 1;
            this.playerLantern0.TabStop = false;
            // 
            // playerLantern1
            // 
            this.playerLantern1.BackColor = System.Drawing.Color.Transparent;
            this.playerLantern1.Image = global::Saboteur.Properties.Resources.light_off;
            this.playerLantern1.Location = new System.Drawing.Point(233, 117);
            this.playerLantern1.Name = "playerLantern1";
            this.playerLantern1.Size = new System.Drawing.Size(130, 130);
            this.playerLantern1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern1.TabIndex = 4;
            this.playerLantern1.TabStop = false;
            // 
            // playerLantern2
            // 
            this.playerLantern2.BackColor = System.Drawing.Color.Transparent;
            this.playerLantern2.Image = global::Saboteur.Properties.Resources.light_off;
            this.playerLantern2.Location = new System.Drawing.Point(381, 117);
            this.playerLantern2.Name = "playerLantern2";
            this.playerLantern2.Size = new System.Drawing.Size(130, 130);
            this.playerLantern2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern2.TabIndex = 5;
            this.playerLantern2.TabStop = false;
            // 
            // playerLantern6
            // 
            this.playerLantern6.BackColor = System.Drawing.Color.Transparent;
            this.playerLantern6.Image = global::Saboteur.Properties.Resources.light_off;
            this.playerLantern6.Location = new System.Drawing.Point(991, -19);
            this.playerLantern6.Name = "playerLantern6";
            this.playerLantern6.Size = new System.Drawing.Size(130, 130);
            this.playerLantern6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern6.TabIndex = 6;
            this.playerLantern6.TabStop = false;
            // 
            // playerLantern3
            // 
            this.playerLantern3.BackColor = System.Drawing.Color.Transparent;
            this.playerLantern3.Image = global::Saboteur.Properties.Resources.light_off;
            this.playerLantern3.Location = new System.Drawing.Point(517, 0);
            this.playerLantern3.Name = "playerLantern3";
            this.playerLantern3.Size = new System.Drawing.Size(130, 130);
            this.playerLantern3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern3.TabIndex = 7;
            this.playerLantern3.TabStop = false;
            // 
            // playerLantern4
            // 
            this.playerLantern4.BackColor = System.Drawing.Color.Transparent;
            this.playerLantern4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.playerLantern4.Image = global::Saboteur.Properties.Resources.light_off;
            this.playerLantern4.Location = new System.Drawing.Point(673, 117);
            this.playerLantern4.Name = "playerLantern4";
            this.playerLantern4.Size = new System.Drawing.Size(130, 130);
            this.playerLantern4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern4.TabIndex = 8;
            this.playerLantern4.TabStop = false;
            // 
            // playerLantern5
            // 
            this.playerLantern5.BackColor = System.Drawing.Color.Transparent;
            this.playerLantern5.Image = global::Saboteur.Properties.Resources.light_off;
            this.playerLantern5.Location = new System.Drawing.Point(841, 117);
            this.playerLantern5.Name = "playerLantern5";
            this.playerLantern5.Size = new System.Drawing.Size(130, 130);
            this.playerLantern5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playerLantern5.TabIndex = 9;
            this.playerLantern5.TabStop = false;
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btn_start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_start.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btn_start.FlatAppearance.BorderSize = 0;
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.ForeColor = System.Drawing.Color.White;
            this.btn_start.Location = new System.Drawing.Point(72, 538);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(234, 78);
            this.btn_start.TabIndex = 11;
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // chatResultBox
            // 
            this.chatResultBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(72)))), ((int)(((byte)(53)))));
            this.chatResultBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatResultBox.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chatResultBox.ForeColor = System.Drawing.Color.White;
            this.chatResultBox.Location = new System.Drawing.Point(567, 271);
            this.chatResultBox.Name = "chatResultBox";
            this.chatResultBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.chatResultBox.Size = new System.Drawing.Size(632, 318);
            this.chatResultBox.TabIndex = 13;
            this.chatResultBox.Text = "";
            // 
            // Room
            // 
            this.AutoScroll = true;
            this.BackgroundImage = global::Saboteur.Properties.Resources.backGround2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.chatResultBox);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.playerLantern6);
            this.Controls.Add(this.playerLantern5);
            this.Controls.Add(this.playerLantern4);
            this.Controls.Add(this.playerLantern1);
            this.Controls.Add(this.playerLantern3);
            this.Controls.Add(this.chatInputBox);
            this.Controls.Add(this.playerLantern2);
            this.Controls.Add(this.playerLantern0);
            this.DoubleBuffered = true;
            this.Name = "Room";
            this.Size = new System.Drawing.Size(1264, 681);
            this.Load += new System.EventHandler(this.Room_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.PictureBox playerLantern0;
        private System.Windows.Forms.TextBox chatInputBox;
        private System.Windows.Forms.PictureBox playerLantern1;
        private System.Windows.Forms.PictureBox playerLantern2;
        private System.Windows.Forms.PictureBox playerLantern6;
        private System.Windows.Forms.PictureBox playerLantern3;
        private System.Windows.Forms.PictureBox playerLantern4;
        private System.Windows.Forms.PictureBox playerLantern5;
        private Button btn_start;
        private RichTextBox chatResultBox;
    }
}
