using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class Day16 : Day00
    {
        public int[] Pattern = new int[] { 0, 1, 0, -1 };

        public Day16(IServiceProvider serviceProvider, ILogger<Day16> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = new[] { "12345678" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var numbers = inputs.Single().Select(x => int.Parse(x.ToString())).ToList();
            foreach (var phase in Enumerable.Range(1, 100))
            {
                var nextPhaseInput = new List<string>();
                for (var digit = 0; digit < numbers.Count; digit++)
                {
                    var enumerator = GeneratePattern(Pattern, digit).GetEnumerator();
                    var phaseTotal = 0;

                    foreach (var number in numbers)
                    {
                        enumerator.MoveNext();
                        phaseTotal += number * enumerator.Current;
                    }

                    nextPhaseInput.Add(Math.Abs(phaseTotal % 10).ToString());
                }

                logger.LogInformation($"{phase}: {string.Join("", nextPhaseInput)}");
                numbers = nextPhaseInput.Select(int.Parse).ToList();
            }

            return numbers.ToString();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var result = "N/A";
            return result.ToString();
        }

        public static IEnumerable<int> GeneratePattern(int[] pattern, int cycle = 1)
        {
            bool skip = true;
            while (true)
            {
                foreach (var value in pattern)
                {
                    for (var i = 0; i < cycle; i++)
                    {
                        if (skip)
                        {
                            skip = false;
                            continue;
                        }

                        yield return value;
                    }
                }
            }
        }
    }
}
