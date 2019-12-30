namespace AdventOfCode_2019.Arcade
{
    public static class CommandFactory
    {
        public static CommandInputBuffer CreateCommandBuffer(int firstCommandInput)
        {
            if ((CommandType)firstCommandInput == CommandType.UpdateScore)
            {
                return new UpdateScoreCommand();
            }
            else
            {
                return new UpdateTileCommand();
            }
        }
    }
}
