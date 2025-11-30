using ReDominoWPF.Interfaces;

namespace ReDominoWPF.Models
{
    class Board : IBoard
    {
        public List<IDomino> dominos { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Board()
        {
            dominos = new List<IDomino>();
        }
    }
}
