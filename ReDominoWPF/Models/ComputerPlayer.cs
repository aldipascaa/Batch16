using ReDominoWPF.Interfaces;

namespace ReDominoWPF.Models
{
    public class ComputerPlayer : IPlayer
    {
        public string Name { get; set; }

        public ComputerPlayer(string name)
        {
            Name = name;
        }
    }
}
