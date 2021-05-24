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
        static void Main(string[] args)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                IPAddress serverIP = IPAddress.Parse("192.168.102.1");
                int serverPort = 7777;
                client.Connect(serverIP, serverPort);


            }
            catch (SocketException e)
            {
                Console.WriteLine("Socket Exception: {0}", e);
            }
            finally
            {

            }
        }
    }
}
