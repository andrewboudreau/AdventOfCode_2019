using System;
using System.Collections.Generic;

namespace AdventOfCode_2019.Maze
{
    public class MazeMovementOption
    {
        public MazeMovementOption(params string[] inputs)
            : this((int.Parse(inputs[0]), int.Parse(inputs[1])), (MazeTileType)Enum.Parse(typeof(MazeTileType), inputs[2].ToString()))
        {
        }

        public MazeMovementOption((int X, int Y) position, MazeTileType tileType)
        {
            Position = position;
            TileType = tileType;
        }

        public int Resolved { get; set; }

        public (int X, int Y) Position { get; private set; }

        public MazeTileType TileType { get; private set; }

        public override string ToString()
        {
            return $"{TileType} Resolved:{Resolved}";
        }
    }
}
