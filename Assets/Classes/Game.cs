using System.Linq;

namespace Blockade
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;

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

        public (int, int) getAvailableMove(Pawn Pawn)
        {
            int x = Pawn.X;
            int y = Pawn.Y;
            Player p1 = player1;
            Player p2 = player2;
            bool topRight = false;
            bool topLeft = false;
            bool bottomLeft = false;
            bool bottomRight = false;
            (int, int)[] movesPossible = new (int, int)[];
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
                if (caseHasPawn(x, y + 2) && !casehasPawn(x, y + 1))
                {
                    movesPossible.Append((x, y + 1));
                }
                //si 2 pions consécutif au top
                if (caseHasPawn(x, y + 1) && casehasPawn(x, y + 2))
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
                if (caseHasPawn(x, y - 2) && !casehasPawn(x, y - 1))
                {
                    movesPossible.Append((x, y - 1));
                }
                //si 2 pions consécutif au top
                if (caseHasPawn(x, y - 1) && casehasPawn(x, y - 2))
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
                    movesPossible.Append((x - 2, y))
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
                if (caseHasPawn(x, y - 2) && !casehasPawn(x, y - 1))
                {
                    movesPossible.Append((x, y - 1));
                }
                //si 2 pions consécutif a gauche    
                if (caseHasPawn(x - 1, y) && casehasPawn(x - 2, y))
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
                    movesPossible.Append((x + 2, y))
                }
                //verif 2/2 pour top right
                if (!board.gsBoard[x + 1, y].hasTopWall)
                {
                    topRight = true
                }
                //verif 2/2 pour bottom right
                if (!board.gsBoard[x + 1, y].hasBottomWall)
                {
                    bottomRight = true
                }

                //verif pour avancer de 1 a droite
                if (caseHasPawn(x - 2, y) && !casehasPawn(x - 1, y))
                {
                    movesPossible.Append((x - 1, y))
                }
                //si 2 pions consécutif a droite  
                if (caseHasPawn(x + 1, y) && casehasPawn(x + 2, y))
                {
                    movesPossible.Append((x + 3, y))
                    movesPossible.Append((x + 2, y - 1))
                    movesPossible.Append((x + 2, y + 1))
                }
            }
            if (bottomLeft) movesPossible.Append((x - 1, y - 1))
            if (bottomRight) movesPossible.Append(MoveType.(x + 1, y - 1))
            if (topRight) movesPossible.Append((x + 1, y + 1))
            if (topLeft) movesPossible.Append((x - 1, y + 1))
        }

        private boolean caBloque(int, x, int y, boolean vertical)
        {

            Board temp = board;

            if (vertical)
            {

                temp[x, y].setRightWall(true);
                temp[x + 1, y].setLeftWall(true);
                temp[x, y + 1].setRightWall(true);
                temp[x + 1, y + 1].setLeftWall(true);
            }
            else
            {

                temp[x, y].setBottomWall(true);
                temp[x, y + 1].setHighWall(true);
                temp[x + 1, y].setBottomWall(true);
                temp[x + 1, y + 1].setHighWall(true);
            }

            // parcourir le tableau a partir de la position des joueurs pour vérifier qu'ils peuvent atteindre la case adverse


            return false;
        }

        public bool canPlaceWall(int x, int y, bool vertical)
        {

            if (x < 0 || x > board.gsBoard.Length) return false;

            if (y < 0 or y > board.gsBoard.Length) return false;

            if (caBloque(x, y, vertical)) return false;

            return true;
        }

        public void placeWall(Player p, int x, int y, bool isHorizontal)
        {
            Wall newWall = new Wall;
            if (isHorizontal)
            {
                p.HorizontalWallLeft = p.HorizontalWallLeft - 1;
                board[x, y].bottomWall = newWall;
                board[x - 1, y].topWall = newWall;
                board[x, y + 1].bottomWall = newWall;
                board[x - 1, y + 1].TopWall = newWall;
            }
            else
            {
                p.VerticalWallLeft = p.VerticalWallLeft - 1;
                board[x, y].rightWall = newWall;
                board[x + 1, y].leftWall = newWall;
                board[x, y - 1].rightWall = newWall;
                board[x + 1, y - 1].leftWall = newWall;
            }
        }
    }
}