using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketLibrary;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Saboteur
{
    class MessageServer
    {
        private int serverPort = 15000;
        private const int MAX_CLIENT_NUM = 7;
        private List<NetworkStream> streams = new List<NetworkStream>();
        private List<int> connectedID = new List<int>();

        private void initializeStream()
        {
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
                streams.Add(new NetworkStream(null));
        }
        private void send(Packet packet, NetworkStream stream)
        {
            lock (this)
            {
                byte[] sendBuffer = new byte[Packet.MAX_SIZE];

                Packet.Serialize(packet).CopyTo(sendBuffer, 0);
                stream.Write(sendBuffer, 0, sendBuffer.Length);
                stream.Flush();
            }
        }
        private void sendEcho(Packet packet)
        {
            MessagePacket msg = packet as MessagePacket;
            if(msg != null)
                foreach(var i in connectedID)
                    send(msg, this.streams[i]);
        }
        public void Run(IPAddress ip, int port)
        {
            TcpListener msgListener = null;
            TcpClient client = null;
            try
            {
                msgListener = new TcpListener(ip, port);
                msgListener.Start();

                while (true)       
                {
                    client = msgListener.AcceptTcpClient();
                    if (client.Connected)
                    {
                        this.streams[connectedID.Count] = client.GetStream();


                        Task.Run(() =>
                        {
                            Network.Receive(sendEcho, this.streams[connectedID.Count]);
                        });
                        

                        Thread.Sleep(100);
                        connectedID.Add(connectedID.Count);
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Socket Exception: {0}", e);
            } finally
            {
                if(msgListener != null)
                    msgListener.Stop();
            }
        }
    }
}
