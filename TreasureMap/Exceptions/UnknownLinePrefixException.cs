using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Exceptions
{
    public class UnknownLinePrefixException : BadInputContentException
    {
        public UnknownLinePrefixException(int line)
            : base(line, "all lines must start with #, T, M or A")
        { }
    }
}
