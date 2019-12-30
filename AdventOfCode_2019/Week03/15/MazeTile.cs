using System.Collections.Generic;

namespace AdventOfCode_2019.Maze
{
    public class MazeTile
    {
        public MazeTile()
        {
            Neighbors = new Dictionary<Direction, MazeTileType>()
            {
                {Direction.Up, MazeTileType.Unknown },
                {Direction.Down, MazeTileType.Unknown },
                {Direction.Left, MazeTileType.Unknown },
                {Direction.Right, MazeTileType.Unknown }
            };
        }

        public (int X, int Y) Position { get; }

        public Dictionary<Direction, MazeTileType> Neighbors { get; private set; }
    }
}
