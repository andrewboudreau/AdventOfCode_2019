﻿using System;
using System.Linq;

namespace AdventOfCode_2019.Week01
{
    /// <summary>
    /// A line on a 2d plane.
    /// </summary>
    public struct WireSection
    {
        public WireSection((int X, int Y) start, string patch)
        {
            Value = patch;
            Length = int.Parse(patch.Substring(1));

            X1 = start.X;
            Y1 = start.Y;
            X2 = start.X;
            Y2 = start.Y;

            switch (patch.First())
            {
                case 'U':
                    Y2 = start.Y + Length;
                    break;

                case 'D':
                    Y2 = start.Y - Length;
                    break;

                case 'L':
                    X2 = start.X - Length;
                    break;

                case 'R':
                    X2 = start.X + Length;
                    break;

                default:
                    throw new NotImplementedException($"Unsupported patch type '{patch.First()}'");
            }
        }

        public string Value;
        public int Length;

        public int X1;
        public int Y1;
        public int X2;
        public int Y2;

        public bool Horizontal => X1 == X2;

        public bool Vertical => !Horizontal;

        /// <summary>
        /// Finds intersections of two wires.
        /// </summary>
        /// <param name="other">The <see cref="WireSection"/> to check for intersections.</param>
        /// <param name="pointOfIntersection">The X,Y coordinates of the intersection.</param>
        /// <returns>True if an intersection exists, false otherwise.</returns>
        public bool Intersects(WireSection other, out (int X, int Y) pointOfIntersection)
        {
            pointOfIntersection = (0, 0);
            if (X1 == X2)
            {
                var horizontal =
                    Math.Min(other.X1, other.X2) <= X1 && X1 <= Math.Max(other.X1, other.X2) &&
                    Math.Min(Y1, Y2) <= other.Y1 && other.Y1 <= Math.Max(Y1, Y2);

                if (horizontal)
                {
                    pointOfIntersection = (X1, other.Y1);
                }

                return horizontal;
            }

            if (Y1 == Y2)
            {
                var vertical =
                    Math.Min(X1, X2) <= other.X1 && other.X1 <= Math.Max(X1, X2) &&
                    Math.Min(other.Y1, other.Y2) <= Y2 && Y2 <= Math.Max(other.Y1, other.Y2);

                if (vertical)
                {
                    pointOfIntersection = (other.X1, Y2);
                }

                return vertical;
            }

            throw new InvalidOperationException($"{this} OTHER: {other}");
        }

        public override string ToString()
        {
            return $"Start: {X1}, {Y1}  End: {X2}, {Y2}";
        }
    }
}
