using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedborne.Objects.TileTypes
{
    public class ForestTile : Tile
    {
        public ForestTile(int x, int y) : base(x, y)
        {
            GroundType = "forest";
            Symbol = '\u2660';
        }
    }
}
