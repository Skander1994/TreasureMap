using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Helpers
{
    public static class Constants
    {
        #region Common
        public const char MinusChar = '-';
        public const string WhiteSpace = " ";
        public const int Zero = 0;
        public const string ZeroStr = "0";
        #endregion

        #region Input Text File Indicators
        public const string CommentIndicator = "#";
        public const string CardIndicator = "C";
        public const string MountainIndicator = "M";
        public const string TreasureIndicator = "T";
        public const string AdventurerIndicator = "A";
        #endregion

        #region Orientations and Directions
        public const char RightDirection = 'D';
        public const char LeftDirection = 'G';
        public const char ForwardDirection = 'A';
        public const char NorthOrientation = 'N';
        public const char SouthOrientation = 'S';
        public const char EastOrientation = 'E';
        public const char WestOrientation = 'O';
        #endregion

        #region Cell Content
        public const string TreasureOnlyCell = "T({0})";
        public const string AdventurerOnlyCell = "A({0})";
        public const string AdventurerAndTreasureCell = "A({0})+T({1})";
        #endregion
    }
}
