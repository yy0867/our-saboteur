using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PacketLibrary
{
    public enum PacketType
    {
        RoomInfo = 0
    }

    [Serializable]
    public class Packet
    {
        public int Length;
        public int Type;
        public const int MAX_SIZE = 1024 * 4;
        public const int isEmpty = -1;

        public Packet()
        {
            this.Length = 0;
            this.Type = 0;
        }

        public static byte[] Serialize(Object o)
        {
            MemoryStream ms = new MemoryStream(MAX_SIZE);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }

        public static Object Desserialize(byte[] bt)
        {
            MemoryStream ms = new MemoryStream(MAX_SIZE);
            foreach (byte b in bt)
                ms.WriteByte(b);

            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }

    [Serializable]
    public class RoomInfo : Packet
    {
        public int roomCode;
        public int clientID;
        // roomCode 비어있으면 Create Room, 있으면 Join Room
        public bool[] players;       // 접속한 client 
        public string message;
    }

    //[Serializable]
    //public class CreateRoom : Packet
    //{
    //    public int roomCode;
    //    public int clientID;        // 방장 Client ID
    //}

    //[Serializable]
    //public class JoinRoom : Packet
    //{
    //    public int roomCode;
    //    public int clientID;
    //}
}
