using System.Collections.Generic;

namespace Blockade
{
    public class IaLink
    {
        public Game game;


        public void NewGame(Player p1, Player p2)
        {
            if (game != null)
                game = new Game(p1, p2);
        }
    }
}

