using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class Day12 : Day00
    {
        public Day12(IServiceProvider serviceProvider, ILogger<Day12> logger)
            : base(serviceProvider, logger)
        {
            /*
             Example 1
                <x=-1, y=0, z=2>
                <x=2, y=-10, z=-7>
                <x=4, y=-8, z=8>
                <x=3, y=5, z=-1>
            */

            DirectInput = new[] { "-1,0,2", "2,-10,-7", "4,-8,8", "3,5,-1" };

            /*
             Example 2
                <x=-8, y=-10, z=0>
                <x=5, y=5, z=10>
                <x=2, y=-7, z=3>
                <x=9, y=-8, z=-3>
            
            */
           // DirectInput = new[] { "-8,-10,0", "5,5,10", "2,-7,3", "9,-8,-3" };
            

            /* Puzzle
                <x=3, y=3, z=0>
                <x=4, y=-16, z=2>
                <x=-10, y=-6, z=5>
                <x=-3, y=0, z=-13>
             */

            // DirectInput = new[] { "3,3,0", "4,-16,2", "-10,-6,5", "-3,0,-13" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var bodies = inputs.Select(x => new Body(x)).ToArray();
            var simulation = new GravitySimulation(bodies);
            simulation.Step(10);
            return "\r\n" + simulation.Print();
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var bodies = inputs.Select(x => new Body(x)).ToArray();
            return "N/a";
        }
    }
}
