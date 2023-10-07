namespace Arcadia.GameWorld.Algorithms
{
    /// <summary>
    /// The <see cref="FillArea"/> class is an algorithm that fills in an area with some ink.
    /// </summary>
    public static class FillArea
    {
        /// <summary>
        /// Fills in the given <paramref name="area"/> with the given <paramref name="ink"/>.
        /// </summary>
        /// <param name="area">The area to fill.</param>
        /// <param name="ink">The ink used for filling.</param>
        public static void Run(int[,] area, int ink)
        {
            for (int x = 0; x < area.GetLength(0); ++x)
            {
                for (int y = 0; y < area.GetLength(1); ++y)
                {
                    area[x, y] = ink;
                }
            }
        }
    }
}
