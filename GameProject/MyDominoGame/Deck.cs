namespace MyDominoGame;

public class Deck
{
    public Stack<DominoPiece>? pieces;

    public bool IsEmpty() => pieces?.Count == 0;
}