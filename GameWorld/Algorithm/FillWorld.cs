namespace Arcadia.GameWorld.Algorithm
{
    /// <summary>
    /// <see cref="FillWorld"/> is a simple algorithm that fills a world.
    /// </summary>
    public sealed class FillWorld
    {
        /// <summary>
        /// Runs the algorithm that fills a world with a specific tile.
        /// </summary>
        /// <param name="world">A 2D integer array representation of a world.</param>
        /// <param name="tileType">An integer representation of a specific tile.</param>
        public static void Run(int[,] world, int tileType)
        {
            for (int tileX = 0; tileX < world.GetLength(0); ++tileX)
            {
                for (int tileY = 0; tileY < world.GetLength(1); ++tileY)
                {
                    world[tileX, tileY] = tileType;
                }
            }
        }
    }
}
