namespace AdventOfCode_2019.Arcade
{
    public class UpdateTileCommand : CommandInputBuffer
    {
        public UpdateTileCommand()
            : base(3)
        {
        }

        public int X => (int)Parameters[0];

        public int Y => (int)Parameters[1];

        public TileType TileType => (TileType)(int)Parameters[2];
    }

    public class UpdateScoreCommand : CommandInputBuffer
    {
        public UpdateScoreCommand()
            : base(3)
        { }

        public int Score => (int)Parameters[2];
    }
}
