using System;

namespace AdventOfCode_2019.Maze
{
    public sealed class ConsolePrintAttribute : Attribute
    {
        public ConsolePrintAttribute(string print)
        {
            Print = print;
        }

        public string Print { get; }
    }
}
