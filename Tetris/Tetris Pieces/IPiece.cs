using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class IPiece : TetrisPiece
    {
        public IPiece(Color color = Color.Teal)
        {
            XPositions = new int[4] { -1, 0, 1, 2 };
            YPositions = new int[4] { 0, 0, 0, 0 };
            Color = color;
        }
    }
}
