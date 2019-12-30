using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode_2019.Maze;
using AdventOfCode_2019.Cpu;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;

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

            var data = new StringBuilder();
            foreach (var tile in maze.Tiles.Values.OrderBy(x => x.Position.X).ThenBy(x => x.Position.Y))
            {
                data.AppendLine($"{tile.Position.X},{tile.Position.Y},{tile.TileType}");
            }

            SaveToFile(data.ToString());

            return "Maze Rendered";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var program = inputs.First().ToProgram();

            var minutes = 0;
            var maze = ReadFile().Select(x => new MazeMovementOption(x.Split(","))).ToList();
            var hasOxygen = maze.Where(x => x.TileType == MazeTileType.Goal).ToList();

            while (hasOxygen.Any(x => x.Resolved == 0))
            {
                foreach (var tile in hasOxygen.Where(x => x.Resolved == 0).ToList())
                {
                    foreach (var neighbor in MazeWalker.GetNeighbors(tile.Position, maze))
                    {
                        if (neighbor.TileType == MazeTileType.Empty)
                        {
                            tile.Resolved = minutes;
                            hasOxygen.Add(neighbor);
                        }
                    }
                }

                minutes++;
            }

            MazeWalker.Render(maze);

            var result = maze.Max(x => x.Resolved);
            return result.ToString();
        }
    }
}
