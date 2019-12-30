namespace AdventOfCode_2019.Maze
{
    public class MazeMovementOption
    {
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
