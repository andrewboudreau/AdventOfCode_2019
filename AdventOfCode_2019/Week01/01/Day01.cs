using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    class Day01 : Day00
    {
        public Day01(IServiceProvider serviceProvider, ILogger<Day01> logger)
            : base(serviceProvider, logger)
        {
            // DirectInput = new[] { "1969" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var count = 0;
            int result = 0;
            foreach (var item in inputs)
            {
                result += FuelRequiredForLaunch(int.Parse(item));
                count++;
            }

            logger.LogInformation($"Counted {count} items.");
            return result.ToString();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var count = 0;
            int result = 0;
            foreach (var item in inputs)
            {
                result += FuelWithFuelRequired(int.Parse(item));
                count++;
            }

            return result.ToString();
        }

        private int FuelRequiredForLaunch(int mass)
        {
            var result = (mass / 3) - 2;
            return result;
        }

        private int FuelWithFuelRequired(int mass)
        {
            var total = 0;
            int fuel;

            while ((fuel = FuelRequiredForLaunch(mass)) > 0)
            {
                total += fuel;
                mass = fuel;
            }

            return total;
        }
    }
}
