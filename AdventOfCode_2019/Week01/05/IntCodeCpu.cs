using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class IntCodeCpu
    {
        private readonly ILogger logger;

        private int[] memory;
        private int pc;
        private int steps;

        public IntCodeCpu(ILogger logger)
        {
            this.logger = logger;
        }

        public int this[int index]
        {
            get
            {
                return memory[index];
            }
        }

        public long Steps => steps;

        public int LastOutput { get; private set; }

        public IntCodeCpu Load(IEnumerable<int> memory)
        {
            this.memory = memory.ToArray();
            pc = 0;
            steps = 0;

            logger.LogInformation($"Loaded program {this.memory.Length.ToString("N0")} bytes.");
            return this;
        }

        public IntCodeCpu Patch(int index, int value)
        {
            logger.LogInformation($"Patched program at Memory[{index}] = '{value}' from '{memory[index]}'");

            memory[index] = value;
            return this;
        }

        public bool Step()
        {
            try
            {
                var instruction = new Instruction(pc, memory);
                logger.LogInformation(instruction.Decompile());

                pc += Execute(instruction);
                steps += 1;
            }
            catch (HaltException halt)
            {
                logger.LogInformation(halt.Message);
                return false;
            }

            return true;
        }

        public int Execute(Instruction instruction)
        {
            int result;

            switch (instruction.Operation)
            {
                case Operations.Add:
                    result = instruction.ReadOperand1(memory) + instruction.ReadOperand2(memory);
                    instruction.WriteOperand3(result, memory);
                    break;

                case Operations.Multiply:
                    result = instruction.ReadOperand1(memory) * instruction.ReadOperand2(memory);
                    instruction.WriteOperand3(result, memory);
                    break;

                case Operations.Input:
                    Console.WriteLine("Input a value:");
                    var input = int.Parse(Console.ReadLine());
                    instruction.WriteOperand1(input, memory);
                    break;

                case Operations.Output:
                    LastOutput = instruction.ReadOperand1(memory);
                    logger.LogCritical(LastOutput.ToString());
                    break;

                case Operations.Halt:
                    throw new HaltException($"Program Halted with M[0]={memory[0].ToString("N0")}");

                default:
                    throw new InvalidOperationException($"Unknown Operation '{instruction.Operation}'.");
            }

            return instruction.Size;
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
    }
}