using Blockade;

public enum MoveType{
    top,
    right,
    left,
    bottom,
    topRight,
    topLeft,
    bottomLeft,
    bottomRight
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

    public void move(MoveType type) 
    {
        switch (type)
        {
            case MoveType.top:
                y += 2;
                break;

            case MoveType.topRight:
                x += 1;
                y += 1;
                break;

            case MoveType.right:
                x += 2;
                break;

            case MoveType.BottomRight:
                x += 1;
                y -= 1;
                break;

            case MoveType.bottom:
                y -= 2;
                break;

            case MoveType.bottomLeft:
                x -= 1;
                y -= 1;
                break;

            case MoveType.left:
                x -= 2;
                break;

            case MoveType.topLeft:
                x -= 1;
                y += 1;
                break;
        }
    }
}
