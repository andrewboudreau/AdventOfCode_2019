using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode_2019.Cpu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class Day17 : Day00
    {
        public Day17(IServiceProvider serviceProvider, ILogger<Day17> logger)
            : base(serviceProvider, logger)
        {
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            var line = 0;
            var col = 0;
            var map = new char[52, 52];

            cpu.Load(program)
                .Output((int x) =>
                {
                    var tile = (char)x;
                    map[line, col] = tile;

                    Console.Write(map[line, col]);

                    if (tile == 10)
                    {
                        line++;
                        col = 0;
                    }
                    else
                    {
                        col++;
                    }
                })
                .Run();

            var sum = 0;
            for (var row = 6; row < 50; row++)
            {
                for (var column = 1; column < 50; column++)
                {
                    var intersect =
                        map[row, column] == '#' &&
                        map[row - 1, column] == '#' &&
                        map[row + 1, column] == '#' &&
                        map[row, column - 1] == '#' &&
                        map[row, column + 1] == '#';

                    if (intersect)
                    {
                        map[row, column] = 'O';
                        sum += row * column;
                    }
                }
            }

            Console.Clear();
            foreach (var c in map)
            {
                Console.Write(c);
            }

            AssertExpectedResult(6680, sum);
            return sum.ToString();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            return "na";
        }
    }
}
