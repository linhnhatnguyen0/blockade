namespace Blockade
{
    public class Player
    {

        public enum PlayerType
        {

            X,
            O
        }

        protected int verticalWallLeft;
        protected int horizontalWallLeft;

        protected Pawn pawn1;
        protected Pawn pawn2;

        public Pawn Pawn1
        {

            get { return pawn1; }

            set { pawn1 = value; }
        }

        public Pawn Pawn2
        {

            get { return pawn2; }

            set { pawn2 = value; }
        }

        public Player(PlayerType type)
        {

            this.verticalWallLeft = 9;
            this.horizontalWallLeft = 9;

            if (type == PlayerType.X)
            {

                this.pawn1 = new Pawn(4, 4);
                this.pawn2 = new Pawn(8, 4);
            }
            else
            {

                this.pawn1 = new Pawn(4, 11);
                this.pawn2 = new Pawn(8, 11);
            }
        }

        public int VerticalWallLeft
        {

            get { return verticalWallLeft; }

            set { verticalWallLeft = value; }
        }

        public int HorizontalWallLeft
        {

            get { return horizontalWallLeft; }

            set { horizontalWallLeft = value; }
        }
    }
}
