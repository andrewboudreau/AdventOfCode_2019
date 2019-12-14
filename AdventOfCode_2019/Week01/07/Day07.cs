using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class Day07 : Day00
    {
        public Day07(IServiceProvider serviceProvider, ILogger<Day07> logger)
            : base(serviceProvider, logger)
        {
            //DirectInput = new[] { "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "4,3,2,1,0" };
            //DirectInput = new[] { "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", "0,1,2,3,4" };
            //DirectInput = new[] { "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", "1,0,4,3,2" };
            // DirectInput = new[] { "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            return "n/a";
            var count = 0;
            var program = inputs.First().IntegersFromCsv();

            var settings = new int[] { 0, 1, 2, 3, 4 };
            (int MaxThrust, int[] Inputs) result = (0, new int[5]);

            var amps = CreateCpuArray().LoadProgram(program);
            foreach (var setting in settings.Permutations())
            {
                count++;
                var current = amps.CalculateThrust(setting);
                if (result.MaxThrust < current)
                {
                    result.MaxThrust = current;
                    setting.CopyTo(result.Inputs.AsMemory());
                    logger.LogInformation($"Max Thrust:{result.MaxThrust:N0} Settings:{string.Join(", ", result.Inputs)} after {count} evaluations");
                }

                amps.Reset();
            }

            logger.LogInformation($"Completed {count} permutations");

            var msg = $"Max Thrust:{result.MaxThrust:N0} Settings:{string.Join(", ", result.Inputs)} after {count} evaluations";
            logger.LogInformation(msg);
            SaveToFile(msg);

            return msg;
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var count = 0;
            var program = inputs.First().IntegersFromCsv();

            var settings = new int[] { 5, 6, 7, 8, 9 };
            (int MaxThrust, int[] Inputs) result = (0, new int[5]);

            var amps = CreateCpuArray().LoadProgram(program);
            foreach (var setting in settings.Permutations())
            {
                count++;
                var current = amps.CalculateFeedbackThrust(setting);
                if (result.MaxThrust < current)
                {
                    result.MaxThrust = current;
                    setting.CopyTo(result.Inputs.AsMemory());
                    logger.LogInformation($"Max Thrust:{result.MaxThrust:N0} Settings:{string.Join(", ", result.Inputs)} after {count} evaluations");
                }

                amps.Reset();
            }

            logger.LogInformation($"Completed {count} permutations");

            var msg = $"Max Thrust:{result.MaxThrust:N0} Settings:{string.Join(", ", result.Inputs)} after {count} evaluations";
            logger.LogInformation(msg);
            SaveToFile(msg);

            return msg;
        }

        private IntCodeCpu[] CreateCpuArray()
        {
            return new[] {
                ServiceProvider.GetRequiredService<IntCodeCpu>(),
                ServiceProvider.GetRequiredService<IntCodeCpu>(),
                ServiceProvider.GetRequiredService<IntCodeCpu>(),
                ServiceProvider.GetRequiredService<IntCodeCpu>(),
                ServiceProvider.GetRequiredService<IntCodeCpu>(),
            };
        }
    }

    public static class IntCodeCpuExtensions
    {
        public static int CalculateThrust(this IntCodeCpu[] amps, params int[] settings)
        {
            if (settings.Length != amps.Length)
            {
                throw new InvalidOperationException($"{settings.Length} settings must equal the numbers of amps, {amps.Length}.");
            }

            var buffer = new List<BigInteger>() { 0 };
            for (var i = 0; i < amps.Length; i++)
            {
                amps[i]
                    .UseSequenceForInput(settings[i], buffer.Last())
                    .UseBufferForOutput(buffer)
                    .Run();
            }

            return (int)amps[^1].LastOutput;
        }

        public static int CalculateFeedbackThrust(this IntCodeCpu[] amps, params int[] settings)
        {
            if (settings.Length != amps.Length)
            {
                throw new InvalidOperationException($"{settings.Length} settings must equal the numbers of amps, {amps.Length}.");
            }

            var buffer = new List<BigInteger>() { 0 };
            for (var i = 0; i < amps.Length; i++)
            {
                amps[i]
                    .UseSequenceForInput(settings[i], buffer.Last())
                    .UseBufferForOutput(buffer)
                    .Run();
            }

            return (int)amps[^1].LastOutput;
        }
        public static IntCodeCpu[] LoadProgram(this IntCodeCpu[] amps, int[] program)
        {
            for (var i = 0; i < amps.Length; i++)
            {
                amps[i].Load(program);
            }

            return amps;
        }

        public static IntCodeCpu[] Reset(this IntCodeCpu[] amps)
        {
            for (var i = 0; i < amps.Length; i++)
            {
                amps[i].Reset();
            }

            return amps;
        }
    }
}
