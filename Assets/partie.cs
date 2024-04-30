public class Partie
{
    public Partie()
    {
        // Constructeur
    }

    public bool canMovePosition(int x, int y)
    {
        return true;
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