using Blockade;

public enum MoveType{
    top,
    right,
    left,
    bottom,
    topRight,
    topLeft,
    bottomLeft,
    bottomRight,
    closeTop,
    closeRight,
    closeLeft,
    closeBottom,

}

public class Pawn {

    private int x;
    private int y;

    public Pion(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int x 
    {
        get { return x; }
        set { x = value; }
    }

    public int y
    {
        get { return y; }
        set { y = value; }
    }

    public void move(int x, int y){
        this.x = x;
        this.y = y;
    }
}
