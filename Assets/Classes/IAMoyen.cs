using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockade {
    public class IAMoyen
    {


        /* fonction : poser un mur intelligemment */
        public Sommet MinMaxWall(Player player1, Player player2, Graphe graphe)
        {
            // sommets des pions du player 1
            Sommet sommetPion1 = new Sommet(player1.pawn1.X, player1.pawn1.Y);
            Sommet sommetPion2 = new Sommet(player1.pawn2.X, player1.pawn2.Y);

            // sommets de départ du player 2
            Sommet sommetDepartX = new Sommet(player2.pawn1.X, player2.pawn1.Y);
            Sommet sommetDepartY = new Sommet(player2.pawn2.X, player2.pawn2.Y);

            // listes nécessaires
            List<int> listCount = new List<int>();
            List<Sommet> list1 = new List<Sommet>();
            List<Sommet> list2 = new List<Sommet>();
            List<Sommet> list3 = new List<Sommet>();
            List<Sommet> list4 = new List<Sommet>();

            // vérifier d'avoir au moins un mur
            if (player2.getAvailableWall())
            {
                if (graphe.ExisteChemin(sommetPion1, sommetDepartX))
            {
                list1 = graphe.Dijkstra(sommetPion1, sommetDepartX);
                listCount.Add(list1.Count);
            }

            if (graphe.ExisteChemin(sommetPion1, sommetDepartY))
            {
                list2 = graphe.Dijkstra(sommetPion1, sommetDepartY);
                listCount.Add(list2.Count);
            }

            if (graphe.ExisteChemin(sommetPion2, sommetDepartX))
            {
                list3 = graphe.Dijkstra(sommetPion2, sommetDepartX);
                listCount.Add(list3.Count);
            }

            if (graphe.ExisteChemin(sommetPion2, sommetDepartY))
            {
                list4 = graphe.Dijkstra(sommetPion2, sommetDepartY);
                listCount.Add(list4.Count);
            }

            // trier la liste
            listCount.Sort();

            // choisir le plus petit chemin
            Sommet SommetWall = null;
            if (listCount[0] == list1.Count)
            {
                SommetWall = list1[0];
            }
            else if (listCount[0] == list2.Count)
            {
                SommetWall = list2[0];
            }
            else if (listCount[0] == list3.Count)
            {
                SommetWall = list3[0];
            }
            else if (listCount[0] == list4.Count)
            {
                SommetWall = list4[0];
            }
            }
        

            return SommetWall;
        }

        /* fonction : déplacer un pion intelligement */
        public Sommet BestMove(Player player1, Player player2, Graphe graphe)
        {
            // sommets des pions du player 1
            Pawn pion1 = player1.pawn1;
            Sommet sommetPion1 = new Sommet(player1.pawn1.X, player1.pawn1.Y);

            Pawn pion2 = player1.pawn2;
            Sommet sommetPion2 = new Sommet(player1.pawn2.X, player1.pawn2.Y);

            // sommets de départ du player 2
            Sommet sommetDepartX = new Sommet(player2.pawn1.X, player2.pawn1.Y);
            Sommet sommetDepartY = new Sommet(player2.pawn2.X, player2.pawn2.Y);

            List<int> listCount = new List<int>();
            List<Sommet> list1 = new List<Sommet>();
            List<Sommet> list2 = new List<Sommet>();
            List<Sommet> list3 = new List<Sommet>();
            List<Sommet> list4 = new List<Sommet>();

            if (graphe.ExisteChemin(sommetPion1, sommetDepartX))
            {
                list1 = graphe.Dijkstra(sommetPion1, sommetDepartX);
                listCount.Add(list1.Count);
            }

            if (graphe.ExisteChemin(sommetPion1, sommetDepartY))
            {
                list2 = graphe.Dijkstra(sommetPion1, sommetDepartY);
                listCount.Add(list2.Count);
            }

            if (graphe.ExisteChemin(sommetPion2, sommetDepartX))
            {
                list3 = graphe.Dijkstra(sommetPion2, sommetDepartX);
                listCount.Add(list3.Count);
            }

            if (graphe.ExisteChemin(sommetPion2, sommetDepartY))
            {
                list4 = graphe.Dijkstra(sommetPion2, sommetDepartY);
                listCount.Add(list4.Count);
            }

            // trier la liste
            listCount.Sort();

            // choisir le plus petit chemin
            Sommet SommetMove = null;
            if (listCount[0] == list1.Count)
            {
                SommetMove = list1[0];
            }
            else if (listCount[0] == list2.Count)
            {
                SommetMove = list2[0];
            }
            else if (listCount[0] == list3.Count)
            {
                SommetMove = list3[0];
            }
            else if (listCount[0] == list4.Count)
            {
                SommetMove = list4[0];
            }

            return SommetMove;
        }
    }
}