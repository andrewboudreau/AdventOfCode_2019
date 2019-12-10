using System;
using System.Collections.Generic;

namespace AdventOfCode_2019.Week01
{
    public struct Instruction
    {
        public readonly int Address;
        public readonly Operations Operation;
        public readonly int Size;

        public readonly int ParameterCount;
        public readonly List<Parameter> Parameters;

        public Instruction(int address, int[] memory)
        {
            const int opcodeSize = 1;
            Address = address;

            var opcode = memory.Read(address, ParameterMode.Immediate);
            Operation = (Operations)(opcode % 100);

            Size = InstructionSizeLookup(Operation);
            ParameterCount = ParameterCountLookup(Operation);
            Parameters = new List<Parameter>(ParameterCount);

            for (var i = 0; i < ParameterCount; i++)
            {
                var parameterCodes = opcode / 100;
                var parameterAddress = i + (address + opcodeSize);
                int divider = (int)Math.Pow(10, i);
                int parameterBit = parameterCodes / divider;
                var parameterModeValue = parameterBit % 10;
                var parameterMode = (ParameterMode)parameterModeValue;

                var parameter = new Parameter()
                {
                    Address = parameterAddress,
                    Value = memory[parameterAddress],
                    Mode = parameterMode,
                    Order = i,
                };

                Parameters.Add(parameter);
            }
        }

        private static int ParameterCountLookup(Operations operation)
        {
            switch (operation)
            {
                case Operations.Add:
                case Operations.Multiply:
                    return 3;

                case Operations.Input:
                case Operations.Output:
                    return 1;

                case Operations.Halt:
                    return 0;

                default:
                    throw new InvalidOperationException($"Unknown Operation '{operation}'.");
            }
        }

        public string Decompile()
        {
            var msg = $"{Address.ToString().PadLeft(5, '0')}: {Operation} {string.Join(", ", Parameters)}"; return msg;
        }

        public override string ToString()
        {
            return Decompile();
        }

        private static int InstructionSizeLookup(Operations operation)
        {
            switch (operation)
            {
                case Operations.Add:
                case Operations.Multiply:
                    return 4;

                case Operations.Input:
                case Operations.Output:
                    return 2;

                case Operations.Halt:
                    return 1;

                default:
                    throw new InvalidOperationException($"Unknown Operation '{operation}'.");
            }
        }
    }

    public static class InstructionExtensions
    {
        public static int ReadOperand1(this Instruction instruction, int[] memory)
        {
            int index = 0;
            return memory.Read(instruction.Parameters[index].Address, instruction.Parameters[index].Mode);
        }

        public static int ReadOperand2(this Instruction instruction, int[] memory)
        {
            int index = 1;
            return memory.Read(instruction.Parameters[index].Address, instruction.Parameters[index].Mode);
        }

        public static void WriteOperand1(this Instruction instruction, int value, int[] memory)
        {
            memory.Write(value, instruction.Parameters[0].Address, instruction.Parameters[0].Mode);
        }

        public static void WriteOperand3(this Instruction instruction, int value, int[] memory)
        {
            memory.Write(value, instruction.Parameters[2].Address, instruction.Parameters[2].Mode);
        }
    }
}

