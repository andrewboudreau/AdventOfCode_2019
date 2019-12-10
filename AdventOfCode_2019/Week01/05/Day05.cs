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
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.Single().Split(',').Select(x => int.Parse(x.Trim()));
            var result = RunProgram(program);

            if (result != 16209841)
            {
                throw new InvalidOperationException($"Expected known correct answer '16,209,841' but returned '{result.ToString("N0")}'");
            }

            return result.ToString("N0");
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var program = inputs.Single().Split(',').Select(x => int.Parse(x.Trim()));

            return "N/A";
        }
        private int RunProgram(IEnumerable<int> program)
        {
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            cpu.Load(program);

            logger.LogDebug($"Start {cpu.DumpMemory()}");

            cpu.RunTillHalt();

            logger.LogDebug($"Executed {cpu.Steps} operations.");
            logger.LogDebug($"End {cpu.DumpMemory()}");

            return cpu.LastOutput;
        }
    }
}
