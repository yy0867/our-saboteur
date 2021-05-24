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
    class Server
    {
        static TcpListener listener;
        static List<Socket> sockets;

        //static string serverIP = "192.";
        static IPAddress serverIP = IPAddress.Parse("192.168.102.1");
        static int serverPort = 7777;
        static NetworkStream networkStream;

        static byte[] sendBuffer = new byte[1024 * 4];
        static byte[] receiveBuffer = new byte[1024 * 4];

        const int MAX_CLIENT_NUM = 7;       // 클라이언트 최대 수(플레이어 최대 수)
        static int numClient = 0;          // 연결된 클라이언트 수
        static bool clientOn = false;

        static Thread[] thread;            // 클라이언트 수 마다 thread 존재


        /*
         * public 패킷 클래스들
        */


        static void Main(string[] args)
        {
            //string serverIP = "192.";
            //int serverPort = 7777;
            try
            {
                listener = new TcpListener(serverIP, serverPort);
                listener.Start();
                
                while (true)        // listening loop
                {
                    Console.WriteLine("클라이언트 connect 대기...\n");

                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("클라이언트 connected\n");

                    client.Close();
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





            //IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            //foreach (var ip in ipEntry.AddressList)
            //{
            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //        Console.WriteLine("IP Address = " + ip.ToString());
            //}


            // 모든 클라이언트가 끌 때까지 대기
            //while (true)
            //{

            //}

            Console.WriteLine("서버 종료\n");
        }
    }
}
