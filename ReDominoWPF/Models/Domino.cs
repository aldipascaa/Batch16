using ReDominoWPF.Interfaces;

namespace ReDominoWPF.Models
{
    class Domino : IDomino
    {
        public int A { get; set; }
        public int B { get; set; }
        public Domino(int a, int b)
        {
            this.A = a;
            this.B = b;
        }
    }
}
