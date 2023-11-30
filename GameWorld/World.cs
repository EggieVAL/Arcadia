using Arcadia.GameObjects;
using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld.Algorithms;
using Arcadia.Graphics;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading;

namespace Arcadia.GameWorld
{
    /// <summary>
    /// The <see cref="World"/> class is a representation of a world. A world is composed of a grid of tiles.
    /// </summary>
    public class World
    {
        public List<RenderableObject> _entities;

        Texture2D[][] _miscEntityTextures;

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
        public Grid Grid { get; private set; }

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
        public World(long seed, int width, int height, Camera camera, Texture2D[][] EntityTextures)
        {
            Grid = new Grid(width, height);
            Seed = seed;
            _camera = camera;

            _miscEntityTextures = EntityTextures;

            _entities = new List<RenderableObject>();

            UniversalRandom.SetSeed(seed);
        }

        // this method will be changed; should not have any parameters
        public void Generate(Texture2D[] texture)
        {
            int[,] world = new int[Width, Height];

            EmptyArea.Run(world);
            GenerateTerrain.Run(world, Height / 2, (int)Ink.Default);
            //GenerateCaves.Run(world, 50, 5);
            //RemoveAirBubbles.Run(world, 15);
            //RemovePatchesOfBlocks.Run(world, 15);

            for (int tileX = 0; tileX < Width; ++tileX)
            {
                for (int tileY = 0; tileY < Height; ++tileY)
                {
                    int ink = world[tileX, tileY];
                    Grid[tileX, tileY] = (ink == (int)Ink.Transparent || ink == (int)Ink.Ignore)
                        ? null : new Dirt(texture, tileX, tileY);
                }
            }
        }

        public void CreateProjectile(int me_id, int X, int Y, float VelocityX, float VelocityY)
        {
            _entities.Add(new Projectile(_miscEntityTextures[me_id], new Rectangle(0, 0, 2 * Grid.Size, 1 * Grid.Size), X, Y, VelocityX, VelocityY, this));
        }

        public Vector2 GetMousePosition()
        {
            MouseListener.PositionRelativeToCamera(_camera, out float x, out float y);
            return new Vector2(x, y);
        }

        public void Destroy(RenderableObject g)
        {
            _entities.Remove(g);
        }

        // currently updates all tiles in the world; may be prone to change
        public void Update(GameTime gameTime)
        {
            for (int tileX = 0; tileX < Width; ++tileX)
            {
                for (int tileY = 0; tileY < Height; ++tileY)
                {
                    Tile tile = Grid[tileX, tileY];
                    tile?.Update(gameTime);
                }
            }
        }

        // currently draws all tiles in the world; will be changed.
        public void Draw(GameTime gameTime)
        {
            _camera.GetExtents(out float left, out float right, out float top, out float bottom);

            int[] minPos = Grid.GetPosition(left, top);
            int[] maxPos = Grid.GetPosition(right, bottom);

            for (int tileX = minPos[0]; tileX <= maxPos[0]; ++tileX)
            {
                for (int tileY = minPos[1]; tileY <= maxPos[1]; ++tileY)
                {
                    Tile tile = Grid[tileX, tileY];
                    tile?.Draw(gameTime);
                }
            }
        }

        private Camera _camera;
    }
}