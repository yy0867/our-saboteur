using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
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
    public class Card
    {
        private int CardNum = NumInfo.TOTAL_CARD;
        public int getCardNum()
        {
            return CardNum;
        }

        private void disCard()
        {
            CardNum--;
        }
        public virtual bool Action()
        {
            try
            {
                if (CardNum == 0)
                    throw new Exception("No Card");
                disCard();            
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }

    public class CaveCard : Card
    {
        private int CardNum = NumInfo.CAVE_CARD;
        protected bool isConnected;
        protected Dir dir;

        public CaveCard()
        {
            dir = Dir.NONE;
            isConnected = false;
        }
        public CaveCard(Dir dir, bool isConnected)
        {
            this.dir = dir;
            this.isConnected = isConnected;
        }

        public Dir getDir()
        {
            return dir;
        }

        public bool getIsConnected()
        {
            return isConnected;
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

        public override bool Action()
        {
            return base.Action();
        }
    }

    public class StartCard : CaveCard
    {
        public StartCard()
        {
            this.dir = Dir.ALL;
            this.isConnected = true;
        }
    }
    public class DestCard : CaveCard
    {
        private bool isOpen;
        private bool nearByCardExist;
        private bool isGoldCave;

        public DestCard(bool isOpen, bool nearByCardExist, bool isGoldCave)
        {
            this.isOpen = isOpen;
            this.nearByCardExist = nearByCardExist;
            this.isGoldCave = isGoldCave;
        }

        public bool getIsOpen()
        {
            return isOpen;
        }

        public bool getNearByCardExist()
        {
            return nearByCardExist;
        }

        protected bool getIsGoldCave()
        {
            return isGoldCave;
        }
    }
    public class ActionCard : Card
    {
        protected int RPcardNum = 
            NumInfo.RP_CART_CARD + NumInfo.RP_LANTERN_CARD + NumInfo.RP_MANDREL_CARD;
        protected int DTcardNum = 
            NumInfo.DT_CART_CARD + NumInfo.DT_LANTERN_CARD + NumInfo.DT_MANDREL_CARD;
        protected int RockFallcardNum = NumInfo.ROCKFALL_CARD;
        protected int MapcardNum = NumInfo.MAP_CARD;

        public int getRPardNum()
        {
            return RPcardNum;
        }

        public int getEDcardNum()
        {
            return DTcardNum;
        }

        public int getRockFallcardNum()
        {
            return RockFallcardNum;
        }

        public int getMapcardNum()
        {
            return MapcardNum;
        }
    }

    public class RockFallCard : ActionCard
    {
       
    }

    public class EquipmentCard : ActionCard
    {

    }

    public class EqDestructionCard : EquipmentCard
    {

    }

    public class EqRepairCard : EquipmentCard
    {

    }

    public class MapCard : ActionCard
    {
        
    }
}
