using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockade
{
    public class Graphe
    {
        private List<Sommet> sommets;
        private List<Arete> aretes;

        public List<Sommet> Sommets
        {
            get { return sommets; }
            set { sommets = value; }
        }
        public List<Arete> Aretes
        {
            get { return aretes; }
            set { aretes = value; }
        }

        // Constructeur
        public Graphe()
        {

            sommets = new List<Sommet>();
            aretes = new List<Arete>(); // Initialisez la liste des arêtes ici

            // Initialisation des sommets
            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    sommets.Add(new Sommet(x, y));
                }
            }

            // Ajout des arêtes entre les sommets selon les règles spécifiées
            AjouterAretes();
        }

        #region AjouterArete
        private void AjouterAretes()
        {
            foreach (Sommet sommet in sommets)
            {
                int x = sommet.X;
                int y = sommet.Y;

                AjouterArete(sommet, x, y + 2);   // Case (x, y+2)
                AjouterArete(sommet, x, y - 2);   // Case (x, y-2)
                AjouterArete(sommet, x - 2, y);   // Case (x-2, y)
                AjouterArete(sommet, x + 2, y);   // Case (x+2, y)
                AjouterArete(sommet, x - 1, y - 1); // Case (x-1, y-1)
                AjouterArete(sommet, x + 1, y + 1); // Case (x+1, y+1)
                AjouterArete(sommet, x + 1, y - 1); // Case (x+1, y-1)
                AjouterArete(sommet, x - 1, y + 1); // Case (x-1, y+1)
            }
        }

        private void AjouterArete(Sommet sommet, int voisinX, int voisinY)
        {
            // Vérifier si les coordonnées du voisin sont valides dans le plateau
            if (voisinX >= 0 && voisinX < 11 && voisinY >= 0 && voisinY < 14)
            {
                Sommet voisin = TrouverSommet(voisinX, voisinY);
                if (voisin != null)
                {
                    // Créer une arête entre le sommet actuel et le voisin
                    Arete arete = new Arete(sommet, voisin);
                    aretes.Add(arete); // Ajouter l'arête à la liste des arêtes
                }
            }
        }

        private Sommet TrouverSommet(int x, int y)
        {
            return sommets.FirstOrDefault(s => s.X == x && s.Y == y);
        }

        #endregion AjouterArete

        public List<Sommet> Dijkstra(Sommet depart, Sommet arrivee)
        {
            // Initialiser les distances des sommets à l'infini sauf le sommet de départ
            foreach (Sommet sommet in sommets)
            {
                sommet.Distance = (sommet == depart) ? 0 : int.MaxValue;
            }

            // Ensemble pour stocker les sommets non visités
            HashSet<Sommet> nonVisites = new HashSet<Sommet>(sommets);

            while (nonVisites.Count > 0)
            {
                // Extraire le sommet non visité avec la plus petite distance actuelle
                Sommet sommetActuel = nonVisites.OrderBy(s => s.Distance).FirstOrDefault();
                if (sommetActuel == null || sommetActuel.Distance == int.MaxValue)
                    break; // Aucun chemin trouvé jusqu'ici

                nonVisites.Remove(sommetActuel);

                // Parcourir les arêtes sortantes du sommet actuel pour mettre à jour les distances
                foreach (Arete arete in aretes.Where(a => a.Depart == sommetActuel))
                {
                    Sommet voisin = arete.Fin;
                    int distanceViaSommetActuel = sommetActuel.Distance + 1; // Poids de l'arête = 1

                    // Mettre à jour la distance du voisin si une meilleure distance est trouvée
                    if (distanceViaSommetActuel < voisin.Distance)
                    {
                        voisin.Distance = distanceViaSommetActuel;
                    }
                }
            }

            // Reconstructeur le chemin à partir du sommet d'arrivée vers le sommet de départ
            List<Sommet> chemin = new List<Sommet>();
            Sommet etape = arrivee;
            while (etape != null)
            {
                chemin.Add(etape);

                // Trouver le prochain sommet avec la distance précédente
                Sommet prochain = null;
                foreach (Sommet voisin in sommets)
                {
                    if (aretes.Any(a => a.Depart == voisin && a.Fin == etape) && voisin.Distance == etape.Distance - 1)
                    {
                        prochain = voisin;
                        break;
                    }
                }

                etape = prochain; // Passer au prochain sommet
            }

            chemin.Reverse(); // Inverser la liste pour avoir le chemin du départ à l'arrivée
            return chemin;
        }

        #region FonctionUtil
        public void SupprimerArete(Sommet sommet1, Sommet sommet2)
        {
            // Recherche de l'arête à supprimer
            Arete areteASupprimer = null;
            foreach (Arete arete in aretes)
            {
                if ((arete.Depart == sommet1 && arete.Fin == sommet2) || (arete.Depart == sommet2 && arete.Fin == sommet1))
                {
                    areteASupprimer = arete;
                    break;
                }
            }

            // Suppression de l'arête de la liste des arêtes
            if (areteASupprimer != null)
            {
                aretes.Remove(areteASupprimer);
            }
        }


        public bool ExisteChemin(Sommet depart, Sommet arrivee)
        {
            // Utiliser une recherche en profondeur (DFS) pour vérifier la connectivité entre le départ et l'arrivée
            HashSet<Sommet> visite = new HashSet<Sommet>(); // Ensemble des sommets visités

            return DFS(depart, arrivee, visite);
        }

        private bool DFS(Sommet sommetActuel, Sommet arrivee, HashSet<Sommet> visite)
        {
            visite.Add(sommetActuel); // Marquer le sommet actuel comme visité

            // Si le sommet actuel est l'arrivée, un chemin a été trouvé
            if (sommetActuel == arrivee)
            {
                return true;
            }

            // Parcourir les arêtes pour trouver les voisins (autres sommets) connectés
            foreach (Arete arete in aretes)
            {
                Sommet voisin = null;

                // Déterminer le voisin connecté à partir de l'arête
                if (arete.Depart == sommetActuel)
                {
                    voisin = arete.Fin;
                }
                else if (arete.Fin == sommetActuel)
                {
                    voisin = arete.Depart;
                }

                // Si un voisin est trouvé et n'a pas été visité, explorer à partir de ce voisin
                if (voisin != null && !visite.Contains(voisin))
                {
                    if (DFS(voisin, arrivee, visite))
                    {
                        return true; // Chemin trouvé à travers ce voisin
                    }
                }
            }

            // Aucun chemin trouvé à partir du sommet actuel
            return false;
        }

        public Sommet GetSommet(int x, int y)
        {
            Sommet Result = null;
            for (int i = 0;i < sommets.Count; i++)
            {
                if (sommets[i].X == x && sommets[i].Y == y) { Result = sommets[i]; break; }
            }
            return Result;
        }

        #endregion FonctionUtil
    }

}
