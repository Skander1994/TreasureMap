using System.Collections.Generic;
using System.Linq;
using TreasureMap.Exceptions;
using TreasureMap.Helpers;
using TreasureMap.Models;

namespace TreasureMap.Application
{
    public class MapBuilder
    {
        /// <summary>
        /// Permet de construire la carte avec son contenu sous forme de matrice
        /// </summary>
        /// <param name="map">Objet qui contient l'input et la carte résultante (matrice)</param>
        public void BuildInputMap(MapModel map)
        {
            map.Adventurers = new Dictionary<string, AdventurerModel>();
            map.Map = SetMapBody(map.InputFileContent);
            FillMapBody(map);
        }

        /// <summary>
        /// Construire la squelette de la carte
        /// </summary>
        /// <param name="input">Une collection de texte décrivant la carte</param>
        /// <returns>Matrice vide représentant la carte</returns>
        private string[,] SetMapBody(IEnumerable<string> input)
        {
            if (input == null || !input.Any())
                return new string[0, 0];

            var matrixSizeLine = input
                .First(l => l.StartsWith(Constants.CardIndicator))
                .Replace(Constants.WhiteSpace, string.Empty)
                .Split(Constants.MinusChar);
            var matrixSize = new CellModel(matrixSizeLine);
            return new string[matrixSize.VerticalAxe, matrixSize.HorizontalAxe];
        }

        /// <summary>
        /// Remplir la carte selon les données dans l'input
        /// </summary>
        /// <param name="map"></param>
        private void FillMapBody(MapModel map)
        {
            int lineIndex = 1;
            int adventurerPriority = 0;

            foreach (var line in map.InputFileContent)
            {
                var cleanLine = MapHelper.SplitInputLine(line);

                switch (cleanLine[0])
                {
                    case (Constants.CardIndicator):
                        break;
                    case (Constants.MountainIndicator):
                        var axe = new CellModel(cleanLine);
                        map.Map[axe.VerticalAxe, axe.HorizontalAxe] = Constants.MountainIndicator;
                        break;
                    case (Constants.TreasureIndicator):
                        MapHelper.SetTreasureCell(map.Map, cleanLine);
                        break;
                    case (Constants.AdventurerIndicator):
                        SetMapAdventurer(map, cleanLine, lineIndex, ref adventurerPriority);
                        break;
                    default:
                        if (cleanLine[0].StartsWith(Constants.CommentIndicator))
                            break;
                        else
                            throw new UnknownLinePrefixException(lineIndex);
                }
                lineIndex++;
            }
        }

        /// <summary>
        /// Construire l'aventurier et la case qui lui contient
        /// </summary>
        /// <param name="map">L'objet contenant la carte, les aventurires et l'input</param>
        /// <param name="line">contenu de la ligne de l'input</param>
        /// <param name="lineIndex">numéro de la ligne dans l'input</param>
        /// <param name="priority">priorité de l'aventurier dans les déplacements</param>
        private void SetMapAdventurer(MapModel map, string[] line, int lineIndex, ref int priority)
        {
            var adventurer = new AdventurerModel(line, priority);
            if (map.Adventurers.ContainsKey(adventurer.Name))
                throw new DuplicatedAdventurerException(lineIndex);
            map.Adventurers.Add(adventurer.Name, adventurer);
            MapHelper.SetAdventurerCell(map.Map, adventurer);
            priority++;
        }
    }
}
