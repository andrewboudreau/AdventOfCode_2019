using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using AdventOfCode_2019.Week01;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019
{
    public partial class Day11 : Day00
    {
        public Day11(IServiceProvider serviceProvider, ILogger<Day11> logger)
            : base(serviceProvider, logger)
        {
            DirectInput = null;
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();
            var robot = new Robot();

            cpu.Load(program);
            cpu.UseRobot(robot);
            cpu.Run();

            return $"{robot.PanitedPanelsCount}";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var program = inputs.First().ToProgram();
            var cpu = ServiceProvider.GetRequiredService<IntCodeCpu>();

            var robot = new Robot();
            robot.Panels[new Vector2(0, 0)].Paint(1);

            cpu.Load(program)
                .UseRobot(robot)
                .Run();

            var print = new StringBuilder();
            print.AppendLine();

            for (var y = 1; y >= robot.RepairArea.Y-1; y--)
            {
                for (var x = 0; x < robot.RepairArea.X; x++)
                {
                    var location = new Vector2(x, y);
                    var output = robot.IsPaintedWhite(location) ? FullBlock : ".";
                    print.Append(output);
                }

                print.AppendLine();
            }

            return print.ToString();
        }
    }
}