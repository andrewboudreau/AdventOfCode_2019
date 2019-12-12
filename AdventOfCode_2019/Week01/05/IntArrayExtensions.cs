﻿using System;

namespace AdventOfCode_2019.Week01
{
    public static class IntArrayExtensions
    {
        public static int Read(this int[] memory, int address, ParameterMode mode)
        {
            return mode == ParameterMode.Position
                ? memory[memory[address]]
                : memory[address];
        }

        public static int Write(this int[] memory, int value, int address, ParameterMode mode)
        {
            return mode == ParameterMode.Position
                ? memory[memory[address]] = value
                : memory[address] = value;
        }
    }
}