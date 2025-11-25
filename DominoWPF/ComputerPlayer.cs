namespace DominoWPF;
public class ComputerPlayer : IPlayer
{
    private readonly string _name;
    private readonly List<DominoPiece> _hand;
    private readonly Random _random;
    public ComputerPlayer(string name)
    {
        _name = name;
        _hand = new List<DominoPiece>();
        _random = new Random();
    }
    public string GetName() => _name;
    public void AddPiece(DominoPiece piece)
    {
        _hand.Add(piece);
    }
    // Expose read-only hand for UI
    public IReadOnlyList<DominoPiece> Hand => _hand.AsReadOnly();

    // Allow removal / play by index (if UI decides)
    public DominoPiece? PlayPieceAt(int index)
    {
        if (index < 0 || index >= _hand.Count) return null;
        var piece = _hand[index];
        _hand.RemoveAt(index);
        return piece;
    }

    public bool HasPieces() => _hand.Count > 0;
    public int GetScore() => _hand.Sum(piece => piece.GetScore());
    public bool CanMakeMove(Board board)
    {
        foreach (DominoPiece piece in _hand)
        {
            if (piece.Matches(board.LeftEnd) || piece.Matches(board.RightEnd))
            {
                return true;
            }
        }
        return false;
    }
    public DominoPiece? MakeMove(Board board)
    {
        Console.WriteLine($"\n{_name} is thinking...");
        Thread.Sleep(1000); // Simulate thinking

        var leftEnd = board.LeftEnd;
        var rightEnd = board.RightEnd;

        // Try to find a playable piece
        foreach (var piece in _hand.OrderBy(_ => _random.Next()))
        {
            if (board.CanPlacePiece(piece))
            {
                _hand.Remove(piece);
                return piece;
            }
        }

        return null; // No playable piece found
    }
    public override string ToString()
    {
        return $"Computer own :{_hand.Count}";
    }
}