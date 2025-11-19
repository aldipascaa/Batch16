namespace MyDominoGame;

public class Game
{
    private readonly List<IPlayer> _players;
    private readonly Deck _deck;
    private readonly Board _board;
    private Random _random;
    private int _currentPlayerIndex;
    bool _isGameOver;
    int _dominoEachPlayer;
    public Game()
    {
        _players = new List<IPlayer>();
        _deck = new Deck();
        _board = new Board();
        _random = new Random();
        _currentPlayerIndex = 0;
        _isGameOver = false;
        _dominoEachPlayer = 7;
    }

    public void StartGame()
    {

    }
    public void AddPlayer()
    {

    }
    private void PlayGame()
    {
        while (!_isGameOver)
        {
            var currentPlayer = _players[_currentPlayerIndex];

            Console.WriteLine($"\n=== {currentPlayer.GetName()}'s Turn ===");
            Display();

            var playedPiece = currentPlayer.MakeMove(_board);

            if (playedPiece != null)
            {
                try
                {
                    PlacePiece(_board, playedPiece);
                    Console.WriteLine($"{currentPlayer.GetName()} played {playedPiece}");

                    // Check for winner
                    CheckForWinner();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine($"{currentPlayer.GetName()} passes the turn.");
            }

            // Move to next player
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        }
    }
    private void CheckForWinner()
    {
        if (_isGameOver) return;
    }
    #region Board Functions
    //Board Functions
    public bool CanPlacePiece(Board board, DominoPiece piece)
    {
        if (board.IsFirstPiece) return true;
        return Matches(piece,board.LeftEnd) || Matches(piece,board.RightEnd);
    }
    public void PlacePiece(Board board, DominoPiece piece)
    {
        if (board.IsFirstPiece)
        {
            board.PiecesOnBoard?.Add(piece);
            board.LeftEnd = piece.SideA;
            board.RightEnd = piece.SideB;
            board.IsFirstPiece = false;
            return;
        }

        if (Matches(piece, board.LeftEnd))
        {
            // Place on left side
            if (piece.SideB == board.LeftEnd)
            {
                // Piece needs to be rotated
                SwapValues(piece);
            }
            board.PiecesOnBoard?.Insert(0, piece);
            board.LeftEnd = piece.SideA;
            Console.WriteLine($"Placed on left. New left end: {board.LeftEnd}");
        }
        else if (Matches(piece, board.RightEnd))
        {
            // Place on right side
            if (piece.SideA == board.RightEnd)
            {
                // Piece needs to be rotated
                SwapValues(piece);
            }
            board.PiecesOnBoard?.Add(piece);
            board.RightEnd = piece.SideB;
            Console.WriteLine($"Placed on right. New right end: {board.RightEnd}");
        }
        else
        {
            throw new InvalidOperationException("Cannot place piece on the board.");
        }
    }
    public void Display()
    {
        Console.WriteLine("\nCurrent Board:");
        foreach (var piece in _board.PiecesOnBoard!)
        {
            Console.Write($"{piece} ");
        }
        Console.WriteLine($"\nBoard Ends: { _board.LeftEnd} | { _board.RightEnd}");
    }
    #endregion Board Functions
    #region Deck Functions
    //Deck Functions
    public void DominoSet(Deck deck)
    {
        deck.pieces = new Stack<DominoPiece>();
        InitializeSet(deck);
    }
    private void InitializeSet(Deck deck)
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
            _deck.pieces?.Push(piece);
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
    public DominoPiece DrawPiece(Deck deck)
    {
        return deck.pieces.Count > 0 ? deck.pieces.Pop() : null;
    }
    public bool IsEmpty(Deck deck)
    {
        return deck.pieces?.Count == 0;
    }
    #endregion Deck Functions
    #region DominoPiece Functions
    //DominoPiece Functions
    private bool Matches(DominoPiece piece, int value)
    {
        return piece.SideA == value || piece.SideB == value;
    }

    private void SwapValues(DominoPiece piece)
    {
        (piece.SideA, piece.SideB) = (piece.SideB, piece.SideA);
    }
    #endregion DominoPiece Functions
}