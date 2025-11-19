namespace DominoGame;
public interface IPlayer
{
    string GetName();
    DominoPiece MakeMove(Board board);
    void AddPiece(DominoPiece piece);
    bool HasPieces();
    int GetScore();
    void DisplayHand();
}