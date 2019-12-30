using System;

namespace AdventOfCode_2019.Cpu
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

