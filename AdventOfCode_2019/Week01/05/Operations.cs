using System;

namespace AdventOfCode_2019.Week01
{
    public enum Operations
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        Jump_If_True = 5,
        Jump_If_False = 6,
        Less_Than = 7,
        Equals = 8,
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

                case Operations.Jump_If_False:
                case Operations.Jump_If_True:
                    return 3;

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
}