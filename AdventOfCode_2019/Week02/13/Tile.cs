using System;
using System.Numerics;

namespace AdventOfCode_2019.Arcade
{
    public class Tile
    {
        public Tile(Vector2 position, TileType type)
        {
            Position = position;
            Type = type;
        }

        public Vector2 Position { get; }

        public TileType Type { get; set; }

        public override string ToString()
        {
            return Type switch
            {
                TileType.Empty => " ",
                TileType.Wall => "#",
                TileType.Block => "=",
                TileType.Paddle => "_",
                TileType.Ball => "@",
                _ => throw new NotSupportedException($"Invalid tile type '{Type}'."),
            };
        }
    }
}
