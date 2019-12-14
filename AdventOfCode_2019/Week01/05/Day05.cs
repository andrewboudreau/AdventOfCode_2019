using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    class Day05 : Day00
    {
        public Day05(IServiceProvider serviceProvider, ILogger<Day02> logger)
            : base(serviceProvider, logger)
        {
            // DirectInput = new[] { "1002,4,3,4,33" };
            // DirectInput = new[] { "1101,100,-1,4,0" };
            // DirectInput = new[] { "3,0,4,0,99" };

            // Using position mode, consider whether the input is equal to 8; output 1 (if it is) or 0 (if it is not).
            // DirectInput = new[] { "3,9,8,9,10,9,4,9,99,-1,8" };

            // Using position mode, consider whether the input is less than 8; output 1 (if it is) or 0 (if it is not).
            // DirectInput = new[] { "3,9,7,9,10,9,4,9,99,-1,8" };

            // Using immediate mode, consider whether the input is equal to 8; output 1(if it is) or 0(if it is not).
            // DirectInput = new[] { "3,3,1108,-1,8,3,4,3,99" };

            // Using immediate mode, consider whether the input is less than 8; output 1(if it is) or 0(if it is not). 
            // DirectInput = new[] { "3,3,1107,-1,8,3,4,3,99" };

            /*
             * Here are some jump tests that take an input, then output 0 if the input was zero or 1 if the input was non-zero:
             */
            // position output 0 if the input was zero or 1 if the input was non - zero:
            // DirectInput = new[] { "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9" };

            // immediate output 0 if the input was zero or 1 if the input was non - zero:
            //DirectInput = new[] { "3,3,1105,-1,9,1101,0,0,12,4,12,99,1 " };

            /*
             * The above example program uses an input instruction to ask for a single number. 
             * The program will then output 999 if the input value is below 8, output 1000 if the
             * input value is equal to 8, or output 1001 if the input value is greater than 8.
             */
            //DirectInput = new[] { "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99"};
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.Single().Split(',').Select(x => int.Parse(x.Trim()));
            var result = RunProgram(program, 1);

            AssertExpectedResult(16209841, result);
            return result.ToString("N0");
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var program = inputs.Single().Split(',').Select(x => int.Parse(x.Trim()));

            var result = RunProgram(program, 5);
            AssertExpectedResult(8834787, result);

            return result.ToString("N0");
        }

        private int RunProgram(IEnumerable<int> program, int input)
        {
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            cpu.Load(program)
                .UseConstantValueForInput(input)
                .UseLoggingForOutput();

            logger.LogDebug($"Start {cpu.DumpMemory()}");

            cpu.Run();

            logger.LogDebug($"Executed {cpu.Steps} operations.");
            logger.LogDebug($"End {cpu.DumpMemory()}");

            return (int)cpu.LastOutput;
        }
    }
}
