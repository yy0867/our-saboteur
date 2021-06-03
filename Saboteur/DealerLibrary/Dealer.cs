using System;
using System.Collections.Generic;
using System.Linq;

namespace DealerLibrary
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
        public Dealer()
        {
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

        public bool[] DefineRole(bool[] players)
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
            Suffle(jobList);

            return jobList.ToArray();

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
