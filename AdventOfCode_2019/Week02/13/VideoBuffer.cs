using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode_2019.Arcade
{
    public class VideoBuffer
    {
        public VideoBuffer()
        {
            Tiles = new Dictionary<Vector2, Tile>();
            Reset();
        }

        public Vector2 Boundary;

        public Dictionary<Vector2, Tile> Tiles { get; private set; }


        public Tile UpdateTileMap(Vector2 position, TileType tileType)
        {
            if (position.X < 0)
            {
                throw new InvalidOperationException($"X value cannot be less than 0, '{position.X}'");
            }

            if (position.Y < 0)
            {
                throw new InvalidOperationException($"Y value cannot be less than 0, '{position.Y}'");
            }

            if (position.X > Boundary.X)
            {
                Boundary.X = position.X;
            }

            if (position.Y > Boundary.Y)
            {
                Boundary.Y = position.Y;
            }

            if (!Tiles.ContainsKey(position))
            {
                Tiles.Add(position, new Tile(position, tileType));
            }
            else
            {
                Tiles[position].Type = tileType;
            }

            return new Tile(position, tileType);
        }

        public VideoBuffer Reset()
        {
            Boundary = Vector2.Zero;
            Tiles.Clear();

            return this;
        }
    }
}
