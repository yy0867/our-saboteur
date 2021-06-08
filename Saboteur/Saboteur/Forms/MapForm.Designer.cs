
namespace Saboteur.Forms
{
    partial class MapForm
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
            this.button = new System.Windows.Forms.Button();
            this.resultPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.resultPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button.Location = new System.Drawing.Point(0, 388);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(274, 68);
            this.button.TabIndex = 2;
            this.button.Text = "확인";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // resultPicture
            // 
            this.resultPicture.BackgroundImage = global::Saboteur.Properties.Resources.goal_stone_down_left;
            this.resultPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.resultPicture.Dock = System.Windows.Forms.DockStyle.Top;
            this.resultPicture.Location = new System.Drawing.Point(0, 0);
            this.resultPicture.Name = "resultPicture";
            this.resultPicture.Size = new System.Drawing.Size(274, 388);
            this.resultPicture.TabIndex = 0;
            this.resultPicture.TabStop = false;
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 456);
            this.Controls.Add(this.button);
            this.Controls.Add(this.resultPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapForm";
            ((System.ComponentModel.ISupportInitialize)(this.resultPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox resultPicture;
        private System.Windows.Forms.Button button;
    }
}