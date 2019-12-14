using System;
using System.Numerics;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class IntCodeCpuMemory
    {
        const int MemorySize = 4096;

        readonly BigInteger[] memory = new BigInteger[MemorySize];
        private readonly ILogger<IntCodeCpuMemory> logger;

        public IntCodeCpuMemory(ILogger<IntCodeCpuMemory> logger)
        {
            this.logger = logger;
        }

        public int Length => MemorySize;

        public int RelativeBase { get; private set; }

        public BigInteger this[int index]
        {
            get
            {
                return memory[index];
            }
            set
            {
                memory[index] = value;
            }
        }

        public IntCodeCpuMemory SetRelativeBase(int delta)
        {
            RelativeBase += delta;
            return this;
        }

        public IntCodeCpuMemory Load(BigInteger[] program)
        {
            program.CopyTo(memory.AsSpan());
            return this;
        }

        public void Reset()
        {
            for (var i = 0; i < MemorySize; i++)
            {
                memory[i] = 0;
            }

            SetRelativeBase(0);
        }
    }
}