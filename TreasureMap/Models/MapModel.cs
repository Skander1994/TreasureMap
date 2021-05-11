using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Models
{
    public class MapModel
    {
        public IEnumerable<string> InputFileContent { get; set; }
        public Dictionary<string, AdventurerModel> Adventurers { get; set; }
        public string[,] Map { get; set; }
    }
}
