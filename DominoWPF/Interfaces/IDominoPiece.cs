using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoWPF.Interfaces
{
    internal interface IDominoPiece
    {
        string LeftValue { get; set; }
        string RightValue { get; set; }
    }
}
