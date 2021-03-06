﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode_2019
{
    public class Robot
    {
        private RobotCommmand command = RobotCommmand.Paint;

        private readonly HashSet<Vector2> painted = new HashSet<Vector2>();

        public Robot()
        {
            Panels = new Dictionary<Vector2, HullPanel>();
            AddPanel(new Vector2(0, 0));

            Angle = Direction.Up;
        }

        public Dictionary<Vector2, HullPanel> Panels { get; private set; }

        public Vector2 Location { get; private set; }

        public Direction Angle { get; private set; }

        public Vector2 RepairArea { get; private set; }

        public int PanitedPanelsCount => painted.Count;

        public BigInteger IsPaintedWhite()
        {
            return IsPaintedWhite(Location) ? 1 : 0;
        }

        public bool IsPaintedWhite(Vector2 inspect)
        {
            if (Panels.ContainsKey(inspect))
            {
                return Panels[inspect].White;
            }

            return false;
        }

        public void AcceptCommand(int value)
        {
            if (command == RobotCommmand.Paint)
            {
                Paint(value);
            }
            else
            {
                Turn(value);
                Move();
            }

            command = NextCommand();
        }


        internal RobotCommmand NextCommand()
        {
            return command == RobotCommmand.Paint ?
                RobotCommmand.TurnAndMove :
                RobotCommmand.Paint;
        }

        internal void Paint(int white)
        {
            Panels[Location].Paint(white);
            painted.Add(Location);
        }

        internal void Turn(int turnRight)
        {
            if (turnRight == 1)
            {
                Angle = Angle switch
                {
                    Direction.Up => Direction.Right,
                    Direction.Right => Direction.Down,
                    Direction.Down => Direction.Left,
                    Direction.Left => Direction.Up,
                    _ => throw new NotImplementedException($"Could not turn right when angle is {Angle}"),
                };
            }
            else
            {
                Angle = Angle switch
                {
                    Direction.Up => Direction.Left,
                    Direction.Right => Direction.Up,
                    Direction.Down => Direction.Right,
                    Direction.Left => Direction.Down,
                    _ => throw new NotImplementedException($"Could not turn right when angle is {Angle}"),
                };
            }
        }

        internal void Move()
        {
            Location += Angle switch
            {
                Direction.Up => new Vector2(0, 1),
                Direction.Right => new Vector2(1, 0),
                Direction.Down => new Vector2(0, -1),
                Direction.Left => new Vector2(-1, 0),
                _ => throw new NotImplementedException($"Could not move forward when angle is {Angle}."),
            };

            AddPanel(Location);

            if (Location.X > RepairArea.X)
            {
                RepairArea = new Vector2(Location.X, RepairArea.Y);
            }
            else if (Location.Y < RepairArea.Y)
            {
                // goes down for +
                RepairArea = new Vector2(RepairArea.X, Location.Y);
            }
        }

        private void AddPanel(Vector2 location)
        {
            if (!Panels.ContainsKey(Location))
            {
                var panel = new HullPanel(location);
                Panels.Add(location, panel);
            }
        }
    }
}