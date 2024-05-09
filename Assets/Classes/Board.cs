namespace Blockade {
    public class Board
    {

        private Case[,] gsboard;

        public Case[,] gsBoard
        {

            get { return gsboard; }

            set { gsboard = value; }
        }

        public Board()
        {

            gsboard = new Case[14, 11];

            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    gsboard[i, j] = new Case();
                }
            }

            gsboard[3, 3].StartingCase = true;
            gsboard[3, 7].StartingCase = true;
            gsboard[10, 3].StartingCase = true;
            gsboard[10, 7].StartingCase = true;
        }
    }
}

