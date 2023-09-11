namespace Arcadia.GameWorld.Algorithm
{
    /// <summary>
    /// <see cref="EmptyWorld"/> is a simple algorithm that empties a world.
    /// </summary>
    public sealed class EmptyWorld
    {
        /// <summary>
        /// Runs the algorithm that empties a world.
        /// </summary>
        /// <param name="world">A 2D integer array representation of a world.</param>
        public static void Run(int[,] world)
        {
            for (int tileX = 0; tileX < world.GetLength(0); ++tileX)
            {
                for (int tileY = 0; tileY < world.GetLength(1); ++tileY)
                {
                    world[tileX, tileY] = 0;
                }
            }
        }
    }
}
