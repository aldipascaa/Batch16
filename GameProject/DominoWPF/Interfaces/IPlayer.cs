using DominoWPF.Models;

namespace DominoWPF;
public interface IPlayer
{
    string GetName();
    bool CanMakeMove(Board board);
    DominoPiece? MakeMove(Board board);
    void AddPiece(DominoPiece piece);
    bool HasPieces();
    int GetScore();
}