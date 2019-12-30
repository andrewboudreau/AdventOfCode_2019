using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode_2019.Arcade
{
    public class CommandInputBuffer
    {
        public CommandInputBuffer(int parameterCount)
        {
            ParameterCount = parameterCount;
            Parameters = new List<BigInteger>(parameterCount);
        }

        public BigInteger this[int index]
        {
            get { return Parameters[index]; }
        }

        public int ParameterCount { get; }

        public List<BigInteger> Parameters { get; set; }

        public bool AddInput(BigInteger input)
        {
            Parameters.Add(input);
            return ParameterCount == Parameters.Count;
        }

        public CommandInputBuffer Reset()
        {
            Parameters.Clear();
            return this;
        }
    }
}
