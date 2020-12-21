using System;

namespace Tetris
{
    /// <summary>
    /// An n x m grid where <see cref="TetrisPiece"/>s can spawn.
    /// This is meant to be a typical Tetris board, but has a bit of flexibility in terms of the playing field size.
    /// The field size represents the boundaries of the board, pieces can only be filled within the region specified.
    /// If a <see cref="Block"/> is placed into the region (i.e. within the bounds), the grid should automatically attempt to find a portion of the region where it does fit.
    /// If a piece appears where a <see cref="Cell"/> has already been filled by a block, this is a game over.
    /// </summary>
    public class Grid : IGrid
    {
        public int Width { get; }
        public int Height { get; }
        public Cell[,] Cells { get; private set; }

        public Grid(int width = 10, int height = 24)
        {
            Width = width;
            Height = height;

            Cells = new Cell[width, height];
        }
    }
}
