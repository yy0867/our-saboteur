using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace ClientTest
{
    class ClientTest
    {
        private static int clientID;

        static void Main(string[] args)
        {
            TcpClient client = null;
            try
            {
                //IPAddress serverIP = IPAddress.Parse("192.168.0.6");
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");
                int serverPort = 7777;
                client = new TcpClient();
                client.Connect(serverIP, serverPort);


            }
            catch (SocketException e)
            {
                Console.WriteLine("Socket Exception: {0}", e);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
