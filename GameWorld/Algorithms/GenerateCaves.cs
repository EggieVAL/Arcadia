using Arcadia.Graphics;
using Microsoft.Xna.Framework;

namespace Arcadia.GameWorld.Algorithms
{
    /// <summary>
    /// The <see cref="GenerateCaves"/> class is an algorithm that generate caves.
    /// </summary>
    public static class GenerateCaves
    {
        /// <summary>
        /// Generates caves in the given <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to generate caves in.</param>
        /// <param name="density">The density affects the size of the caves.</param>
        /// <param name="smoothRate">The smooth rate affects the roughness of the caves.</param>
        public static void Run(int[,] area, int density, int smoothRate)
        {
            GenerateTilesRandomly.Run(area, density, new Brush(Ink.Transparent));

            for (int i = 0; i < smoothRate; ++i)
            {
                CellularAutomata(area);
            }
        }

        private static void CellularAutomata(int[,] area)
        {
            for (int x = 0; x < area.GetLength(0); ++x)
            {
                for (int y = 0; y < area.GetLength(1); ++y)
                {
                    HandleCaveGrowth(area, x, y);
                }
            }
        }

        private static void HandleCaveGrowth(int[,] area, int x, int y)
        {
            int numberOfSurroundingAir = NumberOfSurroundingAir(area, x, y);
            if (numberOfSurroundingAir > 4)
            {
                area[x, y] = Ink.Transparent;
            }
            else if (numberOfSurroundingAir < 4)
            {
                area[x, y] = Ink.Default;
            }
        }

        private static int NumberOfSurroundingAir(int[,] area, int x, int y)
        {
            Point north = new(x, y-1);
            Point east = new(x+1, y);
            Point south = new(x, y+1);
            Point west = new(x-1, y);

            Point northwest = new(x-1, y-1);
            Point northeast = new(x+1, y-1);
            Point southwest = new(x-1, y+1);
            Point southeast = new(x+1, y+1);

            int numberOfSurroundingAir = 0;

            if (IsAir(area, north.X, north.Y))
            {
                numberOfSurroundingAir++;
            }
            if (IsAir(area, east.X, east.Y))
            {
                numberOfSurroundingAir++;
            }
            if (IsAir(area, south.X, south.Y))
            {
                numberOfSurroundingAir++;
            }
            if (IsAir(area, west.X, west.Y))
            {
                numberOfSurroundingAir++;
            }
            if (IsAir(area, northwest.X, northwest.Y))
            {
                numberOfSurroundingAir++;
            }
            if (IsAir(area, northeast.X, northeast.Y))
            {
                numberOfSurroundingAir++;
            }
            if (IsAir(area, southwest.X, southwest.Y))
            {
                numberOfSurroundingAir++;
            }
            if (IsAir(area, southeast.X, southeast.Y))
            {
                numberOfSurroundingAir++;
            }

            return numberOfSurroundingAir;
        }

        private static bool IsAir(int[,] area, int x, int y)
        {
            return !Grid.InBounds(area, x, y) || area[x, y] == Ink.Transparent;
        }
    }
}
