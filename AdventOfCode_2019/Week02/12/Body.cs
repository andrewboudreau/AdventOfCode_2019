
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

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
        //if (Position.X < other.X)
        //{
        //    Position.X += 1;
        //    other.X = -1;
        //}

        //if (Position.Y < other.Y)
        //{
        //    Position.Y += 1;
        //    other.Y = -1;
        //}

        //if (Position.Z < other.Z)
        //{
        //    Position.Z += 1;
        //    other.Z = -1;
        //}
    }

    public void StepPosition(Vector3 other)
    {

    }

    public override string ToString()
    {
        return $"pos=<{Position.X}, {Position.Y}, {Position.Z}>, vel=<{Velocity.X} ,{Velocity.Y}, {Velocity.Z}>";
    }
}



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
