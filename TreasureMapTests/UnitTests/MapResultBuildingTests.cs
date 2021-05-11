using NUnit.Framework;
using System;
using System.Linq;
using TreasureMap.Application;
using TreasureMap.Helpers;
using TreasureMap.Models;
using TreasureMapTests.Setup;

namespace TreasureMapTests.UnitTests
{
    public class MapResultBuildingTests
    {
        public MapResultBuildingTests()
        {
            ResultBuilder = new MapResultBuilder();
            MapSetup = new MapSetup();
            MapInfo = new MapModel();
        }
        private MapResultBuilder ResultBuilder { get; set; }
        private MapSetup MapSetup { get; set; }
        private MapModel MapInfo { get; set; }

        [SetUp]
        public void Setup()
        {
            MapInfo.InputFileContent = MapSetup.SetupInputContent();
            MapInfo.Map = MapSetup.SetupMapExampleTest();
            MapInfo.Adventurers = MapSetup.SetupAdventurers();
        }

        [Test]
        public void CreateResult_AfterMapTransformation()
        {
            var result = ResultBuilder.GetMapTransformationResult(MapInfo);
            Assert.NotNull(result);
            Assert.AreEqual(8, result.Count);
            var advLine = result.First(r => r.StartsWith(Constants.AdventurerIndicator));
            Assert.AreEqual("A - Adv1 - 0 - 0 - S - 0", advLine);

            var adv = MapInfo.Adventurers["Adv1"];
            adv.VerticalAxe = 2;
            adv.HorizontalAxe = 3;
            adv.Orientation = 'E';
            adv.CollectedTreasures = 2;
            result = ResultBuilder.GetMapTransformationResult(MapInfo);
            advLine = result.First(r => r.StartsWith(Constants.AdventurerIndicator));
            Assert.AreEqual("A - Adv1 - 3 - 2 - E - 2", advLine);
        }
    }
}
