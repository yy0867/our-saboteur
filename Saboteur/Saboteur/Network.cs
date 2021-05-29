using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using PacketLibrary;

namespace Saboteur
{
    enum BufferType
    {
        Read = 0,
        Send
    }

    class Network
    {
        public static IPAddress ServerIP;
        public const int Port = 7777;

        private static TcpClient client;
        public static bool isConnected = false;
        public static NetworkStream networkStream;

        public static byte[] sendBuffer = new byte[Packet.MAX_SIZE];
        public static byte[] readBuffer = new byte[Packet.MAX_SIZE];

        // 클라이언트 --> 서버 연결
        // true -> Success / false -> Fail 
        // false면 폼에서 호출한 후 Error MessageBox 띄우기
        public static bool Connect()
        {
            try
            {
                client = new TcpClient();
                client.Connect(ServerIP, Port);
            }
            catch
            {
                return false;
            }

            isConnected = true;
            networkStream = client.GetStream();

            return true;
        }

        // 서버 정보 수신
        // Packet으로 정보를 반환, Form에서는 반환된 정보를 통해
        // Form Update를 진행
        public static Packet Receive()
        {
            try
            {
                networkStream.Read(readBuffer, 0, Packet.MAX_SIZE);
            }
            catch
            {
                if (!client.Connected || networkStream == null)
                    return null;

                isConnected = false;
                networkStream.Close();

                return null;
            }

            // 패킷 타입 추출
            Packet packet = (Packet)Packet.Desserialize(readBuffer);

            switch ((int)packet.Type)
            {
                case (int)PacketType.RoomInfo:  // RoomInfo 패킷 받으면
                    RoomInfo info = ParseRoomInfo(packet);
                    ClearBuffer(BufferType.Read);
                    return info;

                    // 나중에 패킷 타입 추가되면 작성하기 ##########################
            }

            return null;
        }

        // 패킷 전송
        public static void Send(Packet p)
        {
            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
            networkStream.Flush();

            ClearBuffer(BufferType.Send);
        }

        // 타입별 버퍼 초기화 
        // Usage: ClearBuffer(BufferType.Send / Read)
        private static void ClearBuffer(BufferType type)
        {
            for (int i = 0; i < Packet.MAX_SIZE; i++)
            {
                if (type == BufferType.Read)
                    readBuffer[i] = 0;
                else
                    sendBuffer[i] = 0;
            }
        }

        /// ############################################
        /// ##           Parse Information            ##
        /// ############################################
        // Room info Parsing [ RoomInfo Packet --> RoomForm Information ]
        public static RoomInfo ParseRoomInfo(Packet packet)
        {
            RoomInfo received = (RoomInfo)packet;
            RoomInfo info = new RoomInfo();

            received.players.CopyTo(info.players, 0); // info.players에 복사
            received.message.CopyTo(info.message, 0); // info.message에 복사
            info.roomCode = received.roomCode; // roomCode 복사

            return received;
        }
    }
}
