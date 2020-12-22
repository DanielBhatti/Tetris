﻿using System;
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
            Positions = new Position[4]
            {
                new Position(-1, 0),
                new Position(0, 0),
                new Position(0, -1),
                new Position(1, -1)
            };
            Color = color;
        }
    }
}