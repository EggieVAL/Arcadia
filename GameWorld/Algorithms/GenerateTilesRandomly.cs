using Arcadia.Graphics;

namespace Arcadia.GameWorld.Algorithms
{
    public static class GenerateTilesRandomly
    {
        public static void Run(int[,] area, int density, Brush brush)
        {
            for (int x = 0; x < area.GetLength(0); ++x)
            {
                for (int y = 0; y < area.GetLength(1); ++y)
                {
                    HandleTilePlacement(area, x, y, density, brush);
                }
            }
        }

        private static void HandleTilePlacement(int[,] area, int x, int y, int density, Brush brush)
        {
            if (area[x, y] < 0)
            {
                return;
            }

            int numberGenerated = UniversalRandom.Next(1, 100);
            if (numberGenerated <= density)
            {
                PaintAreaAt(area, x, y, brush);
            }
        }

        private static void PaintAreaAt(int[,] area, int centerX, int centerY, Brush brush)
        {
            int[,] paintedArea = brush.Paint();
            int paintedWidth = paintedArea.GetLength(0);
            int paintedHeight = paintedArea.GetLength(1);

            int minX = centerX - paintedWidth / 2;
            int minY = centerY - paintedHeight / 2;
            int maxX = centerX + paintedWidth;
            int maxY = centerY + paintedHeight;

            for (int x = minX; x < maxX; ++x)
            {
                for (int y = minY; y < maxY; ++y)
                {
                    int indexX = x - minX;
                    int indexY = y - minY;
                    int ink = paintedArea[indexX, indexY];

                    if (ink != (int) Ink.Ignore)
                    {
                        area[x, y] = ink;
                    }
                }
            }
        }
    }
}
