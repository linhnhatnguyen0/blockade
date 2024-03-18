public class Player{
    protected Wall nbVerticalWallsRemaining;
    protected Wall nbHorizonalWallsRemaining;
    protected Pawn pawn1;
    protected Pawn pawn2;

    public Game()
    {

    }

    public void movePawn(Pawn pawn, int x , int y){
        
        pawn.x = x
        pawn.y = y
       
    }


    public void PlaceWall(int x, int y, string typePlacedWall)
    {
        Case case1 = new Case();
        Case case2 = new Case();
        Case case3 = new Case();
        Case case4 = new Case();

        case1.X = x;
        case1.Y = y;

        case2.X = x;
        case2.Y = y;

        case3.X = x;
        case3.Y = y;

        case4.X = x;
        case4.Y = y;

        if (typePlacedWall == "vertical")
        {
            case1.HighWall = true;

            case2.X = x;
            case2.Y = y + 1;
            case2.HighWall = true;

            case3.X = x - 1;
            case3.Y = y;
            case3.LowWall = true;

            case4.X = x - 1;
            case4.Y = y + 1;
            case4.LowWall = true;
        }
        else if (typePlacedWall == "horizontal")
        {
            case1.RightWall = true;

            case2.X = x + 1;
            case2.Y = y;
            case2.RightWall = true;

            case3.X = x;
            case3.Y = y + 1;
            case3.LeftWall = true;

            case4.X = x + 1;
            case4.Y = y + 1;
            case4.LeftWall = true;
        }
    }
}