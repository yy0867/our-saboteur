using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CardLibrary;
using MapLibrary;

namespace PacketLibrary
{
    public enum PacketType
    {
        RoomInfo = 0,
        Error,
        GameInfo,
        Message
    }

    [Serializable]
    public class Packet
    {
        public int Length;
        public int Type;
        public int clientID;
        public const int MAX_SIZE = 1024 * 20;
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
        // roomCode 비어있으면 Create Room, 있으면 Join Room
        public bool[] players;       // 접속한 client 
        public string message;

        public RoomInfo()
        {
            this.Type = (int)PacketType.RoomInfo;
            roomCode = Packet.isEmpty;
            clientID = Packet.isEmpty;
            players = new bool[] { false, false, false, false, false, false, false };
            message = "";
        }
    }

    // ~~Packet 생성자에 PacketType 초기화해주기

    public enum ErrorCode
    {
        RoomExistException = 0,
        NoRoomExistException,
    }

    [Serializable]
    public class Error: Packet
    {
        public ErrorCode code;
        public Error(ErrorCode code)
        {
            this.Type = (int)PacketType.Error;
            this.code = code;
        }
    }

    [Serializable]
    public class PlayerState
    {
        public bool isDestroyedPickaxe;
        public bool isDestroyedLantern;
        public bool isDestroyedCart;

        public PlayerState()
        {
            isDestroyedPickaxe = false;
            isDestroyedLantern = false;
            isDestroyedCart = false;
        }

        public bool hasDestroyed()
        {
            return this.isDestroyedPickaxe || this.isDestroyedLantern || this.isDestroyedCart;
        }
    }

    [Serializable]
    public class GameInfo : Packet
    {
        public bool isSaboteur;
        public bool isTurn;                 // 해당 Player가 현재 Turn인지
        public string message;
        public int restCardNum;            // 남은 카드 개수

        public Map fields;                // 현재까지 놓여진 맵(필드), Dest Card 포함
        public List<Card> holdingCards;         // 해당 Player가 소지하고 있는 Card들
        public Stack<Card> usedCards;
        public List<PlayerState> playersState;  // 모든 플레이어의 state 정보(장비 파괴 상태)

        public GameInfo()
        {
            this.Type = (int)PacketType.GameInfo;

            this.isSaboteur = false;
            this.isTurn = false;
            this.message = "";
            this.restCardNum = 0;

            this.fields = new Map();
            this.holdingCards = new List<Card>();
            this.usedCards = new Stack<Card>();
            this.playersState = new List<PlayerState>();
        }
    }

    [Serializable]
    public class MessagePacket: Packet
    {
        public string message;
        public MessagePacket()
        {
            message = "";
        }

        public MessagePacket(string msg)
        {
            message = msg;
        }

    }
}
