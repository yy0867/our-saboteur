using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    [Serializable]
    static class NumInfo
    {
        public const int TOTAL_CARD = 67;
        public const int DT_LANTERN_CARD = 3;
        public const int DT_MANDREL_CARD = 3;
        public const int DT_CART_CARD = 3;
        public const int RP_LANTERN_CARD = 4;
        public const int RP_MANDREL_CARD = 4;
        public const int RP_CART_CARD = 4;
        public const int MAP_CARD = 6;
        public const int ROCKFALL_CARD = 3;
        public const int CAVE_CARD = 40;
    }

    [Flags]
    public enum Dir
    {
        NONE = 0,
        RIGHT = 1,
        LEFT = 2,
        DOWN = 4,
        UP = 8,
        RIGHTLEFT = RIGHT | LEFT,
        RIGHTDOWN = RIGHT | DOWN,
        RIGHTUP = RIGHT | UP,
        LEFTDOWN = LEFT | DOWN,
        LEFTUP = LEFT | UP,
        DOWNUP = DOWN | UP,
        NOUP= RIGHT | LEFT | DOWN,
        NODOWN = RIGHT | LEFT | UP,
        NOLEFT = UP | DOWN | RIGHT,
        NORIGHT = UP | DOWN | LEFT,
        ALL = UP | DOWN | LEFT | RIGHT
    }

    public enum CType
    {
        NONE = 0,
        CAVE = 1,
        MAP,
        ROCK_DOWN,
        EQ_REPAIR,
        EQ_DESTRUCTION
    }

    public enum Tool
    {
        PICKAXE = 0,
        LATTERN = 1,
        CART,
        PICKLATTERN,
        PICKCART,
        LATTERNCART
    }

    [Serializable]
    public class Card
    {
        protected CType cType = CType.NONE;

        public void setType(CType cType)
        {
            this.cType = cType;
        }

        public CType getType()
        {
            return this.cType;
        }
    }

    [Serializable]
    public class CaveCard : Card
    {
        protected bool isConnected;
        protected Dir dir;
        
        public CaveCard()
        {
            dir = Dir.NONE;
            isConnected = false;
            cType = CType.CAVE;
        }
        public CaveCard(Dir dir, bool isConnected)
        {
            this.dir = dir;
            this.isConnected = isConnected;
        }

        public void rotate()
        {
            switch (dir)
            {
                case Dir.UP:
                    dir = Dir.DOWN;
                    break;
                case Dir.DOWN:
                    dir = Dir.UP;
                    break;
                case Dir.LEFT:
                    dir = Dir.RIGHT;
                    break;
                case Dir.RIGHT:
                    dir = Dir.LEFT;
                    break;
                case Dir.RIGHTDOWN:
                    dir = Dir.LEFTUP;
                    break;
                case Dir.RIGHTUP:
                    dir = Dir.LEFTDOWN;
                    break;
                case Dir.LEFTUP:
                    dir = Dir.RIGHTDOWN;
                    break;
                case Dir.LEFTDOWN:
                    dir = Dir.RIGHTUP;
                    break;
                case Dir.NOLEFT:
                    dir = Dir.NORIGHT;
                    break;
                case Dir.NORIGHT:
                    dir = Dir.NOLEFT;
                    break;
                case Dir.NODOWN:
                    dir = Dir.NOUP;
                    break;
                case Dir.NOUP:
                    dir = Dir.NODOWN;
                    break;
                default:
                    break;
            }
        }

        public Dir getDir()
        {
            return dir;
        }

        public void setDir(Dir dir)
        {
            this.dir = dir;
        }

        public bool getIsConnected()
        {
            return isConnected;
        }

        public void setIsConnected(bool isConnected)
        {
            this.isConnected = isConnected;
        }
        public bool isEmpty()
        {
            if (this.dir == Dir.NONE)
                return true;
            else return false;
        }

        public static bool operator ==(CaveCard lhs, CaveCard rhs)
        {
            return lhs.dir == rhs.dir
                && lhs.isConnected == rhs.isConnected;
        }
        public static bool operator !=(CaveCard lhs, CaveCard rhs)
        {
            return lhs.dir != rhs.dir
                || lhs.isConnected != rhs.isConnected;
        }
    }

    [Serializable]
    public class StartCard : CaveCard
    {
        public StartCard()
        {
            this.dir = Dir.ALL;
            this.isConnected = true;
        }
    }

    [Serializable]
    public class DestCard : CaveCard
    {
        public bool isOpen { get; set; }
        public bool nearByCardExist { get; set; }
        private bool isGoldCave;

        public DestCard(bool isOpen, bool nearByCardExist, bool isGoldCave)
        {
            this.dir = Dir.ALL;
            this.isConnected = true;
            this.isOpen = isOpen;
            this.nearByCardExist = nearByCardExist;
            this.isGoldCave = isGoldCave;
        }

        public  bool getIsGoldCave()
        {
            return isGoldCave;
        }
    }

    [Serializable]
    public class ActionCard : Card
    {
    }

    [Serializable]
    public class EquipmentCard : ActionCard
    {
        public Tool tool;
        public EquipmentCard(CType cType, Tool tool)
        {
            this.cType = cType;
            this.tool = tool;
        }
    }

    [Serializable]
    public class RockDownCard : ActionCard {
        public RockDownCard()
        {
            this.cType = CType.ROCK_DOWN;
        }
    }

    [Serializable]
    public class MapCard : ActionCard
    {
        public MapCard()
        {
            this.cType = CType.MAP;
        }
    }
}
