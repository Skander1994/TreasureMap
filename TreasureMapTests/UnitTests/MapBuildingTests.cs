using NUnit.Framework;
using System;
using System.Linq;
using TreasureMap.Application;
using TreasureMap.Models;
using TreasureMapTests.Setup;

namespace TreasureMapTests.UnitTests
{
    public class MapBuildingTests
    {
        public MapBuildingTests()
        {
            Builder = new MapBuilder();
            MapSetup = new MapSetup();
            MapInfo = new MapModel();
        }
        private MapBuilder Builder { get; set; }
        private MapSetup MapSetup { get; set; }
        private MapModel MapInfo { get; set; }

        [SetUp]
        public void Setup()
        {
            MapInfo.InputFileContent = MapSetup.SetupInputContent();
        }

        [Test]
        public void CreateMap_FromInputFileContent()
        {
            Builder.BuildInputMap(MapInfo);
            Assert.AreEqual(2, MapInfo.Adventurers.Count());
            Assert.AreEqual(4, MapInfo.Map.GetLength(0));
            Assert.AreEqual(3, MapInfo.Map.GetLength(1));
            Assert.AreEqual("A(Adv1)", MapInfo.Map[0, 0]);
            Assert.AreEqual("T(2)", MapInfo.Map[2, 0]);
            Assert.AreEqual("M", MapInfo.Map[1, 2]);
            Assert.AreEqual(1, MapInfo.Adventurers["007"].Priority);
        }
    }
}
