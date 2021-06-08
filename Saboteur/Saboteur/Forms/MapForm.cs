using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saboteur.Forms
{
    public partial class MapForm : Form
    {
        public MapForm(Image image)
        {
            InitializeComponent();
            this.resultPicture.BackgroundImage = image;
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
