using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode_2019.Cpu
{
    public static class IntCodeCpuExtensions
    {
        public static IntCodeCpu Load(this IntCodeCpu cpu, IEnumerable<int> program)
        {
            var bigIntProgram = program.Select(x => new BigInteger(x)).ToArray();
            return cpu.Load(bigIntProgram);
        }

        public static IntCodeCpu Load(this IntCodeCpu cpu, string csv)
        {
            var program = csv.ToProgram();
            return cpu.Load(program);
        }
    }
}
