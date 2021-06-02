
namespace Saboteur.Forms
{
    partial class Game
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
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.panDeck = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDeck = new System.Windows.Forms.Label();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.picCard1 = new System.Windows.Forms.PictureBox();
            this.picCard2 = new System.Windows.Forms.PictureBox();
            this.picCard7 = new System.Windows.Forms.PictureBox();
            this.picCard6 = new System.Windows.Forms.PictureBox();
            this.picCard5 = new System.Windows.Forms.PictureBox();
            this.picCard4 = new System.Windows.Forms.PictureBox();
            this.picCard3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.panDeck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard3)).BeginInit();
            this.SuspendLayout();
            // 
            // picBackground
            // 
            this.picBackground.BackColor = System.Drawing.Color.Transparent;
            this.picBackground.Image = global::Saboteur.Properties.Resources.game_background;
            this.picBackground.Location = new System.Drawing.Point(0, 76);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(1375, 623);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackground.TabIndex = 0;
            this.picBackground.TabStop = false;
            // 
            // panDeck
            // 
            this.panDeck.BackColor = System.Drawing.SystemColors.Control;
            this.panDeck.BackgroundImage = global::Saboteur.Properties.Resources.borad0;
            this.panDeck.Controls.Add(this.label1);
            this.panDeck.Controls.Add(this.lblDeck);
            this.panDeck.Controls.Add(this.pictureBox10);
            this.panDeck.Controls.Add(this.pictureBox9);
            this.panDeck.ForeColor = System.Drawing.Color.Transparent;
            this.panDeck.Location = new System.Drawing.Point(1381, 76);
            this.panDeck.Name = "panDeck";
            this.panDeck.Size = new System.Drawing.Size(200, 623);
            this.panDeck.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("예스체", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(86, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "7";
            // 
            // lblDeck
            // 
            this.lblDeck.AutoSize = true;
            this.lblDeck.Font = new System.Drawing.Font("예스체", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDeck.ForeColor = System.Drawing.Color.Black;
            this.lblDeck.Location = new System.Drawing.Point(82, 170);
            this.lblDeck.Name = "lblDeck";
            this.lblDeck.Size = new System.Drawing.Size(38, 28);
            this.lblDeck.TabIndex = 11;
            this.lblDeck.Text = "40";
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::Saboteur.Properties.Resources.card;
            this.pictureBox10.Location = new System.Drawing.Point(59, 263);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(82, 132);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox10.TabIndex = 10;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::Saboteur.Properties.Resources.card;
            this.pictureBox9.Location = new System.Drawing.Point(59, 35);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(82, 132);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 9;
            this.pictureBox9.TabStop = false;
            // 
            // picCard1
            // 
            this.picCard1.Image = global::Saboteur.Properties.Resources.card;
            this.picCard1.Location = new System.Drawing.Point(114, 716);
            this.picCard1.Name = "picCard1";
            this.picCard1.Size = new System.Drawing.Size(82, 132);
            this.picCard1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCard1.TabIndex = 2;
            this.picCard1.TabStop = false;
            this.picCard1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseDown);
            this.picCard1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseMove);
            this.picCard1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseUp);
            // 
            // picCard2
            // 
            this.picCard2.Image = global::Saboteur.Properties.Resources.card;
            this.picCard2.Location = new System.Drawing.Point(310, 716);
            this.picCard2.Name = "picCard2";
            this.picCard2.Size = new System.Drawing.Size(82, 132);
            this.picCard2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCard2.TabIndex = 3;
            this.picCard2.TabStop = false;
            this.picCard2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseDown);
            this.picCard2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseMove);
            this.picCard2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseUp);
            // 
            // picCard7
            // 
            this.picCard7.Image = global::Saboteur.Properties.Resources.card;
            this.picCard7.Location = new System.Drawing.Point(1302, 716);
            this.picCard7.Name = "picCard7";
            this.picCard7.Size = new System.Drawing.Size(82, 132);
            this.picCard7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCard7.TabIndex = 4;
            this.picCard7.TabStop = false;
            this.picCard7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseDown);
            this.picCard7.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseMove);
            this.picCard7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseUp);
            // 
            // picCard6
            // 
            this.picCard6.Image = global::Saboteur.Properties.Resources.card;
            this.picCard6.Location = new System.Drawing.Point(1117, 716);
            this.picCard6.Name = "picCard6";
            this.picCard6.Size = new System.Drawing.Size(82, 132);
            this.picCard6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCard6.TabIndex = 5;
            this.picCard6.TabStop = false;
            this.picCard6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseDown);
            this.picCard6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseMove);
            this.picCard6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseUp);
            // 
            // picCard5
            // 
            this.picCard5.Image = global::Saboteur.Properties.Resources.card;
            this.picCard5.Location = new System.Drawing.Point(923, 716);
            this.picCard5.Name = "picCard5";
            this.picCard5.Size = new System.Drawing.Size(82, 132);
            this.picCard5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCard5.TabIndex = 6;
            this.picCard5.TabStop = false;
            this.picCard5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseDown);
            this.picCard5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseMove);
            this.picCard5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseUp);
            // 
            // picCard4
            // 
            this.picCard4.Image = global::Saboteur.Properties.Resources.card;
            this.picCard4.Location = new System.Drawing.Point(720, 716);
            this.picCard4.Name = "picCard4";
            this.picCard4.Size = new System.Drawing.Size(82, 132);
            this.picCard4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCard4.TabIndex = 7;
            this.picCard4.TabStop = false;
            this.picCard4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseDown);
            this.picCard4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseMove);
            this.picCard4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseUp);
            // 
            // picCard3
            // 
            this.picCard3.Image = global::Saboteur.Properties.Resources.card;
            this.picCard3.Location = new System.Drawing.Point(516, 716);
            this.picCard3.Name = "picCard3";
            this.picCard3.Size = new System.Drawing.Size(82, 132);
            this.picCard3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCard3.TabIndex = 8;
            this.picCard3.TabStop = false;
            this.picCard3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseDown);
            this.picCard3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseMove);
            this.picCard3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseUp);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picCard3);
            this.Controls.Add(this.picCard4);
            this.Controls.Add(this.picCard5);
            this.Controls.Add(this.picCard6);
            this.Controls.Add(this.picCard7);
            this.Controls.Add(this.picCard2);
            this.Controls.Add(this.picCard1);
            this.Controls.Add(this.panDeck);
            this.Controls.Add(this.picBackground);
            this.Name = "Game";
            this.Size = new System.Drawing.Size(1581, 861);
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.panDeck.ResumeLayout(false);
            this.panDeck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCard3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.Panel panDeck;
        private System.Windows.Forms.PictureBox picCard1;
        private System.Windows.Forms.PictureBox picCard2;
        private System.Windows.Forms.PictureBox picCard7;
        private System.Windows.Forms.PictureBox picCard6;
        private System.Windows.Forms.PictureBox picCard5;
        private System.Windows.Forms.PictureBox picCard4;
        private System.Windows.Forms.PictureBox picCard3;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDeck;
    }
}
