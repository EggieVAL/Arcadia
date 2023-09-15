using Arcadia.Graphics;
using Microsoft.Xna.Framework;

namespace Arcadia.GameWorld.Algorithms
{
    public static class RemovePatchesOfBlocks
    {
        public static void Run(int[,] area, int minimumBlocksNeeded)
        {
            for (int x = 0; x < area.GetLength(0); ++x)
            {
                for (int y = 0; y < area.GetLength(1); ++y)
                {
                    FloodFill.Run(area, x, y, minimumBlocksNeeded, (int) Ink.Transparent, IsBlock);
                }
            }
        }

        private static bool IsBlock(int[,] area, Point point)
        {
            return Grid.InBounds(area, point.X, point.Y)
                && area[point.X, point.Y] != (int) Ink.Transparent;
        }
    }
}
