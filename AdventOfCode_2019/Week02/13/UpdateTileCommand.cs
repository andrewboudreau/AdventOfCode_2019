using AdventOfCode_2019.Week01;

namespace AdventOfCode_2019
{
    public class UpdateTileCommand : CommandInputBuffer
    {
        public UpdateTileCommand()
            : base(3)
        {
        }
    }

    public class UpdateScoreCommand : CommandInputBuffer
    {
        public UpdateScoreCommand()
            : base(3)
        {
        }
    }
}
