using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Arcadia.GameWorld.Algorithms
{
    /// <summary>
    /// The <see cref="RemoveAirBubbles"/> class is an algorithm that removes air bubbles in an area. Air bubbles are pockets
    /// of air that were formed from running generational algorithms. These air bubbles may be unnatural and unwanted.
    /// </summary>
    public static class RemoveAirBubbles
    {
        /// <summary>
        /// Removes air bubbles in the given <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The area to remove air bubbles in.</param>
        /// <param name="maximumAir">Any connected area of air greater than this value will not be considered an air bubble.</param>
        /// <param name="ink">The ink used for filling air bubbles.</param>
        public static void Run(int[,] area, int maximumAir, int ink)
        {
            if (ink == Ink.Transparent)
            {
                return;
            }

            for (int x = 0; x < area.GetLength(0); ++x)
            {
                for (int y = 0; y < area.GetLength(1); ++y)
                {
                    FloodFill.Run(area, x, y, maximumAir, ink, IsAir);
                }
            }
        }

        private static bool IsAir(int[,] area, Point point)
        {
            return Grid.InBounds(area, point.X, point.Y) && area[point.X, point.Y] == Ink.Transparent;
        }
    }
}
