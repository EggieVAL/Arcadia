using Arcadia.GameObject.Tiles;
using Microsoft.Xna.Framework;
using System;

namespace Arcadia.GameWorld
{
    /// <summary>
    /// The <c>World</c> class is a representation of a game world.
    /// </summary>
    public class World
    {
        /// <summary>
        /// The size of a small world.
        /// </summary>
        public static readonly Point Small = new Point(4200, 1200);

        /// <summary>
        /// The size of a medium world.
        /// </summary>
        public static readonly Point Medium = new Point(6400, 1800);

        /// <summary>
        /// The size of a large world.
        /// </summary>
        public static readonly Point Large = new Point(8400, 2400);

        /// <summary>
        /// The tile of a game world at (<paramref name="tileX"/>, <paramref name="tileY"/>).
        /// </summary>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        /// <returns></returns>
        public Tile this[int tileX, int tileY]
        {
            get => Grid[tileX, tileY];
            set => Grid[tileX, tileY] = value;
        }

        /// <summary>
        /// The tile of a game world at (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns></returns>
        public Tile this[float x, float y]
        {
            get => Grid[x, y];
            set => Grid[x, y] = value;
        }

        /// <summary>
        /// The grid representation of a game world.
        /// </summary>
        public Grid Grid { get; private set; }

        /// <summary>
        /// The tile width of the world.
        /// </summary>
        public int TileWidth => Grid.TileWidth;

        /// <summary>
        /// The tile height of the world.
        /// </summary>
        public int TileHeight => Grid.TileHeight;

        /// <summary>
        /// The width of the world in units.
        /// </summary>
        public float Width => Grid.Width;

        /// <summary>
        /// The height of the world in units.
        /// </summary>
        public float Height => Grid.Height;

        /// <summary>
        /// The seed of a game world.
        /// </summary>
        public long Seed { get; init; }

        /// <summary>
        /// Constructs a pseudo-random generated world of some <paramref name="tileWidth"/> and
        /// <paramref name="tileHeight"/> based on seed.
        /// </summary>
        /// <param name="seed">The seed of a world.</param>
        /// <param name="tileWidth">The tile width of a world.</param>
        /// <param name="tileHeight">The tile height of a world.</param>
        public World(long seed, int tileWidth, int tileHeight)
        {
            Grid = new Grid(tileWidth, tileHeight);
            Seed = seed;
        }

        /// <summary>
        /// Constructs a pseudo-random generated world of some <paramref name="width"/> and
        /// <paramref name="height"/> based on seed.
        /// </summary>
        /// <param name="seed">The seed of a world.</param>
        /// <param name="width">The width of a world in units.</param>
        /// <param name="height">The height of a world in units.</param>
        public World(long seed, float width, float height)
        {
            Grid = new Grid(width, height);
            Seed = seed;
        }

        /// <summary>
        /// The update method is called multiple times a second, updating the state of a
        /// character.
        /// </summary>
        /// <param name="gameTime">The time state of the game.</param>
        public void Update(GameTime gameTime)
        {
            for (int tileX = 0; tileX < TileWidth; ++tileX)
            {
                for (int tileY = 0; tileY < TileHeight; ++tileY)
                {
                    Tile tile = Grid[tileX, tileY];
                    if (tile != null)
                    {
                        tile.Update(gameTime);
                    }
                }
            }
        }

        /// <summary>
        /// Similar to the update method, the draw method is also called multiple times per
        /// second. This, as the name suggests, is responsible for drawing content to the
        /// screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            for (int tileX = 0; tileX < TileWidth; ++tileX)
            {
                for (int tileY = 0; tileY < TileHeight; ++tileY)
                {
                    Tile tile = Grid[tileX, tileY];
                    if (tile != null)
                    {
                        tile.Draw(gameTime);
                    }
                }
            }
        }
    }
}
