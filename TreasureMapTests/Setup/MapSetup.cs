using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Models;

namespace TreasureMapTests.Setup
{
    public class MapSetup
    {
        public List<string> SetupInputContent()
        {
            return new List<string>()
            {
                "C - 3 - 4",
                "M - 1 - 0",
                "M - 2 - 1",
                "#{T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}",
                "T - 0 - 2 - 2",
                "# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} - {Orientation} - {Nb.trésors ramassés}",
                "A - Adv1 - 0 - 0 - S - GAA",
                "A - 007 - 0 - 3 - N - AADAA"
            };
        }

        public Dictionary<string, AdventurerModel> SetupAdventurers()
        {
            var firstAdv = new AdventurerModel()
            {
                Name = "Adv1",
                VerticalAxe = 0,
                HorizontalAxe = 0,
                MovementsSequence = "GAA",
                Orientation = 'S'
            };
            var secondAdv = new AdventurerModel()
            {
                Name = "007",
                VerticalAxe = 3,
                HorizontalAxe = 0,
                MovementsSequence = "AADAA",
                Orientation = 'N'
            };
            return new Dictionary<string, AdventurerModel>()
            {
                { firstAdv.Name, firstAdv },
                { secondAdv.Name, secondAdv }
            };
        }

        public string[,] SetupMapExampleTest()
        {
            var map = new string[4, 4];
            map[1, 1] = "M";
            map[1, 2] = "M";
            map[2, 0] = "T(2)";
            map[0, 0] = "A(Adv1)";
            map[3, 0] = "A(007)";

            return map;
        }
    }
}
