using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode_2019.Maze;
using AdventOfCode_2019.Cpu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class Day15 : Day00
    {
        public Day15(IServiceProvider serviceProvider, ILogger<Day15> logger)
            : base(serviceProvider, logger)
        {

            ////DirectInput = new[] { "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99" };
            ////DirectInput = new[] { "1102,34915192,34915192,7,4,7,99,0" };
            ////DirectInput = new[] { "104,1125899906842624,99" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            var maze = new MazeWalker(cpu, program);
            maze.Run().Render();

            return "Maze Rendered";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var program = inputs.First().ToProgram();

            var maze = new MazeWalker(cpu, program);
            var result = cpu.LastOutput;

            return result.ToString();
        }
    }
}
