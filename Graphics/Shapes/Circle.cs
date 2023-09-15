using System;

namespace Arcadia.Graphics.Shapes
{
    public class Circle : Shape
    {
        public float Area => MathF.PI * Radius * Radius;

        public float Perimeter => MathF.PI * Diameter;

        public float Circumference => Perimeter;

        public float Radius { get; set; }

        public Vertex Center { get; set; }

        public float Diameter => Radius + Radius;

        public Circle(float radius, Vertex center)
        {
            Radius = radius;
            Center = center;
        }

        public bool ContainsPoint(float x, float y, bool inclusive)
        {
            float calculatedValue = (x - Center.X) * (x - Center.X) +
                                    (y - Center.Y) * (y - Center.Y);

            float radiusSquared = Radius * Radius;

            return inclusive ? calculatedValue <= radiusSquared
                             : calculatedValue < radiusSquared;
        }

        public bool IsPointOnEdge(float x, float y)
        {
            float calculatedValue = (x - Center.X) * (x - Center.X) +
                                    (y - Center.Y) * (y - Center.Y);

            float radiusSquared = Radius * Radius;

            return calculatedValue == radiusSquared;
        }
    }
}
