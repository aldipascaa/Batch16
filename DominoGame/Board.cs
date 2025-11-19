namespace DominoGame
{
    public class Board
    {
        public readonly List<DominoPiece> pieces;
        public int LeftEnd { get; private set; }
        public int RightEnd { get; private set; }
        public bool isFirstPiece;

        public Board()
        {
            pieces = new List<DominoPiece>();
            LeftEnd = -1;
            RightEnd = -1;
            isFirstPiece = true;
        }

        public bool CanPlacePiece(DominoPiece piece)
        {
            if (isFirstPiece) return true;
            
            return piece.Matches(LeftEnd) || piece.Matches(RightEnd);
        }

        public void PlacePiece(DominoPiece piece)
        {
            if (isFirstPiece)
            {
                pieces.Add(piece);
                LeftEnd = piece.LeftValue;
                RightEnd = piece.RightValue;
                isFirstPiece = false;
                return;
            }

            if (piece.Matches(LeftEnd))
            {
                // Place on left side
                if (piece.RightValue == LeftEnd)
                {
                    // Piece needs to be rotated
                    piece.SwapValues();
                }
                pieces.Insert(0, piece);
                LeftEnd = piece.LeftValue;
                Console.WriteLine($"Placed on left. New left end: {LeftEnd}");
            }
            else if (piece.Matches(RightEnd))
            {
                // Place on right side
                if (piece.LeftValue == RightEnd)
                {
                    // Piece needs to be rotated
                    piece.SwapValues();
                }
                pieces.Add(piece);
                RightEnd = piece.RightValue;
                Console.WriteLine($"Placed on right. New right end: {RightEnd}");
            }
            else
            {
                throw new InvalidOperationException("Piece cannot be placed on the board");
            }
        }
    }
}