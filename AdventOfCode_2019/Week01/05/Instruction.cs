using System;
using System.Collections.Generic;

namespace AdventOfCode_2019.Week01
{
    public partial struct Instruction
    {
        public readonly int Address;
        public readonly int OperationCode; 
        public readonly Operations Operation;
        public readonly int Size;

        public readonly int ParameterCount;
        public readonly List<Parameter> Parameters;

        public Instruction(int address, int[] memory)
        {
            const int opcodeSize = 1;
            Address = address;

            OperationCode = memory.Read(address, ParameterMode.Immediate);
            Operation = (Operations)(OperationCode % 100);

            Size = InstructionSizeLookup(Operation);
            ParameterCount = ParameterCountLookup(Operation);
            Parameters = new List<Parameter>(ParameterCount);

            for (var i = 0; i < ParameterCount; i++)
            {
                var parameterCodes = OperationCode / 100;
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
                    ResolvedValue = memory.Read(parameterAddress, parameterMode)
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
                case Operations.Equals:
                case Operations.Less_Than:
                    return 3;

                case Operations.Jump_If_False:
                case Operations.Jump_If_True:
                    return 2;

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
            var msg = $"{Address.ToString().PadLeft(3, '0')}: {OperationCode.ToString().PadLeft(4, '0')} {Operation} {string.Join(", ", Parameters)}"; 
            return msg;
        }

        public override string ToString()
        {
            return Decompile();
        }
    }
}

