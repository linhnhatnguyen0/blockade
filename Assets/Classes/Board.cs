using Blockade;

public class Board
{

    private Case[,] board;

    public Case[,] gsBoard { get; set; }

    public Board()
    {

        board = new Case[14, 11];

        board[4, 4].StartingCase = true;
        board[8, 4].StartingCase = true;
        board[4, 11].StartingCase = true;
        board[8, 11].StartingCase = true;
    }
}