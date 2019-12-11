using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    partial class Day06 : Day00
    {
        public Day06(IServiceProvider serviceProvider, ILogger<Day06> logger)
            : base(serviceProvider, logger)
        {
            // DirectInput = new[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" };
            // DirectInput = new[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L", "K)YOU", "I)SAN" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var orbits = inputs.Select(ParseOrbits);
            var orbitMap = new OrbitMap<string>(orbits);
            int result = orbitMap.CountDirectAndIndirectOrbits();

            var expectedAnswer = 227612;
            if (result != expectedAnswer)
            {
                throw new InvalidOperationException($"Expected known correct answer '{expectedAnswer:N0}' but returned '{result:N0}'");
            }

            return result.ToString("N0");
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var orbits = inputs.Select(ParseOrbits);
            var orbitMap = new OrbitMap<string>(orbits);
            var result = orbitMap.CountOribitalTransfersRequired("SAN", "YOU");
            
            var expectedAnswer = 454;
            if (result != expectedAnswer)
            {
                throw new InvalidOperationException($"Expected known correct answer '{expectedAnswer:N0}' but returned '{result:N0}'");
            }

            return result.ToString("N0");
        }

        private (string, string) ParseOrbits(string orbit)
        {
            var parts = orbit.Split(")");
            return (parts[0], parts[1]);
        }
    }
}
