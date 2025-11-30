using ReDominoWPF.Interfaces;

namespace ReDominoWPF.Models
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; set; }

        public HumanPlayer(string name)
        {
            Name = name;
        }
    }
}
