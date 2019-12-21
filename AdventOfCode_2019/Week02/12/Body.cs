using System;
using System.Numerics;

namespace AdventOfCode_2019
{
    public class Body
    {
        public Body(string xyz)
        : this(xyz.IntegersFromCsv()[0], xyz.IntegersFromCsv()[1], xyz.IntegersFromCsv()[2])
        {
        }

        public Body(int x, int y, int z)
        {
            Position = new Vector3(x, y, z);
            Velocity = new Vector3();
        }

        public Vector3 Position { get; private set; }

        public Vector3 Velocity { get; private set; }

        public float Potential => Vector3.Dot(Vector3.Abs(Position), Vector3.One);

        public float Kinetic => Vector3.Dot(Vector3.Abs(Velocity), Vector3.One);

        public float Energy => Potential * Kinetic;

        public string StateOfAxis(int axis)
        {
            switch (axis)
            {
                case 0:
                    return $"{Position.X},{Velocity.X}";

                case 1:
                    return $"{Position.Y},{Velocity.Y}";

                case 2:
                    return $"{Position.Z},{Velocity.Z}";

                default:
                    throw new NotSupportedException($"{axis} is not supported axis");
            }
        }

        public void StepVelocity(Body other)
        {
            var delta = new Vector3(
                StepAxisVelocity(Position.X, other.Position.X),
                StepAxisVelocity(Position.Y, other.Position.Y),
                StepAxisVelocity(Position.Z, other.Position.Z));

            Velocity += delta;
            other.Velocity -= delta;
        }

        public void StepPosition()
        {
            Position += Velocity;
        }

        public override string ToString()
        {
            return $"E={Energy,3}\tpos=<{Position.X,3},{Position.Y,3},{Position.Z,3}>  vel=<{Velocity.X,3},{Velocity.Y,3},{Velocity.Z,3}>";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Velocity);
        }

        private int StepAxisVelocity(float mine, float other)
        {
            if (mine == other)
            {
                return 0;
            }

            return mine < other ? 1 : -1;
        }
    }
}