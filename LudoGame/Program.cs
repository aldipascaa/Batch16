public class Board
{
    private readonly int size;
    private char[,] grid;  // Removed readonly to allow changes

    public Board(int size = 15)
    {
        this.size = size;
        grid = new char[size, size];
        ClearBoard();
    }

    // ========== PUBLIC METHODS TO MODIFY BOARD ==========

    public void SetCell(int row, int col, char value)
    {
        if (IsValidPosition(row, col))
            grid[row, col] = value;
    }

    public char GetCell(int row, int col)
    {
        return IsValidPosition(row, col) ? grid[row, col] : '?';
    }

    public void ClearBoard()
    {
        for (int r = 0; r < size; r++)
            for (int c = 0; c < size; c++)
                grid[r, c] = '_';
    }

    public void FillArea(int startRow, int startCol, int width, int height, char fillChar)
    {
        for (int r = startRow; r < startRow + height && r < size; r++)
        {
            for (int c = startCol; c < startCol + width && c < size; c++)
            {
                grid[r, c] = fillChar;
            }
        }
    }

    public void DrawLine(int startRow, int startCol, int endRow, int endCol, char lineChar)
    {
        int rowStep = Math.Sign(endRow - startRow);
        int colStep = Math.Sign(endCol - startCol);
        
        for (int r = startRow, c = startCol; 
             r != endRow + rowStep || c != endCol + colStep; 
             r += rowStep, c += colStep)
        {
            if (IsValidPosition(r, c))
                grid[r, c] = lineChar;
        }
    }

    public void PlacePlayerHome(int row, int col, char playerSymbol)
    {
        // Place a 3x3 home area
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                if (IsValidPosition(r, c))
                    grid[r, c] = playerSymbol;
            }
        }
    }

    private bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < size && col >= 0 && col < size;
    }

    // Your existing Print method remains the same
    public void Print()
    {
        for (int r = 0; r < size; r++)
        {
            Console.WriteLine();
            for (int c = 0; c < size; c++)
            {
                if ((c == 0 && r == 7)||(c == size - 1 && r == 7)||(r == 0 && c == 7)||(r == size - 1 && c == 7))
                    Console.Write("[ "+grid[r, c] + " ]");
                else if ((c > 5 && c <9 ) && (r > 5 && r <9))
                    Console.Write("  "+grid[r, c] + "  ");
                else if ((c ==7 && r > 0 && r < size -1)||(r == 7 && c > 0 && c < size -1))
                    Console.Write("(("+grid[r, c] + "))");
                else if ((c == 6 || c == size - 7 || r == 6 || r == size -7))
                    Console.Write("[ "+grid[r, c] + " ]");
                else
                    Console.Write("  "+grid[r, c] + "  ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        Board board = new Board();
        
        // Example 1: Create a custom board layout
        board.ClearBoard();
        
        // Draw paths
        board.DrawLine(0, 7, 14, 7, '═'); // Vertical path
        board.DrawLine(7, 0, 7, 14, '║'); // Horizontal path
        
        // Place player homes
        board.PlacePlayerHome(3, 3, 'R');    // Red home
        board.PlacePlayerHome(1, 13, 'B');   // Blue home  
        board.PlacePlayerHome(13, 1, 'G');   // Green home
        board.PlacePlayerHome(13, 13, 'Y');  // Yellow home
        
        // Add safe zones
        board.FillArea(6, 6, 3, 3, '★');
        
        board.Print();

        // Example 2: Dynamic changes during gameplay
        Console.WriteLine("\n=== MOVING A PLAYER ===");
        board.SetCell(7, 1, 'R');  // Place red player at position
        board.Print();
        
        Console.WriteLine("\n=== MOVING TO NEW POSITION ===");
        board.SetCell(7, 1, '_');  // Clear old position
        board.SetCell(7, 2, 'R');  // Set new position
        board.SetCell(6, 7, 'B');  // Place blue player at position
        board.Print();
    }
}