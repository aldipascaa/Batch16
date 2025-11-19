namespace MyDominoGame;

public interface IPlayer
{
    string GetName();
    DominoPiece MakeMove(Board board);
    void AddPiece(DominoPiece piece);
    bool HasPieces();
    void DisplayHand();
}