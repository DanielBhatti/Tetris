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
            Positions = new Position[4]
            {
                new Position(0, 1),
                new Position(0, 0),
                new Position(0, -1),
                new Position(1, -1)
            };
            Color = color;
        }
    }
}
