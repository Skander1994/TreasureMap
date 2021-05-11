using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Application;
using TreasureMap.Models;
using TreasureMapTests.Setup;

namespace TreasureMapTests.UnitTests
{
    public class MapTransformationTests
    {
        public MapTransformationTests()
        {
            Transformer = new MapTransformer();
            MapSetup = new MapSetup();
        }
        private MapTransformer Transformer { get; set; }
        private MapSetup MapSetup { get; set; }
        private string[,] Map { get; set; }
        private Dictionary<string, AdventurerModel> AdventuresDic { get; set; }

        [SetUp]
        public void Setup()
        {
            AdventuresDic = MapSetup.SetupAdventurers();
            Map = MapSetup.SetupMapExampleTest();
        }

        [Test]
        public void MoveAdventurer_WithoutActions()
        {
            // 1- Check adventurer initial properties
            var adv = AdventuresDic["Adv1"];
            Assert.AreEqual('S', adv.Orientation);
            Assert.AreEqual("GAA", adv.MovementsSequence);

            // 2- Check basic adventurer moves 
            // 2.1- Turn
            Transformer.MoveAdventurer(Map, adv);
            Assert.AreEqual('E', adv.Orientation);
            Assert.AreEqual("AA", adv.MovementsSequence);

            // 2.2- Move forward
            Transformer.MoveAdventurer(Map, adv);
            Assert.AreEqual("A", adv.MovementsSequence);
            Transformer.MoveAdventurer(Map, adv);
            Assert.AreEqual(string.Empty, adv.MovementsSequence);
            Assert.AreEqual(0, adv.VerticalAxe);
            Assert.AreEqual(2, adv.HorizontalAxe);
        }

        [Test]
        public void MoveAdventurer_WithMountains()
        {
            // 1- Check adventurer initial properties
            var adv = AdventuresDic["007"];
            Assert.AreEqual('N', adv.Orientation);
            Assert.AreEqual("AADAA", adv.MovementsSequence);

            // 2- Adventurer moves correctly next to mountain
            Transformer.MoveAdventurer(Map, adv);
            Transformer.MoveAdventurer(Map, adv);
            Transformer.MoveAdventurer(Map, adv);
            Assert.AreEqual(Map[3, 0], "");
            Assert.AreEqual(Map[1, 0], "A(007)");
            Assert.AreEqual(Map[1, 1], "M");
            Assert.AreEqual("AA", adv.MovementsSequence);

            // 3- Adventurer can't step on a cell containing a mountain
            Transformer.MoveAdventurer(Map, adv);
            Assert.AreEqual(Map[1, 0], "A(007)");
            Assert.AreEqual("A", adv.MovementsSequence);
            Transformer.MoveAdventurer(Map, adv);
            Assert.AreEqual("", adv.MovementsSequence);
            Assert.AreEqual(1, adv.VerticalAxe);
            Assert.AreEqual(0, adv.HorizontalAxe);
        }

        [Test]
        public void MoveAdventurer_WithAnotherAdventurer_WithOutBoundMoves()
        {
            // 1- Check adventurers initial positions
            var adv1 = AdventuresDic["Adv1"];
            var adv2 = AdventuresDic["007"];
            Assert.AreEqual(Map[0, 0], "A(Adv1)");
            Assert.AreEqual(Map[3, 0], "A(007)");
            Assert.AreEqual('N', adv2.Orientation);

            // 2- Adventurer can't step on a cell containing another one
            adv2.MovementsSequence = "AAAA";
            Transformer.MoveAdventurer(Map, adv2);
            Transformer.MoveAdventurer(Map, adv2);
            Transformer.MoveAdventurer(Map, adv2);
            Transformer.MoveAdventurer(Map, adv2);
            Assert.AreEqual(Map[0, 0], "A(Adv1)");
            Assert.AreEqual(Map[1, 0], "A(007)");
            Assert.AreEqual(Map[3, 0], string.Empty);

            // 3- Adventurer can't move when he tries to leave the map
            adv2.MovementsSequence = "GA";
            Transformer.MoveAdventurer(Map, adv2);
            Transformer.MoveAdventurer(Map, adv2);
            Assert.AreEqual(Map[1, 0], "A(007)");
        }
        [Test]
        public void MoveAdventurer_WithTreasureCollection()
        {
            // 1- Check adventurer & treasure initial positions
            var adv2 = AdventuresDic["007"];
            adv2.MovementsSequence = "ADAGGA";
            Assert.AreEqual("T(2)", Map[2, 0]);
            Assert.AreEqual("A(007)", Map[3, 0]);
            Assert.AreEqual('N', adv2.Orientation);

            // 2- Adventurer move to treasure cell and collect one treasure
            Transformer.MoveAdventurer(Map, adv2);
            Assert.AreEqual("A(007)+T(1)", Map[2, 0]);
            Assert.AreEqual(string.Empty, Map[3, 0]);
            Assert.AreEqual(1, adv2.CollectedTreasures);

            // 3- Adventurer leave treasure cell
            Transformer.MoveAdventurer(Map, adv2);
            Transformer.MoveAdventurer(Map, adv2);
            Assert.AreEqual("T(1)", Map[2, 0]);
            Assert.AreEqual("A(007)", Map[2, 1]);

            // 3- Adventurer go back to treasure cell and collect the rest
            Transformer.MoveAdventurer(Map, adv2);
            Transformer.MoveAdventurer(Map, adv2);
            Transformer.MoveAdventurer(Map, adv2);
            Assert.AreEqual("A(007)", Map[2, 0]);
            Assert.AreEqual(2, adv2.CollectedTreasures);
        }
    }
}
