using System;

namespace AdventOfCode_2019.Arcade
{
    public class ConsoleRenderer
    {
        private readonly VideoBuffer videoBuffer;

        public ConsoleRenderer(VideoBuffer videoBuffer)
        {
            this.videoBuffer = videoBuffer;
        }

        public void Render()
        {
            Console.Clear();
            foreach (var tile in videoBuffer.Tiles)
            {
                Console.SetCursorPosition((int)tile.Key.X, (int)tile.Key.Y);
                Console.Write(tile.Value);
            }
        }

        public void Update(Tile tile)
        {
            Console.SetCursorPosition((int)tile.Position.X, (int)tile.Position.Y);
            Console.Write(tile.ToString());
        }
    }
}
