using Arcadia.GameObjects;
using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld.Algorithms;
using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arcadia.GameWorld
{
    /// <summary>
    /// The <see cref="World"/> class is a representation of a world. A world is composed of a grid of tiles.
    /// </summary>
    public class World
    {
        /// <summary>
        /// The width and height of a small world, respectively.
        /// </summary>
        public static readonly Point Small = new(4200, 1200);

        /// <summary>
        /// The width and height of a medium world, respectively.
        /// </summary>
        public static readonly Point Medium = new(6400, 1800);

        /// <summary>
        /// The width and height of a large world, respectively.
        /// </summary>
        public static readonly Point Large = new(8400, 2400);

        /// <summary>
        /// The <see cref="Tile"/> at the given tile coordinates.
        /// </summary>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY"></param>
        /// <returns></returns>
        public Tile this[int tileX, int tileY]
        {
            get => Grid[tileX, tileY];
            set => Grid[tileX, tileY] = value;
        }

        /// <summary>
        /// The grid of a world.
        /// </summary>
        public Grid Grid { get; init; }

        public List<Camera> Cameras { get; init; }

        /// <summary>
        /// The width of the grid in terms of tiles.
        /// </summary>
        public int Width => Grid.Width;

        /// <summary>
        /// The height of the grid in terms of tiles.
        /// </summary>
        public int Height => Grid.Height;

        /// <summary>
        /// The width of the world in units.
        /// </summary>
        public float WidthInUnits => Grid.WidthInUnits;

        /// <summary>
        /// The height of the world in units.
        /// </summary>
        public float HeightInUnits => Grid.HeightInUnits;

        /// <summary>
        /// The seed of the world.
        /// </summary>
        public long Seed { get; init; }
      
        /// <summary>
        /// Constructs a world based on the given seed.
        /// </summary>
        /// <param name="seed">The seed of the world.</param>
        /// <param name="width">The width of the world in terms of tiles.</param>
        /// <param name="height">The height of the world in terms of tiles.</param>
        public World(long seed, int width, int height)
        {
            Grid = new Grid(width, height);
            Seed = seed;

            Cameras = new List<Camera>();

            UniversalRandom.SetSeed(seed);
        }

        // this method will be changed; should not have any parameters
        public virtual void Generate(Texture2D texture)
        {
            int[,] world = new int[Width, Height];

            EmptyArea.Run(world);
            GenerateTerrain.Run(world, Height/2, Ink.Default);
            //GenerateCaves.Run(world, 50, 5);
            //RemoveAirBubbles.Run(world, 15);
            //RemovePatchesOfBlocks.Run(world, 15);

            for (int tileX = 0; tileX < Width; ++tileX)
            {
                for (int tileY = 0; tileY < Height; ++tileY)
                {
                    int ink = world[tileX, tileY];
                    Grid[tileX, tileY] = (ink == Ink.Transparent || ink == Ink.Ignore)
                        ? null : new Dirt(texture, tileX, tileY);
                }
            }
        }
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (Camera camera in Cameras)
            {
                RenderCameraView(gameTime, camera);
            }
        }

        public void AddCamera(Camera camera)
        {
            if (camera != null)
            {
                Cameras.Add(camera);
            }
        }

        private void RenderCameraView(GameTime gameTime, Camera camera)
        {
            camera.GetExtents(out float left, out float right, out float top, out float bottom);

            int minTileX = MathHelper.Clamp(Grid.GetPosition(left), 0, Width-1);
            int maxTileX = MathHelper.Clamp(Grid.GetPosition(right), 0, Width-1);
            int minTileY = MathHelper.Clamp(Grid.GetPosition(top), 0, Height-1);
            int maxTileY = MathHelper.Clamp(Grid.GetPosition(bottom), 0, Height-1);

            for (int tileX = minTileX; tileX <= maxTileX; ++tileX)
            {
                for (int tileY = minTileY; tileY <= maxTileY; ++tileY)
                {
                    Tile tile = Grid[tileX, tileY];
                    tile?.Draw(gameTime);
                }
            }
        }
    }
}
