using System;
using System.Collections.Generic;
using System.Linq;
using TreasureMap.Helpers;
using TreasureMap.Models;

namespace TreasureMap.Application
{
    public class MapTransformer
    {
        /// <summary>
        /// Permet d'éxécuter tous les mouvements des aventuriers à tour de role et faire la collecte des trésors
        /// </summary>
        /// <param name="map">Objet contenant les aventuriers et la carte de trésors</param>
        public void ExecuteAllMovements(MapModel map)
        {
            var stillMovingAdventurersCount = map.Adventurers.Count();

            while (stillMovingAdventurersCount > Constants.Zero)
            {
                foreach (var adv in map.Adventurers.Values.OrderBy(a => a.Priority))
                {
                    if (string.IsNullOrEmpty(adv.MovementsSequence))
                        continue;
                    MoveAdventurer(map.Map, adv);
                    if (string.IsNullOrEmpty(adv.MovementsSequence))
                        stillMovingAdventurersCount--;
                }
            }
        }

        /// <summary>
        /// Permet de réaliser un mouvement pour un aventurier (rotation ou avancement)
        /// </summary>
        /// <param name="map">la carte dans laquelle l'aventurier se déplace</param>
        /// <param name="adventurer">l'aventurier qui va se déplacer</param>
        public void MoveAdventurer(string[,] map, AdventurerModel adventurer)
        {
            switch (adventurer.MovementsSequence[0])
            {
                case (Constants.ForwardDirection):
                    AdvanceAdventurer(map, adventurer);
                    break;
                case (Constants.RightDirection):
                    TurnAdventurerRight(adventurer);
                    break;
                case (Constants.LeftDirection):
                    TurnAdventurerLeft(adventurer);
                    break;
                default:
                    break;
            }
            adventurer.MovementsSequence = adventurer.MovementsSequence.Remove(0, 1);
        }
        private void AdvanceAdventurer(string[,] map, AdventurerModel adventurer)
        {
            var row = adventurer.VerticalAxe;
            var column = adventurer.HorizontalAxe;

            switch (adventurer.Orientation)
            {
                case (Constants.NorthOrientation):
                    row--;
                    break;
                case (Constants.EastOrientation):
                    column++;
                    break;
                case (Constants.SouthOrientation):
                    row++;
                    break;
                case (Constants.WestOrientation):
                    column--;
                    break;
                default:
                    break;
            }
            CheckActionsOnMovingFromCell(map, adventurer, column, row);
        }
        private void TurnAdventurerRight(AdventurerModel adventurer)
        {
            switch (adventurer.Orientation)
            {
                case (Constants.NorthOrientation):
                    adventurer.Orientation = Constants.EastOrientation;
                    break;
                case (Constants.EastOrientation):
                    adventurer.Orientation = Constants.SouthOrientation;
                    break;
                case (Constants.SouthOrientation):
                    adventurer.Orientation = Constants.WestOrientation;
                    break;
                case (Constants.WestOrientation):
                    adventurer.Orientation = Constants.NorthOrientation;
                    break;
                default:
                    break;
            }
        }
        private void TurnAdventurerLeft(AdventurerModel adventurer)
        {
            switch (adventurer.Orientation)
            {
                case (Constants.NorthOrientation):
                    adventurer.Orientation = Constants.WestOrientation;
                    break;
                case (Constants.EastOrientation):
                    adventurer.Orientation = Constants.NorthOrientation;
                    break;
                case (Constants.SouthOrientation):
                    adventurer.Orientation = Constants.EastOrientation;
                    break;
                case (Constants.WestOrientation):
                    adventurer.Orientation = Constants.SouthOrientation;
                    break;
                default:
                    break;
            }
        }
        private void CheckActionsOnMovingFromCell(string[,] map, AdventurerModel adventurer, int column, int row)
        {
            if (CanAdventurerMove(map, column, row))
                return;

            MapHelper.SetCellAndAdventurerAfterLeaving(map, adventurer, row, column);
            if (string.IsNullOrEmpty(map[row, column]))
                MapHelper.SetAdventurerCell(map, adventurer);
            else if (map[row, column].StartsWith(Constants.TreasureIndicator))
                CheckActionsOnMovingToTreasure(map, adventurer, column, row);
        }
        private void CheckActionsOnMovingToTreasure(string[,] map, AdventurerModel adventurer, int column, int row)
        {
            adventurer.CollectedTreasures++;
            var treasuresCount = Convert.ToInt32(MapHelper.GetTreasureValueFromCell(map[row, column]));
            treasuresCount--;
            if (treasuresCount == Constants.Zero)
                MapHelper.SetAdventurerCell(map, adventurer);
            else
                MapHelper.SetAdventurerAndTreasureCell(map, adventurer, treasuresCount);
        }
        private bool CanAdventurerMove(string[,] map, int column, int row)
        {
            return (row > map.GetLength(0) || column > map.GetLength(1)
                    || row < Constants.Zero || column < Constants.Zero
                    || (!string.IsNullOrEmpty(map[row, column])
                        && (map[row, column] == Constants.MountainIndicator
                            || map[row, column].StartsWith(Constants.AdventurerIndicator))));
        }
    }
}
