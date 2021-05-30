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
            Server server = new Server();
            server.Run();
        }
    }
}
