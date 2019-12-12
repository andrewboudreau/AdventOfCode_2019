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

        public Func<int> ReadNextInputValue { get; private set; }

        public Action<int> WriteNextOutputValue { get; private set; }

        public IntCodeCpu Load(IEnumerable<int> memory)
        {
            this.memory = memory.ToArray();
            pc = 0;
            steps = 0;

            logger.LogInformation($"Loaded program {this.memory.Length:N0} bytes.");
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
                logger.LogDebug(instruction.Decompile());

                var increment = Execute(instruction);
                pc += increment;
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
                    Console.Write("Input a value: ");
                    var input = ReadNextInputValue?.Invoke() ?? int.Parse(Console.ReadLine());
                    Console.WriteLine(input.ToString());

                    instruction.WriteOperand1(input, memory);
                    break;

                case Operations.Output:
                    LastOutput = instruction.ReadOperand1(memory);
                    WriteNextOutputValue?.Invoke(LastOutput);
                    break;

                /*
                 * 
                 * Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                 * Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
               */

                case Operations.Jump_If_True:
                    if (instruction.ReadOperand1(memory) != 0)
                    {
                        pc = instruction.ReadOperand2(memory);
                        return 0;
                    }

                    break;

                case Operations.Jump_If_False:
                    if (instruction.ReadOperand1(memory) == 0)
                    {
                        pc = instruction.ReadOperand2(memory);
                        return 0;
                    }
                    break;

                /*
                 * Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                 * Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                 */
                case Operations.Less_Than:
                    if (instruction.ReadOperand1(memory) < instruction.ReadOperand2(memory))
                    {
                        instruction.WriteOperand3(1, memory);
                    }
                    else
                    {
                        instruction.WriteOperand3(0, memory);
                    }
                    break;

                case Operations.Equals:
                    if (instruction.ReadOperand1(memory) == instruction.ReadOperand2(memory))
                    {
                        instruction.WriteOperand3(1, memory);
                    }
                    else
                    {
                        instruction.WriteOperand3(0, memory);
                    }
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
            while (Step()) { }
        }

        public IntCodeCpu UseConstantValueForInput(int value)
        {
            ReadNextInputValue = () => value;
            return this;
        }

        public IntCodeCpu UseLoggingForOutput()
        {
            WriteNextOutputValue = (value) => logger.LogWarning($"Output: {value}");
            return this;
        }

        public string DumpMemory()
        {
            return string.Join(',', memory);
        }
    }

    public static class InstructionSpecifications
    {

    }
}