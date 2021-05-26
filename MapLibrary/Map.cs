using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace MapLibrary
{
    static class CONST
    {
        public const int MAPSIZE = 20;
        public const int START = 3;
        public const int DESTINATION = 10;
    }
    public class Map
    {
        private CaveCard[,] caveCards;

        public Map()
        {
            caveCards = new CaveCard[CONST.MAPSIZE, CONST.MAPSIZE];
        }

        public void MapInit()
        {
            bool[] dest = { false, false, false };
            Random rm = new Random();
            dest[rm.Next(0, 2)] = true;

            for (int i = 0; i<CONST.MAPSIZE; i++)
            {
                for(int j = 0; j<CONST.MAPSIZE; j++)
                {
                    if (i == 9 && j == 3) caveCards[i, j] = new StartCard();
                    else if(j == 9)
                    {
                        if(i == 7)
                            caveCards[i, j] = new DestCard(false, false, dest[0]);
                        else if(i == 9)
                            caveCards[i, j] = new DestCard(false, false, dest[1]);
                        else if(i == 11)
                            caveCards[i, j] = new DestCard(false, false, dest[2]);
                    }  
                    else
                        caveCards[i, j] = new CaveCard();
                }
            }
        }

        
    }
}
