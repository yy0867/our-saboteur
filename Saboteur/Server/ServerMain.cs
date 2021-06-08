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
    class ServerMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IP를 입력해주세요 ...");
            string serverIP = Console.ReadLine();
            
            Server server = new Server(serverIP);
            server.Run();
        }
    }
}
