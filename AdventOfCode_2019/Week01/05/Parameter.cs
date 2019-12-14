using System.Numerics;

namespace AdventOfCode_2019.Week01
{
    public struct Parameter
    {
        public int Address;
        public BigInteger Value;
        public ParameterMode Mode;

        // Dereferenced value stored at decompile time, can be useful for debugging.
        public BigInteger ResolvedValue;

        public override string ToString()
        {
            if(Mode == ParameterMode.Immediate)
            {
                return "#" + Value;
            }
            
            return $"[${Value}]=>{ResolvedValue}";
        }
    }
}
