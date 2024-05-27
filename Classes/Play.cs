using System;

namespace Blockade
{
    public class Play
    {
        private Game game;
        private bool isGameOver;

        public Play()
        {
            game = new Game();
            isGameOver = false;
        }

        public void StartGame()
        {
            while (!isGameOver)
            {
                ExecuteTurn(game.Player1);
                if (isGameOver) break;
                
                ExecuteTurn(game.Player2);
            }

            Console.WriteLine("Fin du jeu !");
        }

        private void ExecuteTurn(Player player)
        {
            Console.WriteLine($"C'est le tour de {player.PlayerType}.");

            MovePawn(player);
            PlaceWall(player);
            ValidateTurn(player);

            isGameOver = CheckIfGameOver(player);
        }

        private void MovePawn(Player player)
        {
            // Exemple de logique pour déplacer un pion. Remplacer par une gestion d'entrée réelle.
            Pawn pawnToMove = player.Pawn1;
            List<(int, int)> availableMoves = game.getAvailableMove(pawnToMove);
            
            if (availableMoves.Count > 0)
            {
                (int newX, int newY) = availableMoves[0]; // Exemple de déplacement
                pawnToMove.X = newX;
                pawnToMove.Y = newY;
                Console.WriteLine($"{player.PlayerType} a déplacé un pion à ({newX}, {newY})");
            }
        }

        private void PlaceWall(Player player)
        {
            // Exemple de logique pour placer un mur. Remplacer par une gestion d'entrée réelle.
            int x = 1; // Coordonnées d'exemple
            int y = 1; // Coordonnées d'exemple
            bool isHorizontal = true; // Orientation de mur d'exemple

            if (game.canPlaceWall(x, y, isHorizontal))
            {
                game.placeWall(player, x, y, isHorizontal);
                Console.WriteLine($"{player.PlayerType} a placé un mur à ({x}, {y}) - Horizontal: {isHorizontal}");
            }
        }

        private void ValidateTurn(Player player)
        {
            Console.WriteLine($"{player.PlayerType} a terminé son tour.");
        }

        private bool CheckIfGameOver(Player player)
        {
            // Implémentez votre fonction `gagné` ici.
            return false;
        }
    }
}
