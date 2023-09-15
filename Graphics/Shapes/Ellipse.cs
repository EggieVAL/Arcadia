using System;

namespace Arcadia.Graphics.Shapes
{
    public class Ellipse : Shape
    {
        public float Area => MathF.PI * RadiusX * RadiusY;

        public float Perimeter => throw new NotImplementedException();

        public float Circumference => Perimeter;

        public float RadiusX { get; set; }

        public float RadiusY { get; set; }

        public Vertex Center { get; set; }

        public Ellipse(float radiusX, float radiusY, Vertex center)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
            Center = center;
        }

        public bool ContainsPoint(float x, float y, bool inclusive)
        {
            float calculatedValue = (x - Center.X) * (x - Center.X) / (RadiusX * RadiusX) +
                                    (y - Center.Y) * (y - Center.Y) / (RadiusY * RadiusY);

            return inclusive ? calculatedValue <= 1 : calculatedValue < 1;
        }

        public bool IsPointOnEdge(float x, float y)
        {
            float calculatedValue = (x - Center.X) * (x - Center.X) / (RadiusX * RadiusX) +
                                    (y - Center.Y) * (y - Center.Y) / (RadiusY * RadiusY);

            return calculatedValue == 1;
        }
    }
}
