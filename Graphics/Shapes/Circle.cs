using System;

namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// The <see cref="Circle"/> class is a representation of a circle.
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// The area of a circle.
        /// </summary>
        public float Area => MathF.PI * Radius * Radius;

        /// <summary>
        /// The perimeter of a circle.
        /// </summary>
        public float Perimeter => MathF.PI * Diameter;

        /// <summary>
        /// The circumference of a circle.
        /// </summary>
        public float Circumference => Perimeter;

        /// <summary>
        /// The radius of a circle.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// The center point of a circle.
        /// </summary>
        public Vertex Center { get; set; }

        /// <summary>
        /// The diameter of a circle.
        /// </summary>
        public float Diameter => Radius + Radius;

        /// <summary>
        /// Constructs a circle with some radius at a <paramref name="center"/> point.
        /// </summary>
        /// <param name="radius">The radius of a circle in units.</param>
        /// <param name="center">The center point of a circle.</param>
        public Circle(float radius, Vertex center)
        {
            Radius = radius;
            Center = center;
        }

        /// <summary>
        /// Determines whether the circle contain the point (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <param name="inclusive">Should the edge of the circle be included in the set of points it contains?</param>
        /// <returns><c>true</c> if the circle contains (<paramref name="x"/>, <paramref name="y"/>); otherwise <c>false</c>.</returns>
        public bool ContainsPoint(float x, float y, bool inclusive)
        {
            float calculatedValue = (x - Center.X) * (x - Center.X) +
                                    (y - Center.Y) * (y - Center.Y);

            float radiusSquared = Radius * Radius;

            return inclusive ? calculatedValue <= radiusSquared
                             : calculatedValue < radiusSquared;
        }

        /// <summary>
        /// Determines whether the given point <paramref name="x"/>, <paramref name="y"/>) is on any edge of a circle.
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns><c>true</c> if <paramref name="x"/>, <paramref name="y"/>) is on any edge; otherwise <c>false</c>.</returns>
        public bool IsPointOnEdge(float x, float y)
        {
            float calculatedValue = (x - Center.X) * (x - Center.X) +
                                    (y - Center.Y) * (y - Center.Y);

            float radiusSquared = Radius * Radius;

            return calculatedValue == radiusSquared;
        }
    }
}
