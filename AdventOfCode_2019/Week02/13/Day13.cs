using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using AdventOfCode_2019.Week01;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public class Day13 : Day00
    {
        public Day13(IServiceProvider serviceProvider, ILogger<Day13> logger)
            : base(serviceProvider, logger)
        {
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var arcade = new ArcadeCabinet(cpu, program);

            var blockTiles = arcade.VideoBuffer.Tiles.Values.Where(x => x.Type == TileType.Block);
            AssertExpectedResult(398, blockTiles.Count());

            return $"\r\nThe progam starts with {blockTiles.Count()} block tiles.";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            var arcade = new ArcadeCabinet(cpu, program).SetFreePlay();

            return $"{ arcade.HighScore}";
        }
    }

    public class ConsoleRenderer
    {
        private readonly VideoBuffer videoBuffer;

        public ConsoleRenderer(VideoBuffer videoBuffer)
        {
            this.videoBuffer = videoBuffer;
        }
        public void Render()
        {
            Console.Clear();
            foreach (var tile in videoBuffer.Tiles)
            {
                Console.SetCursorPosition((int)tile.Key.X, (int)tile.Key.Y);
                Console.Write(tile.Value);
            }
        }

        public void Update(Tile tile)
        {
            Console.SetCursorPosition((int)tile.Position.X, (int)tile.Position.Y);
            Console.Write(tile.ToString());
        }
    }

    public class VideoBuffer
    {
        public VideoBuffer()
        {
            Tiles = new Dictionary<Vector2, Tile>();
            Reset();
        }

        public Vector2 Boundary;

        public Dictionary<Vector2, Tile> Tiles { get; private set; }


        public Tile UpdateTileMap(Vector2 position, TileType tileType)
        {
            if (position.X < 0)
            {
                throw new InvalidOperationException($"X value cannot be less than 0, '{position.X}'");
            }

            if (position.Y < 0)
            {
                throw new InvalidOperationException($"Y value cannot be less than 0, '{position.Y}'");
            }

            if (position.X > Boundary.X)
            {
                Boundary.X = position.X;
            }

            if (position.Y > Boundary.Y)
            {
                Boundary.Y = position.Y;
            }

            if (!Tiles.ContainsKey(position))
            {
                Tiles.Add(position, new Tile(position, tileType));
            }
            else
            {
                Tiles[position].Type = tileType;
            }

            return new Tile(position, tileType);
        }

        public VideoBuffer Reset()
        {
            Boundary = Vector2.Zero;
            Tiles.Clear();

            return this;
        }
    }

    public class CommandInputBuffer
    {
        public CommandInputBuffer(int parameterCount)
        {
            ParameterCount = parameterCount;
            Parameters = new List<BigInteger>(parameterCount);
        }

        public BigInteger this[int index]
        {
            get { return Parameters[index]; }
        }

        public int ParameterCount { get; }

        public List<BigInteger> Parameters { get; set; }

        public bool AddInput(BigInteger input)
        {
            Parameters.Add(input);
            return ParameterCount == Parameters.Count;
        }

        public CommandInputBuffer Reset()
        {
            Parameters.Clear();
            return this;
        }
    }

    public enum CommandType
    {
        UpdateScore = -1,
        UpdateTileMap = 0,
    }

    public class CommandFactory
    {
        public static CommandInputBuffer CreateCommandBuffer(int firstCommandInput)
        {
            if ((CommandType)firstCommandInput == CommandType.UpdateScore)
            {
                return new UpdateScoreCommand();
            }
            else
            {
                return new UpdateTileCommand();
            }
        }
    }

    public class ArcadeCabinet
    {
        private readonly IntCodeCpu cpu;
        private readonly BigInteger[] program;

        public ArcadeCabinet(IntCodeCpu cpu, BigInteger[] program)
        {
            this.cpu = cpu;
            this.program = program;

            VideoBuffer = new VideoBuffer();
            Screen = new ConsoleRenderer(VideoBuffer);

            Reset();
        }

        public VideoBuffer VideoBuffer { get; }

        public ConsoleRenderer Screen { get; }
        public int Ball { get; internal set; }
        public int Paddle { get; internal set; }
        public int HighScore { get; internal set; }

        public ArcadeCabinet SetFreePlay()
        {
            // Memory address 0 represents the number of quarters that
            // have been inserted; set it to 2 to play for free.
            program.Patch(0, 2);
            Reset();

            return this;
        }

        public ArcadeCabinet Reset()
        {
            VideoBuffer.Reset();

            cpu.Reset()
                .Load(program)
                .UseArcadeCabinet(this)
                .Run();

            return this;
        }
    }

    public class JoyStick
    {
        public int Value()
        {
            /*
             * If the joystick is in the neutral position, provide 0.
             * If the joystick is tilted to the left, provide -1.
             * If the joystick is tilted to the right, provide 1.
             */

            return new Random().Next(-1, 1);
        }
    }

    public class Tile
    {
        public Tile(Vector2 position, TileType type)
        {
            Position = position;
            Type = type;
        }

        public Vector2 Position { get; }

        public TileType Type { get; set; }

        public override string ToString()
        {
            return Type switch
            {
                TileType.Empty => " ",
                TileType.Wall => "#",
                TileType.Block => "=",
                TileType.Paddle => "_",
                TileType.Ball => "@",
                _ => throw new NotSupportedException($"Invalid tile type '{Type}'."),
            };
        }
    }

    public enum TileType
    {
        [Description("An empty tile. No game object appears in this tile.")]
        Empty = 0,

        [Description("A wall tile. Walls are indestructible barriers.")]
        Wall = 1,

        [Description("A block tile. Blocks can be broken by the ball.")]
        Block = 2,

        [Description("a horizontal paddle tile. The paddle is indestructible.")]
        Paddle = 3,

        [Description("A ball tile. The ball moves diagonally and bounces off objects.")]
        Ball = 4
    }
}
