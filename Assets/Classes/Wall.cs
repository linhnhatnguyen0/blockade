namespace Blockade
{
    public class Wall
    {

        public enum WallType
        {

            vertical,
            horizontal
        }

        public WallType type;

        public Wall(WallType type)
        {

            this.type = type;
        }

        public WallType Type
        {

            get { return type; }

            set { type = value; }
        }
    }
}

