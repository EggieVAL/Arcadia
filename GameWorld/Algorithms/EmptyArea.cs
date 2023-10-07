using Arcadia.Graphics;

namespace Arcadia.GameWorld.Algorithms
{
    /// <summary>
    /// The <see cref="EmptyArea"/> class is an algorithm that empties an area.
    /// </summary>
    public static class EmptyArea
    {
        /// <summary>
        /// Empties the given <paramref name="area"/> by making the ink transparent.
        /// </summary>
        /// <param name="area">The area to empty.</param>
        public static void Run(int[,] area)
        {
            for (int x = 0; x < area.GetLength(0); ++x)
            {
                for (int y = 0; y < area.GetLength(1); ++y)
                {
                    area[x, y] = (int) Ink.Transparent;
                }
            }
        }
    }
}
