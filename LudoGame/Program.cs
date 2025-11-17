using System;
using System.Collections.Generic;

enum PawnState { Home, Active, Complete }
enum GameState { NotStarted, Running, Finished }

class Dice
{
    private Random random = new Random();
    public int Roll() => random.Next(1, 7);
}

class Pawn
{
    public string Owner { get; private set; }
    public int Position { get; set; } = 0;
    public int Id { get; private set; }
    public PawnState State { get; set; } = PawnState.Home;

    public Pawn(string owner, int id)
    {
        Owner = owner;
        Id = id;
    }

    public string Symbol => $"{Owner[0]}{Id}";
    public bool IsComplete => State == PawnState.Complete;
}

class Player
{
    public string Name { get; private set; }
    public List<Pawn> Pawns { get; private set; }

    public Player(string name, int pawnCount = 2)
    {
        Name = name;
        Pawns = new List<Pawn>();
        for (int i = 0; i < pawnCount; i++)
        {
            Pawns.Add(new Pawn(name, i + 1));
        }
    }

    public bool AllPawnsComplete()
    {
        foreach (var pawn in Pawns)
            if (!pawn.IsComplete) return false;
        return true;
    }
}

class Board
{
    public const int TrackLength = 20;

    public void Display(List<Player> players)
    {
        string[] track = new string[TrackLength];
        for (int i = 0; i < TrackLength; i++)
            track[i] = "__"; // empty square

        foreach (var player in players)
        {
            foreach (var pawn in player.Pawns)
            {
                if (pawn.State == PawnState.Active)
                {
                    int pos = Math.Min(pawn.Position - 1, TrackLength - 1);
                    track[pos] = pawn.Symbol;
                }
            }
        }

        Console.WriteLine("Board:");
        for (int i = 0; i < TrackLength; i++)
        {
            Console.Write(track[i] + " ");
        }
        Console.WriteLine("\n");
    }
}

class Game
{
    public List<Player> Players { get; private set; }
    private Dice dice = new Dice();
    private int currentPlayerIndex = 0;
    public GameState State { get; private set; } = GameState.NotStarted;
    private Board board = new Board();

    public Game(List<Player> players)
    {
        Players = players;
    }

    public void Start()
    {
        State = GameState.Running;
        Console.WriteLine("Game Started!");
        board.Display(Players);

        while (State == GameState.Running)
        {
            PlayTurn();
        }
    }

    private void PlayTurn()
    {
        Player current = Players[currentPlayerIndex];
        Console.WriteLine($"\n{current.Name}'s turn. Press Enter to roll dice...");
        Console.ReadLine();

        int roll = dice.Roll();
        Console.WriteLine($"{current.Name} rolled a {roll}!");

        Pawn pawnToMove = ChoosePawn(current, roll);
        if (pawnToMove != null)
        {
            MovePawn(pawnToMove, roll);
            board.Display(Players);
        }
        else
        {
            Console.WriteLine("No pawn can move this turn.");
        }

        if (current.AllPawnsComplete())
        {
            Console.WriteLine($"{current.Name} has moved all pawns to the goal! They win!");
            State = GameState.Finished;
            return;
        }

        currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
    }

    private Pawn ChoosePawn(Player player, int roll)
    {
        // Prioritize active pawns
        foreach (var pawn in player.Pawns)
        {
            if (pawn.State == PawnState.Active)
                return pawn;
        }

        // If rolled 6, try to move a pawn out of home
        if (roll == 6)
        {
            foreach (var pawn in player.Pawns)
            {
                if (pawn.State == PawnState.Home)
                    return pawn;
            }
        }

        return null; // No pawn can move
    }

    private void MovePawn(Pawn pawn, int steps)
    {
        if (pawn.State == PawnState.Home && steps == 6)
        {
            pawn.State = PawnState.Active;
            pawn.Position = 1;
            Console.WriteLine($"{pawn.Owner}'s pawn {pawn.Id} moves out of home!");
        }
        else if (pawn.State == PawnState.Active)
        {
            pawn.Position += steps;
            if (pawn.Position >= Board.TrackLength)
            {
                pawn.Position = Board.TrackLength;
                pawn.State = PawnState.Complete;
                Console.WriteLine($"{pawn.Owner}'s pawn {pawn.Id} reached the goal!");
            }
        }
    }

    class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Console Ludo!");
        List<Player> players = new List<Player>
        {
            new Player("Red"),
            new Player("Blue")
        };

        Game game = new Game(players);
        game.Start();

        Console.WriteLine("Game Over!");
    }
}

}
