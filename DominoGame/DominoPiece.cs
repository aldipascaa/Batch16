namespace DominoGame;
public class DominoPiece
{
    public int LeftValue { get ; private set; }
    public int RightValue { get; private set; }
    public DominoPiece(int left, int right)
    {
        LeftValue = left;
        RightValue = right;
    }
    public bool Matches(int value)
    {
        return LeftValue == value || RightValue == value;
    }
    public void SwapValues()
    {
        (LeftValue, RightValue) = (RightValue, LeftValue);
    }
    public int GetScore()=>LeftValue + RightValue;
    public override string ToString()
    {
        return $"[{LeftValue}|{RightValue}]";
    }
}