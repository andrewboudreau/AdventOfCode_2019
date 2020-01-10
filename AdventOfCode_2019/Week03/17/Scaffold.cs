using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2019
{
    public partial class Day17
    {
        public class Scaffold : IEnumerable<(int X, int Y, char Tile)>
        {
            private readonly char[,] map;

            private int steps = 0;

            public Scaffold(char[,] map)
            {
                this.map = map;
                var (X, Y, Tile) = this.Single(x => x.Tile == '^');

                Position = (X, Y);
                Direction = Direction.Up;
            }

            public List<string> Steps { get; } = new List<string>();

            public (int X, int Y) Position { get; set; }

            public Direction Direction { get; set; }

            public char Map((int X, int Y) position)
            {
                if (position.X >= map.GetLength(1) || position.X < 0 || position.Y >= map.GetLength(0) || position.Y < 0)
                {
                    return '.';
                }

                return map[position.Y, position.X];
            }

            public void WalkMap()
            {
                while (Turn())
                {
                    while (MoveForward()) { }
                }
            }

            public IEnumerator<(int X, int Y, char Tile)> GetEnumerator()
            {
                return new TwoDimensionalArrayEnumerator<char>(map);
            }

            protected static bool IsTileTraversable(char tile)
            {
                return tile == '#' || tile == 'O';
            }

            protected bool Turn()
            {
                if (CanMoveLeft())
                {
                    Direction = Direction.TurnLeft();
                    Steps.Add("L");
                    return true;
                }
                else if (CanMoveRight())
                {
                    Direction = Direction.TurnRight();
                    Steps.Add("R");
                    return true;
                }

                return false;
            }

            protected bool MoveForward()
            {
                if (CanMoveForward())
                {
                    Position = Position.Forward(Direction);
                    steps++;
                    return true;
                }

                Steps.Add(steps.ToString());
                steps = 0;
                return false;
            }

            protected bool CanMoveForward()
            {
                var step = Position.Forward(Direction);
                return IsTileTraversable(Map(step));
            }

            protected bool CanMoveLeft()
            {
                var step = Position.Forward(Direction.TurnLeft());
                return IsTileTraversable(Map(step));
            }

            protected bool CanMoveRight()
            {
                var step = Position.Forward(Direction.TurnRight());
                return IsTileTraversable(Map(step));
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
