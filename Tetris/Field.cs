using System;
using System.Collections.Generic;

namespace Tetris
{
    /// <summary>
    /// An n x m grid where <see cref="TetrisPiece"/>s can spawn.
    /// This is meant to be a typical Tetris board, but has a bit of flexibility in terms of the playing field size.
    /// The field size represents the boundaries of the board, pieces can only be filled within the region specified.
    /// If a block is placed into the region (i.e. within the bounds), the grid should automatically attempt to find a portion of the region where it does fit.
    /// If a piece appears where a <see cref="Cell"/> has already been filled by a block, this is a game over.
    /// </summary>
    public class Field : IField
    {
        public int Width { get; }
        public int Height { get; }
        public Cell[,] Cells { get; }

        public Field(int height = 24, int width = 10)
        {
            if (width < 4 || height < 4) throw new ArgumentOutOfRangeException("Width and height of the Grid must both be greater than 4.");

            Width = width;
            Height = height;
            Cells = new Cell[height, width];
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    Cells[i, j] = new Cell(Color.White);
                }
            }
        }
    }
}
