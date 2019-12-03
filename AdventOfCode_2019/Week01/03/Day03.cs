using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    class Day03 : Day00
    {
        public Day03(IServiceProvider serviceProvider, ILogger<Day03> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = new[] { "R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83" };
        }


        protected override string Solve(IEnumerable<string> inputs)
        {
            var input = inputs.ToArray();
            var wire1 = new Wire(input[0]);
            var wire2 = new Wire(input[1]);

            return "N/A";

        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            return "N/A";
        }
    }

    public class Wire
    {
        public Wire(string wire)
        {
            foreach (var patch in wire.Split(','))
            {
                AddPatch(patch);
            }
        }

        (int X, int Y) Current { get; set; }

        List<Patch> Patches { get; } = new List<Patch>();

        public void AddPatch(string patch)
        {
            var section = new Patch(Current, patch);
            Patches.Add(section);
            Current = section.End;
        }

        public IEnumerable<(int X, int Y)> Intersections(Wire otherWire)
        {
            var occupied =
            foreach (var patch in otherWire.Patches)
            {

            }
        }
    }

    public struct Patch
    {
        public Patch((int X, int Y) start, string patch)
        {
            Start = start;
            End = start;
            Value = patch;

            void swap()
            {
                var temp = Start;
                var Start = End;
                var End = temp;
            }
            var length = int.Parse(patch.Substring(1));
            switch (patch.First())
            {
                case 'U':
                    End.Y += length;
                    IsVertical = true;
                    break;

                case 'D':
                    End.Y -= length;
                    IsVertical = true;
                    swap();
                    break;

                case 'L':
                    End.X -= length;
                    IsVertical = false;
                    swap();
                    break;

                case 'R':
                    End.X += length;
                    IsVertical = false;
                    break;

                default:
                    throw new NotImplementedException($"Unsupported patch type '{patch.First()}'");
            }
        }

        public (int X, int Y) Start;
        public (int X, int Y) End;
        public string Value;
        public bool IsVertical;

        public bool Intersects(Patch patch, out (int X, int Y) cross)
        {
            cross = (0, 0);

            if (IsVertical == patch.IsVertical)
            {
                return false;
            }

            if (IsVertical)
            {
                var bottom = Start.Y;
                var top = End.Y;

                if (patch.Start.X <= Start.X && patch.Start.X >= Start.X)
                {
                    if (Start.Y <= patch.Start.Y && End.Y <= patch.Start.Y)
                    {

                    }
                }
                else
                {
                    return false;
                }
                var left = Start.X;
                var right = End.Y;
            }
            else
            {

            }
        }

        public override string ToString()
        {
            return $"Start: {Start.X}, {Start.Y}  End: {End.X}, {End.Y}";
        }
    }
}
