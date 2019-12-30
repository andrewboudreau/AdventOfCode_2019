using System;
using System.Numerics;
using AdventOfCode_2019.Week01;

namespace AdventOfCode_2019.Arcade
{
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

            cpu.Reset().Load(program);
            
            SetAutoPlay();

            cpu.Run();

            return this;
        }

        public ArcadeCabinet SetAutoPlay()
        {
            CommandInputBuffer command = null;
            var outputs = 0;

            var joystick = new AutoPlayJoyStick(this);
            cpu.Input(joystick.Value);

            cpu.Output((int value) =>
            {
                if (command == null)
                {
                    command = CommandFactory.CreateCommandBuffer(value);
                }

                if (command.AddInput(value))
                {
                    if (command is UpdateTileCommand)
                    {
                        var cmd = command as UpdateTileCommand;
                        outputs++;

                        Console.SetCursorPosition(cmd.X, cmd.Y);
                        Console.Write(new Tile(Vector2.One, cmd.TileType).ToString());

                        var type = cmd.TileType;
                        if (type == TileType.Ball)
                        {
                            Ball = (int)command[0];
                        }
                        if (type == TileType.Paddle)
                        {
                            Paddle = (int)command[0];
                        }

                        var tile = VideoBuffer.UpdateTileMap(new Vector2(cmd.X, cmd.Y), cmd.TileType);
                        if (outputs > 1000 && outputs < 2000)
                        {
                            System.Threading.Thread.Sleep(8);
                        }
                    }
                    else if (command is UpdateScoreCommand)
                    {
                        Console.SetCursorPosition(0, 26);
                        Console.Write($"SCORE IS: {command[2]}");
                        HighScore = (int)command[2];
                    }

                    command = null;
                }
            });

            return this;
        }
    }
}
