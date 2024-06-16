using Blockade;

class IA : Player
{
    //Attributs of the class
    private Database db; 
    private Game game; 
    private Graphe graphe;

    //Override construct of the class Player
    public IA(PlayerType type, Game game, Database db, Graphe graphe) : base(type)
    {
        this.db = db;
        this.game = game;
        this.graphe = graphe;
        this.verticalWallLeft = 9;
            this.horizontalWallLeft = 9;

            if (type == PlayerType.X)
            {

                this.pawn1 = new Pawn(3, 3);
                this.pawn2 = new Pawn(3, 7);
            }
            else
            {

                this.pawn1 = new Pawn(10, 3);
                this.pawn2 = new Pawn(10, 7);
            }
    }

    public ((int, int), (int, int)) chooseMove(){
        List<int> listeMoves = this.game.getListMoves();

        //If all the wall played
        if(listeMoves.Count > 35){
            graphe.TrouverSommet();
        }
        else{
            int lastIdMove = listeMoves[listeMoves.Count - 1];
            List<(int, int, int)> listIdChildrens = this.db.getChildrens(lastIdMove);
            //    On regarde si le coup actuel a des enfants.
            //    Si non les créer avec la liste des coups possibles puis on les récupère
            if(listIdChildrens.Count == 0){
                List<(int,int)> listVerticalWall = this.game.getAvailableWallVertical();
                List<(int,int)> listHorizontalWall = this.game.getAvailableWallHorizontal();

                List<(int,int)> possibleMovePawn1 = this.game.getAvailableMove(this.pawn1);
                List<(int,int)> possibleMovePawn2 = this.game.getAvailableMove(this.pawn2);

                db.createChildrensMove(lastIdMove, possibleMovePawn1, listHorizontalWall, listVerticalWall, 0);
                db.createChildrensMove(lastIdMove, possibleMovePawn1, listHorizontalWall, listVerticalWall, 1);
                listIdChildrens = this.db.getChildrens(lastIdMove);
            }
            //    On effectue calcul pour choisir le coup, on fait le calcul pour le choix de l'exploitation et l'exploration
            int idMoveToPlay = -2;
            double resultCalculation = 0;
            int N = listIdChildrens.Count;
            double c = Math.Sqrt(2);
            foreach ((int, int, int) children in listIdChildrens){
                int w = children.Item3;
                int n = children.Item2;
                double childrenCalculation = (w/n) + c * Math.Sqrt( Math.Log(N)/n)  ;
                if(childrenCalculation > resultCalculation){
                    resultCalculation = childrenCalculation;
                    idMoveToPlay = children.Item1;
                }
            }
            return db.getMove(idMoveToPlay);
        }
    }
    
}

