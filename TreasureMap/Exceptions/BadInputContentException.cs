using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Exceptions
{
    public class BadInputContentException : Exception
    {
        public BadInputContentException(int line, string message) :
            base(message: string.Format("The input file content is not valid at line {0}, {1} ", line, message))
        { }
    }
}
