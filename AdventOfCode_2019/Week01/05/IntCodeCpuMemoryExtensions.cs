using System.Numerics;

namespace AdventOfCode_2019.Week01
{
    public static class IntCodeCpuMemoryExtensions
    {
        public static BigInteger Read(this IntCodeCpuMemory memory, int address, ParameterMode mode)
        {
            if (mode == ParameterMode.Relative)
            {
                return memory[(int)memory[address] + memory.RelativeBase];
            }

            return mode == ParameterMode.Position
                ? memory[(int)memory[address]]
                : memory[address];
        }

        public static BigInteger Write(this IntCodeCpuMemory memory, BigInteger value, int address, ParameterMode mode)
        {
            if (mode == ParameterMode.Relative)
            {
                return memory[(int)memory[address] + memory.RelativeBase] = value;
            }

            return mode == ParameterMode.Position
                ? memory[(int)memory[address]] = value
                : memory[address] = value;
        }
    }
}
