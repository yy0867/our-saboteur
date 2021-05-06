using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saboteur
{

    public partial class MainForm : Form
    {
        public static MainForm mainForm;
        private ViewController viewController;

        public MainForm()
        {
            InitializeComponent();
            mainForm = this;
            viewController = new ViewController();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            viewController.switchScreen(Screen.MainMenu);
        }
    }
}
