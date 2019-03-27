using System;
using System.Text;
using System.Collections.Generic;
using Seedborne;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackWeekTests
{
    /// <summary>
    /// Summary description for RngTests
    /// </summary>
    [TestClass]
    public class RngTests
    {
        //public RngTests()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        //private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_Rng()
        {
            const string testSeed = "HelloWorld";
            var testRng = new Rng(testSeed);

            var testSeedValue = (testRng.SeedValue > 0);

            Assert.AreEqual(true, testSeedValue);
        }

        [TestMethod]
        public void Test_Rand()
        {
            const string testSeed = "test";
            var testRng = new Rng(testSeed);

            var testRand = testRng.Rand();

            bool isGreater = testRand > 0;

            Assert.AreEqual(true, isGreater); 
        }
    }
}
