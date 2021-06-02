
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.picFieldBackground = new System.Windows.Forms.PictureBox();
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
            this.lblDeckNum = new System.Windows.Forms.Label();
            this.lblUsedCard = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picFieldBackground)).BeginInit();
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
            // picFieldBackground
            // 
            this.picFieldBackground.BackColor = System.Drawing.Color.Transparent;
            this.picFieldBackground.Image = global::Saboteur.Properties.Resources.game_background;
            this.picFieldBackground.Location = new System.Drawing.Point(0, 0);
            this.picFieldBackground.Name = "picFieldBackground";
            this.picFieldBackground.Size = new System.Drawing.Size(1365, 881);
            this.picFieldBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFieldBackground.TabIndex = 0;
            this.picFieldBackground.TabStop = false;
            this.picFieldBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.picFieldBackground_Paint);
            // 
            // panDeck
            // 
            this.panDeck.BackColor = System.Drawing.SystemColors.Control;
            this.panDeck.ForeColor = System.Drawing.Color.Transparent;
            this.panDeck.Location = new System.Drawing.Point(1365, 0);
            this.panDeck.Name = "panDeck";
            this.panDeck.Size = new System.Drawing.Size(216, 881);
            this.panDeck.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("예스체", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1469, 967);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "7";
            // 
            // lblDeck
            // 
            this.lblDeck.AutoSize = true;
            this.lblDeck.BackColor = System.Drawing.Color.Transparent;
            this.lblDeck.Font = new System.Drawing.Font("예스체", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDeck.ForeColor = System.Drawing.Color.White;
            this.lblDeck.Location = new System.Drawing.Point(1196, 967);
            this.lblDeck.Name = "lblDeck";
            this.lblDeck.Size = new System.Drawing.Size(38, 28);
            this.lblDeck.TabIndex = 11;
            this.lblDeck.Text = "40";
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::Saboteur.Properties.Resources.card;
            this.pictureBox10.Location = new System.Drawing.Point(1332, 897);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(77, 125);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox10.TabIndex = 10;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::Saboteur.Properties.Resources.card;
            this.pictureBox9.Location = new System.Drawing.Point(1064, 897);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(77, 125);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 9;
            this.pictureBox9.TabStop = false;
            // 
            // picCard1
            // 
            this.picCard1.Image = global::Saboteur.Properties.Resources.card;
            this.picCard1.Location = new System.Drawing.Point(24, 897);
            this.picCard1.Name = "picCard1";
            this.picCard1.Size = new System.Drawing.Size(77, 125);
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
            this.picCard2.Location = new System.Drawing.Point(135, 897);
            this.picCard2.Name = "picCard2";
            this.picCard2.Size = new System.Drawing.Size(77, 125);
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
            this.picCard7.Location = new System.Drawing.Point(698, 897);
            this.picCard7.Name = "picCard7";
            this.picCard7.Size = new System.Drawing.Size(77, 125);
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
            this.picCard6.Location = new System.Drawing.Point(586, 897);
            this.picCard6.Name = "picCard6";
            this.picCard6.Size = new System.Drawing.Size(77, 125);
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
            this.picCard5.Location = new System.Drawing.Point(472, 897);
            this.picCard5.Name = "picCard5";
            this.picCard5.Size = new System.Drawing.Size(77, 125);
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
            this.picCard4.Location = new System.Drawing.Point(360, 897);
            this.picCard4.Name = "picCard4";
            this.picCard4.Size = new System.Drawing.Size(77, 125);
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
            this.picCard3.Location = new System.Drawing.Point(247, 897);
            this.picCard3.Name = "picCard3";
            this.picCard3.Size = new System.Drawing.Size(77, 125);
            this.picCard3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCard3.TabIndex = 8;
            this.picCard3.TabStop = false;
            this.picCard3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseDown);
            this.picCard3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseMove);
            this.picCard3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCard_MouseUp);
            // 
            // lblDeckNum
            // 
            this.lblDeckNum.AutoSize = true;
            this.lblDeckNum.BackColor = System.Drawing.Color.Transparent;
            this.lblDeckNum.Font = new System.Drawing.Font("예스체", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDeckNum.ForeColor = System.Drawing.Color.White;
            this.lblDeckNum.Location = new System.Drawing.Point(1155, 921);
            this.lblDeckNum.Name = "lblDeckNum";
            this.lblDeckNum.Size = new System.Drawing.Size(119, 28);
            this.lblDeckNum.TabIndex = 12;
            this.lblDeckNum.Text = "남은 카드 수";
            // 
            // lblUsedCard
            // 
            this.lblUsedCard.AutoSize = true;
            this.lblUsedCard.BackColor = System.Drawing.Color.Transparent;
            this.lblUsedCard.Font = new System.Drawing.Font("예스체", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblUsedCard.ForeColor = System.Drawing.Color.White;
            this.lblUsedCard.Location = new System.Drawing.Point(1426, 921);
            this.lblUsedCard.Name = "lblUsedCard";
            this.lblUsedCard.Size = new System.Drawing.Size(113, 28);
            this.lblUsedCard.TabIndex = 13;
            this.lblUsedCard.Text = "사용된 카드";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.lblUsedCard);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDeckNum);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.picCard3);
            this.Controls.Add(this.lblDeck);
            this.Controls.Add(this.picCard4);
            this.Controls.Add(this.picCard5);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.picCard6);
            this.Controls.Add(this.picCard7);
            this.Controls.Add(this.picCard2);
            this.Controls.Add(this.picCard1);
            this.Controls.Add(this.panDeck);
            this.Controls.Add(this.picFieldBackground);
            this.Name = "Game";
            this.Size = new System.Drawing.Size(1581, 1041);
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFieldBackground)).EndInit();
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
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picFieldBackground;
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
        private System.Windows.Forms.Label lblDeckNum;
        private System.Windows.Forms.Label lblUsedCard;
    }
}
