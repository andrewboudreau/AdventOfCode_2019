using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class Day13 : Day00
    {
        public Day13(IServiceProvider serviceProvider, ILogger<Day13> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = new[] {
@".#..#
.....
#####
....#
...##"
            };

            DirectInput = new[] {
@".#..#
.....
#####
....#
...##"
            };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return "N/a";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var data = inputs.ToList();
            return "N/a";
        }
    }
}
