using System;
using System.Collections.Generic;
using System.Text;
using TreasureMap.Helpers;
using TreasureMap.Models;

namespace TreasureMap.Application
{
    public class MapResultBuilder
    {
        /// <summary>
        /// Construit le résultat après l'éxecution de tous les mouvements des aventurier
        /// </summary>
        /// <param name="map">Contient la carte (matrice)</param>
        /// <returns>Liste de lignes de text à mettre dans le fichier output</returns>
        public List<string> GetMapTransformationResult(MapModel map)
        {
            List<string> result = new List<string>();
            foreach (var line in map.InputFileContent)
            {
                if (!string.IsNullOrEmpty(line))
                    SetResultLineContent(map, line, result);
                else
                    result.Add(line);
            }
            return result;  
        }

        private void SetResultLineContent(MapModel map, string line, List<string> result)
        {
            if (line.StartsWith(Constants.AdventurerIndicator))
                result.Add(SetAdventurerResultLine(map, line));
            else if (line.StartsWith(Constants.TreasureIndicator))
            {
                var treasureLine = SetTreasureResultLine(map, line);
                if (!string.IsNullOrEmpty(treasureLine))
                    result.Add(treasureLine);
            }
            else
                result.Add(line);
        }
        private string SetAdventurerResultLine(MapModel map, string line)
        {
            var splittedLine = MapHelper.SplitInputLine(line);
            var adventurer = map.Adventurers[splittedLine[1]];
            return SetLineFromAdventurer(adventurer);
        }
        private string SetLineFromAdventurer(AdventurerModel adventurer)
        {
            return string.Format("{0} - {1} - {2} - {3} - {4} - {5}",
                Constants.AdventurerIndicator, adventurer.Name,
                adventurer.HorizontalAxe.ToString(), adventurer.VerticalAxe.ToString(),
                adventurer.Orientation, adventurer.CollectedTreasures.ToString());
        }
        private string SetTreasureResultLine(MapModel map, string line)
        {
            var splittedLine = MapHelper.SplitInputLine(line);
            var treasure = MapHelper.GetTreasureCell(map.Map, splittedLine);
            return SetLineFromTreasure(splittedLine, treasure);
        }
        private string SetLineFromTreasure(string[] splittedLine, string treasureCell)
        {
            var treasureValue = MapHelper.GetTreasureValueFromCell(treasureCell);
            return (string.IsNullOrEmpty(treasureValue) || treasureValue == Constants.ZeroStr)?
                string.Empty : string.Format("{0} - {1} - {2} - {3}",
                    splittedLine[0], splittedLine[1], splittedLine[2], treasureValue);
        }
    }
}
