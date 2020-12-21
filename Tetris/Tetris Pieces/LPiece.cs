using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class LPiece : TetrisPiece
    {
        public LPiece(Color color = Color.Orange)
        {
            XPositions = new int[4] { 0, 0, 0, 1 };
            YPositions = new int[4] { 1, 0, -1, -1 };
            Color = color;
        }
    }
}
