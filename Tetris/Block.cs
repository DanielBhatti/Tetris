using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Block
    {
        public Color Color { get; set; }

        public Block(Color color = Color.Teal)
        {
            Color = color;
        }
    }
}
