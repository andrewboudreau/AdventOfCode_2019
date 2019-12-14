using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventOfCode_2019.Week01;

namespace AdventOfCode_2019
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
