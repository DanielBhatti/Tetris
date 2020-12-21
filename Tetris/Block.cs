using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// Represents the blocks composing the different Tetris pieces.
    /// Distinct from them, since parts of the pieces can be eliminated when on the <see cref="Grid"/>
    /// </summary>
    public class Block
    {
        public Color Color { get; set; }

        public Block(Color color = Color.Teal)
        {
            Color = color;
        }
    }
}
