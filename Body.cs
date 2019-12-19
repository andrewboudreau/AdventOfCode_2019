
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* Problem 1
<x=3, y=3, z=0>
<x=4, y=-16, z=2>
<x=-10, y=-6, z=5>
<x=-3, y=0, z=-13>
 */

namespace AdventOfCode_2019
{
    public class Body
    {
        public Body(int x, int y, int z)
        {
            Position = new Vector3(x, y, z);
            Velocity = new Vector3();
        }

        public Vector3 Position { get; private set; }

        public Vector3 Velocity { get; private set; }


        public void StepVelocity(Vector3 other)
        {
            if (Position.X < other.X)
            {
                Position.X += 1;
                other.X = -1;
            }

            if (Position.Y < other.Y)
            {
                Position.Y += 1;
                other.Y = -1;
            }

            if (Position.Z < other.Z)
            {
                Position.Z += 1;
                other.Z = -1;
            }
        }

        public void StepPosition(Vector3 other)
        {

        }

        public override string ToString()
        {
            return $"pos=<{Position.X}, {Position.Y}, {Position.Z}>, vel=<{Velocity.X} ,{Velocity.Y}, {Velocity.Z}>";
        }
    }



}