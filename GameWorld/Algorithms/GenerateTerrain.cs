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
                //General noise
                float noise = ImprovedNoise.Noise(positionX, positionY, 0) * 0.5f + 0.5f;
                //Jaggedness
                float noise2 = ImprovedNoise.Noise(positionX*0.5f, positionY*0.1f, 10);
                //Elongation
                float noise3 = ImprovedNoise.Noise(positionX*0.1f, positionY * 0.1f, 100);

                int perlinHeight = (int) ((noise + noise2 * 10 + noise3 * 8) + height / 2f);

                for (int y = height-1; y >= perlinHeight; --y)
                {
                    area[x, y] = ink;
                }
            }
        }
    }
}
