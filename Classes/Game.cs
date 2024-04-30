namespace Blockade
public class Game{
    private Board board;
    private Player player1;
    private Player player2;

    public Game(Player p1, Player p2)
    {
        self.board = Board();
        self.player1 = p1;
        self.player2 = p2;
    }

    public boolean chooseMoove(int index, Pawn: pawn){
        MoveType[] ListMoove = getAvailableMoove(pawn);
        if (index > ListMoove.Length-1){Return False;}
        pawn.moove(ListMoove[index]);
        Return True;
    }

   public boolean ChooseWall(int index, Player p, boolean vertical){
        (int,int)[] ListWall = getAvailableWall(p,vertical);
        if (index > ListWall.Length-1){Return False;}
        p.placewall(ListWall[index][0],ListWall[index][1],vertical);
    }
        
    public moveType[] getAvailableMove(Pawn pawn){
        int x = pawn.x
        int y = pawn.y
        bool topRight = false
        bool topLeft = false
        bool bottomLeft = false
        bool bottomRight = false
        MoveType[] = movesPossible
        //top
        if(!board.board[x][y].hasTopWall || y+1 >= 0){
            if(!board.board[x][y+1].hastopWall){
                movesPossible.append((x,y+2))
            }
            //verif 1/2 pour top right
            if(!board.board[x][y+1].hasRightWall){
                topRight = true
            }
            //verif 1/2 pour top left
            if(!board.board[x][y+1].hasLeftWall){
                topLeft = true
            }
        }
        //bottom
        if(!board.board[x][y].hasBottomWall || y-1 <= 14){

            if(!board.board[x][y-1].hasBottomWall){
                movesPossible.append((x,y-2))
            }
            //verif 1/2 pour bottom right
            if(!board.board[x][y-1].hasRightWall){
                bottomRight = true
            }
            //verif 1/2 pour bottom left
            if(!board.board[x][y-1].hasRightWall){
                bottomLeft = true
            }
        }
        //left
        if(!board.board[x][y].hasLeftWall || x-1 >= 0){

            if(!board.board[x-1][y].hasLeftWall){
                movesPossible.append((x-2,y))
            }
            //verif 2/2 pour bottom left
            if(!board.board[x-1][y].hasBottomWall){
                bottomLeft = true
            }
            //verif 2/2 pour top left
            if(!board.board[x-1][y].hasTopWall){
                topLeft = true
            }
        }
        //right
        if(!board.board[x][y].hasRightWall || x+1 <= 11){

            if(!board.board[x][y].hasRightWall){
                movesPossible.append((x+2,y))
            }
            //verif 2/2 pour top right
            if(!board.board[x+1][y].hasTopWall){
                topRight = true
            }
            //verif 2/2 pour bottom right
            if(!board.board[x+1][y].hasBottomWall){
                    bottomRight = true
            }
        }
        if(bottomLeft) movesPossible.append((x-1,y-1))
        if(bottomRight) movesPossible.append(MoveType.(x+1,y-1))
        if(topRight) movesPossible.append((x+1,y+1))
        if(topLeft) movesPossible.append((x-1,y+1))
    }

    private boolean caBloque(int, x, int y, boolean vertical) {

        Board temp = board;

        if (vertical) {

            temp[x][y].setRightWall(true);
            temp[x + 1][y].setLeftWall(true);
            temp[x][y + 1].setRightWall(true);
            temp[x + 1][y + 1].setLeftWall(true);
        }
        else {

            temp[x][y].setBottomWall(true);
            temp[x][y + 1].setHighWall(true);
            temp[x + 1][y].setBottomWall(true);
            temp[x + 1][y + 1].setHighWall(true);
        }

        // parcourir le tableau a partir de la position des joueurs pour vérifier qu'ils peuvent atteindre la case adverse
            

        return false;
    }

    public boolean getAvailableWall(int x, int y, boolean vertical) {

        if (x < 0 or x > board.board.Length) return false;

        if (y < 0 or y > board.board.Length) return false;

        if (caBloque(x, y, vertical)) return false;

        return true;
    }
        
    public void canMove(int y, int x){
            
    }
}