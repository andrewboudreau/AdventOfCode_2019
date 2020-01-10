using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode_2019.Cpu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public partial class Day17 : Day00
    {
        private static char[,] Map;

        public Day17(IServiceProvider serviceProvider, ILogger<Day17> logger)
            : base(serviceProvider, logger)
        {
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            var map = new char[52, 52];
            var line = 0;
            var col = 0;

            cpu.Load(program)
                .Output((int x) =>
                {
                    map[line, col++] = (char)x;

                    if ((char)x == 10)
                    {
                        line++;
                        col = 0;
                    }
                })
                .Run();

            var sum = SumIntersections(map);
            AssertExpectedResult(6680, sum);

            //FancyPrint(map);
            Map = map;

            return sum.ToString();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var map = Map;

            var scaffold = new Scaffold(map);
            scaffold.WalkMap();

            // Instructions to walk the scaffold.
            var instructions = string.Join("", scaffold.Steps);

            logger.LogDebug($"Largest Substring is {FindLargestSubstring(instructions)}");
            logger.LogInformation(instructions);

            cpu
                .Load(program)
                .Patch(0, 2) // wake up by changing the program at address 0 from 1 to 2.
                .UseAsciiForInput("A,B,A,C,C,A,B,C,B,B", "L,8,R,10,L,8,R,8", "L,12,R,8,R,8", "L,8,R,6,R,6,R,10,L,8", "N")
                .Output((int x) =>
                {
                    Console.Write((char)x);
                    return;
                })
                .Run();

            var result = cpu.LastOutput;
            return result.ToString("N0");
        }

        protected int SumIntersections(char[,] map)
        {
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

            return sum;
        }

        protected void FancyPrint(char[,] map)
        {
            Console.Clear();
            Console.SetBufferSize(Console.BufferWidth, 70);

            var scaffold = '#'; //'▒';
            foreach (var c in map)
            {
                if (c == '#')
                {
                    Console.Write(scaffold);
                }
                else if (c == '.')
                {
                    Console.Write(FullBlock);
                }
                else
                {
                    Console.Write(c);
                }
            }
        }

        protected string FindLargestSubstring(string input, int maxPatternLength = 20)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            var index = new List<int>(20);
            var result = "";

            for (var length = 3; length < Math.Min(maxPatternLength, input.Length); length++)
            {
                var start = 0;
                var attempt = input.Substring(start, length);
                var working = input;
                int match;
                index.Clear();

                while ((match = working.IndexOf(attempt)) >= 0)
                {
                    working = working.Remove(match, length);
                    index.Add(match + (length * index.Count));
                }

                if (index.Count >= 3)
                {
                    logger.LogInformation($"Found {index.Count} matches at [{string.Join(", ", index)}] with {attempt} ({attempt.Length})");
                    if (input.Split(attempt).Distinct().Count() <= 4)
                    {
                        var items = input.Split(attempt).Concat(new[] { attempt });
                        logger.LogCritical($"Found distinct matches {string.Join(", ", items)}");
                        return attempt;
                    }
                    else
                    {
                        FindLargestSubstring(input.Split(attempt)[1]);
                    }
                }
            }

            return result;
        }
    }
}



/*
 * if (!video)
                    {
                        if (x == 10)
                        {
                            lineCount++;
                        }

                        Console.Write((char)x);
                        if (lineCount >= 58)
                        {
                            //video = true;
                            Console.Clear();
                            foreach (var tile in scaffold)
                            {
                                Console.SetCursorPosition(tile.X, tile.Y);
                                Console.Write(tile.Tile);
                            }

                            Console.WriteLine();
                        }

                        return;
                    }

                    if (index >= map.IndexArea())
                    {
                        index = 0;
                    }

                    var width = map.GetLength(1);
                    (int X, int Y) position = (index % width, index / width);

                    if ((char)map.GetValue(position.Y, position.X) != (char)x)
                    {
                        map.SetValue((char)x, position.Y, position.X);

                        Console.SetCursorPosition(position.X, position.Y);
                        Console.Write((char)x);
                    }

                    index++;
 * */
