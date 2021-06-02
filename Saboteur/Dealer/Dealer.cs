using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;
using MapLibrary;
using PacketLibrary;

namespace Dealer
{
    static class CardNumInfo
    {
        public const int TOTAL_CARD = 67;
        public const int DT_LANTERN_CARD = 3;
        public const int DT_PICKAXE_CARD = 3;
        public const int DT_CART_CARD = 3;
        public const int RP_LANTERN_CARD = 4;
        public const int RP_PICKAXE_CARD = 4;
        public const int RP_CART_CARD = 4;
        public const int MAP_CARD = 6;
        public const int ROCKFALL_CARD = 3;
        public const int CAVE_CARD = 40;
    }
    public static class JOB
    {
        public const bool MINER = false;
        public const bool SABOTUER = true;
    }

    public class Dealer
    {
        static public List<Card> cardLIst = new List<Card>();
        private int playerNum;
        public int totalCard { get; set; }
        public int dtLantern { get; set; }
        public int dtPickaxe { get; set; }
        public int dtCart { get; set; }
        public int rpLantern { get; set; }
        public int rpPickaxe { get; set; }
        public int rpCart { get; set; }
        public int mapCardNum { get; set; }
        public int rockfallCardNum { get; set; }
        public int caveCardNum { get; set; }
        public Dealer(int playerNum)
        {
            this.playerNum = playerNum;
            totalCard = CardNumInfo.TOTAL_CARD;
            dtLantern = CardNumInfo.DT_LANTERN_CARD;
            dtPickaxe = CardNumInfo.DT_PICKAXE_CARD;
            dtCart = CardNumInfo.DT_CART_CARD;
            rpLantern = CardNumInfo.RP_LANTERN_CARD;
            rpPickaxe = CardNumInfo.RP_PICKAXE_CARD;
            rpCart = CardNumInfo.RP_CART_CARD;
            mapCardNum = CardNumInfo.MAP_CARD;
            rockfallCardNum = CardNumInfo.ROCKFALL_CARD;
            caveCardNum = CardNumInfo.CAVE_CARD;
        }

        public void CardInit()
        {
            cardLIst.Add(new CaveCard(Dir.RIGHTDOWN, false));
            cardLIst.Add(new CaveCard(Dir.LEFTDOWN, false));
            cardLIst.Add(new CaveCard(Dir.DOWNUP, false));
            cardLIst.Add(new CaveCard(Dir.RIGHT, false));
            cardLIst.Add(new CaveCard(Dir.UP, false));
            cardLIst.Add(new CaveCard(Dir.NOLEFT, false));
            cardLIst.Add(new CaveCard(Dir.ALL, false));
            cardLIst.Add(new CaveCard(Dir.RIGHTLEFT, false));
            cardLIst.Add(new CaveCard(Dir.NODOWN, false));
            for(int i = 0; i<4; i++)
                cardLIst.Add(new CaveCard(Dir.DOWNUP, true));
            for(int i = 0; i<5; i++)
                cardLIst.Add(new CaveCard(Dir.NORIGHT, true));
            for(int i = 0; i < 5; i++)
                cardLIst.Add(new CaveCard(Dir.ALL, true));
            for(int i = 0; i<5; i++)
                cardLIst.Add(new CaveCard(Dir.NOUP, true));
            for(int i = 0; i < 4; i++)
                cardLIst.Add(new CaveCard(Dir.RIGHTDOWN, true));
            for(int i = 0; i<5; i++)
                cardLIst.Add(new CaveCard(Dir.LEFTDOWN, true));
            for(int i = 0; i<3; i++)
                cardLIst.Add(new CaveCard(Dir.RIGHTLEFT, true));
            //굴카드 채움

            for (int i = 0; i < 6; i++)
                cardLIst.Add(new MapCard()); // 맵카드

            for (int i = 0; i < 3; i++)
                cardLIst.Add(new RockDownCard()); //낙석카드

            for(int i = 0; i<3; i++)
                cardLIst.Add(new EquipmentCard(CType.EQ_DESTRUCTION, Tool.CART));
            for (int i = 0; i < 3; i++)
                cardLIst.Add(new EquipmentCard(CType.EQ_DESTRUCTION, Tool.LATTERN));
            for (int i = 0; i < 3; i++)
                cardLIst.Add(new EquipmentCard(CType.EQ_DESTRUCTION, Tool.PICKAXE));
            for (int i = 0; i < 2; i++) 
                cardLIst.Add(new EquipmentCard(CType.EQ_REPAIR, Tool.CART));
            for (int i = 0; i < 2; i++)
                cardLIst.Add(new EquipmentCard(CType.EQ_REPAIR, Tool.LATTERN));
            for (int i = 0; i < 2; i++)
                cardLIst.Add(new EquipmentCard(CType.EQ_REPAIR, Tool.PICKAXE));
            cardLIst.Add(new EquipmentCard(CType.EQ_REPAIR, Tool.PICKLATTERN));
            cardLIst.Add(new EquipmentCard(CType.EQ_REPAIR, Tool.PICKCART));
            cardLIst.Add(new EquipmentCard(CType.EQ_REPAIR, Tool.LATTERNCART));
            // 도구카드(디버프 & 버프)
            cardLIst = Suffle(cardLIst);


        }
        public bool[] defineRole(bool[] players)
        {
            int playerCount = 0;
            foreach (bool player in players)
            {
                if (player)
                    playerCount++;
            }

            List<bool> jobList;
            switch (playerCount)
            {
                case 4:
                    jobList = new List<bool>{JOB.SABOTUER,
                        JOB.MINER, JOB.MINER, JOB.MINER, JOB.MINER };
                    break;
                case 5:
                    jobList = new List<bool>{ JOB.SABOTUER, JOB.SABOTUER,
                        JOB.MINER, JOB.MINER, JOB.MINER, JOB.MINER };
                    break;
                case 6:
                    jobList = new List<bool>{ JOB.SABOTUER, JOB.SABOTUER,
                        JOB.MINER, JOB.MINER, JOB.MINER, JOB.MINER, JOB.MINER };
                    break;
                default:
                    jobList = new List<bool>{ JOB.SABOTUER, JOB.SABOTUER, JOB.SABOTUER,
                        JOB.MINER, JOB.MINER, JOB.MINER, JOB.MINER, JOB.MINER };
                    break;
            }

            var rnd = new Random();
            jobList.RemoveAt(rnd.Next(0, playerCount));
            jobList = Suffle(jobList);

            return jobList.ToArray();

        }

        
        public Dictionary<int, List<Card>> cardDivide()
        {
            Dictionary<int, List<Card>> cardDict = new Dictionary<int, List<Card>>();

            for(int i = 0; i<this.playerNum; i++)
            {
                switch (playerNum)
                {
                    case 4:
                    case 5:
                        cardDict.Add(i, cardLIst.GetRange(6*i, 6*(i+1)-1));
                        break;
                    case 6:
                    default:
                        cardDict.Add(i, cardLIst.GetRange(5 * i, 5 * (i + 1) - 1));
                        break;
                }
            }
            return cardDict;
        }
        
        private List<T> Suffle<T>(List<T> list)
        {
            Random prng = new Random();

            for (int i = 0; i < list.Count - 1; i++)
            {
                int randomIndex = prng.Next(i, list.Count);
                T tempItem = list[randomIndex];
                list[randomIndex] = list[i];
                list[i] = tempItem;
            }

            return list;
        }
    }
}
