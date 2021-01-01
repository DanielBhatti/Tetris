using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// Base class for a Tetris piece.
    /// XPositions and YPositions are expected to have the same length; they are intended to contain the X and Y relative coordinates of the pieces.
    /// We assume that (0, 0) represents the "center" of the piece.
    /// </summary>
    public class TetrisPiece : ITetrisPiece
    {
        public int NumberOfBlocks { get => RelativePositions.Length; }
        public Position[] RelativePositions { get; protected set; }
        public Color Color { get; set; }

        public void RotateClockwise()
        {
            for(int i = 0; i < RelativePositions.Length; i++)
            {
                RelativePositions[i] = new Position(RelativePositions[i].Y, -1 * RelativePositions[i].X);
            }
        }

        public void RotateCounterclockwise()
        {
            for (int i = 0; i < RelativePositions.Length; i++)
            {
                RelativePositions[i] = new Position(-1 * RelativePositions[i].Y, RelativePositions[i].X);
            }
        }
    }
}
