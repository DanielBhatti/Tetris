using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// Container class for a <see cref="Block"/>.
    /// A Block object is either locked or not locked in a Cell.
    /// If it is locked, it is assumed to be fixed in place (i.e. the player cannot move it)
    /// </summary>
    public class Cell
    {
        public Block Block { get; set; }
        public bool IsLocked { get; set; }

        public Cell(Block block)
        {
            Block = block;
        }

        public bool IsEmpty()
        {
            if (Block == null) return true;
            else return false;
        }
    }
}
