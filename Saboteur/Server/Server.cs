using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using PacketLibrary;

namespace Server
{
    class Server
    {
        private static TcpListener listener;
        private static List<Socket> sockets;



        private static IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private static int serverPort = 7777;
        private static NetworkStream[] networkStream = new NetworkStream[MAX_CLIENT_NUM];
        //static NetworkStream networkStream;

        private static byte[] sendBuffer = new byte[1024 * 4];
        private static byte[] receiveBuffer = new byte[1024 * 4];

        private const int MAX_CLIENT_NUM = 7;       // 클라이언트 최대 수(플레이어 최대 수)
        private static int numClient = 0;           // 연결된 클라이언트 수
        private static bool isAllClientOn = false;  // 모든 클라이언트가 입장 했는지
        // 방장 Client가 게임 시작 누르면 true로 바뀜

        private static Thread[] thread;             // 클라이언트 수 마다 thread

        private static int roomCode;
        private static CreateRoom PacketCreateRoom;
        private static JoinRoom PacketJoinRoom;

        private static void Send(int clientID)
        {
            networkStream[clientID].Write(sendBuffer, 0, sendBuffer.Length);
            networkStream[clientID].Flush();

            for (int i = 0; i < Packet.MAX_SIZE; i++)
                sendBuffer[i] = 0;
        }

        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(serverIP, serverPort);
                listener.Start();

                TcpClient client = null;
                
                while (true)        // listening loop
                {
                    if (!isAllClientOn)     // 게임 시작 전, 클라이언트 연결 대기
                    {
                        Console.WriteLine("클라이언트 connect 대기...\n");

                        client = listener.AcceptTcpClient();
                        if (client.Connected)
                        {
                            Console.WriteLine("클라이언트 {0} 연결", numClient);
                            networkStream[numClient] = client.GetStream();

                            if (numClient == 0)     // 방장 Client, CreateRoom
                            {
                                // CreateRoom 빈 패킷 Send
                                CreateRoom CreateRoomData = new CreateRoom();
                                CreateRoomData.Type = (int)PacketType.CreateRoom;
                                CreateRoomData.roomCode = 0;

                                Packet.Serialize(CreateRoomData).CopyTo(sendBuffer, 0);
                                Send(0);

                                Console.WriteLine("CreateRoom 빈 패킷 Send: {0}\n",
                                    CreateRoomData.roomCode);
                            }
                            else    // JoinRoom
                            {
                                JoinRoom JoinRoomData = new JoinRoom();
                                JoinRoomData.Type = (int)PacketType.JoinRoom;
                                JoinRoomData.roomCode = 1000;
                                JoinRoomData.clientID = numClient;

                                Packet.Serialize(JoinRoomData).CopyTo(sendBuffer, 0);
                                Send(JoinRoomData.clientID);

                                Console.WriteLine("JoinRoom 패킷 Send: {0}\n",
                                    JoinRoomData.roomCode);
                            }

                            numClient++;
                        }


                    }
                    else        // 게임 시작, 클라이언트 요청 패킷 분류하여 통신
                    {
                        try
                        {
                            // Client로부터 Packet Receive
                            networkStream[0].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                            networkStream[1].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                            networkStream[2].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                            networkStream[3].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                            networkStream[4].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                            networkStream[5].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                            networkStream[6].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                        }
                        catch
                        {
                            for (int i = 0; i < MAX_CLIENT_NUM; i++)
                                networkStream[i] = null;
                            break;
                        }

                        Packet packet = (Packet)Packet.Desserialize(receiveBuffer);
                        switch ((int)packet.Type)
                        {
                            case (int)PacketType.CreateRoom:
                                {
                                    // 방장 Client로부터 받은 CreateRoom 패킷
                                    PacketCreateRoom = (CreateRoom)Packet.Desserialize(receiveBuffer);

                                    // 방장 Client에게 CreateRoom 패킷 Send
                                    CreateRoom CreateRoomData = new CreateRoom();
                                    CreateRoomData.Type = (int)PacketType.CreateRoom;
                                    CreateRoomData.roomCode = roomCode;
                                    CreateRoomData.clientID = PacketCreateRoom.clientID;

                                    Packet.Serialize(CreateRoomData).CopyTo(sendBuffer, 0);
                                    Send(CreateRoomData.clientID);

                                    Console.WriteLine("CreateRoom 패킷 Send: {0}\n",
                                        CreateRoomData.roomCode);
                                    break;
                                }
                            case (int)PacketType.JoinRoom:
                                {
                                    break;
                                }
                        }
                    }



                    //thread[numClient] = new Thread();


                    //client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Socket Exception: {0}", e);
            }
            finally
            {
                listener.Stop();
            }





            Console.WriteLine("서버 종료\n");
        }
    }
}
