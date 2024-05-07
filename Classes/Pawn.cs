public class Pawn {

    private int x;
    private int y;

    public Pawn(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public void move(int _x, int _y){
        x = _x;
        y = _y;
    }

    public bool victoire(int _x, int _y){
        if(_x == 4 && _y == 11 || _x == 8 && _y == 11){
            return true;
        }
        else return false;
    }
}