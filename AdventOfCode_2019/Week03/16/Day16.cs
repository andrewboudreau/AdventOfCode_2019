using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class Day16 : Day00
    {
        public static int[] Pattern = new int[] { 0, 1, 0, -1 };

        public Day16(IServiceProvider serviceProvider, ILogger<Day16> logger)
            : base(serviceProvider, logger)
        {
            // DirectInput = new[] { "19617804207202209144916044189917" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var numbers = inputs.Single().Select(x => int.Parse(x.ToString())).ToArray();

            foreach (var phase in Enumerable.Range(1, 100))
            {
                numbers = FFTransform(numbers);
            }

            AssertExpectedResult("63794407", string.Join("", numbers.Take(8)));
            return string.Join("", numbers);
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var data = inputs.Single().Select(x => int.Parse(x.ToString())).ToList();
            var offset = int.Parse(string.Join("", data.Take(7).ToList()));

            var numbers = RepeatPattern(data, 10_000).ToArray();

            foreach (var phase in Enumerable.Range(1, 100))
            {
                numbers = FFTransform_Optimized(numbers, offset);
            }

            var result = string.Join("", numbers.Skip(offset).Take(8));
            AssertExpectedResult("77247538", result);
            return result.ToString();
        }

        public static IEnumerable<int> GeneratePattern(int[] pattern, int cycle = 0)
        {
            cycle += 1;
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

        public static IEnumerable<int> RepeatPattern(List<int> pattern, int repeats = 1)
        {
            foreach (var repeat in Enumerable.Range(1, repeats))
            {
                foreach (var digit in pattern)
                {
                    yield return digit;
                }
            }
        }

        public static int[] FFTransform(int[] numbers)
        {
            for (var digit = 0; digit < numbers.Length; digit++)
            {
                var enumerator = GeneratePattern(Pattern, digit).GetEnumerator();
                var phaseTotal = 0;

                foreach (var number in numbers)
                {
                    enumerator.MoveNext();
                    phaseTotal += number * enumerator.Current;
                }

                numbers[digit] = Math.Abs(phaseTotal) % 10;
            }

            return numbers;
        }

        public static int[] FFTransform_Optimized(int[] numbers, int offset)
        {
            var phaseTotal = 0;
            for (var digit = numbers.Length - 1; digit >= offset; digit--)
            {
                phaseTotal = (phaseTotal + numbers[digit]) % 10;
                numbers[digit] = phaseTotal;
            }

            return numbers;
        }
    }
}
