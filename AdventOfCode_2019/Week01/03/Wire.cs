using System;
using System.Collections.Generic;

namespace AdventOfCode_2019.Week01
{
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
            Current = (section.X2, section.Y2);
        }

        public IEnumerable<(int X, int Y, int Steps)> Intersections(Wire otherWire)
        {
            var otherSteps = 0;
            var mySteps = 0;

            foreach (var other in otherWire.Patches)
            {
                foreach (var mine in Patches)
                {
                    if (mine.Intersects(other, out var cross))
                    {
                        if (cross.X == 0 && cross.Y == 0)
                        {
                            continue;
                        }

                        var steps = otherSteps + mySteps;
                        steps += Math.Abs(mine.Horizontal ?
                             mine.X2 - cross.X :
                             mine.Y2 - cross.Y);

                        steps += Math.Abs(other.Horizontal ?
                            other.X2 - cross.X :
                            other.Y2 - cross.Y);

                        yield return (cross.X, cross.Y, steps);
                    }

                    mySteps += mine.Length;
                }

                otherSteps += other.Length;
                mySteps = 0;
            }
        }
    }
}
