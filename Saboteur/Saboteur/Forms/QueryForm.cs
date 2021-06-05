using CardLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Saboteur.Forms
{
    public partial class QueryForm : Form
    {
        Action<Tool> action;
        public QueryForm(Tool tools, Action<Tool> action)
        {
            InitializeComponent();
            this.action = action;
            switch (tools)
            {
                case Tool.PICKLATTERN:
                    this.btn_top.BackgroundImage = Properties.Resources.item_unblock_pickax;
                    this.btn_top.Tag = Tool.PICKAXE;
                    this.btn_bottom.BackgroundImage = Properties.Resources.item_unblock_lantern;
                    this.btn_bottom.Tag = Tool.LATTERN;
                    break;
                case Tool.PICKCART:
                    this.btn_top.BackgroundImage = Properties.Resources.item_unblock_pickax;
                    this.btn_top.Tag = Tool.PICKAXE;
                    this.btn_bottom.BackgroundImage = Properties.Resources.item_unblock_cart;
                    this.btn_bottom.Tag = Tool.CART;
                    break;
                case Tool.LATTERNCART:
                    this.btn_top.BackgroundImage = Properties.Resources.item_unblock_lantern;
                    this.btn_top.Tag = Tool.LATTERN;
                    this.btn_bottom.BackgroundImage = Properties.Resources.item_unblock_cart;
                    this.btn_bottom.Tag = Tool.CART;
                    break;
            }
        }

        private void btn_top_Click(object sender, EventArgs e)
        {
            this.action((Tool)btn_top.Tag);
            this.Close();
        }

        private void btn_bottom_Click(object sender, EventArgs e)
        {
            this.action((Tool)btn_bottom.Tag);
            this.Close();
        }
    }
}
