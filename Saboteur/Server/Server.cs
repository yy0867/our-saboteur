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
using CardLibrary;
using MapLibrary;
using DealerLibrary;

namespace Server
{
    class Server
    {
        private TcpListener listener;
        private NetworkStream[] networkStream = new NetworkStream[MAX_CLIENT_NUM];

        private IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        private int serverPort = 11000;

        private const int MAX_CLIENT_NUM = 7;
        private int numConnectedClient = 0;
        private bool[] connectedClients = { false, false, false, false, false, false, false };
        private bool[] enteredPlayers = { false, false, false, false, false, false, false };
        private bool isGameStarted = false;

        private int roomCode = 1000;
        private bool isRoomExist = false;

        private int curTurnPlayer = 0;      // 현재 Turn인 Player

        //private object lockObject = new object();
        //private Semaphore sem = new Semaphore(1, 1);


        /* -------------------- Map, Card -------------------- */
        private Map fields;
        private List<Card> deckCards;
        private List<Card> frontUsedCards = new List<Card>();
        private List<Card> backUsedCards = new List<Card>();
        private List<PlayerState> playersState = new List<PlayerState>();

        private bool isFirstGameInfo = true;


        public Server()
        {
            // networkStream 배열 초기화
            for (int i = 0; i < MAX_CLIENT_NUM; i++)
                networkStream[i] = null;
        }

        public void Run()
        {
            Connect();
        }

        public void Send(int clientID, Packet packet)
        {
            Console.WriteLine("client {0} send message", packet.clientID);
            byte[] sendBuffer = new byte[Packet.MAX_SIZE];

            Packet.Serialize(packet).CopyTo(sendBuffer, 0);
            networkStream[clientID].Write(sendBuffer, 0, sendBuffer.Length);
            networkStream[clientID].Flush();
        }

        public void SendToAllClient(Packet packet)
        {
            Task task = Task.Run(() => {
                for (int i = 0; i < numConnectedClient; i++)
                    Send(i, packet);
            });
        }
 
        public void SendToExistClient(Packet packet)
        {
            Task task = Task.Run(() => {
                for (int i = 0; i < numConnectedClient; i++)
                {
                    if(enteredPlayers[i])
                        Send(i, packet);
                }
            });
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

        private int GetNextTurnPlayer()
        {
            this.curTurnPlayer++;
            if (this.curTurnPlayer == this.numConnectedClient)
                this.curTurnPlayer = 0;
            return this.curTurnPlayer;
        }

        // ########## Receive Functions #########

        private void ProcessRoomInfo(RoomInfo receiveInfo)
        {
            Console.WriteLine("Client {0}으로부터 RoomInfo 패킷 Receive", receiveInfo.clientID);

            RoomInfo sendRoomInfo = new RoomInfo();
            sendRoomInfo.roomCode = this.roomCode;
            sendRoomInfo.clientID = receiveInfo.clientID == Packet.isEmpty 
                ? FindEmptyClientID() : receiveInfo.clientID;
            sendRoomInfo.message = receiveInfo.message;

            Console.WriteLine("Client[{0}]: {1}", sendRoomInfo.clientID, sendRoomInfo.message);

            // Client가 CreateRoom 또는 JoinRoom 요청

            enteredPlayers[receiveInfo.clientID] = true;
            enteredPlayers.CopyTo(sendRoomInfo.players, 0); // 서버에 저장된 최신 데이터를 클라이언트로 보냄

            // 1. 방장 Client가 CreateRoom 요청
            if (receiveInfo.roomCode == Packet.isEmpty)
            {
                if (isRoomExist)
                {
                    Error error = new Error(ErrorCode.RoomExistException);
                    Send(receiveInfo.clientID, error);
                    return;
                }

                SendToAllClient(sendRoomInfo);
                isRoomExist = true;
            }
            else    // 2. Client가 Join Room 요청
            {
                if (!isRoomExist)
                {
                    Error error = new Error(ErrorCode.NoRoomExistException);
                    Send(receiveInfo.clientID, error);
                    return;
                }

                SendToExistClient(sendRoomInfo);
            }
        }

        private void ProcessGameInfo(GameInfo receiveInfo)
        {
            GameInfo sendGameInfo = new GameInfo();
            Map fields = new Map();
            Dealer dealer = new Dealer(this.numConnectedClient);
            dealer.CardListInit();
            dealer.DeckCardsInit();
            bool[] roleArr = dealer.defineRole(this.connectedClients);

            // 게임 시작 직후 받은 GameInfo 패킷
            if (this.isFirstGameInfo)
            {
                fields.MapInit();
                this.fields = fields;
                Dictionary<int, List<Card>> DicCardsPerPlayer = dealer.cardDivide();

                for (int i = 0; i < this.numConnectedClient; i++)
                {
                    sendGameInfo.clientID = i;
                    sendGameInfo.holdingCards = DicCardsPerPlayer[i];
                    sendGameInfo.fields = fields;
                    sendGameInfo.deckCards = dealer.deckCards;

                    // deckCards 계산 처리

                    if (i == 0)     // 0번 플레이어부터 Turn 시작
                        sendGameInfo.isTurn = true;
                    else
                        sendGameInfo.isTurn = false;

                    if (roleArr[i])
                        sendGameInfo.isSaboteur = true;
                    else
                        sendGameInfo.isSaboteur = false;

                    Send(i, sendGameInfo);
                }

                this.isFirstGameInfo = false;
            }
            // 게임 진행
            else
            {
                Console.WriteLine("Client {0}으로부터 GameInfo 패킷 Receive", receiveInfo.clientID);

                for (int i = 0; i < this.numConnectedClient; i++)
                {
                    sendGameInfo.clientID = i;
                    Send(i, sendGameInfo);
                }



            }
        }


        // ########## Receive Functions - END #########

        public void ReceiveByClientID(int clientID)
        {
            byte[] receiveBuffer = new byte[Packet.MAX_SIZE];

            while (true)
            {
                initBuffer(receiveBuffer);

                // 정보 수신
                Console.WriteLine("Receive Thread[{0}]: {1}", clientID, networkStream[clientID]);

                networkStream[clientID].Read(receiveBuffer, 0, receiveBuffer.Length);
                networkStream[clientID].Flush();

                Packet receivePacket = (Packet)Packet.Desserialize(receiveBuffer);
                receivePacket.clientID = clientID;

                switch ((int)receivePacket.Type)
                {
                    case (int)PacketType.RoomInfo:
                        ProcessRoomInfo((RoomInfo)receivePacket);
                        break;

                    case (int)PacketType.GameInfo:
                        ProcessGameInfo((GameInfo)receivePacket);
                        break;

                    // 다른 패킷 case



                }
            }

            Console.WriteLine("Receive Thread[{0}] Off", clientID);
        }

        private void initBuffer(byte[] buffer)
        {
            for (int i = 0; i < Packet.MAX_SIZE; i++)
                buffer[i] = 0;
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
