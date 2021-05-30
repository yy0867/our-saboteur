using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.IO;
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

        private int numConnectedClient = 0;
        private bool[] connectedClients = { false, false, false, false, false, false, false };
        private bool[] enteredPlayers = { false, false, false, false, false, false, false };
        private bool isGameStarted = false;

        private int roomCode = 1000;
        private bool isRoomExist = false;

        //private Semaphore sem = new Semaphore(1, 1);



        public Server()
        {
            // networkStream 배열, sendBuffers, receiveBuffer 초기화 및 할당
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
            {
                networkStream[i] = null;
            }
        }

        public void Run()
        {
            Connect();
        }

        public void Send(int clientID, Packet packet)
        {
            byte[] sendBuffer = new byte[Packet.MAX_SIZE];

            Packet.Serialize(packet).CopyTo(sendBuffer, 0);
            networkStream[clientID].Write(sendBuffer, 0, sendBuffer.Length);
            networkStream[clientID].Flush();
        }

        public void SendToAllClient(Packet packet)
        {
            for (int i = 0; i < numConnectedClient; i++)
            {
                Send(i, packet);
            }
        }

        private int FindEmptyClientID()
        {
            for (int i = 0; i < enteredPlayers.Length; i++)
            {
                if (!enteredPlayers[i])
                    return i;
            }
            return -1;
        }

        // ########## Receive Functions #########

        private void ProcessRoomInfo(RoomInfo receiveInfo)
        {
            if (isRoomExist)
            {
                Error error = new Error(ErrorCode.RoomExistException);
                Send(receiveInfo.clientID, error);
                return;
            }

            Console.WriteLine("Client {0}으로부터 RoomInfo 패킷 Receive", receiveInfo.clientID);

            // Client에게 Send할 패킷 구성
            RoomInfo sendRoomInfo = new RoomInfo();
            sendRoomInfo.Type = (int)PacketType.RoomInfo;
            sendRoomInfo.roomCode = this.roomCode;
            sendRoomInfo.clientID = receiveInfo.clientID;

            // Client가 CreateRoom 또는 JoinRoom 요청
            sendRoomInfo.clientID = FindEmptyClientID();

            if (sendRoomInfo.clientID == -1)
            {
                // error
            }

            enteredPlayers[sendRoomInfo.clientID] = true;
            enteredPlayers.CopyTo(sendRoomInfo.players, 0); // 서버에 저장된 최신 데이터를 클라이언트로 보냄

            for (int i = 0; i < sendRoomInfo.players.Length; i++)
                Console.WriteLine("player{0}: {1}, NetworkStream[{0}]: {2}", i, sendRoomInfo.players[i], networkStream[i]);

            // 1. 방장 Client가 CreateRoom 요청
            if (receiveInfo.roomCode == Packet.isEmpty)
            {
                SendToAllClient(sendRoomInfo);
                isRoomExist = true;
            }
            else    // 2. Client가 Join Room 요청
            {

            }
        }

        // ########## Receive Functions - END #########

        public void ReceiveByClientID(int clientID)
        {
            byte[] receiveBuffer = new byte[Packet.MAX_SIZE];
            Console.WriteLine("ReceiveThread({0}) On", clientID);

            while (true)
            {
                initBuffer(receiveBuffer);

                // 정보 수신
                Console.WriteLine("{0}.Receive Thread", clientID);
                networkStream[clientID].Read(receiveBuffer, 0, receiveBuffer.Length);
                networkStream[clientID].Flush();

                Packet receivePacket = (Packet)Packet.Desserialize(receiveBuffer);
                receivePacket.clientID = clientID;

                switch ((int)receivePacket.Type)
                {
                    case (int)PacketType.RoomInfo:
                        ProcessRoomInfo((RoomInfo)receivePacket);
                        break;

                        // 다른 패킷 case
                }
            }

            Console.WriteLine("ReceiveThread({0}) Off", clientID);
        }

        private void initBuffer(byte[] buffer)
        {
            for (int i = 0; i < Packet.MAX_SIZE; i++)
            {
                buffer[i] = 0;
            }
        }

        public void Connect()
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

                            Thread receiveThread = new Thread(() => ReceiveByClientID(numConnectedClient));
                            receiveThread.Start();

                            Thread.Sleep(100);

                            numConnectedClient++;
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
    }
}
