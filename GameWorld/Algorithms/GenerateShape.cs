using Arcadia.Graphics;
using Arcadia.Graphics.Shapes;
using System.Drawing;

namespace Arcadia.GameWorld.Algorithms
{
    /// <summary>
    /// The <see cref="GenerateShape"/> class is an algorithm that generates a shape.
    /// </summary>
    public static class GenerateShape
    {
        /// <summary>
        /// Generates a <paramref name="shape"/> as an <paramref name="area"/>.
        /// </summary>
        /// <param name="area">The shape in the form of a 2D integer array.</param>
        /// <param name="shape">The shape to generate.</param>
        public static void Run(out int[,] area, Shape shape)
        {
            area = null;

            if (shape is Polygon)
            {
                area = GeneratePolygon((Polygon) shape);
            }
            else if (shape.GetType() == typeof(Circle))
            {
                area = GenerateCircle((Circle) shape);
            }
            else if (shape.GetType() == typeof(Ellipse))
            {
                area = GenerateEllipse((Ellipse) shape);
            }
        }

        private static int[,] GeneratePolygon(Polygon polygon)
        {
            polygon.GetBoundingBox(out Vertex min, out Vertex max);

            int minX = (int) min.X;
            int minY = (int) min.Y;
            int maxX = (int) max.X;
            int maxY = (int) max.Y;

            int width = maxX - minX + 1;
            int height = maxY - minY + 1;
            int[,] area = new int[width, height];

            for (int x = minX; x <= maxX; ++x)
            {
                for (int y = minY; y <= maxY; ++y)
                {
                    HandleInkPlacement(area, new Point(x - minX, y - minY), new Point(x, y), polygon);
                }
            }
            return area;
        }

        private static int[,] GenerateCircle(Circle circle)
        {
            int centerX = (int) circle.Center.X;
            int centerY = (int) circle.Center.Y;
            int radius = (int) circle.Radius;

            int minX = centerX - radius;
            int minY = centerY - radius;
            int maxX = centerX + radius;
            int maxY = centerY + radius;

            int size = radius + radius;
            int[,] area = new int[size, size];

            for (int x = minX; x < maxX; ++x)
            {
                for (int y = minY; y < maxY; ++y)
                {
                    HandleInkPlacement(area, new Point(x - minX, y - minY), new Point(x, y), circle);
                }
            }
            return area;
        }

        private static int[,] GenerateEllipse(Ellipse ellipse)
        {
            int centerX = (int) ellipse.Center.X;
            int centerY = (int) ellipse.Center.Y;
            int radiusX = (int) ellipse.RadiusX;
            int radiusY = (int) ellipse.RadiusY;

            int minX = centerX - radiusX;
            int minY = centerY - radiusY;
            int maxX = centerX + radiusX;
            int maxY = centerY + radiusY;

            int width = radiusX + radiusX;
            int height = radiusY + radiusY;
            int[,] area = new int[width, height];

            for (int x = minX; x < maxX; ++x)
            {
                for (int y = minY; y < maxY; ++y)
                {
                    HandleInkPlacement(area, new Point(x - minX, y - minY), new Point(x, y), ellipse);
                }
            }
            return area;
        }

        private static void HandleInkPlacement(int[,] area, Point placeAt, Point inShape, Shape shape)
        {
            area[placeAt.X, placeAt.Y] = (shape.ContainsPoint(inShape.X + 0.5f, inShape.Y + 0.5f, true))
                ? Ink.Default : Ink.Ignore;
        }
    }
}
