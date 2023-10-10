using System;

namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// A <see cref="Shape"/> consists of all polygons (i.e. triangles, squares, rectangles, trapezoids) and ellipses (i.e.
    /// ellipses and circles) and more.
    /// </summary>
    public interface Shape
    {
        /// <summary>
        /// The area of a shape.
        /// </summary>
        float Area { get; }

        /// <summary>
        /// The perimeter of a shape.
        /// </summary>
        float Perimeter { get; }

        /// <summary>
        /// Determines whether the shape contain the point (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <param name="inclusive">Should the edge of the shape be included in the set of points it contains?</param>
        /// <returns><c>true</c> if the shape contains (<paramref name="x"/>, <paramref name="y"/>); otherwise <c>false</c>.</returns>
        bool ContainsPoint(float x, float y, bool inclusive);

        /// <summary>
        /// Determines whether the given point <paramref name="x"/>, <paramref name="y"/>) is on any edge of a shape.
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns><c>true</c> if <paramref name="x"/>, <paramref name="y"/>) is on any edge; otherwise <c>false</c>.</returns>
        bool IsPointOnEdge(float x, float y);
    }
}
