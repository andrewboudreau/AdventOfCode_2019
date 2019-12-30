using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventOfCode_2019.Week01;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class Day15 : Day00
    {
        public Day15(IServiceProvider serviceProvider, ILogger<Day15> logger)
            : base(serviceProvider, logger)
        {

            ////DirectInput = new[] { "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99" };
            ////DirectInput = new[] { "1102,34915192,34915192,7,4,7,99,0" };
            ////DirectInput = new[] { "104,1125899906842624,99" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var maze = new MazeWalker(cpu, program);
            maze.Run();

            return maze.ToString();
        }


        protected override string Solve2(IEnumerable<string> inputs)
        {
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var program = inputs.First().ToProgram();

            var maze = new MazeWalker(cpu, program);
            var result = cpu.LastOutput;

            return result.ToString();
        }
    }

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
            Direction = Direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new NotImplementedException($"Foward should never be '{Direction}'"),
            };
        }

        public void TurnLeft()
        {
            Direction = Direction switch
            {
                Direction.Up => Direction.Left,
                Direction.Right => Direction.Up,
                Direction.Down => Direction.Right,
                Direction.Left => Direction.Down,
                _ => throw new NotImplementedException($"Foward should never be '{Direction}'"),
            };
        }

        public Direction DirectionLeft
        {
            get
            {
                return Direction switch
                {
                    Direction.Up => Direction.Left,
                    Direction.Right => Direction.Up,
                    Direction.Down => Direction.Right,
                    Direction.Left => Direction.Down,
                    _ => throw new NotImplementedException($"Foward should never be '{Direction}'"),
                };
            }
        }

        public (int X, int Y) Forward
        {
            get
            {
                (int X, int Y) = Direction switch
                {
                    Direction.Up => (0, 1),
                    Direction.Down => (0, -1),
                    Direction.Left => (-1, 0),
                    Direction.Right => (1, 0),
                    _ => throw new NotImplementedException($"Foward should never be '{Direction}'"),
                };

                return (Position.X + X, Position.Y + Y);
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
                _ => throw new NotImplementedException($"Foward should never be '{Direction}'"),
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
                AddTile(Position, (MazeTileType)(int)output);
                TurnLeft();
            }
            else if (output == 2)
            {
                throw new NotSupportedException("Completed");
            }
            else
            {
                throw new NotImplementedException($"Foward should never be '{step}'");
            }
        }

        public void Render()
        {
            var xOffset = 19;
            var yOffset = 20;

            Console.Clear();
            foreach (var tile in Tiles.Values)
            {
                Console.SetCursorPosition(tile.Position.X + xOffset, tile.Position.Y + yOffset);
                var print = tile.TileType switch
                {
                    MazeTileType.Wall => "▒",
                    MazeTileType.Empty => "▓",
                    MazeTileType.Goal => "@",
                    _ => throw new NotImplementedException($"No support for rendering {tile.TileType}"),
                };

                Console.Write(print);
            }

            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine("S");

            Console.SetCursorPosition(Position.X + xOffset, Position.Y + yOffset);
            Console.WriteLine("?");
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

            var print = Tiles.Count() % 500 == 0;
            if (print)
            { Render(); }

        }

        internal void Run()
        {
            cpu.Run();
        }
    }

    public class MazeMovementOption
    {
        public MazeMovementOption((int X, int Y) position, MazeTileType tileType)
        {
            Position = position;
            TileType = tileType;
        }

        public int Resolved { get; set; }

        public (int X, int Y) Position { get; private set; }

        public MazeTileType TileType { get; private set; }

        public override string ToString()
        {
            return $"{TileType} Resolved:{Resolved}";
        }
    }

    public class MazeTile
    {
        public MazeTile()
        {
            Neighbors = new Dictionary<Direction, MazeTileType>()
            {
                {Direction.Up, MazeTileType.Unknown },
                {Direction.Down, MazeTileType.Unknown },
                {Direction.Left, MazeTileType.Unknown },
                {Direction.Right, MazeTileType.Unknown }
            };
        }

        public (int X, int Y) Position { get; }

        public Dictionary<Direction, MazeTileType> Neighbors { get; private set; }
    }

    public enum MazeTileType
    {
        Unknown = -1,

        [ConsolePrint(Day00.FullBlock)]
        Wall = 0,

        [ConsolePrint(".")]
        Empty = 1,

        [ConsolePrint("@")]
        Goal = 2
    }

    public sealed class ConsolePrintAttribute : Attribute
    {
        public ConsolePrintAttribute(string print)
        {
            Print = print;
        }

        public string Print { get; }
    }
}
