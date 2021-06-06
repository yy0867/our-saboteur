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

        public MainForm()
        {
            InitializeComponent();
            mainForm = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ViewController.SwitchScreen(Screen.Game);
        }
    }
}
