using System.Linq;
using System.Collections.Generic;
using UnityEngine.Analytics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Blockade
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private Graphe graph;
        private IAFacile iAFacile;

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

        public Board Board
        {

            get { return board; }

            set { board = value; }
        }
        public Game()
        {
            board = new Board();
            graph = new Graphe();
            player1 = new Player(Player.PlayerType.X);
            player2 = new Player(Player.PlayerType.O);
            iAFacile = new IAFacile(player2.Pawn1,player2.Pawn2 );
        }

        public IAFacile IAFacile
        {

            get { return iAFacile; }

            set { iAFacile = value; }
        }
        
        public Graphe Graphe
        {

            get { return graph; }

            set { graph = value; }
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

        public bool wallBetweenStraight(int x, int y, int xEnd, int yEnd)
        {
            if (xEnd == x + 1 && yEnd == y)
            { // verif a droite
                return board.gsBoard[x, y].hasRightWall();
            }
            else if (xEnd == x - 1 && yEnd == y)
            { //verif a gauche
                return board.gsBoard[x, y].hasLeftWall();
            }
            else if (xEnd == x && yEnd == y + 1)
            { //verif en haut
                return board.gsBoard[x, y].hasTopWall();
            }
            else if (xEnd == x && yEnd == y - 1)
            { //verif en bas
                return board.gsBoard[x, y].hasBottomWall();
            }
            return true;
        }

        public bool wallBetweenDiagonal(int x, int y, int xEnd, int yEnd)
        {
            if (xEnd == x + 1 && yEnd == y + 1)
            { //verif en haut a droite
                if (!wallBetweenStraight(x, y, x + 1, y))
                {
                    if (!wallBetweenStraight(x + 1, y, x + 1, y + 1)) return false;
                }
                if (!wallBetweenStraight(x, y, x, y + 1))
                {
                    if (!wallBetweenStraight(x, y + 1, x + 1, y + 1)) return false;
                }
                return true;
            }
            else if (xEnd == x - 1 && yEnd == y + 1)
            { //verif en haut a gauche
                if (!wallBetweenStraight(x, y, x - 1, y))
                {
                    if (!wallBetweenStraight(x - 1, y, x - 1, y + 1)) return false;
                }
                if (!wallBetweenStraight(x, y, x, y + 1))
                {
                    if (!wallBetweenStraight(x, y + 1, x - 1, y + 1)) return false;
                }
                return true;
            }
            else if (xEnd == x + 1 && yEnd == y - 1)
            { // verif en bas a droite
                if (!wallBetweenStraight(x, y, x + 1, y))
                {
                    if (!wallBetweenStraight(x + 1, y, x + 1, y - 1)) return false;
                }
                if (!wallBetweenStraight(x, y, x, y - 1))
                {
                    if (!wallBetweenStraight(x, y - 1, x + 1, y - 1)) return false;
                }
                return true;
            }
            else if (xEnd == x - 1 && yEnd == y - 1)
            { // verif en bas a gauche
                if (!wallBetweenStraight(x, y, x - 1, y))
                {
                    if (!wallBetweenStraight(x - 1, y, x - 1, y - 1)) return false;
                }
                if (!wallBetweenStraight(x, y, x, y - 1))
                {
                    if (!wallBetweenStraight(x, y - 1, x - 1, y - 1)) return false;
                }
                return true;
            }
            return true;
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

        public List<(int, int)> getAvailableMove(Pawn Pawn)
        {
            int x = Pawn.X;
            int y = Pawn.Y;
            Player p1 = player1;
            Player p2 = player2;
            bool topRight = false;
            bool topLeft = false;
            bool bottomLeft = false;
            bool bottomRight = false;
            List<(int, int)> movesPossible = new List<(int, int)>();
            //top
            if (!board.gsBoard[x, y].hasTopWall() && y - 1 >= 0)
            {
                //verif 1/2 pour top right
                if (!board.gsBoard[x, y - 1].hasRightWall())
                {
                    topRight = true;
                }
                //verif 1/2 pour top left
                if (!board.gsBoard[x, y - 1].hasLeftWall())
                {
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
                        if (!CasehasPawn(x, y - 3)) movesPossible.Add((x, y - 3));
                        if (!CasehasPawn(x - 1, y - 2)) movesPossible.Add((x - 1, y - 2));
                        if (!CasehasPawn(x + 1, y - 2)) movesPossible.Add((x + 1, y - 2));
                    }
                }
            }
            //bottom
            if (!board.gsBoard[x, y].hasBottomWall() && y + 1 < 11)
            {
                //verif 1/2 pour bottom right
                if (!board.gsBoard[x, y + 1].hasRightWall())
                {
                    bottomRight = true;
                }
                //verif 1/2 pour bottom left
                if (!board.gsBoard[x, y + 1].hasLeftWall())
                {
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
                    if (CasehasPawn(x, y + 1) && CasehasPawn(x, y + 2) && y + 3 < 11)
                    {
                        if (!CasehasPawn(x, y + 3)) movesPossible.Add((x, y + 3));
                        if (!CasehasPawn(x - 1, y + 2)) movesPossible.Add((x - 1, y + 2));
                        if (!CasehasPawn(x + 1, y + 2)) movesPossible.Add((x + 1, y + 2));
                    }
                }
            }
            //left
            if (!board.gsBoard[x, y].hasLeftWall() && x - 1 >= 0)
            {
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
                    if (CasehasPawn(x - 1, y) && CasehasPawn(x - 2, y) && x - 3 >= 0)
                    {
                        if (!CasehasPawn(x - 3, y)) movesPossible.Add((x - 3, y));
                        if (!CasehasPawn(x - 2, y - 1)) movesPossible.Add((x - 2, y - 1));
                        if (!CasehasPawn(x - 2, y + 1)) movesPossible.Add((x - 2, y + 1));

                    }
                }
            }
            //right
            if (!board.gsBoard[x, y].hasRightWall() && x + 1 < 14)
            {
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
                        if (!CasehasPawn(x + 3, y)) movesPossible.Add((x + 3, y));
                        if (!CasehasPawn(x + 2, y - 1)) movesPossible.Add((x + 2, y - 1));
                        if (!CasehasPawn(x + 2, y + 1)) movesPossible.Add((x + 2, y + 1));
                    }
                }
            }


            if (bottomLeft && x - 1 >= 0 && y + 1 < 11) movesPossible.Add((x - 1, y + 1));
            if (bottomRight && x + 1 < 14 && y + 1 < 11) movesPossible.Add((x + 1, y + 1));
            if (topRight && x + 1 < 14 && y - 1 >= 0) movesPossible.Add((x + 1, y - 1));
            if (topLeft && x - 1 >= 0 && y - 1 >= 0) movesPossible.Add((x - 1, y - 1));


            for (int i = 0; i < movesPossible.Count; i++)
            {
                if (CasehasPawn(movesPossible[i].Item1, movesPossible[i].Item2))
                {
                    movesPossible.RemoveAt(i);
                }
            }

            if (board.gsBoard[x, y - 1].StartingCase)
            {
                movesPossible.Add((x, y - 1));
            }

            if (board.gsBoard[x, y + 1].StartingCase)
            {
                movesPossible.Add((x, y + 1));
            }

            if (board.gsBoard[x + 1, y].StartingCase)
            {
                movesPossible.Add((x + 1, y));
            }

            if (board.gsBoard[x - 1, y].StartingCase)
            {
                movesPossible.Add((x - 1, y));
            }

            return movesPossible;
        }


        public bool canPlaceWall(int x, int y, bool isHorizontal)
        {
            if (x < 0 || x > board.gsBoard.Length) return false;

            if (y < 0 || y > board.gsBoard.Length) return false;

            // voir si il y a déjà un mur
            if (!isHorizontal)
            {
                if (IsVerticalWallHere(x, y) || (board.gsBoard[x, y].hasBottomWall() && board.gsBoard[x + 1, y].hasBottomWall()))
                {
                    return false;
                }
            }
            else
            {
                if (IsHorizontalWallHere(x, y) || (board.gsBoard[x, y].hasRightWall() && board.gsBoard[x, y + 1].hasRightWall()))
                {
                    return false;
                }
            }

            // voir si on bloque
            Sommet sommetFinal1;
            Sommet sommetFinal2;
            Sommet sommetPion1;
            Sommet sommetPion2;

            if (PlayerPrefs.GetInt("currentPlayer") == 1)
            {
                sommetPion1 = graph.GetSommet(player1.Pawn1.X, player1.Pawn1.Y);
                sommetPion2 = graph.GetSommet(player1.Pawn2.X, player1.Pawn2.Y);
                sommetFinal1 = graph.GetSommet(10, 3);
                sommetFinal2 = graph.GetSommet(10, 7);
            }
            else
            {
                sommetPion1 = graph.GetSommet(player2.Pawn1.X, player2.Pawn1.Y);
                sommetPion2 = graph.GetSommet(player2.Pawn2.X, player2.Pawn2.Y);
                sommetFinal1 = graph.GetSommet(3, 3);
                sommetFinal2 = graph.GetSommet(3, 7);
            }
            if (isHorizontal)
            {
                graph.SupprimerArete(graph.GetSommet(x, y), graph.GetSommet(x, y + 1));
                graph.SupprimerArete(graph.GetSommet(x + 1, y), graph.GetSommet(x + 1, y + 1));
                bool temp = graph.ExisteChemin(sommetPion1, sommetFinal1) || graph.ExisteChemin(sommetPion1, sommetFinal2) || graph.ExisteChemin(sommetPion2, sommetFinal1) || graph.ExisteChemin(sommetPion2, sommetFinal2);
                Arete temp1 = new Arete(graph.GetSommet(x, y), graph.GetSommet(x, y + 1));
                Arete temp2 = new Arete(graph.GetSommet(x + 1, y), graph.GetSommet(x + 1, y + 1));
                graph.Aretes.Add(temp1);
                graph.Aretes.Add(temp2);
                return temp;
            }
            else
            {
                graph.SupprimerArete(graph.GetSommet(x, y), graph.GetSommet(x + 1, y));
                graph.SupprimerArete(graph.GetSommet(x, y + 1), graph.GetSommet(x + 1, y + 1));
                bool temp = graph.ExisteChemin(sommetPion1, sommetFinal1) || graph.ExisteChemin(sommetPion1, sommetFinal2) || graph.ExisteChemin(sommetPion2, sommetFinal1) || graph.ExisteChemin(sommetPion2, sommetFinal2);
                Arete temp1 = new Arete(graph.GetSommet(x, y), graph.GetSommet(x + 1, y));
                Arete temp2 = new Arete(graph.GetSommet(x, y + 1), graph.GetSommet(x + 1, y + 1));
                graph.Aretes.Add(temp1);
                graph.Aretes.Add(temp2);
                return temp;
            }

            return true;
        }

        public List<(int, int)> GetAvailableWall(bool isHorizontal)
        {
            List<(int, int)> Result = new List<(int, int)>();
            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    if (canPlaceWall(x, y, isHorizontal))
                    {
                        Result.Add((x, y));
                    }
                }
            }
            return Result;
        }

        private bool IsVerticalWallHere(int x, int y)
        {
            return (board.gsBoard[x, y].hasRightWall()) || (board.gsBoard[x, y + 1].hasRightWall());
        }

        private bool IsHorizontalWallHere(int x, int y)
        {
            Debug.Log("Check x and y" + x + " yyyyyyy " + y);
            if(x == 13)
            {
                return board.gsBoard[x, y].hasBottomWall();
            }
            return (board.gsBoard[x, y].hasBottomWall()) || (board.gsBoard[x + 1, y].hasBottomWall());
        }

        public void placeWall(Player p, int x, int y, bool isHorizontal)
        {

            if (isHorizontal)
            {
                Wall newWall = new Wall(Wall.WallType.horizontal);
                p.HorizontalWallLeft--;
                board.gsBoard[x, y].BottomWall = newWall;
                board.gsBoard[x + 1, y + 1].TopWall = newWall;
                board.gsBoard[x + 1, y].BottomWall = newWall;
                board.gsBoard[x, y + 1].TopWall = newWall;
                graph.SupprimerArete(graph.GetSommet(x, y), graph.GetSommet(x, y + 1));
                graph.SupprimerArete(graph.GetSommet(x + 1, y), graph.GetSommet(x + 1, y + 1));
            }
            else
            {
                Wall newWall = new Wall(Wall.WallType.vertical);
                p.VerticalWallLeft--;
                board.gsBoard[x, y].RightWall = newWall;
                board.gsBoard[x + 1, y].LeftWall = newWall;
                board.gsBoard[x, y + 1].RightWall = newWall;
                board.gsBoard[x + 1, y + 1].LeftWall = newWall;
                graph.SupprimerArete(graph.GetSommet(x, y), graph.GetSommet(x + 1, y));
                graph.SupprimerArete(graph.GetSommet(x, y + 1), graph.GetSommet(x + 1, y + 1));
            }
        }
    }
}