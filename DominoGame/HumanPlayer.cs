namespace DominoGame;
public class HumanPlayer : IPlayer
{
    private readonly string _name;
    private readonly List<DominoPiece> _hand;

    public HumanPlayer(string name)
    {
        _name = name;
        _hand = new List<DominoPiece>();
    }

    public string GetName() => _name;

    public void AddPiece(DominoPiece piece)
    {
        _hand.Add(piece);
    }

    public bool HasPieces() => _hand.Count > 0;

    public int GetScore() => _hand.Sum(piece => piece.GetScore());

    public void DisplayHand()
    {
        Console.WriteLine($"\n{_name}'s hand:");
        for (int i = 0; i < _hand.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {_hand[i]}");
        }
    }

    public DominoPiece MakeMove(Board board)
    {
        DisplayHand();
        Console.WriteLine($"\nBoard ends: {board.LeftEnd} | {board.RightEnd}");

        while (true)
        {
            Console.Write("Choose a piece to play (1-{0}) or 0 to draw: ", _hand.Count);
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0)
                {
                    return null; // Signal to draw
                }

                if (choice >= 1 && choice <= _hand.Count)
                {
                    var selectedPiece = _hand[choice - 1];
                    
                    // Check if piece can be placed
                    if (board.CanPlacePiece(selectedPiece))
                    {
                        _hand.RemoveAt(choice - 1);
                        return selectedPiece;
                    }
                    else
                    {
                        Console.WriteLine("This piece cannot be placed. Choose another or draw.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
}