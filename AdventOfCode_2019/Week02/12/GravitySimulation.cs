using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode_2019
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
            foreach (var pair in bodies.MakePairs())
            {
                pair.Item1.StepVelocity(pair.Item2);
            }

            foreach (var body in bodies)
            {
                body.StepPosition();
            }
        }

        public void Step(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                System.Console.WriteLine($"After {i} steps:");
                System.Console.WriteLine(Print());
                Step();
            }
        }

        public string Print()
        {
            var stringBuilder = new StringBuilder();
            foreach (var body in bodies)
            {
                stringBuilder.AppendLine(body.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
