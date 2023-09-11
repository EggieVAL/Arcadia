using Arcadia.Graphics.Shapes;

namespace Arcadia.GameWorld.Algorithm
{
    /// <summary>
    /// <see cref="GenerateTilesRandomly"/> is an algorithm that places tiles randomly in an area based on the density
    /// and seed provided. If an integer representation of a tile in an area is negative, then that tile is considered
    /// not part of the area. The size and shape of the placed tiles can be adjusted (i.e. circle, square). Also, this
    /// algorithm can handle multiple tile types at the same time.
    /// </summary>
    public sealed class GenerateTilesRandomly
    {
        /// <summary>
        /// Runs the algorithm that randomly places a tile in an <paramref name="area"/>.
        /// </summary>
        /// <param name="area">A 2D integer array representation of an area.</param>
        /// <param name="tileType">An integer representation of a specific tile.</param>
        /// <param name="density">
        /// The higher the value, the more tiles are placed.
        /// <br>Ranges from [0, 100]; anything higher or lower has no effect.</br>
        /// </param>
        public static void Run(int[,] area, int tileType, int density)
        {
            for (int tileX = 0; tileX < area.GetLength(0); ++tileX)
            {
                for (int tileY = 0;  tileY < area.GetLength(1); ++tileY)
                {
                    HandleTilePlacement(area, tileX, tileY, tileType, density);
                }
            }
        }

        /// <summary>
        /// Runs the algorithm that randomly places tiles in an <paramref name="area"/>. Each given tile type have the
        /// same chance of being placed.
        /// </summary>
        /// <param name="area">A 2D integer array representation of an area.</param>
        /// <param name="tileTypes">An array of integers representing specific tiles.</param>
        /// <param name="density">
        /// The higher the value, the more tiles are placed.
        /// <br>Ranges from [0, 100]; anything higher or lower has no effect.</br>
        /// </param>
        public static void Run(int[,] area, int[] tileTypes, int density)
        {
            for (int tileX = 0; tileX < area.GetLength(0); ++tileX)
            {
                for (int tileY = 0; tileY < area.GetLength(1); ++tileY)
                {
                    HandleTilePlacement(area, tileX, tileY, tileTypes, density);
                }
            }
        }

        /// <summary>
        /// Runs the algorithm that randomly places tiles in an <paramref name="area"/>. The placed tile type will be
        /// randomly selected based on their weights. A higher weight value will increase its chances of being selected.
        /// </summary>
        /// <param name="area">A 2D integer array representation of an area.</param>
        /// <param name="tileTypes">An array of integers representing specific tiles.</param>
        /// <param name="tileWeights">The weights of each tile type.</param>
        /// <param name="density">
        /// The higher the value, the more tiles are placed.
        /// <br>Ranges from [0, 100]; anything higher or lower has no effect.</br>
        /// </param>
        public static void Run(int[,] area, int[] tileTypes, int[] tileWeights, int density)
        {
            if (tileTypes.Length != tileWeights.Length)
            {
                return;
            }

            int[] weightThresholds = GetWeightThresholds(tileWeights);

            for (int tileX = 0; tileX < area.GetLength(0); ++tileX)
            {
                for (int tileY = 0; tileY < area.GetLength(1); ++tileY)
                {
                    HandleTilePlacement(area, tileX, tileY, tileTypes, weightThresholds, density);
                }
            }
        }
        public static void Run(int[,] area, int tileType, int density, Shape shape)
        {
            for (int tileX = 0; tileX < area.GetLength(0); ++tileX)
            {
                for (int tileY = 0; tileY < area.GetLength(1); ++tileY)
                {
                    HandleTilePlacement(area, tileX, tileY, tileType, density, shape);
                }
            }
        }

        public static void Run(int[,] area, int[] tileTypes, int density, Shape shape)
        {

        }

        public static void Run(int[,] area, int[] tileTypes, int[] tileWeights, int density, Shape shape)
        {
        }

        private static void HandleTilePlacement(int[,] world, int tileX, int tileY, int tileType, int density)
        {
            int numberGenerated = UniversalRandom.Next(1, 100);
            if (numberGenerated <= density)
            {
                world[tileX, tileY] = tileType;
            }
        }

        private static void HandleTilePlacement(int[,] world, int tileX, int tileY, int[] tileTypes, int density)
        {
            int numberGenerated = UniversalRandom.Next(1, 100);
            if (numberGenerated > density)
            {
                return;
            }

            int randomIndex = UniversalRandom.Next(1, tileTypes.Length) - 1;
            world[tileX, tileY] = tileTypes[randomIndex];
        }

        private static void HandleTilePlacement(int[,] world, int tileX, int tileY, int[] tileTypes, int[] weightThresholds, int density)
        {
            int numberGenerated = UniversalRandom.Next(1, 100);
            if (numberGenerated > density)
            {
                return;
            }

            int numberOfTiles = tileTypes.Length;
            int totalWeight = weightThresholds[numberOfTiles - 1];
            int randomIndex = UniversalRandom.Next(1, totalWeight);

            for (int i = 0; i < numberOfTiles; ++i)
            {
                if (randomIndex <= weightThresholds[i])
                {
                    world[tileX, tileY] = tileTypes[i];
                    return;
                }
            }
        }

        private static void HandleTilePlacement(int[,] world, int tileX, int tileY, int tileType, int density, Shape shape)
        {
            int numberGenerated = UniversalRandom.Next(1, 100);
            if (numberGenerated <= density)
            {
                world[tileX, tileY] = tileType;
            }
        }

        private static int[] GetWeightThresholds(int[] weights)
        {
            int numberOfWeights = weights.Length;
            int[] thresholds = new int[numberOfWeights];

            thresholds[0] = weights[0];
            for (int i = 1; i < numberOfWeights; ++i)
            {
                thresholds[i] = thresholds[i-1] + weights[i];
            }

            return thresholds;
        }
    }
}
