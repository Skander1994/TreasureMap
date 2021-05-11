using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Models
{
    public class CellModel
    {
        public CellModel()
        {}
        public CellModel(string[] line)
        {
            HorizontalAxe = Convert.ToInt32(line[1]);
            VerticalAxe = Convert.ToInt32(line[2]);
        }

        public int HorizontalAxe { get; set; }
        public int VerticalAxe { get; set; }
    }
}
