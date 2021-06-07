using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketLibrary;

namespace Saboteur.Forms
{
    public partial class Chatting_form : Form
    {
        const int SERVER_ID = -1;
        int playerID;
        NetworkStream stream = null;
        public Chatting_form(int playerID)
        {
            this.playerID = playerID;
            InitializeComponent();
            Network.setServerIP ="127.0.0.1";
            Network.Connect(15000, ref stream);
            Task.Run(() =>
            {
                Network.Receive(updateInfo, stream);
            });
            this.ShowDialog();
        }

        private string convertMessage(string msg, int ID)
        {
            if (ID == this.playerID)
                return msg + " : [ " + this.playerID + " ] \r\n";
            else if (ID == SERVER_ID)
                return "******** " + msg + " ********\r\n";
            return "[ " + ID + " ] : " + msg + "\r\n";
        }

        private void updateInfo(Packet packet)
        {
            MessagePacket msgPacket = packet as MessagePacket;
            if(msgPacket != null)
            {
                updateChattingLog(msgPacket.message, msgPacket.clientID);
            }
        }
        private void updateChattingLog(string newMessage, int receivedID)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() => {
                    if (!newMessage.Equals(""))
                    {
                        var convertedMessage = convertMessage(newMessage, receivedID);
                        this.chatResultBox.AppendText(convertedMessage);
                        int endPosition = convertedMessage.Length;
                        int startPosition = this.chatResultBox.Text.Length - endPosition + 1;
                        this.chatResultBox.Select(startPosition, endPosition);
                        if (receivedID == this.playerID)
                        {
                            this.chatResultBox.SelectionAlignment = HorizontalAlignment.Right;
                            this.chatResultBox.SelectionColor = Color.Goldenrod;
                            this.chatResultBox.SelectionFont = new Font(this.chatResultBox.Font, FontStyle.Bold | FontStyle.Underline);
                        } else if (receivedID == SERVER_ID)
                        {
                            this.chatResultBox.SelectionAlignment = HorizontalAlignment.Center;
                            this.chatResultBox.SelectionColor = Color.Green;
                            this.chatResultBox.SelectionFont = new Font("돋움", 15, FontStyle.Italic | FontStyle.Bold);
                        }
                    }
                    this.chatResultBox.Select(this.chatResultBox.Text.Length, 0);
                    this.chatResultBox.ScrollToCaret();
                }));
            }
        }
        private MessagePacket setMessagePacket(string msg)
        {
            var packet = new MessagePacket(msg);
            packet.clientID = this.playerID;
            return packet;
        }
        private void chatInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var newChat = this.chatInputBox.Text;
                Task task = Task.Run(() =>
                {
                    Network.Send(setMessagePacket(newChat), this.stream);
                });

                this.chatInputBox.ResetText();
            }
        }
    }
}
