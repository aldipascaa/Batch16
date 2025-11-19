namespace DominoGame;
public class DominoSet
{
    private readonly Stack<DominoPiece> _pieces;
    private readonly Random _random;

    public DominoSet()
    {
        _pieces = new Stack<DominoPiece>();
        _random = new Random();
        InitializeSet();
    }

    private void InitializeSet()
    {
        var pieces = new List<DominoPiece>();

        // Create all possible domino combinations (0-6)
        for (int i = 0; i <= 6; i++)
        {
            for (int j = i; j <= 6; j++)
            {
                pieces.Add(new DominoPiece(i, j));
            }
        }

        Shuffle(pieces);
        
        foreach (var piece in pieces)
        {
            _pieces.Push(piece);
        }
    }

    private void Shuffle(List<DominoPiece> pieces)
    {
        int n = pieces.Count;
        while (n > 1)
        {
            n--;
            int k = _random.Next(n + 1);
            (pieces[k], pieces[n]) = (pieces[n], pieces[k]);
        }
    }

    public DominoPiece DrawPiece()
    {
        return _pieces.Count > 0 ? _pieces.Pop() : null;
    }

    public bool IsEmpty() => _pieces.Count == 0;
}