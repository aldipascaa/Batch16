namespace DominoGame;
class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();

        //Add players
        game.AddPlayer(new HumanPlayer("Player 1"));
        game.AddPlayer(new ComputerPlayer("Computer"));

        game.StartGame();

        Console.WriteLine("Nice Game!!!");
        Console.ReadKey();
    }
}