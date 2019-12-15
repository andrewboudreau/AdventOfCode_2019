using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class Day09 : Day00
    {
        public Day09(IServiceProvider serviceProvider, ILogger<Day09> logger)
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

            cpu.Load(program)
                .UseLoggingForOutput()
                .UseConstantValueForInput(1)
                .Run();

            var result = cpu.LastOutput;
            AssertExpectedResult("2789104029", result.ToString());
            return result.ToString();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var program = inputs.First().ToProgram();

            cpu.Load(program)
                .UseLoggingForOutput()
                .UseConstantValueForInput(2)
                .Run();

            var result = cpu.LastOutput;

            AssertExpectedResult("32869", result.ToString());
            return result.ToString();
        }
    }
}
