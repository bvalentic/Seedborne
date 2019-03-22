using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedborne.Objects.TileTypes
{
    public class SoilTile : Tile
    {
        public SoilTile(int x, int y) : base(x, y)
        {
            GroundType = "soil";
            Symbol = '.';
        }
    }
}
