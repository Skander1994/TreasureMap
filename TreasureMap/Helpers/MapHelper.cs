using TreasureMap.Models;

namespace TreasureMap.Helpers
{
    public static class MapHelper
    {
        public static void SetTreasureCell(string[,] map, string[] line)
        {
            var axe = new CellModel(line);
            map[axe.VerticalAxe, axe.HorizontalAxe] = string.Format(Constants.TreasureOnlyCell, line[3]);
        }
        public static string GetTreasureCell(string[,] map, string[] line)
        {
            var axe = new CellModel(line);
            return map[axe.VerticalAxe, axe.HorizontalAxe];
        }
        public static void SetAdventurerCell(string[,] map, AdventurerModel adventurer)
        {
            map[adventurer.VerticalAxe, adventurer.HorizontalAxe] = string.Format(Constants.AdventurerOnlyCell, adventurer.Name);
        }
        public static void SetAdventurerAndTreasureCell(string[,] map, AdventurerModel adventurer, int treasuresCount)
        {
            map[adventurer.VerticalAxe, adventurer.HorizontalAxe] = string.Format(Constants.AdventurerAndTreasureCell, adventurer.Name, treasuresCount.ToString());
        }
        public static void SetCellAndAdventurerAfterLeaving(string[,] map, AdventurerModel adventurer, int newRow, int newColumn)
        {
            if (map[adventurer.VerticalAxe, adventurer.HorizontalAxe].Contains(Constants.TreasureIndicator))
                map[adventurer.VerticalAxe, adventurer.HorizontalAxe] = map[adventurer.VerticalAxe, adventurer.HorizontalAxe].Split('+')[1];
            else
                map[adventurer.VerticalAxe, adventurer.HorizontalAxe] = string.Empty;
            adventurer.VerticalAxe = newRow;
            adventurer.HorizontalAxe = newColumn;
        }
        public static string GetTreasureValueFromCell(string cellContent)
        {
            return cellContent.Contains("T(")? 
                cellContent.TrimStart('T').TrimStart('(').TrimEnd(')') : string.Empty;
        }
        public static string[] SplitInputLine(string line)
        {
            return line.Replace(Constants.WhiteSpace, string.Empty).Split(Constants.MinusChar);
        }
    }
}
