using Blockade
public class Player{
    protected Wall nbVerticalWallsRemaining;
    protected Wall nbHorizonalWallsRemaining;
    protected Pawn pawn1;
    protected Pawn pawn2;

    public Player()
    {
        
    }

    public void movePawn(Pawn pawn, int y , int x){
        pawn.y = y
        pawn.x = y
    }
    
    public void PlaceWall(int x, int y, bool vertical, bool horizontal)
    {
        board = new Board();
        

        if (vertical) //le mur se place à gauche de la case à la position (x,y)
        {
            // case (x,y) du plateau
            board[x][y];
            //le mur à la position donnée est placé à gauche(première position)
            board.setLeftWall = true;

            // la deuxieme partie du mur est placée en dessous de la première
            // position de la case x+1,y
            board[x+1][y];
            case2.setLeftWall = true;

            // les cases en y-1 sont affectée sur leur mur droit

            board[x][y-1]
            board.setRightWall = true;

            board[x+1][y-1];
            board.setRightWall = true;
        }
        else if (horizontal)// le  mur se place en haut de la case à la position (x,y)
        {
            // case (x,y) du plateau
            board[x][y];
            //le mur à la position donnée est placé en haut(première position)
            board.setTopWall = true;

            // la deuxieme partie du mur est placée après de la première sur la droite(case d'après sur la droite en haut)
            // position de la case x,y+1
            board[x][y+1];
            case2.setTopWall = true;

            // les cases en x-1 sont affectée sur leur mur bottom

            board[x-1][y]
            board.setBottomWall = true;

            board[x-1][y+1];
            board.setBottomWall = true;
        }
    }
}