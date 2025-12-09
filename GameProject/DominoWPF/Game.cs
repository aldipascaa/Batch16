namespace DominoWPF;
public class Game
{
    private readonly List<IPlayer> _players;
    private DominoSet _dominoSet;
    private DominoPiece _piece;
    private Board _board;
    private int _currentPlayerIndex;
    private int _initialDomino;
    public Action<IPlayer>? OnWinnerFound;

    public Game()
    {
        _players = new List<IPlayer>();
        _dominoSet = new DominoSet();
        _board = new Board();
        _currentPlayerIndex = 0;
        _initialDomino = 3;
    }
    public void AddPlayer(IPlayer player) => _players.Add(player);

    // Expose a read-only view of players for UI
    public IReadOnlyList<IPlayer> Players => _players.AsReadOnly();

    // Expose board and domino set for UI orchestration
    public Board Board => _board;
    public DominoSet DominoSet => _dominoSet;

    // Allow UI to deal initial pieces without starting the console loop
    public void DealInitialPieces(int count)
    {
        if (count <= 0) return;
        _initialDomino = count;
        for (int i = 0; i < _initialDomino; i++)
        {
            foreach (var player in _players)
            {
                var piece = _dominoSet.DrawPiece();
                if (piece != null)
                {
                    player.AddPiece(piece);
                }
            }
        }
    }
    public void StartGame()
    {
        Console.WriteLine("Starting Domino Game!");

        // Deal initial pieces (7 each for 2 players)
        for (int i = 0; i < _initialDomino; i++)
        {
            foreach (var player in _players)
            {
                var piece = _dominoSet.DrawPiece();
                if (piece != null)
                {
                    player.AddPiece(piece);
                }
            }
        }

        PlayGame();
    }
    private void PlayGame()
    {
        while (true)
        {
            var currentPlayer = _players[_currentPlayerIndex];

            Console.WriteLine($"\n=== {currentPlayer.GetName()}'s Turn ===");
            Display(_board);
            Console.WriteLine(currentPlayer);

            var playedPiece = currentPlayer.MakeMove(_board);

            if (playedPiece != null)
            {
                try
                {
                    _board.PlacePiece(playedPiece);
                    Console.WriteLine($"{currentPlayer.GetName()} played {playedPiece}");

                    // Check for winner
                    if (!currentPlayer.HasPieces())
                    {
                        Console.WriteLine($"\n🎉 {currentPlayer.GetName()} wins the game!");
                        break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    // Return the piece to the player's hand
                    currentPlayer.AddPiece(playedPiece);
                    continue; // Don't advance turn
                }
            }
            else
            {
                var drawnPiece = _dominoSet.DrawPiece();
                if (drawnPiece != null)
                {
                    currentPlayer.AddPiece(drawnPiece);
                    Console.WriteLine($"{currentPlayer.GetName()} drew a piece");
                }
                else
                {
                    Console.WriteLine($"{currentPlayer.GetName()} passes (no pieces to draw)");
                }
            }

            // Check for game block
            if (IsGameBlocked())
            {
                Console.WriteLine("\nGame blocked! Calculating scores...");
                DeclareWinnerByScore();
                break;
            }

            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        }
    }
    private bool IsGameBlocked()
    {
        // Game is blocked if no player can make a move and the boneyard is empty
        if (!_dominoSet.IsEmpty()) return false;

        foreach (var player in _players)
        {
            if (player.CanMakeMove(_board))
            {
                return true;
            }
        }

        return false; // Simplified implementation
    }
    public void SwapValues(int a, int b)
    {
        _piece.LeftValue = a;
        _piece.RightValue = b;
    }
    private void DeclareWinnerByScore()
    {
        var winner = _players.OrderBy(p => p.GetScore()).First();
        Console.WriteLine($"🏆 {winner.GetName()} wins with lowest score: {winner.GetScore()}!");

        Console.WriteLine("\nFinal Scores:");
        foreach (var player in _players.OrderBy(p => p.GetScore()))
        {
            Console.WriteLine($"{player.GetName()}: {player.GetScore()}");
        }
    }
    #region Board Functions
    // Display the current state of the board
    public void Display(Board board)
    {
        {
            Console.WriteLine("\nCurrent Board:");
            if (board.pieces.Count == 0)
            {
                Console.WriteLine("Empty");
                return;
            }

            foreach (var piece in board.pieces)
            {
                Console.Write(piece + " ");
            }
            Console.WriteLine($"\nEnds: {board.LeftEnd} | {board.RightEnd}");
        }
    }
    #endregion
}