using System;

namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// The <see cref="Ellipse"/> class is a representation of an ellipse. An ellipse is a ellipitical shape that have two radii. If these
    /// radii are the same, a circle is formed.
    /// </summary>
    public class Ellipse : Shape
    {
        /// <summary>
        /// The area of an ellipse.
        /// </summary>
        public float Area => MathF.PI * RadiusX * RadiusY;

        /// <summary>
        /// The perimeter of an ellipse.
        /// </summary>
        public float Perimeter => throw new NotImplementedException();

        /// <summary>
        /// The circumference of an ellipse.
        /// </summary>
        public float Circumference => Perimeter;

        /// <summary>
        /// The x-component radius of an ellipse
        /// </summary>
        public float RadiusX { get; set; }

        /// <summary>
        /// The y-component radius of an ellipse.
        /// </summary>
        public float RadiusY { get; set; }

        /// <summary>
        /// The center point of an ellipse.
        /// </summary>
        public Vertex Center { get; set; }

        /// <summary>
        /// Constructs an ellipse with the given radii at a <paramref name="center"/> point.
        /// </summary>
        /// <param name="radiusX">The x-component radius of an ellipse in units.</param>
        /// <param name="radiusY">The y-component radius of an ellipse in units.</param>
        /// <param name="center">The center point of an ellipse.</param>
        public Ellipse(float radiusX, float radiusY, Vertex center)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
            Center = center;
        }

        /// <summary>
        /// Determines whether the ellipse contain the point (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <param name="inclusive">Should the edge of the ellipse be included in the set of points it contains?</param>
        /// <returns><c>true</c> if the ellipse contains (<paramref name="x"/>, <paramref name="y"/>); otherwise <c>false</c>.</returns>
        public bool ContainsPoint(float x, float y, bool inclusive)
        {
            float calculatedValue = (x - Center.X) * (x - Center.X) / (RadiusX * RadiusX) +
                                    (y - Center.Y) * (y - Center.Y) / (RadiusY * RadiusY);

            return inclusive ? calculatedValue <= 1 : calculatedValue < 1;
        }

        /// <summary>
        /// Determines whether the given point <paramref name="x"/>, <paramref name="y"/>) is on any edge of a ellipse.
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns><c>true</c> if <paramref name="x"/>, <paramref name="y"/>) is on any edge; otherwise <c>false</c>.</returns>
        public bool IsPointOnEdge(float x, float y)
        {
            float calculatedValue = (x - Center.X) * (x - Center.X) / (RadiusX * RadiusX) +
                                    (y - Center.Y) * (y - Center.Y) / (RadiusY * RadiusY);

            return calculatedValue == 1;
        }
    }
}
