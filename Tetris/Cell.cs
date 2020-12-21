using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Cell
    {
        public Block Block { get; set; }

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
