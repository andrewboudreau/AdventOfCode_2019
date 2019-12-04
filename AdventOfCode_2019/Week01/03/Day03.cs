using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    class Day03 : Day00
    {
        public Day03(IServiceProvider serviceProvider, ILogger<Day03> logger)
            : base(serviceProvider, logger)
        {
             DirectInput = new[] {"R8,U5,L5,D3", "U7,R6,D4,L4" };
            // DirectInput = new[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };
            // DirectInput = new[] { "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7" };
        }


        protected override string Solve(IEnumerable<string> inputs)
        {
            var input = inputs.ToArray();
            var wire1 = new Wire(input[0]);
            var wire2 = new Wire(input[1]);

            var distance = int.MaxValue;
            foreach (var (X, Y, Steps) in wire1.Intersections(wire2))
            {
                logger.LogInformation($"Found intersection ({Math.Abs(X)}, {Math.Abs(Y)})");
                distance = Math.Min(distance, Math.Abs(X) + Math.Abs(Y));
            }

            return distance.ToString();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var input = inputs.ToArray();
            var wire1 = new Wire(input[0]);
            var wire2 = new Wire(input[1]);

            var steps = int.MaxValue;
            foreach (var (X, Y, Steps) in wire1.Intersections(wire2))
            {
                logger.LogInformation($"Found intersection after {Steps} ({Math.Abs(X)}, {Math.Abs(Y)})");
                steps = Math.Min(steps, Steps);
            }

            return steps.ToString();
            // 14001 too high
        }
    }
}
