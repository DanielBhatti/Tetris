using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public interface IGame
    {
        TetrisPiece CurrentPiece { get; }
        TetrisPiece HeldPiece { get; }
        IPieceGenerator PieceGenerator { get; }
        IField Field { get; }

        void MoveLeft();
        void MoveRight();
        void MoveDown();
        void HardDrop();
        void RotateClockwise();
        void RotateCounterclockwise();
        void HoldPiece();
    }
}
