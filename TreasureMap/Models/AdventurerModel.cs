using System;
using System.Linq;

namespace TreasureMap.Models
{
    public class AdventurerModel : CellModel
    {
        public AdventurerModel()
        {}
        public AdventurerModel(string[] line, int priority) : base(line.Skip(1).ToArray())
        {
            Name = line[1];
            Orientation = Convert.ToChar(line[4]);
            MovementsSequence = line[5];
            CollectedTreasures = 0;
            Priority = priority;
        }

        public string Name { get; set; }
        public char Orientation { get; set; }
        public string MovementsSequence { get; set; }
        public int CollectedTreasures { get; set; }
        public int Priority { get; set; }
    }
}
