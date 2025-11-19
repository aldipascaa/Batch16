namespace MyDominoGame;

public class DominoPiece
{
    public int SideA { get; set;}
    public int SideB { get; set;}
    public DominoPiece(int sideA, int sideB)
    {
        SideA = sideA;
        SideB = sideB;
    }
    public override string ToString()
    {
        return $"[{SideA}|{SideB}]";
    }
}