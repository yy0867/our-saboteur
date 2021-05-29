﻿using System;
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
        //private static List<Socket> sockets;

        private static IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private static int serverPort = 7777;
        private static NetworkStream[] networkStream = new NetworkStream[MAX_CLIENT_NUM];

        private static byte[][] sendBuffers = new byte[MAX_CLIENT_NUM][];
        private static byte[][] receiveBuffers = new byte[MAX_CLIENT_NUM][];

        private const int MAX_CLIENT_NUM = 7;       // 클라이언트 최대 수(플레이어 최대 수)
        private static int numClient = 0;           // 연결된 클라이언트 수
        private static bool[] connectedClients =
            { false, false, false, false, false, false, false };
        private static bool isAllClientOn = false;  // 모든 클라이언트가 입장 했는지
        // 방장 Client가 게임 시작 누르면 true로 바뀜

        private static bool[] isReceiveThreadOn = 
            { false, false, false, false, false, false, false };
        private static Thread[] receiveThread = new Thread[MAX_CLIENT_NUM];
        // 클라이언트 수 마다 thread

        private static int roomCode = 1000;
        public static RoomInfo PacketRoomInfo;

        private static void TestPrint(int n)
        {
            Console.WriteLine(n);
        } 

        private static void SendByClientID(int clientID)
        {
            networkStream[clientID].Write(sendBuffers[clientID], 0, sendBuffers[clientID].Length);
            networkStream[clientID].Flush();

            for (int i = 0; i < Packet.MAX_SIZE; i++)
                sendBuffers[clientID][i] = 0;
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
                receiveBuffers[clientID][i] = 0;

            networkStream[clientID].Read(receiveBuffers[clientID], 0, receiveBuffers[clientID].Length);
            networkStream[clientID].Flush();

            Packet packet = (Packet)Packet.Desserialize(receiveBuffers[clientID]);
            switch ((int)packet.Type)
            {
                case (int)PacketType.RoomInfo:
                    {
                        Console.WriteLine("Client로부터 RoomInfo 패킷 Receive");

                        PacketRoomInfo = (RoomInfo)Packet.Desserialize(receiveBuffers[clientID]);

                        // Client에게 Send할 패킷 구성
                        RoomInfo roomPacket = new RoomInfo();
                        roomPacket.Type = (int)PacketType.RoomInfo;
                        roomPacket.roomCode = roomCode;

                         // Client가 CreateRoom 또는 JoinRoom 요청
                        if (PacketRoomInfo.clientID == Packet.isEmpty)
                        {
                            roomPacket.clientID = numClient;

                            // 1. 방장 Client가 CreateRoom 요청
                            if (PacketRoomInfo.roomCode == Packet.isEmpty)
                            {
                                //roomPacket.players[0] = true;       // 방장 Client
                                //roomPacket.players[roomPacket.clientID] = true;       // 방장 Client

                                connectedClients[roomPacket.clientID] = true;
                                roomPacket.players = connectedClients;
                                //Array.Copy(connectedClient, )

                                //Packet.Serialize(roomPacket).CopyTo(sendBuffer, 0);
                                Packet.Serialize(roomPacket).CopyTo(sendBuffers[clientID], 0);
                                SendPacket();
                                //SendByClientID(PacketRoomInfo.clientID);
                            }
                            // 2. Client가 Join Room 요청
                            else
                            {
                                connectedClients[roomPacket.clientID] = true;
                                roomPacket.players = connectedClients;
                                //Array.Copy(connectedClient, )

                                //Packet.Serialize(roomPacket).CopyTo(sendBuffer, 0);a
                                Packet.Serialize(roomPacket).CopyTo(sendBuffers[clientID], 0);
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

            isReceiveThreadOn[clientID] = false;
        }

        private static void ReceiveToAllClient()
        {
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
            {
                if (!isReceiveThreadOn[i])
                {
                    receiveThread[i] = new Thread(() => ReceiveByClientID(i));
                    isReceiveThreadOn[i] = true;
                    receiveThread[i].Start();

                    //receiveThread[i] = new Thread(new ParameterizedThreadStart(ReceiveByClientID));
                    //receiveThread[i].Start(i);

                    //receiveThread[i] = new Thread(() => TestPrint(i));
                    //receiveThread[i] = new Thread(new ParameterizedThreadStart(TestPrint));
                    //receiveThread[i].Start(i);
                }
            }

            // 1. Client로부터 Packet Receive
            // 2. Packet 정보 처리 및 Send할 정보 Packet에 담기
            // 3. Client에 Packet Send
        }

        static void Main(string[] args)
        {
            // networkStream 배열 Init
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
            {

                networkStream[i] = null;
            }

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
                            connectedClients[numClient] = true;

                            for (int i = 0; i < MAX_CLIENT_NUM; i++)
                            {
                                Console.WriteLine("Client{0} 연결 여부: {1} ", i, connectedClients[i]);
                                Console.WriteLine("NetowrkStream[{0}]: {1}", i, networkStream[i]);
                                Console.WriteLine();
                            }

                            // Client가 1명이라도 연결되어 있으면, Packet Receive 시작
                            if (numClient != 0)     // 방장 Client, CreateRoom
                            {
                                ReceiveToAllClient();



                            }
                            else    // JoinRoom
                            {
                            }

                            numClient++;
                        }


                    }
                    else        // 게임 시작, 클라이언트 요청 패킷 분류하여 통신
                    {
                        try
                        {
                            // Client로부터 Packet Receive
                            ReceiveToAllClient();
                        }
                        catch
                        {
                            for (int i = 0; i < MAX_CLIENT_NUM; i++)
                                networkStream[i] = null;
                            break;
                        }

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
                    }

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
