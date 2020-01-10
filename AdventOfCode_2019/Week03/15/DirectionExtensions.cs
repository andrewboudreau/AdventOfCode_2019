using System;
using System.Collections.Generic;

namespace AdventOfCode_2019
{
    public static class DirectionExtensions
    {
        public static IEnumerable<Direction> GetDirections()
        {
            yield return Direction.Up;
            yield return Direction.Right;
            yield return Direction.Down;
            yield return Direction.Left;
        }

        public static Direction TurnRight(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new NotImplementedException($"Forward should never be '{direction}'"),
            };
        }

        public static Direction TurnLeft(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Left,
                Direction.Right => Direction.Up,
                Direction.Down => Direction.Right,
                Direction.Left => Direction.Down,
                _ => throw new NotImplementedException($"Forward should never be '{direction}'"),
            };
        }

        public static (int X, int Y) Vector(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => (0, -1),
                Direction.Down => (0, 1),
                Direction.Left => (-1, 0),
                Direction.Right => (1, 0),
                _ => throw new NotImplementedException($"Forward should never be '{direction}'"),
            };
        }

        public static (int X, int Y) Forward(this (int X, int Y) position, Direction direction)
        {
            (int X, int Y) = direction.Vector();
            return (position.X + X, position.Y + Y);
        }
    }
}
