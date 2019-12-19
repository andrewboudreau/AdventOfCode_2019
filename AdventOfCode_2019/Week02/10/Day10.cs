using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public partial class Day10 : Day00
    {
        public Day10(IServiceProvider serviceProvider, ILogger<Day10> logger)
            : base(serviceProvider, logger)
        {
            //DirectInput = new[]
            //{
            //    "..##.",
            //    ".....",
            //    ".....",
            //    "#....",
            //    "....#"
            //};

            //DirectInput = new[]
            //{
            //    "#####",
            //    ".....",
            //    "#.#.#",
            //    ".....",
            //    "#####"
            //};

            //DirectInput = new[] { ".#..#", ".....", ".....", "....#", "....#" };

            //DirectInput = new[]
            //{
            //    // Best is 5,8 with 33 other asteroids detected.
            //    "......#.#.",
            //    "#..#.#....",
            //    "..#######.",
            //    ".#.#.###..",
            //    ".#..#.....",
            //    "..#....#.#",
            //    "#..#....#.",
            //    ".##.#..###",
            //    "##...#..#.",
            //    ".#....####"
            //};

            // var rock = field.Asteroids[205];
            //DirectInput = new[]
            //{
            //    // Best is 11,13 with 210 other asteroids detected
            //    ".#..##.###...#######",
            //    "##.############..##.",
            //    ".#.######.########.#",
            //    ".###.#######.####.#.",
            //    "#####.##.#.##.###.##",
            //    "..#####..#.#########",
            //    "####################",
            //    "#.####....###.#.#.##",
            //    "##.#################",
            //    "#####.##.###..####..",
            //    "..######..##.#######",
            //    "####.##.####...##..#",
            //    ".#####..#.######.###",
            //    "##...#.##########...",
            //    "#.##########.#######",
            //    ".####.#.###.###.#.##",
            //    "....##.##.###..#####",
            //    ".#.#.###########.###",
            //    "#.#.#.#####.####.###",
            //    "###.##.####.##.#..##"
            //};

            // Run from the file input.
            //DirectInput = null;
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var field = new AsteroidField(inputs);
            var visibleFromRock = new List<(Vector2 Coordinates, int VisibleRocks)>(field.Asteroids.Count);
            var slopes = new HashSet<Vector2>(field.Asteroids.Count);

            foreach (var rock in field.Asteroids)
            {
                foreach (var pair in field.Asteroids.MakePairs(rock))
                {
                    var slope = Slope(pair);
                    slopes.Add(slope);
                }

                visibleFromRock.Add((rock, slopes.Count));
                slopes.Clear();
            }

            var (Coordinates, VisibleRocks) = visibleFromRock.OrderByDescending(x => x.VisibleRocks).First();
            AssertExpectedResult("<14, 17>,260", $"{Coordinates},{VisibleRocks}");

            return $"{Coordinates} visible to {VisibleRocks:N0} other rocks";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var field = new AsteroidField(inputs);
            var visibleFromRock = new SortedDictionary<double, SortedDictionary<float, (Vector2, Vector2)>>();

            var rock = field.Asteroids.Where(x => x.X == 14 && x.Y == 17).Single();
            foreach (var pair in field.Asteroids.MakePairs(rock))
            {
                var length = Lengths(pair);
                var distance = Distance(pair);
                var quadrant = length.ToQuadrant();
                double degrees;

                if (quadrant == Quadrant.Boundary)
                {
                    degrees = (length.X == 0) ?
                        (length.Y < 0 ? 0 : 180) :
                        (length.X < 0 ? 270 : 90);
                }
                else
                {
                    double toa = Math.Abs(length.Y) / (double)Math.Abs(length.X);
                    var angle = Math.Atan(toa);
                    degrees = RadianToDegree(angle);

                    switch (quadrant)
                    {
                        case Quadrant.TopRight:
                            degrees = 90 - degrees;

                            break;
                        case Quadrant.TopLeft:
                            degrees = 270 + degrees;
                            break;

                        case Quadrant.BottomLeft:
                            degrees = 270 - degrees;
                            break;

                        case Quadrant.BottomRight:
                            degrees = 180 - degrees;
                            break;
                        default:
                            throw new NotSupportedException($"{quadrant.ToString()} is not supported to calculate angle.");
                    }
                }

                if (!visibleFromRock.ContainsKey(degrees))
                {
                    visibleFromRock.Add(degrees, new SortedDictionary<float, (Vector2, Vector2)>());
                }

                visibleFromRock[degrees].Add(distance, (pair.Other, length));
            }

            var key = visibleFromRock.Keys.ElementAt(199);
            return $"the 200th rock to be blasted is {visibleFromRock[key].First().Value.Item1}";
        }

        protected static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        protected static float Distance((Vector2, Vector2) pair)
        {
            return Vector2.Distance(pair.Item1, pair.Item2);
        }

        protected static Vector2 Lengths((Vector2, Vector2) pair)
        {
            return pair.Item1.Lengths(pair.Item2);
        }

        protected static Vector2 Slope((Vector2, Vector2) pair)
        {
            return pair.Item1.Slope(pair.Item2);
        }
    }
}
