using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;
using System.Collections;

namespace MapLibrary
{
    [Serializable]
    public static class CONST
    {
        public const int MAP_COL = 17; // 행
        public const int MAP_ROW = 7; // 열
        public const int START_R = 3;
        public const int START_C = 4;
        public const int DESTINATION_R = 10;
        public const int DESTINATION_C = 10;
    }

    [Serializable]
    public class Point
    {
        public int R { get; set; }
        public int C { get; set; }

        public Point(int R, int C)
        {
            this.R = R;
            this.C = C;
        }
    }

    [Serializable]
    public class Map
    {
        private CaveCard[,] caveCards;
        private bool[,] visited;
        private int Goal;

        public Map()
        {
            //caveCards = new CaveCard[CONST.MAPSIZE, CONST.MAPSIZE];
            caveCards = new CaveCard[CONST.MAP_ROW, CONST.MAP_COL];
        }

        public void MapInit()
        {
            bool[] dest = { false, false, false };
            Random rm = new Random();
            Goal = rm.Next(0, 2);
            dest[Goal] = true;

            for (int i = 0; i<CONST.MAP_ROW; i++)
            {
                for(int j = 0; j<CONST.MAP_COL; j++)
                {
                    if (i == 3 && j == 4) caveCards[i, j] = new StartCard();
                    else if(j == 12)
                    {
                        if (i == 1)
                            caveCards[i, j] = new DestCard(false, false, dest[0]);
                        else if (i == 3)
                            caveCards[i, j] = new DestCard(false, false, dest[1]);
                        else if (i == 5)
                            caveCards[i, j] = new DestCard(false, false, dest[2]);
                        else
                            caveCards[i, j] = new CaveCard();
                    }  
                    else
                        caveCards[i, j] = new CaveCard();
                }
            }
        }

        public CaveCard GetCard(int R, int C)
        {
            if (R < 0 || R > CONST.MAP_ROW || C < 0 || C > CONST.MAP_COL)
                return null;
            return caveCards[R, C];
        }

        public CaveCard GetCard(Point point)
        {
            return GetCard(point.R, point.C);
        }
        
        public bool getDestCard(Point point)
        {
            DestCard dest = (DestCard)caveCards[point.R, point.C];
            return dest.getIsGoldCave();
        }

        public void RockDown(Point point)
        {
            caveCards[point.R, point.C].setDir(Dir.NONE);
            caveCards[point.R, point.C].setIsConnected(false);
        }
        public void MapAdd(Point point, CaveCard cave)
        {
            if (caveCards[point.R, point.C].isEmpty() && 
                CanBeConntectedSurrounding(point, cave) &&
                isConntectedStart(point))
            {
                caveCards[point.R, point.C] = cave;
            }
        }
        public bool CanBeConntectedSurrounding(Point point, CaveCard cave)
        {
            int r = point.R, c = point.C;
            bool result = true;
            bool isolated = true;

            if (!isValidated(point)) return false;

            if (r > 0 && !caveCards[r - 1, c].isEmpty()) 
            {
                isolated = false;
                if ((cave.getDir() & Dir.UP) == Dir.UP)
                    result &= (caveCards[r - 1, c].getDir() & Dir.DOWN) == Dir.DOWN;
            }

            if (c > 0 && !caveCards[r, c - 1].isEmpty())
            {
                isolated = false;
                if ((cave.getDir() & Dir.LEFT) == Dir.LEFT)
                    result &= (caveCards[r, c - 1].getDir() & Dir.RIGHT) == Dir.RIGHT;
            }

            if (r < CONST.MAP_ROW - 1 && !caveCards[r + 1, c].isEmpty())
            {
                isolated = false;
                if ((cave.getDir() & Dir.DOWN) == Dir.DOWN)
                    result &= (caveCards[r + 1, c].getDir() & Dir.UP) == Dir.UP;
            }

            if (c < CONST.MAP_COL - 1 && !caveCards[r, c + 1].isEmpty())
            {
                isolated = false;
                if ((cave.getDir() & Dir.RIGHT) == Dir.RIGHT)
                    result &= (caveCards[r, c + 1].getDir() & Dir.LEFT) == Dir.LEFT;
            }

            if (isolated)
                return false;

            return result;
        }      
        
        private bool isConntectedStart(Point currentPoint)
        {
            int[] ctr = { 0, -1, 0, 1, 0 };
            Queue queue = new Queue();
            visited = new bool[CONST.MAP_ROW, CONST.MAP_COL];
            visited[currentPoint.R, currentPoint.C] = true;
            queue.Enqueue(new Point(currentPoint.R, currentPoint.C));

            while (queue.Count != 0)
            {
                Point visitedPoint =  (Point)queue.Dequeue();
                if (isStart(visitedPoint)) return true;
                
                for(int i = 0; i < ctr.Length-1; i++)
                {
                    int r = visitedPoint.R + ctr[i], c = visitedPoint.C + ctr[i + 1];  // 주변 좌표       
                    Point point = new Point(r, c);

                    if (isValidated(point) && visited[r, c] == false)
                    {
                        if (isStart(point)) 
                            return true;

                        visited[r, c] = true;
                        queue.Enqueue(new Point(r, c));
                    }
                }
            }
            return false;
        }

        private bool isStart(Point point)
        {
            if (point.R == CONST.START_R && point.C == CONST.START_C)
                return true;
            return false;
        }
        
        private bool isValidated(Point point) // 현재 좌표 유효성 검사
        {
            int r = point.R, c = point.C;
            return (r >= 0 && r < CONST.MAP_ROW && c >= 0 && c < CONST.MAP_COL/* &&!isBlooked(point)*/);
        }

        private bool isBlooked(Point point)
        {
            return caveCards[point.R, point.C].isEmpty();
        }
    }
}
