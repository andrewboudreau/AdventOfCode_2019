using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    class Day02 : Day00
    {
        public Day02(IServiceProvider serviceProvider, ILogger<Day02> logger)
            : base(serviceProvider, logger)
        {
            // DirectInput = new[] { "1,0,0,0,99" };
            // DirectInput = new[] { "2,3,0,3,99" };
            // DirectInput = new[] { "1,9,10,3,2,3,11,0,99,30,40,50" };
            // DirectInput = new[] { "1,1,1,4,99,5,6,0,99" };
            // DirectInput = new[] { "2,4,4,5,99,0" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.Single().Split(',').Select(x => int.Parse(x.Trim()));
            var result = RunProgram(program, 12, 2);

            if (result != 5866714)
            {
                throw new InvalidOperationException($"Expected known correct answer '5,866,714' but returned '{result.ToString("N0")}'");
            }

            return result.ToString();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var program = inputs.Single().Split(',').Select(x => int.Parse(x.Trim()));
            var target = 19690720;

            logger.LogInformation("Starting search for target output '{target}'");

            for (var noun = 52; noun < 100; noun++)
            {
                for (var verb = 8; verb < 100; verb++)
                {
                    if (target == RunProgram(program, noun, verb))
                    {
                        return $"Noun: {noun} Verb: {verb} (100 * noun + verb) = {100 * noun + verb}";
                    }
                }
            }

            // Solution Part 2: Noun: 52 Verb: 8
            throw new InvalidOperationException($"Solution Part 2: Noun: 52 Verb: 8 but No noun/verb combination returned {target} target value.");
        }

        private int RunProgram(IEnumerable<int> program, int noun, int verb)
        {
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            cpu.Load(program)
                .Patch(1, noun)
                .Patch(2, verb);

            logger.LogDebug($"Start {cpu.DumpMemory()}");

            cpu.Run();

            logger.LogDebug($"Executed {cpu.Steps} operations.");
            logger.LogDebug($"End {cpu.DumpMemory()}");

            return (int)cpu[0];
        }
    }
}
