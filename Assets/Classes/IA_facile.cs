public class IA_facile
{
    private Pawn pawn1;
    private Pawn pawn2;

    public IA_facile(Pawn pawn1, Pawn pawn2) {
        this.pawn1 = pawn1;
        this.pawn2 = pawn2;
    }

    // Getter pour pawn1
    public Pawn getPawn1() {
        return pawn1;
    }

    // Setter pour pawn1
    public void setPawn1(Pawn pawn1) {
        this.pawn1 = pawn1;
    }

    // Getter pour pawn2
    public Pawn getPawn2() {
        return pawn2;
    }

    // Setter pour pawn2
    public void setPawn2(Pawn pawn2) {
        this.pawn2 = pawn2;
    }

// apparement IA est toujours joueur 2
// initialise la position de départ du pion 1
// il y a deux possibilité car l'IA peut être le joueur 1 ou le joueur 2
    // public void setInitPawn1(Pawn pawn1)
    // {
    //     if (joueur1)//test si c'est le joueur 1
    //     {
    //         pawn1.x = 4;
    //         pawn1.y = 4;
    //     }
    //     else// sinon c'est le joueur 2
    //     {
    //         pawn1.x = 4;
    //         pawn1.y = 11;
    //     }
    // }
        
    public void setInitPawn1(Pawn pawn1)
    {
            pawn1.X = 4;
            pawn1.Y = 11;
       
      
    }
    public void setInitPawn2(Pawn pawn2)
    {
            pawn2.x = 8;
            pawn2.y = 11;
    }
        
   
    private void jouerUnTour()
    {
        deplacerPion();
        poserMur();

    }

    private void deplacerPion(Game game, Graphe graphe)
    {
        List<(int,int)> ListMoove1;
        List<(int,int)> ListMoove2;
        // on récupère tous les coups possibles du pion 1
        ListMoove1 = game.getAvailableMove(pawn1);
        // on récupère tous les coups possibles du pion 2
        ListMoove2 = game.getAvailableMove(pawn2);

        chemin1 =0;
        maxchemin1 = 0;
        chemin2 =0;
        maxchemin2 = 0;
        chemin3 =0;
        maxchemin3 = 0;
        chemin4 = 0;
        maxchemin4 = 0;
        // on parcours chacun des moove pour le pawn1
        //le plus court chemin de pawn1 à base1 (4,4)
       

        foreach moove in ListMoove1
        {
            sommetDepart = new Sommet(moove[0], moove[1]);
            sommetArrivee = new Sommet(4 , 4);
            chemin1 = graphe.dijkstra(sommetDepart, sommetArrivee);
            if (maxchemin1 < chemin1)
            {
                chemin1 = graphe.dijkstra(sommetDepart, sommetArrivee);
                sommetfinal1 = sommetDepart;
            }
             

        }
       
        //le plus court chemin de pawn1 à base2 (8,4)

         foreach moove in ListMoove1
        {
            sommetDepart = new Sommet(moove.[0], moove.[1]);
            sommetArrivee = new Sommet(8 , 4);
            chemin2 = graphe.dijkstra(sommetDepart, sommetArrivee);
            if (maxchemin2 < chemin2)
            {
                chemin2 = graphe.dijkstra(sommetDepart, sommetArrivee);
                sommetfinal2 = sommetDepart;
            }
             

        }
        //le plus court chemin de pawn2 à base1 (4,4)

         foreach moove in ListMoove2
        {
            sommetDepart = new Sommet(moove.[0], moove.[1]);
            sommetArrivee = new Sommet(4 , 4);
            chemin3 = graphe.dijkstra(sommetDepart, sommetArrivee);
            if (maxchemin3 < chemin3)
            {
                chemin3 = graphe.dijkstra(sommetDepart, sommetArrivee);
                sommetfinal3 = sommetDepart;
            }
             

        }

        //le plus court chemin de pawn2 à base2 (8,4)
         foreach moove in ListMoove2
        {
            sommetDepart = new Sommet(moove.[0], moove.[1]);
            sommetArrivee = new Sommet(8 , 4);
            chemin1 = graphe.dijkstra(sommetDepart, sommetArrivee);
            
            if (maxchemin4 < chemin4)
            {
                chemin4 = graphe.dijkstra(sommetDepart, sommetArrivee);
                sommetfinal4 = sommetDepart;
            }
             

        }

        // on a quatre chemins et parmis les 4 chemins on prends le plus court
        cheminFinal = chemin1;
        pion1 = true;
        pion2 = false;
        sommetfinal = sommetfinal1;
        if (chemin2< cheminFinal)
        {
            cheminFinal = chemin2;
            pion1 = true;
            pion2 = false;
            sommetfinal = sommetfinal2;
        }
        if (chemin3< cheminFinal)
        {
            cheminFinal = chemin3;
            pion1 = false;
            pion2 = true;
            sommetfinal = sommetfinal3;
        }
        if (chemin4< cheminFinal)
        {
            cheminFinal = chemin4;
            pion1 = false;
            pion2 = true;
            sommetfinal = sommetfinal4;
        }
        // cheminFinal est le plus court de tous

        // on deplace le pion sur la case qui à le cheminFinal
        if(pion1)
        {
            pawn1.X = sommetfinal.X;
            pawn1.Y = sommetfinal.Y;
        }
        else
        {
            pawn2.X = sommetfinal.X;
            pawn2.Y = sommetfinal.Y;
        }
    }

    private void poserMur(Game game)
    {
        listeMur = game.getAvailableWall(); //je n'ai pas trouvé comment se nomme la fonction qui recupère tous les emplacements de murs possibles
        choisirAleatoireMur(listeMur);
    }

    private void choisirAleatoireMur(listeMur)
    {
        //choisi de placer le mur a un endroit aléatoire parmis ceux de la liste des possibles
    }
}