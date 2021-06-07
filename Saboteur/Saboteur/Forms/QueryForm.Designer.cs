
namespace Saboteur.Forms
{
    partial class QueryForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_top = new System.Windows.Forms.Button();
            this.btn_bottom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_top
            // 
            this.btn_top.BackColor = System.Drawing.Color.Transparent;
            this.btn_top.BackgroundImage = global::Saboteur.Properties.Resources.item_unblock_cart;
            this.btn_top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_top.Location = new System.Drawing.Point(24, 125);
            this.btn_top.Name = "btn_top";
            this.btn_top.Size = new System.Drawing.Size(137, 191);
            this.btn_top.TabIndex = 0;
            this.btn_top.UseVisualStyleBackColor = false;
            this.btn_top.Click += new System.EventHandler(this.btn_top_Click);
            // 
            // btn_bottom
            // 
            this.btn_bottom.BackColor = System.Drawing.Color.Transparent;
            this.btn_bottom.BackgroundImage = global::Saboteur.Properties.Resources.item_unblock_cart;
            this.btn_bottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_bottom.Location = new System.Drawing.Point(260, 125);
            this.btn_bottom.Name = "btn_bottom";
            this.btn_bottom.Size = new System.Drawing.Size(137, 191);
            this.btn_bottom.TabIndex = 0;
            this.btn_bottom.UseVisualStyleBackColor = false;
            this.btn_bottom.Click += new System.EventHandler(this.btn_bottom_Click);
            // 
            // QueryForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 447);
            this.Controls.Add(this.btn_bottom);
            this.Controls.Add(this.btn_top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_top;
        private System.Windows.Forms.Button btn_bottom;
    }
}
