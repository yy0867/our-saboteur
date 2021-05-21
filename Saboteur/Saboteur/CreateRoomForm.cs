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
    public partial class CreateRoomForm : Form
    {
        public CreateRoomForm()
        {
            InitializeComponent();
            btnCreateRoomRequest.Enabled = false;
        }

        private void btnCreateRoomRequest_Click(object sender, EventArgs e)
        {
            /// [TODO] Request Create Room For Server [x]
            /// [TODO] Get Room Info & set ViewModel for Room[x]

            /// [TODO] Close this form [v]
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtRoomName_TextChanged(object sender, EventArgs e)
        {
            // button disabled when room name is empty
            btnCreateRoomRequest.Enabled = txtRoomName.TextLength != 0;
        }
    }
}
