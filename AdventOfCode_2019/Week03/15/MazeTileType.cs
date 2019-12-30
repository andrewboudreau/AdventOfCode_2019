namespace AdventOfCode_2019.Maze
{
    public enum MazeTileType
    {
        Unknown = -1,

        [ConsolePrint(Day00.FullBlock)]
        Wall = 0,

        [ConsolePrint(".")]
        Empty = 1,

        [ConsolePrint("@")]
        Goal = 2
    }
}
