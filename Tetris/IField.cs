using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public interface IField
    {
        int Width { get; }
        int Height { get; }
        Cell[,] Cells { get; }
        TetrisPiece CurrentPiece { get; }

        void MoveLeft();
        void MoveRight();
        void MoveDown();
        void HardDrop();
        void RotateClockwise();
        void RotateCounterclockwise();
        void HoldPiece();
    }
}
