using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Blockade
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;

        public Player Player1
        {

            get { return player1; }

            set { player1 = value; }
        }

        public Player Player2
        {

            get { return player2; }

            set { player2 = value; }
        }

        public Game()
        {
            board = new Board();
            player1 = new Player(Player.PlayerType.X);
            player2 = new Player(Player.PlayerType.O);
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
            else if (p2.Pawn1.X == x && p2.Pawn1.Y == y)
            {
                return p2.Pawn1;
            }
            else if (p2.Pawn2.X == x && p2.Pawn2.Y == y)
            {
                return p2.Pawn2;
            }
            return null;
        }

        public List<(int,int)> getAvailableMove(Pawn Pawn)
        {
            Debug.Log("Début Get Available moove");
            int x = Pawn.X;
            int y = Pawn.Y;
            Player p1 = player1;
            Player p2 = player2;
            bool topRight = false;
            bool topLeft = false;
            bool bottomLeft = false;
            bool bottomRight = false;
            List<(int,int)> movesPossible = new List<(int, int)>();
            //top
            if (!board.gsBoard[x, y].hasTopWall() && y - 1 >= 0)
            {
                //verif 1/2 pour top right
                if (!board.gsBoard[x, y - 1].hasRightWall())
                {
                    Debug.Log("Top : Top Right");
                    topRight = true;
                }
                //verif 1/2 pour top left
                if (!board.gsBoard[x, y - 1].hasLeftWall())
                {
                    Debug.Log("Top : Top Left");
                    topLeft = true;
                }
                if (y - 2 >= 0)
                {
                    if (!board.gsBoard[x, y - 1].hasTopWall())
                    {
                        movesPossible.Add((x, y - 2));
                    }
                    //verif pour avancer de 1 au top
                    if (CasehasPawn(x, y - 2) && !CasehasPawn(x, y - 1))
                    {
                        movesPossible.Add((x, y - 1));
                    }
                    //si 2 pions consécutif au top
                    if (CasehasPawn(x, y - 1) && CasehasPawn(x, y - 2) && y - 3 >= 0)
                    {
                        movesPossible.Add((x, y - 3));
                        movesPossible.Add((x - 1, y - 2));
                        movesPossible.Add((x + 1, y - 2));
                    }
                }
            }
            //bottom
            if (!board.gsBoard[x, y].hasBottomWall() && y + 1 < 11)
            {
                //verif 1/2 pour bottom right
                if (!board.gsBoard[x, y + 1].hasRightWall())
                {
                    Debug.Log("Bot : Bot Right");
                    bottomRight = true;
                }
                //verif 1/2 pour bottom left
                if (!board.gsBoard[x, y + 1].hasLeftWall())
                {
                    Debug.Log("Bot : Bot Left");
                    bottomLeft = true;
                }
                if (y + 2 < 11)
                {
                    if (!board.gsBoard[x, y + 1].hasBottomWall())
                    {
                        movesPossible.Add((x, y + 2));
                    }
                    //verif pour avancer de 1 au bottom
                    if (CasehasPawn(x, y + 2) && !CasehasPawn(x, y + 1))
                    {
                        movesPossible.Add((x, y + 1));
                    }
                    //si 2 pions consécutif au bot
                    if (CasehasPawn(x, y + 1) && CasehasPawn(x, y + 2) && y+3 < 11)
                    {
                        movesPossible.Add((x, y + 3));
                        movesPossible.Add((x - 1, y + 2));
                        movesPossible.Add((x + 1, y + 2));
                    }
                }
            }
            //left
            if (!board.gsBoard[x, y].hasLeftWall() && x - 1 >= 0)
            {
                //verif 2/2 pour bottom left
                if (!board.gsBoard[x - 1, y].hasBottomWall())
                {
                    Debug.Log("Left : Bot Left");
                    bottomLeft = true;
                }
                //verif 2/2 pour top left
                if (!board.gsBoard[x - 1, y].hasTopWall())
                {
                    Debug.Log("Left : Top Right");
                    topLeft = true;
                }

                if (x - 2 >= 0)
                {
                    if (!board.gsBoard[x - 1, y].hasLeftWall())
                    {
                        movesPossible.Add((x - 2, y));
                    }
                    //verif pour avancer de 1 a gauche
                    if (CasehasPawn(x - 2, y) && !CasehasPawn(x - 1, y))
                    {
                        movesPossible.Add((x - 1, y));
                    }
                    //si 2 pions consécutif a gauche    
                    if (CasehasPawn(x - 1, y) && CasehasPawn(x - 2, y) && x-3>=0)
                    {
                        movesPossible.Add((x - 3, y));
                        movesPossible.Add((x - 2, y - 1));
                        movesPossible.Add((x - 2, y + 1));
                    }
                }
            }
            //right
            if (!board.gsBoard[x, y].hasRightWall() && x + 1 < 14)
            {
                //verif 2/2 pour top right
                if (!board.gsBoard[x + 1, y].hasTopWall())
                {
                    Debug.Log("Right : Top Right");
                    topRight = true;
                }
                //verif 2/2 pour bottom right
                if (!board.gsBoard[x + 1, y].hasBottomWall())
                {
                    Debug.Log("Right : Bot Right");
                    bottomRight = true;
                }
                if (x + 2 < 14)
                {
                    if (!board.gsBoard[x + 1, y].hasRightWall())
                    {
                        movesPossible.Add((x + 2, y));
                    }
                    //verif pour avancer de 1 a droite
                    if (CasehasPawn(x + 2, y) && !CasehasPawn(x + 1, y))
                    {
                        movesPossible.Add((x + 1, y));
                    }
                    //si 2 pions consécutif a droite  
                    if (CasehasPawn(x + 1, y) && CasehasPawn(x + 2, y))
                    {
                        movesPossible.Add((x + 3, y));
                        movesPossible.Add((x + 2, y - 1));
                        movesPossible.Add((x + 2, y + 1));
                    }
                }
            }
            if (bottomLeft && x - 1 >= 0 && y + 1 < 11) movesPossible.Add((x - 1, y + 1));
            if (bottomRight && x + 1 < 14 && y + 1 < 11) movesPossible.Add((x + 1, y + 1));
            if (topRight && x + 1 < 14 && y - 1 >= 0) movesPossible.Add((x + 1, y - 1));
            if (topLeft && x - 1 >= 0 && y - 1 >= 0) movesPossible.Add((x - 1, y - 1));

            return movesPossible;
        }


        public bool canPlaceWall(int x, int y, bool isHorizontal)
        {

            if (x < 0 || x > board.gsBoard.Length) return false;

            if (y < 0 || y > board.gsBoard.Length) return false;

            // voir si il y a déjà un mur
            if (!isHorizontal)
            {
                if (IsVerticalWallHere(x, y)) return false;
            }
            else
            {
                if (IsHorizontalWallHere(x, y)) return false; 
            }

            // voir si on bloque

            return true;
        }

        private bool IsVerticalWallHere(int x, int y)
        {
            return (board.gsBoard[x, y].hasRightWall()) || (board.gsBoard[x, y + 1].hasRightWall());
        }

        private bool IsHorizontalWallHere(int x, int y)
        {
            return (board.gsBoard[x, y].hasBottomWall()) || (board.gsBoard[x, y+1].hasBottomWall());
        }

        public void placeWall(Player p, int x, int y, bool isHorizontal)
        {
            
            if (isHorizontal)
            {
                Wall newWall = new Wall(Wall.WallType.horizontal);
                p.HorizontalWallLeft--;
                board.gsBoard[x, y].BottomWall = newWall;
                board.gsBoard[x + 1, y].TopWall = newWall;
                board.gsBoard[x, y + 1].BottomWall = newWall;
                board.gsBoard[x + 1, y + 1].TopWall = newWall;
            }
            else
            {
                Wall newWall = new Wall(Wall.WallType.vertical);
                p.VerticalWallLeft--;
                board.gsBoard[x, y].RightWall= newWall;
                board.gsBoard[x + 1, y].LeftWall = newWall;
                board.gsBoard[x, y + 1].RightWall = newWall;
                board.gsBoard[x + 1, y + 1].LeftWall = newWall;
            }
        }
    }
}