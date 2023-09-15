using Arcadia.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Arcadia.GameWorld.Algorithms
{
    public static class RemoveAirBubbles
    {
        public static void Run(int[,] area, int minimumAirNeeded, int ink = (int) Ink.Default)
        {
            if (ink == (int) Ink.Transparent)
            {
                throw new ArgumentException("Ink cannot be transparent.");
            }

            for (int x = 0; x < area.GetLength(0); ++x)
            {
                for (int y = 0; y < area.GetLength(1); ++y)
                {
                    FloodFill.Run(area, x, y, minimumAirNeeded-1, ink, IsAir);
                }
            }
        }

        private static bool IsAir(int[,] area, Point point)
        {
            return Grid.InBounds(area, point.X, point.Y)
                && area[point.X, point.Y] == (int) Ink.Transparent;
        }
    }
}
