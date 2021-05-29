using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using PacketLibrary;

namespace ClientTest
{
    class ClientTest
    {
        private static NetworkStream networkStream;
        private static byte[] sendBuffer = new byte[1024 * 4];
        private static byte[] receiveBuffer = new byte[1024 * 4];

        public static RoomInfo PacketRoomInfo;

        private static void Send()
        {
            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
            networkStream.Flush();

            for (int i = 0; i < Packet.MAX_SIZE; i++)
                sendBuffer[i] = 0;
        }

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

                if (client.Connected)
                {
                    Console.WriteLine("서버와 연결");
                    networkStream = client.GetStream();

                    RoomInfo roomPacket = new RoomInfo();
                    roomPacket.Type = (int)PacketType.RoomInfo;

                    Packet.Serialize(roomPacket).CopyTo(sendBuffer, 0);
                    
                }
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
