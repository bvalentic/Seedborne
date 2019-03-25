

namespace Seedborne.Objects
{
    public class Tile
    {
        protected readonly int[] Position = new int[2];
        protected string GroundType { get; set; }
        protected char Symbol { get; set; }

        //currently array is in order {water, soil, grass, tree, forest, depth}
        public int[] AdjacentTiles = new int[6];
        public int Depth { get; set; }

        public Tile(int x, int y)
        {
            Position[0] = x;
            Position[1] = y;
        }

        public int[] GetPosition()
        {
            return Position;
        }

        public int GetX()
        {
            return Position[0];
        }

        public int GetY()
        {
            return Position[1];
        }

        public string GetGroundType()
        {
            return GroundType;
        }

        public char GetSymbol()
        {
            return Symbol;
        }

        public int[] GetAdjacentValues()
        {
            return AdjacentTiles;
        }


        public void AddTileValue(Tile tile)
        {
            if (tile.GroundType == "water")
            {
                AdjacentTiles[0]++;
            }
            else if (tile.GroundType == "soil")
            {
                AdjacentTiles[1]++;
            }
            else if (tile.GroundType == "grass")
            {
                AdjacentTiles[2]++;
            }
            else if (tile.GroundType == "tree")
            {
                AdjacentTiles[3]++;
            }
            else if (tile.GroundType == "forest")
            {
                AdjacentTiles[4]++;
            }

            AdjacentTiles[5] += tile.Depth;
        }

        public bool IsForestTile()
        {
            return (GroundType == "forest");
        }
    }
}
