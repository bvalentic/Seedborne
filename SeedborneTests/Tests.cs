using Seedborne;
using Seedborne.Objects;
using Seedborne.Objects.TileTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackWeekTests
{
    [TestClass]
    public class ConsoleRetrieverTests
    {
        [TestMethod]
        public void Test_ConsoleRetriever()
        {

        }
    }
    [TestClass]
    public class MenuOptionsTests
    {
        [TestMethod]
        public void Test_CheckYesOrNo_ReturnsTrue()
        {
            var play = MenuOptions.CheckYesOrNo("yes");

            Assert.AreEqual(true, play);
        }

        [TestMethod]
        public void Test_CheckYesOrNo_ReturnsFalse()
        {
            var play = MenuOptions.CheckYesOrNo("no");

            Assert.AreEqual(false, play);
        }

        [TestMethod]
        public void Test_CheckYesOrNo_ReturnsOther()
        {
            var play = MenuOptions.CheckYesOrNo("test");

            Assert.AreEqual(null, play);
        }

        [TestMethod]
        public void Test_KeepPlaying()
        {
            var inputQuestion = "Test?";
            var testResponseRetriever = new TestMethods.TestPositiveResponseRetriever();

            var response = MenuOptions.KeepGoing(inputQuestion, testResponseRetriever);

            Assert.AreEqual(true, response);
        }

        [TestMethod]
        public void Test_Begin()
        {
            var testResponseRetriever = new TestMethods.TestPositiveResponseRetriever();

            var beginResponse = MenuOptions.Begin(testResponseRetriever);

            Assert.AreEqual(true, beginResponse);
        }
    }

    [TestClass]
    public class WorldTests
    {
        [TestMethod]
        public void Test_World()
        {
            World testWorld = new World(5,10);

            Assert.AreEqual(5, testWorld.Length);
            Assert.AreEqual(10, testWorld.Height);
        }

        [TestMethod]
        public void Test_Initial()
        {
            var testWorld = new World(10, 10);
            testWorld.AssignTiles();
            testWorld.Tiles[0, 0] = new WaterTile(0,0);

            Assert.AreEqual("water",testWorld.GetTile(0,0).GetGroundType());
        }

        [TestMethod]
        public void Test_GetAdjacentTiles()
        {
            var testWorld = new World(3,3);
            testWorld.AllWater();
            var testTile = testWorld.Tiles[1, 1];
            
            var testValues = testWorld.GetAdjacentTiles(testTile);

            Assert.AreEqual(8, testValues[0]);
        }

        [TestMethod]
        public void Test_GetAdjacentTiles_EdgeCaseLeft()
        {
            var testWorld = new World(3, 3);
            testWorld.AllWater();
            var testTile = testWorld.Tiles[1, 2];

            var testValues = testWorld.GetAdjacentTiles(testTile);

            Assert.AreEqual(8, testValues[0]);
        }

        [TestMethod]
        public void Test_GetAdjacentTiles_EdgeCaseRight()
        {
            var testWorld = new World(3, 3);
            testWorld.AllWater();
            var testTile = testWorld.Tiles[1, 0];

            var testValues = testWorld.GetAdjacentTiles(testTile);

            Assert.AreEqual(8, testValues[0]);
        }

        [TestMethod]
        public void Test_GetAdjacentTiles_EdgeCaseCorner()
        {
            var testWorld = new World(3, 3);
            testWorld.AllWater();
            var testTile = testWorld.Tiles[0, 0];

            var testValues = testWorld.GetAdjacentTiles(testTile);

            Assert.AreEqual(5, testValues[0]);
        }

        [TestMethod]
        public void Test_TileStats()
        {
            var testWorld = new World(3,3);

            testWorld.AllWater();
            testWorld.UpdateStats();

            Assert.AreEqual(9, testWorld.TileStats[0]);
        }

        [TestMethod]
        public void Test_FillWorld()
        {
            var testResponseRetriever = new TestMethods.TestNegativeResponseRetriever();
            var testWorld = new World(3,3);

            testWorld.FillWorld(testResponseRetriever);

            Assert.AreNotEqual(null, testWorld.Tiles[0,0].GetSymbol());
        }

        [TestMethod]
        public void Test_Refine()
        {
            var testWorld = new World(10, 10);
            testWorld.AssignTiles();
            testWorld.Refine();
            testWorld.Tiles[0, 0] = new WaterTile(0, 0);

            Assert.AreEqual("water", testWorld.GetTile(0, 0).GetGroundType());
        }
    }

    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void Test_Tile()
        {
            var testTile = new Tile(0, 0);

            Assert.AreEqual(0, testTile.GetX());
            Assert.AreEqual(0, testTile.GetY());
        }

        [TestMethod]
        public void Test_TilePosition()
        {
            var testPositionTile = new Tile(6, 8);
            Assert.AreEqual(6, testPositionTile.GetPosition()[0]);
        }

        [TestMethod]
        public void Test_WaterTile()
        {
            var testWaterTile = new WaterTile(1, 0);

            Assert.AreEqual("water", testWaterTile.GetGroundType());
        }

        [TestMethod]
        public void Test_SoilTile()
        {
            var testSoilTile = new SoilTile(0, 1);

            Assert.AreEqual('.', testSoilTile.GetSymbol());
        }

        [TestMethod]
        public void Test_GrassTile()
        {
            var testGrassTile = new GrassTile(1, 1);

            Assert.AreEqual("grass", testGrassTile.GetGroundType());
        }

        [TestMethod]
        public void Test_TreeTile()
        {
            var testTreeTile = new TreeTile(2, 2);

            Assert.AreEqual("tree", testTreeTile.GetGroundType());
        }

        [TestMethod]
        public void Test_AddValues()
        {
            var testTile = new SoilTile(1, 1);
            var testWaterTile = new WaterTile(0, 0);
            var testSoilTile = new SoilTile(0, 1);
            var testGrassTile = new GrassTile(0, 2);
            var testTreeTile = new TreeTile(1, 0);
            var testForestTile = new ForestTile(2, 0);
            var testValues = testTile.GetAdjacentValues();

            testTile.AddTileValue(testWaterTile);
            testTile.AddTileValue(testSoilTile);
            testTile.AddTileValue(testGrassTile);
            testTile.AddTileValue(testTreeTile);
            testTile.AddTileValue(testForestTile);

            Assert.AreEqual(1, testValues[0]);
            Assert.AreEqual(1, testValues[1]);
            Assert.AreEqual(1, testValues[2]);
            Assert.AreEqual(1, testValues[3]);
            Assert.AreEqual(1, testValues[4]);
        }

        [TestMethod]
        public void Test_IsForestTile()
        {
            var testForestTile = new ForestTile(1,1);
            var isForest = testForestTile.IsForestTile();

            Assert.AreEqual(true, isForest);
        }

        [TestMethod]
        public void Test_Depth()
        {
            var testTile = new Tile(1, 1) {Depth = 1};

            Assert.AreEqual(1, testTile.Depth);
        }
}

    [TestClass]
    public class ForestTests
    {
        [TestMethod]
        public void Test_ForestTile()
        {
            var testForestTile = new ForestTile(0, 0);
            
            Assert.AreEqual("forest", testForestTile.GetGroundType());
        }

        [TestMethod]
        public void Test_GrowForest()
        {
            var testWorld = new World(3,3);
            testWorld.AllWater();
            testWorld.Tiles[1, 1] = new TreeTile(1, 1);

            testWorld.GrowForest(1,1);
            var testForest = testWorld.Tiles[1, 1].GetGroundType();

            Assert.AreEqual("forest", testForest);
        }

        [TestMethod]
        public void Test_InitForest()
        {
            var testWorld = new World(3,3);
            testWorld.AllWater();
            testWorld.Tiles[1, 1] = new ForestTile(1, 1);

            var testForest = new Forest(testWorld, 1,1);

            Assert.AreEqual("forest", testForest.GetHomeTile().GetGroundType());
        }
    }

    //[TestClass]
    //public class MainMenuTests
    //{

    //}

    //[TestClass]
    //public class ConsoleRetrieverTests
    //{
    //    [TestMethod]
    //    public void Test_Retriever()
    //    {
    //        ConsoleRetriever testConsoleRetriever = new ConsoleRetriever();
    //        var response = testConsoleRetriever.GetResponse();

    //        Assert.AreEqual(null, response);
    //    }
    //}
}
