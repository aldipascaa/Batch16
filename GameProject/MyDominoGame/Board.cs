namespace MyDominoGame;

public class Board
{
    public List<DominoPiece>? PiecesOnBoard{ get; set; }
    public int LeftEnd{ get; set; }
    public int RightEnd{ get; set; }
    public bool IsFirstPiece;
    public Board()
    {
        PiecesOnBoard = new List<DominoPiece>();
        LeftEnd = -1;
        RightEnd = -1;
        IsFirstPiece = true;
    }
}