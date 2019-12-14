using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class Day09 : Day00
    {
        public Day09(IServiceProvider serviceProvider, ILogger<Day07> logger)
            : base(serviceProvider, logger)
        {
            //DirectInput = new[] { "123456789012", "3", "2" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var data = inputs.ToList();
            return "N/a";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var data = inputs.ToList();
            return "N/a";
        }
    }
}
