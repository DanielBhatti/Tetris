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
        public bool IsFilled { get
            {
                if (Color == Color.Transparent) return false;
                else return true;
            } }

        public Cell(Color color = Color.Transparent)
        {
            Color = color;
        }

        public void Reset()
        {
            Color = Color.Transparent;
        }
    }
}
