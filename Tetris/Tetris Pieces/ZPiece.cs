using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class ZPiece : TetrisPiece
    {
        public ZPiece(Color color = Color.Green)
        {
            XPositions = new int[4] { -1, 0, 0, 1 };
            YPositions = new int[4] { 0, 0, -1, -1 };
            Color = color;
        }
    }
}
