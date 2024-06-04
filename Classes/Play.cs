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
            // Demande à l'utilisateur quel pion il veut déplacer
            Console.WriteLine("Choisissez le pion à déplacer (1 ou 2): ");
            string pawnChoice = Console.ReadLine();
            Pawn pawnToMove = (pawnChoice == "1") ? player.Pawn1 : player.Pawn2;

            // Affiche les mouvements possibles
            List<(int, int)> availableMoves = game.getAvailableMove(pawnToMove);
            if (availableMoves.Count == 0)
            {
                Console.WriteLine("Aucun mouvement disponible pour ce pion.");
                return;
            }

            Console.WriteLine("Mouvements disponibles: ");
            for (int i = 0; i < availableMoves.Count; i++)
            {
                Console.WriteLine($"{i}: ({availableMoves[i].Item1}, {availableMoves[i].Item2})");
            }

            // Demande à l'utilisateur de choisir un mouvement
            Console.WriteLine("Choisissez un mouvement: ");
            string moveChoice = Console.ReadLine();
            int moveIndex = int.Parse(moveChoice);
            (int newX, int newY) = availableMoves[moveIndex];

            // Déplace le pion
            pawnToMove.X = newX;
            pawnToMove.Y = newY;
            Console.WriteLine($"{player.PlayerType} a déplacé un pion à ({newX}, {newY})");
        }

        private void PlaceWall(Player player)
        {
            // Demande à l'utilisateur où placer le mur
            Console.WriteLine("Entrez les coordonnées pour placer un mur:");
            Console.WriteLine("X: ");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Y: ");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine("Le mur est-il horizontal? (oui/non): ");
            bool isHorizontal = Console.ReadLine().ToLower() == "oui";

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
            if (game.Board.gsBoard[x, y].StartingCase)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
