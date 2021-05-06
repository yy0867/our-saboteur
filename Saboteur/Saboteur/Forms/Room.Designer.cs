
namespace Saboteur.Forms
{
    partial class Room
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
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
            this.ClientSize = new System.Drawing.Size(2347, 1362);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Room";
            this.Text = "Room";
            this.Load += new System.EventHandler(this.Room_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerLantern0)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TextBox chatResultBox;
        private System.Windows.Forms.TextBox chatInputBox;
        private System.Windows.Forms.PictureBox playerLantern0;
    }
}