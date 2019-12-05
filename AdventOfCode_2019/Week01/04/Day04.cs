using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    partial class Day04 : Day00
    {
        public Day04(IServiceProvider serviceProvider, ILogger<Day03> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = new[] { "234208", "765869" };
            // DirectInput = new[] { "112233", "112233" };
            // DirectInput = new[] { "123444", "123444" };
            // DirectInput = new[] { "111122", "111122" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var input = inputs.Select(int.Parse).ToList();
            var start = input[0];
            var end = input[1];
            var count = 0;

            var allValuesIncrease = new IncreasingRule();
            var containsRepeatedValue = new RepeatedValueRule();
            for (var password = start; password <= end; password++)
            {
                if (allValuesIncrease.Check(password) && containsRepeatedValue.Check(password))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var range = inputs.Select(int.Parse).ToList();
            var start = range[0];
            var end = range[1];
            var count = 0;

            var allValuesIncrease = new IncreasingRule();
            var containsRepeatedValue = new SingleRepeatedValueRule();
            for (var password = start; password <= end; password++)
            {
                if (allValuesIncrease.Check(password) && containsRepeatedValue.Check(password))
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
