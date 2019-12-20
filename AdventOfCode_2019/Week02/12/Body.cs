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

        public float Energy => Potential + Kinetic;

        public void StepVelocity(Body other)
        {
            var delta = new Vector3(
                Position.X < other.Position.X ? 1 : -1,
                Position.Y < other.Position.Y ? 1 : -1,
                Position.Z < other.Position.Z ? 1 : -1);

            Velocity += delta;
            other.Velocity -= delta;
        }

        public void StepPosition()
        {
            Position += Velocity;
        }

        public override string ToString()
        {
            return $"E={Energy}\tpos=<{Position.X}, {Position.Y}, {Position.Z}>\tvel=<{Velocity.X} ,{Velocity.Y}, {Velocity.Z}>";
        }
    }
}