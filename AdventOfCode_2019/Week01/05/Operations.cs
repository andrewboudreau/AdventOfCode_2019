using System;

namespace AdventOfCode_2019.Cpu
{
    public enum Operations
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        Less_Than = 7,
        Equals = 8,
        SetRelativeBase = 9,
        Halt = 99
    }

    public partial struct Instruction
    {
        public static int InstructionSizeLookup(Operations operation)
        {
            switch (operation)
            {
                case Operations.Add:
                case Operations.Multiply:
                case Operations.Less_Than:
                case Operations.Equals:
                    return 4;

                case Operations.JumpIfFalse:
                case Operations.JumpIfTrue:
                    return 3;

                case Operations.Input:
                case Operations.Output:
                case Operations.SetRelativeBase:
                    return 2;

                case Operations.Halt:
                    return 1;

                default:
                    throw new InvalidOperationException($"Unknown Operation '{operation}'.");
            }
        }
    }
}