using System;

namespace Tetris
{
    /// <summary>
    /// An n x m grid where Tetris pieces can spawn.
    /// This is meant to be a typical Tetris board, but has a bit of flexibility in terms of the playing field size.
    /// </summary>
    public class Grid : IGrid
    {
        public int Width { get; }
        public int Height { get; }
        public Cell[,] Cells { get; private set; }

        public Grid(int width = 8, int height = 12)
        {
            Width = width;
            Height = height;

            Cells = new Cell[width, height];
        }
    }
}
