using System.Collections.Generic;

public class Partie
{
    public Partie()
    {
        // Constructeur
    }

    public List<Point> canMovePosition(Point currentPosition)
    {
        List<Point> mouvablePositions = new List<Point>();
        mouvablePositions.Add(new Point(currentPosition.X - 2, currentPosition.Y));
        mouvablePositions.Add(new Point(currentPosition.X - 1, currentPosition.Y + 1));
        mouvablePositions.Add(new Point(currentPosition.X, currentPosition.Y + 2));
        mouvablePositions.Add(new Point(currentPosition.X + 1, currentPosition.Y + 1));
        mouvablePositions.Add(new Point(currentPosition.X + 2, currentPosition.Y));
        mouvablePositions.Add(new Point(currentPosition.X + 1, currentPosition.Y - 1));
        mouvablePositions.Add(new Point(currentPosition.X, currentPosition.Y - 2));
        mouvablePositions.Add(new Point(currentPosition.X - 1, currentPosition.Y - 1));
        Point point1 = new Point(currentPosition.X, currentPosition.Y + 1);
        Point point2 = new Point(currentPosition.X + 1, currentPosition.Y);
        Point point3 = new Point(currentPosition.X, currentPosition.Y - 1);
        Point point4 = new Point(currentPosition.X - 1, currentPosition.Y);
        List<Point> listPointDestination = new List<Point>();
        listPointDestination.Add(point1);
        listPointDestination.Add(point2);
        listPointDestination.Add(point3);
        listPointDestination.Add(point4);

        List<Point> copy = new List<Point>(mouvablePositions);
        foreach (Point point in copy)
        {
            if (point.X > 13 || point.X < 0 || point.Y < 0 || point.Y > 10)
            {
                mouvablePositions.Remove(point);
            }
            //verifyMovablePosition(point); 
        }
        //if (currentPlayerID == PlayerID.Player1)
        //{
        //    foreach (var point in listPointDestination)
        //    {
        //        if ((point.X == 10 && point.Y == 3) || (point.X == 10 && point.Y == 7))
        //        {
        //            mouvablePositions.Add(point);
        //        }
        //    }
        //}
        //else
        //{
        //    foreach (var point in listPointDestination)
        //    {
        //        if ((point.X == 3 && point.Y == 3) || (point.X == 3 && point.Y == 7))
        //        {
        //            mouvablePositions.Add(point);
        //        }
        //    }
        //}
        return mouvablePositions;
    }

    public void updatePawnPosition(int xP, int yP, int x, int y)
    {
        // Met à jour la position du pion
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