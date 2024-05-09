using System.Linq;

namespace Blockade
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Game(Player p1, Player p2)
        {
            board = new Board();
            player1 = p1;
            player2 = p2;
        }

        public bool CasehasPawn(int x, int y)
        {
            Player p1 = player1;
            Player p2 = player2;
            if (p2.Pawn1.X == x && p2.Pawn1.Y == y || p2.Pawn2.X == x && p2.Pawn2.Y == y || p1.Pawn1.X == x && p1.Pawn1.Y == y || p1.Pawn2.X == x && p1.Pawn2.Y == y)
            {
                return true;
            }
            return false;
        }

        public Pawn getPawnByCase(int x, int y)
        {
            Player p1 = player1;
            Player p2 = player2;
            if (p1.Pawn1.X == x && p1.Pawn1.Y == y)
            {
                return p1.Pawn1;
            }
            else if (p1.Pawn2.X == x && p1.Pawn2.Y == y)
            {
                return p1.Pawn2;
            }
            else if (p2.Pawn1.X == x && p2.Pawn2.Y == y)
            {
                return p2.Pawn1;
            }
            else if (p2.Pawn2.X == x && p2.Pawn2.Y == y)
            {
                return p2.Pawn2;
            }
            return null;
        }

        public (int, int)[] getAvailableMove(Pawn Pawn)
        {
            int x = Pawn.X;
            int y = Pawn.Y;
            Player p1 = player1;
            Player p2 = player2;
            bool topRight = false;
            bool topLeft = false;
            bool bottomLeft = false;
            bool bottomRight = false;
            (int, int)[] movesPossible = new (int, int)[20];
            //top
            if (!board.gsBoard[x, y].hasTopWall() || y + 1 >= 0)
            {
                if (!board.gsBoard[x, y + 1].hasTopWall())
                {
                    movesPossible.Append((x, y + 2));
                }
                //verif 1/2 pour top right
                if (!board.gsBoard[x, y + 1].hasRightWall())
                {
                    topRight = true;
                }
                //verif 1/2 pour top left
                if (!board.gsBoard[x, y + 1].hasLeftWall())
                {
                    topLeft = true;
                }
                //verif pour avancer de 1 au top
                if (CasehasPawn(x, y + 2) && !CasehasPawn(x, y + 1))
                {
                    movesPossible.Append((x, y + 1));
                }
                //si 2 pions consécutif au top
                if (CasehasPawn(x, y + 1) && CasehasPawn(x, y + 2))
                {
                    movesPossible.Append((x, y + 3));
                    movesPossible.Append((x - 1, y + 2));
                    movesPossible.Append((x + 1, y + 2));
                }
            }
            //bottom
            if (!board.gsBoard[x, y].hasBottomWall() || y - 1 <= 14)
            {

                if (!board.gsBoard[x, y - 1].hasBottomWall())
                {
                    movesPossible.Append((x, y - 2));
                }
                //verif 1/2 pour bottom right
                if (!board.gsBoard[x, y - 1].hasRightWall())
                {
                    bottomRight = true;
                }
                //verif 1/2 pour bottom left
                if (!board.gsBoard[x, y - 1].hasRightWall())
                {
                    bottomLeft = true;
                }

                //verif pour avancer de 1 au bottom
                if (CasehasPawn(x, y - 2) && !CasehasPawn(x, y - 1))
                {
                    movesPossible.Append((x, y - 1));
                }
                //si 2 pions consécutif au top
                if (CasehasPawn(x, y - 1) && CasehasPawn(x, y - 2))
                {
                    movesPossible.Append((x, y - 3));
                    movesPossible.Append((x - 1, y - 2));
                    movesPossible.Append((x + 1, y - 2));
                }
            }
            //left
            if (!board.gsBoard[x, y].hasLeftWall() || x - 1 >= 0)
            {

                if (!board.gsBoard[x - 1, y].hasLeftWall())
                {
                    movesPossible.Append((x - 2, y));
                }
                //verif 2/2 pour bottom left
                if (!board.gsBoard[x - 1, y].hasBottomWall())
                {
                    bottomLeft = true;
                }
                //verif 2/2 pour top left
                if (!board.gsBoard[x - 1, y].hasTopWall())
                {
                    topLeft = true;
                }

                //verif pour avancer de 1 a gauche
                if (CasehasPawn(x, y - 2) && !CasehasPawn(x, y - 1))
                {
                    movesPossible.Append((x, y - 1));
                }
                //si 2 pions consécutif a gauche    
                if (CasehasPawn(x - 1, y) && CasehasPawn(x - 2, y))
                {
                    movesPossible.Append((x - 3, y));
                    movesPossible.Append((x - 2, y - 1));
                    movesPossible.Append((x - 2, y + 1));
                }
            }
            //right
            if (!board.gsBoard[x, y].hasRightWall() || x + 1 <= 11)
            {

                if (!board.gsBoard[x, y].hasRightWall())
                {
                    movesPossible.Append((x + 2, y));
                }
                //verif 2/2 pour top right
                if (!board.gsBoard[x + 1, y].hasTopWall())
                {
                    topRight = true;
                }
                //verif 2/2 pour bottom right
                if (!board.gsBoard[x + 1, y].hasBottomWall())
                {
                    bottomRight = true;
                }

                //verif pour avancer de 1 a droite
                if (CasehasPawn(x - 2, y) && !CasehasPawn(x - 1, y))
                {
                    movesPossible.Append((x - 1, y));
                }
                //si 2 pions consécutif a droite  
                if (CasehasPawn(x + 1, y) && CasehasPawn(x + 2, y))
                {
                    movesPossible.Append((x + 3, y));
                    movesPossible.Append((x + 2, y - 1));
                    movesPossible.Append((x + 2, y + 1));
                }
            }
            if (bottomLeft) movesPossible.Append((x - 1, y - 1));
            if (bottomRight) movesPossible.Append((x + 1, y - 1));
            if (topRight) movesPossible.Append((x + 1, y + 1));
            if (topLeft) movesPossible.Append((x - 1, y + 1));

            return movesPossible;
        }


        public bool canPlaceWall(int x, int y, bool vertical)
        {

            if (x < 0 || x > board.gsBoard.Length) return false;

            if (y < 0 || y > board.gsBoard.Length) return false;

            // voir si on bloque

            return true;
        }

        public void placeWall(Player p, int x, int y, bool isHorizontal)
        {
            
            if (isHorizontal)
            {
                Wall newWall = new Wall(Wall.WallType.horizontal);
                p.HorizontalWallLeft = p.HorizontalWallLeft - 1;
                board.gsBoard[x, y].BottomWall = newWall;
                board.gsBoard[x - 1, y].TopWall = newWall;
                board.gsBoard[x, y + 1].BottomWall = newWall;
                board.gsBoard[x - 1, y + 1].TopWall = newWall;
            }
            else
            {
                Wall newWall = new Wall(Wall.WallType.vertical);
                p.VerticalWallLeft = p.VerticalWallLeft - 1;
                board.gsBoard[x, y].RightWall= newWall;
                board.gsBoard[x + 1, y].LeftWall = newWall;
                board.gsBoard[x, y - 1].RightWall = newWall;
                board.gsBoard[x + 1, y - 1].LeftWall = newWall;
            }
        }
    }
}