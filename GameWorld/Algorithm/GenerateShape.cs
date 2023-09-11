using Arcadia.Graphics.Shapes;

namespace Arcadia.GameWorld.Algorithm
{
    public sealed class GenerateShape
    {
        public static void Run(out int[,] area, Shape shape)
        {
            area = null;
            if (shape.GetType() == typeof(Circle))
            {
                area = GenerateCircle((Circle) shape);
            }
            else if (shape is Polygon)
            {
                area = GeneratePolygon((Polygon) shape);
            }
        }

        private static int[,] GenerateCircle(Circle circle)
        {
            float centerX = circle.Center.X;
            float centerY = circle.Center.Y;
            float radius = circle.Radius;

            int centerTileX = Grid.GetTilePosition(centerX);
            int centerTileY = Grid.GetTilePosition(centerY);
            int radiusTile = Grid.GetTilePosition(radius);

            int minTileX = centerTileX - radiusTile + 1;
            int minTileY = centerTileX - radiusTile + 1;
            int maxTileX = centerTileX + radiusTile - 1;
            int maxTileY = centerTileY + radiusTile - 1;

            int size = radiusTile + radiusTile;
            int[,] area = new int[size, size];
            EmptyWorld.Run(area);

            for (int tileX = minTileX; tileX <= maxTileX; ++tileX)
            {
                for (int tileY = minTileY; tileY <= maxTileY; ++tileY)
                {
                    float x = tileX * Grid.Size;
                    float y = tileY * Grid.Size;

                    if (circle.ContainsPoint(x, y, true))
                    {
                        int indexX = tileX - minTileX;
                        int indexY = tileY - minTileY;
                        area[indexX, indexY] = 1;
                    }
                }
            }
            return area;
        }

        private static int[,] GeneratePolygon(Polygon polygon)
        {
            polygon.GetBoundingBox(out Vertex min, out Vertex max);

            int minTileX = Grid.GetTilePosition(min.X) + 1;
            int minTileY = Grid.GetTilePosition(min.Y) + 1;
            int maxTileX = Grid.GetTilePosition(max.X);
            int maxTileY = Grid.GetTilePosition(max.Y);

            int tileWidth = maxTileX - minTileX + 1;
            int tileHeight = maxTileY - minTileY + 1;
            int[,] area = new int[tileWidth, tileHeight];
            EmptyWorld.Run(area);

            for (int tileX = minTileX; tileX <= maxTileX; ++tileX)
            {
                for (int tileY = minTileY; tileY <= maxTileY; ++tileY)
                {
                    float x = tileX * Grid.Size;
                    float y = tileY * Grid.Size;

                    if (polygon.ContainsPoint(x, y, true))
                    {
                        int indexX = tileX - minTileX;
                        int indexY = tileY - minTileY;
                        area[indexX, indexY] = 1;
                    }
                }
            }
            return area;
        }
    }
}
