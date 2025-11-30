using ReDominoWPF.Interfaces;
using ReDominoWPF.Models;

namespace ReDominoWPF
{
    class GameController
    {
        public Dictionary<IPlayer, List<IDomino>> PlayerDominos { get; set; }
        public List<IDomino> DominoSet { get; set; }
        public IBoard Board { get; set; }

        public Random random = new Random();
        public int LeftEnd { get; set; } = -1;
        public int RightEnd { get; set; } = -1;
        public int InitialDomino { get; set; } = 7;
        public int CurrentPlayerIndex { get; set; } = 0;
        public bool IsFirstPiece { get; set; } = true;

        public GameController()
        {
            PlayerDominos = new Dictionary<IPlayer, List<IDomino>>();
            DominoSet = new List<IDomino>();
            Board = new Board();
        }

        public void StartNewGame(string humanName)
        {
            PlayerDominos.Clear();
            DominoSet.Clear();
            LeftEnd = RightEnd = -1;
            IsFirstPiece = true;
            CurrentPlayerIndex = 0;

            InitializeSet();
            AddPlayer(humanName);
            DealPlayer();
        }

        public void AddPlayer(string name)
        {
            IPlayer humanPlayer = new HumanPlayer(name);
            IPlayer computerPlayer = new ComputerPlayer("Computer");
            PlayerDominos.Add(humanPlayer, new List<IDomino>());
            PlayerDominos.Add(computerPlayer, new List<IDomino>());
        }

        public void DealPlayer()
        {
            for (int i = 0; i < InitialDomino; i++)
            {
                foreach (var player in PlayerDominos)
                {
                    if (DominoSet.Count > 0)
                    {
                        player.Value.Add(Draw());
                    }
                }
            }
        }

        public void InitializeSet()
        {
            for (int i = 0; i <= 6; i++)
            {
                for (int j = i; j <= 6; j++)
                {
                    DominoSet.Add(new Domino(i, j));
                }
            }
            Shuffle(DominoSet);
        }

        public void Shuffle(List<IDomino> dominos)
        {
            if (dominos == null) throw new ArgumentNullException(nameof(dominos));
            int n = dominos.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (dominos[i], dominos[j]) = (dominos[j], dominos[i]);
            }
        }

        public IDomino Draw()
        {
            if (DominoSet.Count == 0) return null;
            IDomino domino = DominoSet[0];
            DominoSet.RemoveAt(0);
            return domino;
        }

        public void PlacePiece(IDomino domino)
        {
            if (domino == null) throw new ArgumentNullException(nameof(domino));
            if (Board?.dominos == null) throw new InvalidOperationException("Board not initialized.");

            var boardList = Board.dominos;

            // First piece
            if (boardList.Count == 0)
            {
                boardList.Add(domino);
                LeftEnd = domino.A;
                RightEnd = domino.B;
                IsFirstPiece = false;
                return;
            }

            // Try left end
            if (domino.B == LeftEnd)
            {
                boardList.Insert(0, domino);
                LeftEnd = domino.A;
                return;
            }

            if (domino.A == LeftEnd)
            {
                var newDomino = new Domino(domino.B, domino.A);
                boardList.Insert(0, newDomino);
                LeftEnd = newDomino.A;
                return;
            }

            // Try right end
            if (domino.A == RightEnd)
            {
                boardList.Add(domino);
                RightEnd = domino.B;
                return;
            }

            if (domino.B == RightEnd)
            {
                var newDomino = new Domino(domino.B, domino.A);
                boardList.Add(newDomino);
                RightEnd = newDomino.B;
                return;
            }

            throw new InvalidOperationException("Domino cannot be placed on the board.");
        }

        public bool CanPlacePiece(IDomino domino)
        {
            if (domino == null) return false;
            if (Board?.dominos?.Count == 0) return true;

            return domino.A == LeftEnd || domino.B == LeftEnd ||
                   domino.A == RightEnd || domino.B == RightEnd;
        }

        /// <summary>
        /// Check if a player has any pieces in their hand.
        /// </summary>
        public bool HasPieces(IPlayer player)
        {
            return PlayerDominos.ContainsKey(player) && PlayerDominos[player].Count > 0;
        }

        /// <summary>
        /// Get the total score (sum of dots) for a player's hand.
        /// </summary>
        public int GetScore(IPlayer player)
        {
            if (!PlayerDominos.ContainsKey(player)) return 0;
            return PlayerDominos[player].Sum(d => d.A + d.B);
        }

        /// <summary>
        /// Add a piece to a player's hand.
        /// </summary>
        public void AddPieceToPlayer(IPlayer player, IDomino piece)
        {
            if (!PlayerDominos.ContainsKey(player)) return;
            PlayerDominos[player].Add(piece);
        }

        /// <summary>
        /// Remove a piece from a player's hand by index.
        /// </summary>
        public void RemovePieceFromPlayer(IPlayer player, int index)
        {
            if (!PlayerDominos.ContainsKey(player)) return;
            var hand = PlayerDominos[player];
            if (index >= 0 && index < hand.Count)
            {
                hand.RemoveAt(index);
            }
        }

        public bool IsEmpty() => Board?.dominos?.Count == 0;
    }
}
