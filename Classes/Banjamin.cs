using System.Collections.Generic;
using Game;
using Player;

public class Benjamin
{

    public Game game;

    public Benjamin()
    {
        Player p1 = new Player();
        Player p2 = new Player();
        this.game = new Game(p1,p2);
    }

    public List<Point> canMovePosition(Point currentPosition)
    {
        return game.getAvailableMove(getPawnByCase(Point.x,Point.y));
    }

    public void updatePawnPosition(int xP, int yP, int x, int y)
    {
        game.movePawn(getPawnByCase(xP,yP),x,y);
    }

    public bool canPlaceWall(int x, int y, bool isHorizontal)
    {
        return false;
    }

    public void placeWall(int x, int y, bool isHorizontal)
    {
        // Place un mur
    }
}