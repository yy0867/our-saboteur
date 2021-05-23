using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        private string serverIP;
        private int serverPort;
        private NetworkStream networkStream;
        private TcpListener[] listener;

        private byte[] sendBuffer = new byte[1024 * 4];
        private byte[] receiveBuffer = new byte[1024 * 4];

        const int MAX_CLIENT_NUM = 7;       // 클라이언트 최대 수(플레이어 최대 수)
        private int numClient = 0;          // 연결된 클라이언트 수
        private bool clientOn = false;

        private Thread[] thread;            // 클라이언트 수 마다 thread 존재

        // public 패킷 클래스



        static void Main(string[] args)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in ipEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    Console.WriteLine("IP Address = " + ip.ToString());
            }



            // 모든 클라이언트가 끌 때까지 대기
            //while (true)
            //{

            //}
        }
    }
}
