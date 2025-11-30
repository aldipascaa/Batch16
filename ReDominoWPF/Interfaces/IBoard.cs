namespace ReDominoWPF.Interfaces
{
    interface IBoard
    {
        public List<IDomino> dominos { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
