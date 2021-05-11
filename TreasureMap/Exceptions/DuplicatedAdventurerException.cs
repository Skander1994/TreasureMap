using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureMap.Exceptions
{
    public class DuplicatedAdventurerException : BadInputContentException
    {
        public DuplicatedAdventurerException(int line)
            : base(line, "The adventurer's names must be unique, this name already exists")
        {}
    }
}
