using Arcadia.GameObjects;
using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld.Algorithms;
using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Arcadia.GameWorld
{
    public class World
    {
        public static readonly Point Small = new(4200, 1200);

        public static readonly Point Medium = new(6400, 1800);

        public static readonly Point Large = new(8400, 2400);

        public Tile this[int tileX, int tileY]
        {
            get => Grid[tileX, tileY];
            set => Grid[tileX, tileY] = value;
        }

        public Grid Grid { get; private set; }

        public int GridWidth => Grid.GridWidth;

        public int GridHeight => Grid.GridHeight;

        public float Width => Grid.Width;

        public float Height => Grid.Height;

        public long Seed { get; init; }

        public World(long seed, int gridWidth, int gridHeight, Camera camera)
        {
            Grid = new Grid(gridWidth, gridHeight);
            Seed = seed;
            _camera = camera;

            UniversalRandom.SetSeed(seed);
        }

        public void Generate(Texture2D texture)
        {
            int[,] world = new int[GridWidth, GridHeight];

            EmptyArea.Run(world);
            GenerateTerrain.Run(world, GridHeight/2, (int) Ink.Default);
            //GenerateCaves.Run(world, 50, 5);
            //RemoveAirBubbles.Run(world, 15);
            //RemovePatchesOfBlocks.Run(world, 15);

            for (int gridX = 0; gridX < GridWidth; ++gridX)
            {
                for (int gridY = 0; gridY < GridHeight; ++gridY)
                {
                    int ink = world[gridX, gridY];
                    Grid[gridX, gridY] = (ink == (int) Ink.Transparent || ink == (int) Ink.Ignore)
                        ? null : new Dirt(texture, gridX, gridY);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int tileX = 0; tileX < GridWidth; ++tileX)
            {
                for (int tileY = 0; tileY < GridHeight; ++tileY)
                {
                    Tile tile = Grid[tileX, tileY];
                    tile?.Update(gameTime);
                }
            }
        }

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
