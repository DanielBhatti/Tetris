using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class TPiece : TetrisPiece
    {
        public TPiece(Color color = Color.Purple)
        {
            XPositions = new int[4] { 0, 0, 0, -1 };
            YPositions = new int[4] { -1, 0, 1, 0 };
            Color = color;
        }
    }
}
