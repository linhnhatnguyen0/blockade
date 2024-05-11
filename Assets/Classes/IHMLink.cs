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
            this.game = new Game();
        }

        public List<Point> canMovePosition(Point currentPosition)
        {
            List<(int,int)> possiblemove = game.getAvailableMove(game.getPawnByCase(currentPosition.X, currentPosition.Y));
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
                Debug.Log(game.Player1);
                game.placeWall(game.Player1, x, y, isHorizontal);
            }
            else
            {
                game.placeWall(game.Player2, x, y, isHorizontal);
            }

        }
    }
}

