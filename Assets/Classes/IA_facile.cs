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


// initialise la position de départ du pion 1
// il y a deux possibilité car l'IA peut être le joueur 1 ou le joueur 2
    public void setInitPawn1(Pawn pawn1)
    {
        if (joueur1)//test si c'est le joueur 1
        {
            pawn1.x = 4;
            pawn1.y = 4;
        }
        else// sinon c'est le joueur 2
        {
            pawn1.x = 4;
            pawn1.y = 11;
        }
    }
        

    public void setInitPawn2(Pawn pawn2)
    {
        if (joueur1)//test si c'est le joueur 1
        {
            pawn2.x = 8;
            pawn2.y = 4;
        }
        else// sinon c'est le joueur 2
        {
            pawn2.x = 8;
            pawn2.y = 11;
        }
        
    }
        
   
    private void jouerUnTour()
    {
        deplacerPion();
        poserMur();

    }

    private void deplacerPion()
    {
        MoveType[] ListMoove1;
        MoveType[] ListMoove2;
        // on récupère tous les coups possibles du pion 1
        ListMoove1 = getAvailableMoove(pawn1);
        // on récupère tous les coups possibles du pion 2
        ListMoove2 = getAvailableMoove(pawn2);

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
            sommetDepart = new Sommet(moove.x, moove.y);
            sommetArrivee = new Sommet(4 , 4);
            chemin1 = Dijkstra(sommetDepart, sommetArrivee);
            if maxchemin1 < chemin1
            {
                chemin1 = Dijkstra(sommetDepart, sommetArrivee);
            }
             

        }
       
        //le plus court chemin de pawn1 à base2 (8,4)

         foreach moove in ListMoove1
        {
            sommetDepart = new Sommet(moove.x, moove.y);
            sommetArrivee = new Sommet(8 , 4);
            chemin2 = Dijkstra(sommetDepart, sommetArrivee);
            if maxchemin2 < chemin2
            {
                chemin2 = Dijkstra(sommetDepart, sommetArrivee);
            }
             

        }
        //le plus court chemin de pawn2 à base1 (4,11)

         foreach moove in ListMoove2
        {
            sommetDepart = new Sommet(moove.x, moove.y);
            sommetArrivee = new Sommet(4 , 11);
            chemin3 = Dijkstra(sommetDepart, sommetArrivee);
            if maxchemin3 < chemin3
            {
                chemin3 = Dijkstra(sommetDepart, sommetArrivee);
            }
             

        }

        //le plus court chemin de pawn2 à base2 (8,11)
         foreach moove in ListMoove2
        {
            sommetDepart = new Sommet(moove.x, moove.y);
            sommetArrivee = new Sommet(4 , 11);
            chemin1 = Dijkstra(sommetDepart, sommetArrivee);
            if maxchemin4 < chemin4
            {
                chemin4 = Dijkstra(sommetDepart, sommetArrivee);
            }
             

        }

        // on a quatre chemins et parmis les 4 chemins on prends le plus court
        cheminFinal = chemin1;
        if (chemin2< cheminFinal)
        {
            cheminFinal = chemin2;
        }
        if (chemin3< cheminFinal)
        {
            cheminFinal = chemin3;
        }
        if (chemin4< cheminFinal)
        {
            cheminFinal = chemin4;
        }
        // cheminFinal est le plus court de tous

        // on deplace le pion sur la case qui à le cheminFinal
    }

    private void poserMur()
    {
        listeMur = getAvailableWall();
        choisirAleatoireMur(listeMur);
    }

    private void choisirAleatoireMur(listeMur)
    {
        //choisi de placer le mur a un endroit aléatoire parmis ceux de la liste des possibles
    }
}