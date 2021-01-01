using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public interface ITetrisPiece
    {
        int NumberOfBlocks { get; }
        Position[] RelativePositions { get; }
        Color Color { get; }

        void RotateClockwise();
        void RotateCounterclockwise();
    }
}
