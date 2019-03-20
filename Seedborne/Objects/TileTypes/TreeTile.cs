using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedborne.Objects.TileTypes
{
    public class TreeTile : Tile
    {
        public TreeTile(int x, int y) : base(x, y)
        {
            GroundType = "tree";
            Symbol = '0';
        }
    }
}
