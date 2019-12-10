using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    class IntCodeCpu_old
    {
        private readonly ILogger logger;

        private int[] memory;
        private int pc = 0;

        public IntCodeCpu_old(ILogger<IntCodeCpu_old> logger)
        {
            this.logger = logger;
        }

        public IntCodeCpu_old(IEnumerable<int> memory, ILogger logger)
        {
            this.memory = memory.ToArray();
            this.logger = logger;
        }

        public int this[int index] 
        {
            get {
                return memory[index];
            } }

        public long Steps { get; private set; } = 0;

        public IntCodeCpu_old Load(IEnumerable<int> memory)
        {
            this.memory = memory.ToArray();
            pc = 0;

            logger.LogInformation($"Loaded program {this.memory.Length.ToString("N0")} bytes.");
            return this;
        }

        public IntCodeCpu_old Patch(int index, int value)
        {
            logger.LogInformation($"Patched program at Memory[{index}] = '{value}' from '{memory[index]}'");

            memory[index] = value;
            return this;
        }

        public bool Step()
        {
            var opCode = (Operations)memory[pc++];
            var result = Execute(opCode);
            Steps += 1;
            return result;
        }

        public void RunTillHalt()
        {
            while (Step())
            {
            }
        }

        public string DumpMemory()
        {
            return string.Join(',', memory);
        }

        private bool Execute(Operations opCode)
        {
            int binary(Func<int, int, int> op, string decompile)
            {
                var operand1Addr = memory[pc++];
                var operand1 = memory[operand1Addr];

                var operand2Addr = memory[pc++];
                var operand2 = memory[operand2Addr];

                var outputAddr = memory[pc++];

                var result = op(operand1, operand2);
                memory[outputAddr] = result;

                logger.LogDebug(string.Format(decompile, pc.ToString().PadLeft(2, '0'), operand1Addr, operand2Addr, outputAddr));
                return result;
            }

            switch (opCode)
            {
                case Operations.Add:
                    binary((a, b) => a + b, "{0}: Add {1}, {2}, {3}");
                    break;

                case Operations.Multiply:
                    binary((a, b) => a * b, "{0}: Multiply {1}, {2}, {3}");
                    break;

                case Operations.Halt:
                    return false;

                default:
                    throw new InvalidOperationException($"{opCode.ToString()} is not supported, yet.");
            }

            Steps++;
            return true;
        }
    }

}
