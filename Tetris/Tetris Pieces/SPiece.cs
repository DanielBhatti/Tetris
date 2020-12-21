using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class SPiece : TetrisPiece
    {
        public SPiece(Color color = Color.Red)
        {
            XPositions = new int[4] { 0, 1, -1, 0 };
            YPositions = new int[4] { 0, 0, -1, -1 };
            Color = color;
        }
    }
}
