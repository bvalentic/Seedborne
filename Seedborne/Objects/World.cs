using System;
using Seedborne.Objects.TileTypes;

namespace Seedborne.Objects
{
    public class World
    {
        public int Length { get; set; }
        public int Height { get; set; }

        public Tile[,] Tiles;
        //currently array is in order {water, soil, grass, trees, forests, depth}
        public int[] TileStats = new int[6];

        public World(int length, int height)
        {
            Length = length;
            Height = height;
            Tiles = new Tile[Height,Length];
        }

        public Tile GetTile(int x, int y)
        {
            return Tiles[x, y];
        }

        public int[] GetAdjacentTiles(Tile tile)
        {
            
            var x = tile.GetX();
            var y = tile.GetY();
            var tileValues = tile.GetAdjacentValues();

            if (x > 0)
            {
                if (y > 0)
                {
                    tile.AddTileValue(Tiles[x - 1, y - 1]);
                }
                else
                {
                    tile.AddTileValue(Tiles[x - 1, Length - 1]);
                }
                tile.AddTileValue(Tiles[x - 1, y]);
                if (y + 1 < Length)
                {
                    tile.AddTileValue(Tiles[x - 1, y + 1]);
                }
                else
                {
                    tile.AddTileValue(Tiles[x - 1, 0]);
                }
            }

            if (y > 0)
            {
                tile.AddTileValue(Tiles[x, y - 1]);
            }
            else
            {
                tile.AddTileValue(Tiles[x, Length - 1]);
            }

            if (y + 1 < Length)
            {
                tile.AddTileValue(Tiles[x, y + 1]);
            }
            else
            {
                tile.AddTileValue(Tiles[x, 0]);
            }

            if (x + 1 < Height)
            {
                if (y > 0)
                {
                    tile.AddTileValue(Tiles[x + 1, y - 1]);
                }
                else
                {
                    tile.AddTileValue(Tiles[x + 1, Length - 1]);
                }
                tile.AddTileValue(Tiles[x + 1, y]);
                if (y + 1 < Length)
                {
                    tile.AddTileValue(Tiles[x + 1, y + 1]);
                }
                else
                {
                    tile.AddTileValue(Tiles[x + 1, 0]);
                }
            }

            return tileValues;
        }

        public void UpdateStats()
        {
            Array.Clear(TileStats,0,TileStats.Length);

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    var tileType = Tiles[i, j].GetGroundType();

                    if (tileType == "water")
                        TileStats[0]++;
                    else if (tileType == "soil")
                        TileStats[1]++;
                    else if (tileType == "grass")
                        TileStats[2]++;
                    else if (tileType == "tree")
                        TileStats[3]++;
                    else if (tileType == "forest")
                        TileStats[4]++;

                    TileStats[5] += Tiles[i, j].Depth;
                }
            }

