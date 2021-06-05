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
                IsValidPosition(point, cave))
            {
                caveCards[point.R, point.C] = cave;
            }
        }

        public bool IsValidPosition(Point point, CaveCard cave)
        {
            if (isValidated(point))
                return CanBeConntectedSurrounding(point, cave) && isConntectedStart(point);
            else
                return false;
        }

        public bool CanBeConntectedSurrounding(Point point, CaveCard cave)
        {
            int r = point.R, c = point.C;
            CaveCard watch;

            bool result = true;
            bool isolated = true;

            if (r > 0 && !caveCards[r - 1, c].isEmpty()) 
            {
                watch = caveCards[r - 1, c];
                isolated = false;
                if ((watch.getDir() & Dir.DOWN) == Dir.NONE) // [r - 1, c] no DOWN
                {
                    result &= (cave.getDir() & Dir.UP) == Dir.NONE; // [r, c] no UP
                } 
                else // [r - 1, c] DOWN
                {
                    result &= (cave.getDir() & Dir.UP) == Dir.UP; // [r, c] UP
                }
            }

            if (c > 0 && !caveCards[r, c - 1].isEmpty())
            {
                watch = caveCards[r, c - 1];
                isolated = false;
                if ((watch.getDir() & Dir.RIGHT) == Dir.NONE) // [r, c - 1] no RIGHT
                {
                    result &= (cave.getDir() & Dir.LEFT) == Dir.NONE; // [r, c] no LEFT
                }
                else // [r, c - 1] RIGHT
                {
                    result &= (cave.getDir() & Dir.LEFT) == Dir.LEFT; // [r, c] LEFT
                }
            }

            if (r < CONST.MAP_ROW - 1 && !caveCards[r + 1, c].isEmpty())
            {
                watch = caveCards[r + 1, c];
                isolated = false;
                if ((watch.getDir() & Dir.UP) == Dir.NONE) // [r + 1, c] no UP
                {
                    result &= (cave.getDir() & Dir.DOWN) == Dir.NONE; // [r, c] no DOWN
                }
                else // [r + 1, c] UP
                {
                    result &= (cave.getDir() & Dir.DOWN) == Dir.DOWN; // [r, c] DOWN
                }
            }

            if (c < CONST.MAP_COL - 1 && !caveCards[r, c + 1].isEmpty())
            {
                watch = caveCards[r, c + 1];
                isolated = false;
                if ((watch.getDir() & Dir.LEFT) == Dir.NONE) // [r, c + 1] no LEFT
                {
                    result &= (cave.getDir() & Dir.RIGHT) == Dir.NONE; // [r, c] no RIGHT
                }
                else // [r, c + 1] LEFT
                {
                    result &= (cave.getDir() & Dir.RIGHT) == Dir.RIGHT; // [r, c] RIGHT
                }
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
            queue.Enqueue(currentPoint);

            while (queue.Count != 0)
            {
                Point visitedPoint = (Point)queue.Dequeue();
                if (isStart(visitedPoint)) return true;

                for (int i = 0; i < ctr.Length - 1; i++)
                {
                    int r = visitedPoint.R + ctr[i], c = visitedPoint.C + ctr[i + 1];  // 주변 좌표       
                    Point watch = new Point(r, c);

                    if (isStart(watch))
                        return true;

                    if (checkBoundary(watch))
                    {
                        if (!isValidated(watch) && visited[r, c] == false)
                        {
                            visited[r, c] = true;
                            queue.Enqueue(watch);
                        }
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

        private bool checkBoundary(Point point)
        {
            int r = point.R, c = point.C;
            return r >= 0 && r < CONST.MAP_ROW && c >= 0 && c < CONST.MAP_COL;
        }
        
        private bool isValidated(Point point) // 현재 좌표 유효성 검사
        {
            return (checkBoundary(point) && !isExist(point));
        }

        private bool isExist(Point point)
        {
            return !caveCards[point.R, point.C].isEmpty();
        }
    }
}
