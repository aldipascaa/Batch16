namespace DominoGame;
class Program
{
    static void Main(string[] args)
    {
        //Testing();

        Game game = new Game();

        //Add players
        game.AddPlayer(new HumanPlayer("Player 1"));
        game.AddPlayer(new ComputerPlayer("Computer"));

        game.StartGame();

        Console.WriteLine("Nice Game!!!");
        Console.ReadKey();
    }

    static void Testing()
    {
        
        DominoPiece piece1 = new DominoPiece(2, 5);
        DominoPiece piece2 = new DominoPiece(5, 3);

        Console.WriteLine(piece1.RightValue);
        Console.WriteLine(piece2);
        DominoSet set = new DominoSet();
        Console.WriteLine(set);
        IPlayer player = new HumanPlayer("Aldi");
        for (int i = 0; i < 7; i++)
        {
            var piece = set.DrawPiece();
            if (piece != null)
            {
                player.AddPiece(piece);
            }
        }
        Console.WriteLine(player);
        Console.WriteLine(set);

        Board board = new Board();
        board.PlacePiece(piece1);
        board.PlacePiece(piece2);
        Console.WriteLine(board);

    }

}