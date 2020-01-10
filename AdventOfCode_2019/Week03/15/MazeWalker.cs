using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using AdventOfCode_2019.Cpu;

namespace AdventOfCode_2019.Maze
{
    public class MazeWalker
    {
        private readonly IntCodeCpu cpu;

        public MazeWalker(IntCodeCpu cpu, BigInteger[] program)
        {
            Direction = Direction.Right;
            Position = (0, 0);

            Tiles = new Dictionary<(int X, int Y), MazeMovementOption>()
            {
                { (0, 0), new MazeMovementOption((0, 0), MazeTileType.Empty) }
            };

            this.cpu = cpu;
            this.cpu
                .Load(program)
                .Input(() => (int)DirectionLeft)
                .Output((Action<BigInteger>)this.HandleCpuOutput);
            // .AfterSteps(1000, _ => { RenderMap() });

        }

        public Dictionary<(int X, int Y), MazeMovementOption> Tiles { get; private set; }

        public Direction Direction { get; private set; }

        public (int X, int Y) Position { get; private set; }

        public void Turn()
        {
            Direction = Direction.TurnRight();
        }

        public void TurnLeft()
        {
            Direction = Direction.TurnLeft();
        }

        public Direction DirectionLeft
        {
            get
            {
                return Direction.TurnLeft();
            }
        }

        public (int X, int Y) Left()
        {
            (int X, int Y) = Direction switch
            {
                Direction.Up => (0, 1),
                Direction.Down => (0, -1),
                Direction.Left => (-1, 0),
                Direction.Right => (1, 0),
                _ => throw new NotImplementedException($"Forward should never be '{Direction}'"),
            };

            return (Position.X + X, Position.Y + Y);
        }

        public void HandleCpuOutput(BigInteger output)
        {
            var step = Left();

            if (output == 0)
            {
                AddTile(step, MazeTileType.Wall);
                Turn();
            }
            else if (output == 1 || output == 2)
            {
                Position = step;
                AddTile(Position, output);
                TurnLeft();
            }
            else if (output == 2)
            {
                throw new NotSupportedException("Completed");
            }
            else
            {
                throw new NotImplementedException($"Forward should never be '{step}'");
            }
        }

        public void Render()
        {
            var xOffset = 19;
            var yOffset = 20;

            Render(Tiles.Values.Select(x => x).ToList());

            Console.SetCursorPosition(Position.X + xOffset, Position.Y + yOffset);
            Console.WriteLine("?");
        }

        public MazeWalker Run()
        {
            cpu.Run();
            return this;
        }

        protected void AddTile((int X, int Y) position, BigInteger type)
        {
            AddTile(position, (MazeTileType)(int)type);
        }

        protected void AddTile((int X, int Y) position, MazeTileType type)
        {
            if (!Tiles.ContainsKey(position))
            {
                Tiles.Add(position, new MazeMovementOption(position, type));
            }
            else
            {
                if (Tiles[position].TileType != type)
                {
                    throw new InvalidOperationException($"Cannot update from {Tiles[position].TileType} to {type}.");
                }

                if (Tiles[position].Resolved++ > 30)
                {
                    Render();
                    throw new HaltException($"Resolved {position} {Tiles[position].Resolved} times");
                }
            }
        }

        public static IEnumerable<Direction> GetDirections()
        {
            yield return Direction.Up;
            yield return Direction.Right;
            yield return Direction.Down;
            yield return Direction.Left;
        }

        public static IEnumerable<(int X, int Y)> GetNeighbors((int X, int Y) position)
        {
            foreach (var direction in GetDirections())
            {
                yield return position.Forward(direction);
            }
        }

        public static IEnumerable<MazeMovementOption> GetNeighbors((int X, int Y) tile, List<MazeMovementOption> maze)
        {
            foreach (var (X, Y) in GetNeighbors(tile))
            {
                yield return maze.Single(m => m.Position.X == X && m.Position.Y == Y);
            }
        }

        public static void Render(List<MazeMovementOption> tiles)
        {
            var xOffset = 19;
            var yOffset = 20;

            Console.Clear();
            foreach (var tile in tiles)
            {
                Console.SetCursorPosition(tile.Position.X + xOffset, tile.Position.Y + yOffset);
                var print = tile.TileType switch
                {
                    MazeTileType.Wall => "▒",
                    MazeTileType.Empty => tile.Resolved.ToString().Last().ToString(),
                    MazeTileType.Goal => "@",
                    _ => throw new NotImplementedException($"No support for rendering {tile.TileType}"),
                };

                Console.Write(print);
            }

            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine("S");
        }
    }
}
