using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
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
        public static string setServerIP
        {
            set
            {
                ServerIP = IPAddress.Parse(value);
            }
        }

        public const int Port = 11000;

        private static TcpClient client = null;
        public static bool isConnected = false;
        public static NetworkStream networkStream;

        // 클라이언트 --> 서버 연결
        // true -> Success / false -> Fail 
        // false면 폼에서 호출한 후 Error MessageBox 띄우기

        public static bool isAlreadyConnected() {
            return client != null; 
        }
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

        public static bool Connect(int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ServerIP, port);
            }
            catch
            {
                return false;
            }

            isConnected = true;
            networkStream = client.GetStream();

            return true;
        }

        // 에러 분석
        // 각각의 에러 처리해서 필요한 작업 수행
        // 필요하다면 Form Update
        public static void ParseError(Error error)
        {
            switch (error.code)
            {
                case ErrorCode.RoomExistException:
                    ViewController.MainMenu.HandleError(error.code);
                    break;
                case ErrorCode.NoRoomExistException:
                    ViewController.MainMenu.HandleError(error.code);
                    break;
            }
        }

        // 서버 정보 수신
        // Packet으로 정보를 반환, Form에서는 반환된 정보를 통해
        // Form Update를 진행
        public static void Receive()
        {
            byte[] readBuffer = new byte[Packet.MAX_SIZE];
            while (true)
            {
                try
                {
                    networkStream.Read(readBuffer, 0, Packet.MAX_SIZE);
                }
                catch
                {
                    if (!client.Connected || networkStream == null)
                        return;

                    isConnected = false;
                    networkStream.Close();

                    return;
                }

                // 패킷 타입 추출
                Packet packet = (Packet)Packet.Desserialize(readBuffer);
                ClearBuffer(readBuffer);

                switch ((int)packet.Type)
                {
                    case (int)PacketType.Error:
                        ParseError((Error)packet);
                        break;
                    case (int)PacketType.RoomInfo:  // RoomInfo 패킷 받으면
                        ViewController.Room.updateInfo(packet);
                        break;
                    case (int)PacketType.GameInfo:  // GameInfo 패킷 받으면
                        ViewController.SwitchScreen(Screen.Game);
                        ViewController.Game.updateInfo(packet);
                        break;
                    case (int)PacketType.Message:

                        break;
                        // 나중에 패킷 타입 추가되면 작성하기 ##########################
                }
            }
        }

        public static void Receive(Action<Packet> action)
        {
            Receive(action, networkStream);
        }
        public static void Receive(Action<Packet> action, NetworkStream stream)
        {
            byte[] readBuffer = new byte[Packet.MAX_SIZE];
            while (true)
            {
                try
                {
                    stream.Read(readBuffer, 0, Packet.MAX_SIZE);
                }
                catch
                {
                    if (!client.Connected || stream == null)
                        return;

                    isConnected = false;
                    stream.Close();

                    return;
                }

                // 패킷 타입 추출
                Packet packet = (Packet)Packet.Desserialize(readBuffer);
                ClearBuffer(readBuffer);

                switch (packet.Type)
                {
                    case (int)PacketType.Error:
                        ParseError((Error)packet);
                        break;
                    default:
                        action((MessagePacket)packet);
                        break;
                }
            }
        }

        // 패킷 전송
        public static void Send(Packet p)
        {
            byte[] sendBuffer = new byte[Packet.MAX_SIZE];
            ClearBuffer(sendBuffer);

            Packet.Serialize(p).CopyTo(sendBuffer, 0);

            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
            networkStream.Flush();
        }

        // 타입별 버퍼 초기화 
        // Usage: ClearBuffer(BufferType.Send / Read)
        private static void ClearBuffer(byte[] buffer)
        {
            for (int i = 0; i < Packet.MAX_SIZE; i++)
            {
                buffer[i] = 0;
            }
        }
    }
}
