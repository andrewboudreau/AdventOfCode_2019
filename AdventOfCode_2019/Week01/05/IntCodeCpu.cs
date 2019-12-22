using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class IntCodeCpu
    {
        private readonly ILogger logger;
        private readonly IntCodeCpuMemory memory;

        private int pc;
        private int steps;

        public IntCodeCpu(ILogger logger, IntCodeCpuMemory memory)
        {
            this.logger = logger;
            this.memory = memory;
        }

        public BigInteger this[int index]
        {
            get
            {
                return memory[index];
            }
        }

        public long Steps => steps;

        public BigInteger LastOutput { get; private set; }

        public Func<BigInteger> ReadInputValue { get; private set; }

        public Action<BigInteger> WriteOutputValue { get; private set; }

        public IntCodeCpu Load(BigInteger[] program)
        {
            Reset();
            memory.Load(program);

            logger.LogTrace($"Loaded program {program.Length:N0} bytes.");
            return this;
        }

        public IntCodeCpu Patch(int index, int value)
        {
            logger.LogDebug($"Patched program at Memory[{index}] = '{value}' from '{memory[index]}'");

            memory[index] = value;
            return this;
        }

        public IntCodeCpu Continue(BigInteger value)
        {
            ReadInputValue = () => value;
            return this;
        }

        public IntCodeCpu Reset()
        {
            pc = 0;
            steps = 0;
            memory.Reset();

            return this;
        }

        public bool Step()
        {
            try
            {
                var instruction = new Instruction(pc, memory);
                logger.LogTrace(instruction.Decompile());

                var increment = Execute(instruction);
                pc += increment;
                steps += 1;
            }
            catch (HaltException halt)
            {
                logger.LogTrace(halt.Message);
                return false;
            }

            return true;
        }

        public int Execute(Instruction instruction)
        {
            BigInteger result;

            switch (instruction.Operation)
            {
                case Operations.Add:
                    result = instruction.ReadOperand1(memory) + instruction.ReadOperand2(memory);
                    instruction.WriteOperand3(result, memory);
                    break;

                case Operations.Multiply:
                    result = instruction.ReadOperand1(memory) * instruction.ReadOperand2(memory);
                    instruction.WriteOperand3(result, memory);
                    break;

                case Operations.Input:
                    BigInteger input;
                    if (ReadInputValue == null)
                    {
                        ////throw new InvalidOperationException("ReadInputValue should not be null");
                        Console.Write("Input a value: ");
                        input = int.Parse(Console.ReadLine());
                        Console.WriteLine(input.ToString());
                    }
                    else
                    {
                        input = ReadInputValue();
                    }

                    instruction.WriteOperand1(input, memory);
                    break;

                case Operations.Output:
                    LastOutput = instruction.ReadOperand1(memory);
                    WriteOutputValue?.Invoke(LastOutput);
                    break;

                /*
                 * 
                 * Opcode 5 is jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                 * Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
               */

                case Operations.JumpIfTrue:
                    if (instruction.ReadOperand1(memory) != 0)
                    {
                        pc = (int)instruction.ReadOperand2(memory);
                        return 0;
                    }

                    break;

                case Operations.JumpIfFalse:
                    if (instruction.ReadOperand1(memory) == 0)
                    {
                        pc = (int)instruction.ReadOperand2(memory);
                        return 0;
                    }
                    break;

                /*
                 * Opcode 7 is less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                 * Opcode 8 is equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                 */
                case Operations.Less_Than:
                    if (instruction.ReadOperand1(memory) < instruction.ReadOperand2(memory))
                    {
                        instruction.WriteOperand3(1, memory);
                    }
                    else
                    {
                        instruction.WriteOperand3(0, memory);
                    }
                    break;

                case Operations.Equals:
                    if (instruction.ReadOperand1(memory) == instruction.ReadOperand2(memory))
                    {
                        instruction.WriteOperand3(1, memory);
                    }
                    else
                    {
                        instruction.WriteOperand3(0, memory);
                    }
                    break;

                case Operations.SetRelativeBase:
                    memory.SetRelativeBase((int)instruction.ReadOperand1(memory));
                    break;

                case Operations.Halt:
                    throw new HaltException($"Program Halted with M[0]={memory[0].ToString("N0")}");

                default:
                    throw new InvalidOperationException($"Unknown Operation '{instruction.Operation}'.");
            }

            return instruction.Size;
        }

        public void Run()
        {
            while (Step()) { }
        }

        public IntCodeCpu UseRobot(Robot robot)
        {
            ReadInputValue = () => new BigInteger(robot.Panels[robot.Location].Black ? 0 : 1);
            WriteOutputValue = value => robot.AcceptCommand((int)value);
            return this;
        }

        internal IntCodeCpu UseArcadeCabinet(ArcadeCabinet arcade)
        {
            var joystick = new JoyStick();
            ReadInputValue = () =>
            {
                if (arcade.Ball == arcade.Paddle)
                {
                    return 0;
                }
                else if (arcade.Ball > arcade.Paddle)
                {
                    return 1;
                }
                else if (arcade.Ball< arcade.Paddle)
                {
                    return -1;
                }
                return 0;
            };

            CommandInputBuffer command = null;
            var outputs = 0;

            WriteOutputValue = value =>
            {
                if (command == null)
                {
                    command = CommandFactory.CreateCommandBuffer((int)value);
                }

                if (command.AddInput(value))
                {
                    if (command is UpdateTileCommand)
                    {
                        outputs++;

                        Console.SetCursorPosition((int)command[0], (int)command[1]);
                        Console.Write(new Tile(Vector2.One, (TileType)(int)command[2]).ToString());

                        var type = (TileType)(int)command[2];
                        if (type == TileType.Ball)
                        {
                            arcade.Ball = (int)command[0];
                        }
                        if (type == TileType.Paddle)
                        {
                            arcade.Paddle = (int)command[0];
                        }

                        // var tile = arcade.VideoBuffer.UpdateTileMap(position, (TileType)(int)command[2]);

                    }
                    else if (command is UpdateScoreCommand)
                    {
                        Console.SetCursorPosition(0,28);
                        Console.Write($"SCORE IS: {command[2]}");
                    }

                    command = null;
                }
            };

            return this;
        }

        public IntCodeCpu UseConstantValueForInput(BigInteger value)
        {
            ReadInputValue = () => value;
            return this;
        }

        public IntCodeCpu UseSequenceForInput(params BigInteger[] value)
        {
            var i = 0;
            ReadInputValue = () =>
            {
                logger.LogTrace($"Input: {value[i % value.Length]:N0}");
                return value[i++ % value.Length];
            };

            return this;
        }

        public IntCodeCpu UseInitialValueThenOutputs(BigInteger value, IntCodeCpu inputCpu)
        {
            var first = false;
            ReadInputValue = () =>
            {
                if (first)
                {
                    logger.LogTrace($"Input: {value:N0}");
                    return value;
                }

                return inputCpu.LastOutput;
            };

            return this;
        }

        public IntCodeCpu UseBufferForOutput(List<BigInteger> output)
        {
            WriteOutputValue = value =>
            {
                logger.LogTrace($"Output : {value:N0}");
                output.Add(value);
            };

            return this;
        }

        public IntCodeCpu UseLoggingForOutput()
        {
            WriteOutputValue = (value) => logger.LogCritical($"Output: {value}");
            return this;
        }

        public string DumpMemory()
        {
            return string.Join(',', memory);
        }
    }

    public static class InstructionSpecifications
    {

    }
}