using System;
using System.Collections.Generic;

namespace AdventOfCode_2019.Week01
{
    /// <summary>
    /// Contains a collection <see cref="WireSection"/> that are connected end-to-end on a 2d grid.
    /// </summary>
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

        List<WireSection> Sections { get; } = new List<WireSection>();

        public void AddPatch(string patch)
        {
            var section = new WireSection(Current, patch);
            Sections.Add(section);
            Current = (section.X2, section.Y2);
        }

        public IEnumerable<(int X, int Y, int Steps)> Intersections(Wire otherWire)
        {
            var otherSteps = 0;
            var mySteps = 0;

            foreach (var other in otherWire.Sections)
            {
                foreach (var mine in Sections)
                {
                    if (mine.Intersects(other, out var pointOfIntersection))
                    {
                        if (pointOfIntersection.X == 0 && pointOfIntersection.Y == 0)
                        {
                            continue;
                        }

                        var myPartial = Math.Abs(
                            mine.Horizontal ?
                                mine.Y1 - pointOfIntersection.Y :
                                mine.X1 - pointOfIntersection.X);

                        var otherPartial = Math.Abs(
                            other.Horizontal ?
                                other.Y1 - pointOfIntersection.Y :
                                other.X1 - pointOfIntersection.X);

                        var steps = otherSteps + otherPartial + mySteps + myPartial;
                        yield return (pointOfIntersection.X, pointOfIntersection.Y, steps);
                    }

                    mySteps += mine.Length;
                }

                otherSteps += other.Length;
                mySteps = 0;
            }
        }
    }
}
