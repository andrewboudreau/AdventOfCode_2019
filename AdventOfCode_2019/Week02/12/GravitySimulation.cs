﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode_2019
{
    public class GravitySimulation
    {
        private readonly List<Body> bodies;
        private Action<GravitySimulation> StepCallback;

        public GravitySimulation(IEnumerable<Body> bodies)
        {
            this.bodies = bodies.ToList();
        }

        public float Energy => bodies.Sum(b => b.Energy);

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

            StepCallback?.Invoke(this);
        }

        public void Step(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                ////System.Console.WriteLine($"After {i} steps:");
                ////System.Console.WriteLine(Print());
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

        public string StateOfAxis(int axis)
        {
            var state = new StringBuilder();
            switch (axis)
            {
                case 0:
                    foreach (var body in bodies)
                    {
                        state.Append(body.StateOfAxis(axis));
                    }
                    break;

                case 1:
                    foreach (var body in bodies)
                    {
                        state.Append(body.StateOfAxis(axis));
                    }
                    break;

                case 2:
                    foreach (var body in bodies)
                    {
                        state.Append(body.StateOfAxis(axis));
                    }
                    break;

                default:
                    throw new NotSupportedException($"{axis} is not supported axis");
            }

            return state.ToString();
        }


        internal void OnEachStep(Action<GravitySimulation> sim)
        {
            StepCallback = sim;
        }
    }
}
