namespace MyDominoGame;

public class HumanPlayer : IPlayer
{
    public string Name { get; set; }
    public List<DominoPiece> Hand { get; set; }

    public HumanPlayer(string name)
    {
        Name = name;
        Hand = new List<DominoPiece>();
    }

    public string GetName() => Name;

    public DominoPiece MakeMove(Board board)
    {
        Console.WriteLine($"{Name}, it's your turn.");
        DisplayHand();
        Console.WriteLine("Enter the index of the piece you want to play:");
        int index = int.Parse(Console.ReadLine() ?? "0");
        DominoPiece chosenPiece = Hand[index];
        Hand.RemoveAt(index);
        return chosenPiece;
    }

    public void AddPiece(DominoPiece piece)
    {
        Hand.Add(piece);
    }

    public bool HasPieces() => Hand.Count > 0;

    public void DisplayHand()
    {
        Console.WriteLine($"{Name}'s hand:");
        foreach (var piece in Hand)
        {
            Console.WriteLine(piece);
        }
    }
}