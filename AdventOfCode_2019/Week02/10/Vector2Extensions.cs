using System;
using System.Numerics;

namespace AdventOfCode_2019
{
    public static class Vector2Extensions
    {
        public static Quadrant ToQuadrant(this Vector2 slope)
        {
            var Rise = slope.Y;
            var Run = slope.X;

            if (Rise == 0 || Run == 0)
            {
                return Quadrant.Boundary;
            }

            if (Rise > 0 && Run > 0)
            {
                return Quadrant.BottomRight;
            }

            if (Rise < 0 && Run > 0)
            {
                return Quadrant.TopRight;
            }

            if (Rise > 0 && Run < 0)
            {
                return Quadrant.BottomLeft;
            }

            if (Rise < 0 && Run < 0)
            {
                return Quadrant.TopLeft;
            }

            throw new NotImplementedException($"{Rise} and {Run} don't define a quadrant.");
        }

        public static Vector2 Lengths(this Vector2 one, Vector2 two)
        {
            int rise = (int)two.Y - (int)one.Y;
            int run = (int)two.X - (int)one.X;

            return new Vector2(run, rise);
        }

        public static Vector2 Slope(this Vector2 one, Vector2 two)
        {
            int rise = (int)two.Y - (int)one.Y;
            int run = (int)two.X - (int)one.X;
            int gcd = GreatestCommonDenominator(new Vector2(run, rise));

            return new Vector2(run / gcd, rise / gcd);
        }

        public static int GreatestCommonDenominator(Vector2 slope)
        {
            var rise = Math.Abs(slope.Y);
            var run = Math.Abs(slope.X);

            while (rise != 0 && run != 0)
            {
                if (rise > run)
                {
                    rise %= run;
                }
                else
                {
                    run %= rise;
                }
            }

            if (rise == 0)
            {
                return (int)run;
            }
            else
            {
                return (int)rise;
            }
        }
    }
}
