using System.ComponentModel;

namespace AdventOfCode_2019.Arcade
{
    public enum TileType
    {
        [Description("An empty tile. No game object appears in this tile.")]
        Empty = 0,

        [Description("A wall tile. Walls are indestructible barriers.")]
        Wall = 1,

        [Description("A block tile. Blocks can be broken by the ball.")]
        Block = 2,

        [Description("a horizontal paddle tile. The paddle is indestructible.")]
        Paddle = 3,

        [Description("A ball tile. The ball moves diagonally and bounces off objects.")]
        Ball = 4
    }
}
