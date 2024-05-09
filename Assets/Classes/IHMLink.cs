using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Blockade
{

    public class IHMLink
    {

        public Game game;

        public IHMLink()
        {
            Player p1 = new Player(Player.PlayerType.X);
            Player p2 = new Player(Player.PlayerType.O);
            this.game = new Game(p1, p2);
        }

        public List<Point> canMovePosition(Point currentPosition)
        {
            (int,int)[] possiblemove = game.getAvailableMove(game.getPawnByCase(currentPosition.X, currentPosition.Y));
            return possiblemove.Select(t => new Point(t.Item1, t.Item2)).ToList();
        }

        public void updatePawnPosition(int xP, int yP, int x, int y)
        {
            game.getPawnByCase(xP, yP).move(x, y);
        }

        public bool canPlaceWall(int x, int y, bool isHorizontal)
        {
            return game.canPlaceWall(x, y, isHorizontal);
        }

        public void placeWall(int x, int y, bool isHorizontal)
        {
            if (PlayerPrefs.GetInt("currentPlayer") == 1)
            {
                game.placeWall(game.Player1, x, y, isHorizontal);
            }
            else
            {
                game.placeWall(game.Player2, x, y, isHorizontal);
            }

        }
    }
}

