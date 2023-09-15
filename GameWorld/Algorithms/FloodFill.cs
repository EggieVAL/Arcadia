using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Arcadia.GameWorld.Algorithms
{
    public static class FloodFill
    {
        public static void Run(int[,] area, int gridX, int gridY, int maximumFill, int newInk, Func<int[,], Point, bool> IsCorrectInk)
        {
            List<Point> fillPoints = new();
            List<Point> queue = new();

            Point start = new(gridX, gridY);
            int currentInk = area[gridX, gridY];

            if (IsCorrectInk(area, start))
            {
                area[gridX, gridY] = newInk;
                queue.Add(start);
            }

            while (queue.Count > 0)
            {
                int lastIndex = queue.Count - 1;
                Point gridPosition = queue[lastIndex];
                queue.RemoveAt(lastIndex);
                fillPoints.Add(gridPosition);

                Point north = new(gridPosition.X, gridPosition.Y-1);
                Point east = new(gridPosition.X+1, gridPosition.Y);
                Point south = new(gridPosition.X, gridPosition.Y+1);
                Point west = new(gridPosition.X-1, gridPosition.Y);

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
