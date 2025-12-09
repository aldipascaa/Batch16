using System.Xml;

namespace DominoWPF;
public class DominoSet
{
    private readonly List<DominoPiece> _pieces;
    private readonly Random _random;

    public DominoSet()
    {
        _pieces = new List<DominoPiece>();
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
            _pieces.Add(piece);
        }
    }

    private void Shuffle(List<DominoPiece> pieces)
    {
        int n = pieces.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            var temp = pieces[i];
            int x = _random.Next(0,2);
            if (x == 1)
                pieces[j].SwapValues();
            pieces[i] = pieces[j];
            pieces[j] = temp;
        }
    }

    public DominoPiece? DrawPiece()
    {
        int x = _random.Next(0,_pieces.Count);
        return GetPiece(x);
    }
    public DominoPiece? GetPiece(int index)
    {
        if (index < 0 || index >= _pieces.Count)
            return null;
        DominoPiece piece = _pieces.ElementAt(index);
        _pieces.Remove(_pieces.ElementAt(index));
        return piece;
    }
    public override string ToString()
    {
        string output = "";
        for (int i = 0; i < _pieces.Count; i++)
            output += $"{i+1}: [X|X]\n";
        return output;
    }

    public bool IsEmpty() => _pieces.Count == 0;
}