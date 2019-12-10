using System;

namespace AdventOfCode_2019.Week01
{
    public class HaltException : Exception
    {
        public HaltException(string message)
            : base(message)
        {
        }

        public HaltException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

