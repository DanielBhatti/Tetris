﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class JPiece : TetrisPiece
    {
        public JPiece(Color color = Color.Blue)
        {
            RelativePositions = new Position[4]
            {
                new Position(0, 1),
                new Position(0, 0),
                new Position(0, -1),
                new Position(-1, -1)
            };
            Color = color;
        }
    }
}
