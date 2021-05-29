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

        //private static IPAddress serverIP = IPAddress.Parse("192.168.0.6");
        private static IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private static int serverPort = 7777;
        private static NetworkStream[] networkStream = new NetworkStream[MAX_CLIENT_NUM];
        //static NetworkStream networkStream;

        private static byte[] sendBuffer = new byte[1024 * 4];
        private static byte[] receiveBuffer = new byte[1024 * 4];

        private const int MAX_CLIENT_NUM = 7;       // 클라이언트 최대 수(플레이어 최대 수)
        private static int numClient = 0;           // 연결된 클라이언트 수
        private static bool[] connectedClient =
            { false, false, false, false, false, false, false };
        private static bool isAllClientOn = false;  // 모든 클라이언트가 입장 했는지
        // 방장 Client가 게임 시작 누르면 true로 바뀜

        private static bool[] isReceiveThreadOn = 
            { true, true, true, true, true, true, true };
        private static Thread[] receiveThread;             // 클라이언트 수 마다 thread

        private static int roomCode = 1000;
        private static RoomInfo PacketRoomInfo;
        //private static CreateRoom PacketCreateRoom;
        //private static JoinRoom PacketJoinRoom;

        private static void SendByClientID(int clientID)
        {
            networkStream[clientID].Write(sendBuffer, 0, sendBuffer.Length);
            networkStream[clientID].Flush();

            for (int i = 0; i < Packet.MAX_SIZE; i++)
                sendBuffer[i] = 0;
        }

        private static void SendPacket()
        {
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
            {
                if (networkStream[i] != null)
                    SendByClientID(i);
            }
        }

        private static void ReceiveByClientID(int clientID)
        {
            for (int i = 0; i < Packet.MAX_SIZE; i++)
                receiveBuffer[i] = 0;

            networkStream[clientID].Read(receiveBuffer, 0, receiveBuffer.Length);
            networkStream[clientID].Flush();

            Packet packet = (Packet)Packet.Desserialize(receiveBuffer);
            switch ((int)packet.Type)
            {
                case (int)PacketType.RoomInfo:
                    {
                        PacketRoomInfo = (RoomInfo)Packet.Desserialize(receiveBuffer);

                        // Client에게 Send할 패킷 구성
                        RoomInfo roomPacket = new RoomInfo();
                        roomPacket.Type = (int)PacketType.RoomInfo;
                        roomPacket.roomCode = roomCode;
                        


                         // Client가 CreateRoom 또는 JoinRoom 요청
                        if (PacketRoomInfo.clientID == Packet.isEmpty)
                        {
                            roomPacket.clientID = numClient;

                            // 방장 Client가 CreateRoom 요청
                            if (PacketRoomInfo.roomCode == Packet.isEmpty)
                            {
                                //roomPacket.players[0] = true;       // 방장 Client
                                //roomPacket.players[roomPacket.clientID] = true;       // 방장 Client

                                connectedClient[roomPacket.clientID] = true;
                                roomPacket.players = connectedClient;
                                //Array.Copy(connectedClient, )

                                Packet.Serialize(roomPacket).CopyTo(sendBuffer, 0);
                                SendPacket();
                                //SendByClientID(PacketRoomInfo.clientID);
                            }
                            // Client가 Join Room 요청
                            else
                            {
                                connectedClient[roomPacket.clientID] = true;
                                roomPacket.players = connectedClient;
                                //Array.Copy(connectedClient, )

                                Packet.Serialize(roomPacket).CopyTo(sendBuffer, 0);
                                SendPacket();
                                //SendByClientID(PacketRoomInfo.clientID);
                            }

                            numClient++;
                        }
                        else
                        {

                        }

                        break;
                    }
            }
        }



        private static void ReceivePacket()
        {
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
            {
                if (!isReceiveThreadOn[i])
                    receiveThread[i] = new Thread(() => ReceiveByClientID(i));
            }

            // 1. Client로부터 Packet Receive
            // 2. Packet 정보 처리 및 Send할 정보 Packet에 담기
            // 3. Client에 Packet Send 
        }

        static void Main(string[] args)
        {
            // networkStream 배열 Init
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
                networkStream[i] = null;

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

                            //if (numClient == 0)     // 방장 Client, CreateRoom
                            //{
                            //    Thread t_Receive = new Thread(new ThreadStart(ReceivePacket));



                            //    //// CreateRoom 빈 패킷 Send
                            //    //CreateRoom CreateRoomData = new CreateRoom();
                            //    //CreateRoomData.Type = (int)PacketType.CreateRoom;
                            //    //CreateRoomData.roomCode = 0;

                            //    //Packet.Serialize(CreateRoomData).CopyTo(sendBuffer, 0);
                            //    //Send(0);

                            //    //Console.WriteLine("CreateRoom 빈 패킷 Send: {0}\n",
                            //    //    CreateRoomData.roomCode);
                            //}
                            //else    // JoinRoom
                            //{
                            //    //JoinRoom JoinRoomData = new JoinRoom();
                            //    //JoinRoomData.Type = (int)PacketType.JoinRoom;
                            //    //JoinRoomData.roomCode = 1000;
                            //    //JoinRoomData.clientID = numClient;

                            //    //Packet.Serialize(JoinRoomData).CopyTo(sendBuffer, 0);
                            //    //Send(JoinRoomData.clientID);

                            //    //Console.WriteLine("JoinRoom 패킷 Send: {0}\n",
                            //    //    JoinRoomData.roomCode);
                            //}

                            //numClient++;
                        }


                    }
                    //else        // 게임 시작, 클라이언트 요청 패킷 분류하여 통신
                    //{
                    //    try
                    //    {
                    //        // Client로부터 Packet Receive
                    //        networkStream[0].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                    //        networkStream[1].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                    //        networkStream[2].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                    //        networkStream[3].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                    //        networkStream[4].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                    //        networkStream[5].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                    //        networkStream[6].Read(receiveBuffer, 0, Packet.MAX_SIZE);
                    //    }
                    //    catch
                    //    {
                    //        for (int i = 0; i < MAX_CLIENT_NUM; i++)
                    //            networkStream[i] = null;
                    //        break;
                    //    }

                    //    Packet packet = (Packet)Packet.Desserialize(receiveBuffer);
                    //    switch ((int)packet.Type)
                    //    {
                    //        case (int)PacketType.CreateRoom:
                    //            {
                    //                // 방장 Client로부터 받은 CreateRoom 패킷
                    //                PacketCreateRoom = (CreateRoom)Packet.Desserialize(receiveBuffer);

                    //                // 방장 Client에게 CreateRoom 패킷 Send
                    //                CreateRoom CreateRoomData = new CreateRoom();
                    //                CreateRoomData.Type = (int)PacketType.CreateRoom;
                    //                CreateRoomData.roomCode = roomCode;
                    //                CreateRoomData.clientID = PacketCreateRoom.clientID;

                    //                Packet.Serialize(CreateRoomData).CopyTo(sendBuffer, 0);
                    //                Send(CreateRoomData.clientID);

                    //                Console.WriteLine("CreateRoom 패킷 Send: {0}\n",
                    //                    CreateRoomData.roomCode);
                    //                break;
                    //            }
                    //        case (int)PacketType.JoinRoom:
                    //            {
                    //                break;
                    //            }
                    //    }
                    //}



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
