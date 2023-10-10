namespace Arcadia.GameWorld.Algorithms
{
    /// <summary>
    /// The <see cref="GenerateTerrain"/> class is an algorithm that generates terrain in an area.
    /// </summary>
    public static class GenerateTerrain
    {
        // This is prone to change; not finalized.
        public static void Run(int[,] area, int heightLevel, int ink)
        {
            int width = area.GetLength(0);
            int height = area.GetLength(1);

            float perlinScale = height / 6f;

            for (int x = 0; x < width; ++x)
            {
                float positionX = ((float) x / width) * perlinScale;
                float positionY = ((float) heightLevel / height) * perlinScale;
                float noise = ImprovedNoise.Noise(positionX, positionY, 0) * 0.5f + 0.5f;

                int perlinHeight = (int) (noise * 10f + height / 2f);

                for (int y = height-1; y >= perlinHeight; --y)
                {
                    area[x, y] = ink;
                }
            }
        }
    }
}
