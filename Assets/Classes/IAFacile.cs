using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockade
{
    public class IAFacile
    {
        public Pawn pawn1;
        public Pawn pawn2;

        #region Getter Setter and Constructor
        public IAFacile(Pawn pawn1, Pawn pawn2)
        {
            this.pawn1 = pawn1;
            this.pawn2 = pawn2;
        }

        // Getter pour pawn1
        public Pawn getPawn1()
        {
            return pawn1;
        }

        // Setter pour pawn1
        public void setPawn1(Pawn pawn1)
        {
            this.pawn1 = pawn1;
        }

        // Getter pour pawn2
        public Pawn getPawn2()
        {
            return pawn2;
        }

        // Setter pour pawn2
        public void setPawn2(Pawn pawn2)
        {
            this.pawn2 = pawn2;
        }

        public void setInitPawn1(Pawn pawn1)
        {
            pawn1.X = 4;
            pawn1.Y = 11;
        }
        public void setInitPawn2(Pawn pawn2)
        {
            pawn2.X = 8;
            pawn2.Y = 11;
        }

        #endregion region Getter Setter and Constructor

        #region delacer pion
        public (bool,Sommet) deplacerPion(Game game, Graphe graphe)
        {
            List<Sommet> ListMoovePawn1Final1;
            List<Sommet> ListMoovePawn2Final1;
            List<Sommet> ListMoovePawn1Final2;
            List<Sommet> ListMoovePawn2Final2;
            Sommet sommetPion1 = graphe.GetSommet(game.Player2.Pawn1.X, game.Player2.Pawn1.Y);
            Sommet sommetPion2 = graphe.GetSommet(game.Player2.Pawn2.X, game.Player2.Pawn2.Y);
            Sommet sommetFinal1 = graphe.GetSommet(4, 3);
            Sommet sommetFinal2 = graphe.GetSommet(4, 7);

            // on récupère tous les coups possibles du pion 1
            ListMoovePawn1Final1 = graphe.Dijkstra(sommetPion1 , sommetFinal1);
            ListMoovePawn1Final2 = graphe.Dijkstra(sommetPion1, sommetFinal2);
            // on récupère tous les coups possibles du pion 2
            ListMoovePawn2Final1 = graphe.Dijkstra(sommetPion2, sommetFinal1);
            ListMoovePawn2Final2 = graphe.Dijkstra(sommetPion2, sommetFinal2);

            List<Sommet> smallestList = GetSmallestList(ListMoovePawn1Final1 , ListMoovePawn1Final2 , ListMoovePawn2Final1 , ListMoovePawn2Final2);

            if(smallestList == ListMoovePawn1Final1 || smallestList == ListMoovePawn1Final2)
            {
                return (true ,smallestList[0]); // pion haut
            }
            else 
            {
                return (false ,smallestList[0]);// pion bas

            }

        }

        public List<Sommet> GetSmallestList(List<Sommet> sommet1, List<Sommet> sommet2, List<Sommet> sommet3, List<Sommet> sommet4)
        {
            List<List<Sommet>> allLists = new List<List<Sommet>>()
            {
                sommet1,
                sommet2,
                sommet3,
                sommet4
            };
            List<Sommet> smallestList = allLists.OrderBy(list => list.Count).First();

            return smallestList;

        }

        #endregion deplacer pion

        #region poser mur
        public (bool, Sommet) poserMur(Game game)
        {
            List<(int, int)> listeMur;
            if (game.Player2.HorizontalWallLeft > 0)
            {
                listeMur = game.GetAvailableWall(true);
                (int, int) valeurAleatoire = GetRandomTuple(listeMur);
                game.placeWall(game.Player2, valeurAleatoire.Item1, valeurAleatoire.Item2, true);
                return (true, new Sommet(valeurAleatoire.Item1, valeurAleatoire.Item2));
            }
            else if (game.Player2.VerticalWallLeft > 0)
            {
                listeMur = game.GetAvailableWall(false);
                (int, int) valeurAleatoire = GetRandomTuple(listeMur);
                game.placeWall(game.Player2, valeurAleatoire.Item1, valeurAleatoire.Item2, false);
                return (false, new Sommet(valeurAleatoire.Item1, valeurAleatoire.Item2));
            }
            return (false, new Sommet(0,0));
        }        

        public (int, int) GetRandomTuple(List<(int, int)> liste)
        {
            // Vérifie si la liste n'est pas vide
            if (liste == null || liste.Count == 0)
            {
                throw new ArgumentException("La liste ne doit pas être vide.");
            }

            // Création d'une instance de Random
            Random random = new Random();

            // Génération d'un index aléatoire
            int indexAleatoire = random.Next(liste.Count);

            // Retourne l'élément à l'index aléatoire
            return liste[indexAleatoire];
        }

        #endregion poser mur
    }
}
