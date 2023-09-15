using Arcadia.Graphics;

namespace Arcadia.GameWorld.Algorithms
{
    public static class EmptyArea
    {
        public static void Run(int[,] area)
        {
            for (int gridX = 0; gridX < area.GetLength(0); ++gridX)
            {
                for (int gridY = 0; gridY < area.GetLength(1); ++gridY)
                {
                    area[gridX, gridY] = (int) Ink.Transparent;
                }
            }
        }
    }
}
