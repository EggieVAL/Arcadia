using Arcadia.Graphics;
using Microsoft.Xna.Framework;

namespace Arcadia.GameWorld.Algorithms
{
    /// <summary>
    /// The <see cref="RemovePatchesOfBlocks"/> class is an algorithm that removes patches of blocks in an area. Patches of
    /// blocks are groups of blocks or type of block connected together that were formed from running generational algorithms.
    /// These small patches of blocks can be floating in the air, which may be unnatural and unwanted.
    /// </summary>
    public static class RemovePatchesOfBlocks
    {
        /// <summary>
        /// Removes patches of blocks in the given <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to remove patches of blocks in.</param>
        /// <param name="maximumBlocks">Any connected area of blocks greater than this value will not be considered a patch of blocks.</param>
        public static void Run(int[,] area, int maximumBlocks)
        {
            for (int x = 0; x < area.GetLength(0); ++x)
            {
                for (int y = 0; y < area.GetLength(1); ++y)
                {
                    FloodFill.Run(area, x, y, maximumBlocks, (int) Ink.Transparent, IsBlock);
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
