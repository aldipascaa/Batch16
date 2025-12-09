using DominoWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoWPF.Interfaces
{
    internal interface IBoard
    {
        List<DominoPiece> dominos { get; }  
    }
}
