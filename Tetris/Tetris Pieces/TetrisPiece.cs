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
        public int NumberOfBlocks { get => XPositions.GetLength(0); }
        public int[] XPositions { get; protected set; }
        public int[] YPositions { get; protected set; }
        public Color Color { get; set; }

        public void RotateClockwise()
        {
            int[] temp = YPositions;
            YPositions = XPositions;
            for (int i = 0; i < XPositions.GetLength(0); i++)
            {
                YPositions[i] = -1 * XPositions[i];
            }
            XPositions = temp;
        }

        public void RotateCounterclockwise()
        {
            int[] temp = XPositions;
            XPositions = YPositions;
            for (int i = 0; i < YPositions.GetLength(0); i++)
            {
                XPositions[i] = -1 * YPositions[i];
            }
            YPositions = temp;
        }
    }
}
