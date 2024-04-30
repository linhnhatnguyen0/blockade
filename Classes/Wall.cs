using Blockade
public enum WallType
{
    vertical,
    horizontal
}

public class Wall
{
    private wallType type;

    public Wall(wallType type)
    {
        this.type = type;
    }

    public wallType Type
    {
        get { return type; }
        set { type = value; }
    }
}