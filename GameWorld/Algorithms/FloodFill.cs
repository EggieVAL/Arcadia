using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Arcadia.GameWorld.Algorithms
{
    /// <summary>
    /// The <see cref="FloodFill"/> class is an algorithm that fills an area connected to the given position.
    /// </summary>
    public static class FloodFill
    {
        /// <summary>
        /// Fills in the given <paramref name="area"/> connected to the given position.
        /// </summary>
        /// <param name="area">The area to apply the flood fill algorithm.</param>
        /// <param name="x">The x-coordinate in the grid space.</param>
        /// <param name="y">The y-coordinate in the grid space.</param>
        /// <param name="maximumFill">The maximum number of tiles the algorithm will alter.</param>
        /// <param name="newInk">The ink used for filling.</param>
        /// <param name="IsCorrectInk">The function used to check if a tile is connected to the given position.</param>
        public static void Run(int[,] area, int x, int y, int maximumFill, int newInk, Func<int[,], Point, bool> IsCorrectInk)
        {
            List<Point> fillPoints = new();
            List<Point> queue = new();

            Point start = new(x, y);
            int currentInk = area[x, y];

            if (IsCorrectInk(area, start))
            {
                area[x, y] = newInk;
                queue.Add(start);
            }

            while (queue.Count > 0)
            {
                int lastIndex = queue.Count - 1;
                Point tilePosition = queue[lastIndex];
                queue.RemoveAt(lastIndex);
                fillPoints.Add(tilePosition);

                Point north = new(tilePosition.X, tilePosition.Y-1);
                Point east = new(tilePosition.X+1, tilePosition.Y);
                Point south = new(tilePosition.X, tilePosition.Y+1);
                Point west = new(tilePosition.X-1, tilePosition.Y);

                if (IsCorrectInk(area, north))
                {
                    area[north.X, north.Y] = newInk;
                    queue.Add(north);
                }
                if (IsCorrectInk(area, east))
                {
                    area[east.X, east.Y] = newInk;
                    queue.Add(east);
                }
                if (IsCorrectInk(area, south))
                {
                    area[south.X, south.Y] = newInk;
                    queue.Add(south);
                }
                if (IsCorrectInk(area, west))
                {
                    area[west.X, west.Y] = newInk;
                    queue.Add(west);
                }

                if (fillPoints.Count + queue.Count > maximumFill)
                {
                    fillPoints.AddRange(queue);
                    RevertState(area, currentInk, fillPoints);
                    return;
                }
            }
        }

        private static void RevertState(int[,] area, int ink, List<Point> points)
        {
            foreach (Point point in points)
            {
                area[point.X, point.Y] = ink;
            }
        }
    }
}
