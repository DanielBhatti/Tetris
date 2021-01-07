using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BoardPiece
    {
        public TetrisPiece TetrisPiece { get; set; }
        public Position Position { get; set; }

        public BoardPiece() { }

        public BoardPiece(TetrisPiece tetrisPiece, Position position)
        {
            TetrisPiece = tetrisPiece;
            Position = position;
        }
    }
}
