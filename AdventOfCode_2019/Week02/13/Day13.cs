using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode_2019.Arcade;
using AdventOfCode_2019.Week01;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class Day13 : Day00
    {
        public Day13(IServiceProvider serviceProvider, ILogger<Day13> logger)
            : base(serviceProvider, logger)
        {
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var arcade = new ArcadeCabinet(cpu, program);

            var blockTiles = arcade.VideoBuffer.Tiles.Values.Where(x => x.Type == TileType.Block);
            AssertExpectedResult(398, blockTiles.Count());

            return $"\r\nThe progam starts with {blockTiles.Count()} block tiles.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            var arcade = new ArcadeCabinet(cpu, program)
                .SetFreePlay()
                .SetAutoPlay();

            return $"{ arcade.HighScore}";
        }
    }
}
