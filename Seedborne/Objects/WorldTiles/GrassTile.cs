using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedborne.Objects.TileTypes
{
    public class GrassTile : Tile
    {
        public GrassTile(int x, int y) : base(x, y)
        {
            GroundType = "grass";
            Symbol = '"';
        }
    }
}
