namespace Blockade
{
    public class Pawn
    {

        private int x;
        private int y;

        public Pawn(int _x, int _y)
        {

            x = _x;
            y = _y;
        }

        public void move(int _x, int _y)
        {

            x = _x;
            y = _y;
        }

        public int X
        {

            get { return x; }

            set { x = value; }
        }

        public int Y
        {

            get { return y; }

            set { y = value; }
        }
    }
}


