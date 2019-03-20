using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedborne.Objects
{
    class Forest
    {


        public bool IsForestTile(Tile tile)
        {
            return (tile.GetGroundType() == "forest");
        }
    }
}
