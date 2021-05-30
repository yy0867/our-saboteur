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
        private TcpListener listener;
        private NetworkStream[] networkStream = new NetworkStream[MAX_CLIENT_NUM];

        private IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private int serverPort = 7777;
        private const int MAX_CLIENT_NUM = 7;

        private byte[][] sendBuffers = new byte[MAX_CLIENT_NUM][];
        private byte[][] receiveBuffers = new byte[MAX_CLIENT_NUM][];

        private int numConnectedClient = 0;
        private bool[] connectedClients = { false, false, false, false, false, false, false };
        private bool[] enteredPlayers = { false, false, false, false, false, false, false };
        private bool isGameStarted = false;

        private bool[] isReceiveThreadOn = { false, false, false, false, false, false, false };
        private Thread[] receiveThread = new Thread[MAX_CLIENT_NUM];
        // 클라이언트 수 마다 receiveThread 존재

        private int roomCode = 1000;
        public RoomInfo PacketRoomInfo;

        //private Semaphore sem = new Semaphore(1, 1);



        public Server()
        {
            // networkStream 배열, sendBuffers, receiveBuffers 초기화 및 할당
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
            {
                networkStream[i] = null;
                sendBuffers[i] = new byte[Packet.MAX_SIZE];
                receiveBuffers[i] = new byte[Packet.MAX_SIZE];
            }
        }

        public void Run()
        {
            try
            {
                this.listener = new TcpListener(this.serverIP, this.serverPort);
                this.listener.Start();

                TcpClient client = null;

                while (true)        // listening loop
                {
                    if (!isGameStarted)     // [게임 시작 전]: 클라이언트 연결 대기
                    {
                        Console.WriteLine("[게임 시작 전] Client Connect 대기...\n");

                        client = this.listener.AcceptTcpClient();
                        if (client.Connected)
                        {
                            Console.WriteLine("[Client{0} Connected]", numConnectedClient);
                            networkStream[numConnectedClient] = client.GetStream();
                            connectedClients[numConnectedClient] = true;
                            isReceiveThreadOn[numConnectedClient] = true;

                            Console.WriteLine("Number of Connected Clients: {0}", numConnectedClient);

                            numConnectedClient++;

                            Console.WriteLine("------------------------------");
                            for (int i = 0; i < MAX_CLIENT_NUM; i++)
                            {
                                Console.WriteLine("Client{0} 연결 여부: {1} ", i, connectedClients[i]);
                                Console.WriteLine("NetowrkStream[{0}]: {1}", i, networkStream[i]);
                                Console.WriteLine();
                            }
                            Console.WriteLine("------------------------------\n");

                            // Client가 1명이라도 연결되어 있으면, Packet Receive 시작
                            if (numConnectedClient > 0)
                            {
                                try
                                {
                                    ReceiveFromAllClient();
                                }
                                catch
                                {
                                    for (int i = 0; i < MAX_CLIENT_NUM; i++)
                                        networkStream[i] = null;
                                    break;
                                }
                            }
                            else    // Client와 연결되어 있지 않은 경우
                            {

                            }
                        }
                    }
                    else        // [게임 시작 후]: 클라이언트 요청 패킷 분류하여 통신
                    {
                        try
                        {
                            ReceiveFromAllClient();
                        }
                        catch
                        {
                            for (int i = 0; i < MAX_CLIENT_NUM; i++)
                                networkStream[i] = null;
                            break;
                        }
                    }
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
        }

        public void SendByClientID(int clientID)
        {
            networkStream[clientID].Write(sendBuffers[clientID], 0, sendBuffers[clientID].Length);
            networkStream[clientID].Flush();

            for (int i = 0; i < Packet.MAX_SIZE; i++)
                sendBuffers[clientID][i] = 0;
        }

        public void SendToAllClient()
        {
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
            {
                if (networkStream[i] != null)
                    SendByClientID(i);
            }
        }

        public void ReceiveByClientID(int clientID)
        {
            Console.WriteLine("ReceiveThread({0}) On", clientID);

            while (true)
            {
                for (int i = 0; i < Packet.MAX_SIZE; i++)
                    receiveBuffers[clientID][i] = 0;

                networkStream[clientID].Read(receiveBuffers[clientID], 0, receiveBuffers[clientID].Length);
                networkStream[clientID].Flush();

                Packet receivePacket = (Packet)Packet.Desserialize(receiveBuffers[clientID]);
                switch ((int)receivePacket.Type)
                {
                    case (int)PacketType.RoomInfo:
                        {
                            Console.WriteLine("Client로부터 RoomInfo 패킷 Receive");

                            PacketRoomInfo = (RoomInfo)Packet.Desserialize(receiveBuffers[clientID]);

                            // Client에게 Send할 패킷 구성
                            RoomInfo sendRoomInfo = new RoomInfo();
                            sendRoomInfo.Type = (int)PacketType.RoomInfo;
                            sendRoomInfo.roomCode = this.roomCode;

                            // Client가 CreateRoom 또는 JoinRoom 요청
                            if (PacketRoomInfo.clientID == Packet.isEmpty)
                            {
                                sendRoomInfo.clientID = numConnectedClient;
                                //connectedClients[sendRoomInfo.clientID] = true;
                                //connectedClients.CopyTo(sendRoomInfo.players, 0);
                                enteredPlayers[clientID] = true;
                                enteredPlayers.CopyTo(sendRoomInfo.players, 0);

                                for (int i = 0; i < sendRoomInfo.players.Length; i++)
                                    Console.WriteLine("player{0}: {1}", i, sendRoomInfo.players[i]);

                                // 1. 방장 Client가 CreateRoom 요청
                                if (PacketRoomInfo.roomCode == Packet.isEmpty)
                                {

                                    Packet.Serialize(sendRoomInfo).CopyTo(sendBuffers[clientID], 0);
                                    SendToAllClient();
                                }
                                else    // 2. Client가 Join Room 요청
                                {
                                    Packet.Serialize(sendRoomInfo).CopyTo(sendBuffers[clientID], 0);
                                    SendToAllClient();
                                }

                                //numConnectedClient++;
                            }

                            break;
                        }

                        // 다른 패킷 case
                }

                isReceiveThreadOn[clientID] = false;
            }

            Console.WriteLine("ReceiveThread({0}) Off", clientID);
        }

        public void ReceiveFromAllClient()
        {
            //for (int i = 0; i < MAX_CLIENT_NUM; i++)
            for (int i = 0; i < numConnectedClient; i++)
            {
                if (isReceiveThreadOn[i])
                {
                    receiveThread[i] = new Thread(() => ReceiveByClientID(i));
                    receiveThread[i].Start();
                    Thread.Sleep(100);
                }
            }
        }
    }
}
