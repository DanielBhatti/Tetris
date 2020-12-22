using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public interface IPieceGenerator
    {
        IEnumerable<TetrisPiece> PeekNextPiece(int n);
        TetrisPiece PopNextPiece();
    }
}
