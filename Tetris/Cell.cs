using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// Represents a cell to hold Tetris pieces.
    /// If it is filled, it is assumed that the cell contains a block (i.e. the player cannot move it).
    /// A new piece should not be able to be placed into a filled cell.
    /// </summary>
    public class Cell
    {
        public Color Color { get; set; }
        public bool IsFilled { get; private set; }

        public Cell(Color color = Color.White)
        {
            Color = color;
        }

        public void Fill(Color color)
        {
            Color = color;
            IsFilled = true;
        }

        public void Unfill()
        {
            Color = Color.White;
            IsFilled = false;
        }
    }
}
