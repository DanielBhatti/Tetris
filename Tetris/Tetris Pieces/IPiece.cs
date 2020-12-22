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
            Positions = new Position[4]
            {
                new Position(-1, 0),
                new Position(0, 0),
                new Position(1, 0),
                new Position(2, 0)
            };
            Color = color;
        }
    }
}
