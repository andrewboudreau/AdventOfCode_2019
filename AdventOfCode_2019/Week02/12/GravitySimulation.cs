using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode_2019.Week02._13
{
    public class GravitySimulation
    {
        private readonly List<Body> bodies;

        public GravitySimulation(IEnumerable<Body> bodies)
        {
            this.bodies = bodies.ToList();
        }

        public void Step()
        {
            for (var i = 0; i < bodies.Count; i++)
            {
                for (var j = 0; j < bodies.Count - 1; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    bodies[i].StepVelocity(bodies[j].Velocity);
                    bodies[i].StepPosition(bodies[j].Position);
                }
            }
        }
    }
}
