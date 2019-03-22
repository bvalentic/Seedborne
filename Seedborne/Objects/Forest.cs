using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seedborne.Objects.TileTypes;

namespace Seedborne.Objects
{
    public class Forest
    {
        protected Tile HomeTile;

        public Forest(World world, int x, int y)
        {
            HomeTile = world.Tiles[x, y];
        }

        public Tile GetHomeTile()
        {
            return HomeTile;
        }
    }
}
