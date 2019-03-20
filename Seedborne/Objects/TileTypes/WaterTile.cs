using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seedborne.Objects.TileTypes
{
    public class WaterTile : Tile
    {
        public WaterTile(int x, int y) : base(x, y)
        {
            GroundType = "water";
            Symbol = '~';
        }
    }
}