            TileStats[5] /= (Length * Height);
        }

        public void AllWater()
        {//for testing purposes
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    Tiles[i,j] = new WaterTile(i,j);
                }
            }
        }

        public void FillWorld(ConsoleRetriever consoleRetriever)
        {
            bool refineKeepGoing;
            AllWater();
            
            AssignDepth();
            AssignTiles();

            Smooth();
            do
            {
                Display();
                refineKeepGoing = MenuOptions.KeepGoing("Refine? ", consoleRetriever);
                if (refineKeepGoing) Refine();
            } while (refineKeepGoing);

        }

        public void AssignDepth()
        {//gives each tile a "depth" or elevation which is used to determine tile type
            var rng = new Random();

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    var depthSeed = rng.Next(1, 6);
                    Tiles[i, j].Depth = depthSeed;
                }
            }
        }

        public void AssignTiles()
        {//initial tile generation
            var rng = new Random();
            
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    var tileSeed = rng.Next(1, 100);
                    if (tileSeed < 25)
                    {
                        Tiles[i, j] = new WaterTile(i, j);
                    }
                    else if (tileSeed < 50)
                    {
                        Tiles[i, j] = new SoilTile(i, j);
                    }
                    else if (tileSeed < 75)
                    {
                        Tiles[i, j] = new GrassTile(i, j);
                    }
                    else if (tileSeed < 100)
                    {
                        Tiles[i, j] = new TreeTile(i, j);
                    }
                }
            }
        }

        public void Smooth()
        {//takes depth of surrounding tiles and has good chance of changing tile to avg. depth or within 1 level of surrounding tiles
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    var tile = Tiles[i, j];
                    var tileDepth = tile.Depth;
                    double avgDepth;

                    var rng = new Random();
                    var depthSeed = rng.Next(1, 100);

                    //need to reset the Adjacent Tile value 
                    Array.Clear(tile.AdjacentTiles, 0, tile.AdjacentTiles.Length);

                    if (i < 1 || i == Height - 1)
                    {
                        avgDepth = GetAdjacentTiles(tile)[5] / 5.0;
                    }
                    else avgDepth = GetAdjacentTiles(tile)[5] / 8.0;

                    if (depthSeed >= 90) continue;
                    if (tileDepth - avgDepth > 1)
                    {
                        tile.Depth--;
                    }
                    else if (avgDepth - tileDepth > 1)
                    {
                        tile.Depth++;
                    }
                }
            }
        }

        public void Refine()
        {//refinement (using Conway's Game of Life)
            var rng = new Random();
            UpdateStats();

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    var tile = Tiles[i, j];

                    //need to reset the Adjacent Tile value 
                    Array.Clear(tile.AdjacentTiles, 0, tile.AdjacentTiles.Length);

                    var adjacent = GetAdjacentTiles(tile);
                    var waterValue = adjacent[0];
                    var treeValue = adjacent[3];
                    var forestValue = adjacent[4];
                    var refineSeed = rng.Next(1, 100);

                    if (tile.GetGroundType() == "water")
                    {
                        /*
                         make it so water is more grouped together
                         less small creeks/streams etc. 
                        */
                        if (waterValue == 0 || (waterValue <= 3 && refineSeed < 50))
                        {
                            Tiles[i,j] = new SoilTile(i,j);
                        }
                    }
                    //else if (tile.GetGroundType() == "soil")
                    //{
                    //    /*
                    //     soil could either spontaneously become water or grass
                    //     depending on conditions
                    //     */
                    //    if (refineSeed < 5)
                    //    {
                    //        Tiles[i,j] = new WaterTile(i,j);
                    //    }
                    //    else if (refineSeed < 20)
                    //    {
                    //        Tiles[i,j] = new GrassTile(i,j);
                    //        continue;
                    //    }
                    //}
                    else 
                    {
                        /*
                         chance for tile surrounded by water to become water
                         water eventually overruns the world if refined enough times
                         need to find a way for it to stagnate
                        */
                        if (waterValue == 8 ||(waterValue >= 3 && refineSeed < 33))
                        {
                            Tiles[i,j] = new WaterTile(i,j);
                        }
                    }

                    if (tile.GetGroundType() == "tree")
                    {
                        //chance for surrounded trees to become forests (if no forests adjacent and if there aren't too many already)
                        if (treeValue >= 4 && forestValue < 1 && refineSeed > 95 && TileStats[4] < (Length*Height*4))
                        {
                            GrowForest(i, j);
                        }
                    }
                }
            }
        }

        public void Display()
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    var tileSymbol = Tiles[i, j].GetSymbol();
                    if (tileSymbol == '~')
                    {//water looks blue now
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {//everything else looks green
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write(tileSymbol);
                    Console.ResetColor();
                }

                Console.Write("\n");
            }
            UpdateStats();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"Total: {Height*Length}  Water: {TileStats[0]}  Soil: {TileStats[1]}  Grass: {TileStats[2]}  Trees: {TileStats[3]} \n" +
                              $"Forests: {TileStats[4]}  Avg. Depth: {TileStats[5]}");
        }

        public void DisplayDepth()
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Length; j++)
                {
                    var tileDepth = Tiles[i, j].Depth;
                    switch (tileDepth)
                    {
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case 5:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                    }
                    Console.Write(tileDepth);
                    Console.ResetColor();
                }
                Console.Write("\n");
            }
        }

        public void GrowForest(int x, int y)
        {
            var tile = Tiles[x, y];
            if (tile.GetGroundType() == "tree")
            {
                Tiles[x,y] = new ForestTile(x,y);
            }
        }
    }
}
