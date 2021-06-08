
namespace Saboteur.Forms
{
    partial class Chatting_form
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
            this.chatResultBox = new System.Windows.Forms.RichTextBox();
            this.chatInputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chatResultBox
            // 
            this.chatResultBox.BackColor = System.Drawing.Color.DimGray;
            this.chatResultBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatResultBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatResultBox.Font = new System.Drawing.Font("돋움", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chatResultBox.ForeColor = System.Drawing.Color.White;
            this.chatResultBox.Location = new System.Drawing.Point(0, 0);
            this.chatResultBox.Name = "chatResultBox";
            this.chatResultBox.ReadOnly = true;
            this.chatResultBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.chatResultBox.Size = new System.Drawing.Size(553, 777);
            this.chatResultBox.TabIndex = 15;
            this.chatResultBox.Text = "";
            // 
            // chatInputBox
            // 
            this.chatInputBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chatInputBox.Location = new System.Drawing.Point(0, 777);
            this.chatInputBox.Name = "chatInputBox";
            this.chatInputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatInputBox.Size = new System.Drawing.Size(553, 21);
            this.chatInputBox.TabIndex = 14;
            this.chatInputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chatInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatInputBox_KeyDown);
            // 
            // Chatting_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 798);
            this.Controls.Add(this.chatResultBox);
            this.Controls.Add(this.chatInputBox);
            this.Name = "Chatting_form";
            this.Text = "Chatting_form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatResultBox;
        private System.Windows.Forms.TextBox chatInputBox;
    }
}