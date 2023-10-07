using System;

namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// The <see cref="Edge"/> class is a representation of an edge. Two vertices make up an edge.
    /// </summary>
    public sealed class Edge
    {
        /// <summary>
        /// The first vertex of an edge.
        /// </summary>
        public Vertex VertexA { get; set; }

        /// <summary>
        /// The second vertex of an edge.
        /// </summary>
        public Vertex VertexB { get; set; }

        /// <summary>
        /// The length of an edge.
        /// </summary>
        public float Length => MathF.Sqrt(MathF.Pow(VertexB.X - VertexA.X, 2) + MathF.Pow(VertexB.Y - VertexA.Y, 2));

        /// <summary>
        /// Constructs an edge with the given vertices.
        /// </summary>
        /// <param name="vertexA">The first vertex of an edge.</param>
        /// <param name="vertexB">The second vertex of an edge.</param>
        public Edge(Vertex vertexA, Vertex vertexB)
        {
            VertexA = vertexA;
            VertexB = vertexB;
        }

        /// <summary>
        /// Determines whether shooting a ray at (<paramref name="x"/>, <paramref name="y"/>) to the right will intersect an edge.
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <param name="inclusive">Should the starting point of the ray be included?</param>
        /// <returns><c>true</c> if the ray intersects an edge; otherwise <c>false</c>.</returns>
        public bool RayIntersectsEdge(float x, float y, bool inclusive)
        {
            return InBetweenY(y, inclusive) &&
                   LeftOfIntersectX(x, y, inclusive);
        }
        private bool InBetweenY(float y, bool inclusive)
        {
            return inclusive ? y <= VertexA.Y != y <= VertexB.Y
                             : y < VertexA.Y != y < VertexB.Y;
        }

        private bool LeftOfIntersectX(float x, float y, bool inclusive)
        {
            return inclusive ? x <= VertexA.X + (y - VertexA.Y) / (VertexB.Y - VertexA.Y) * (VertexB.X - VertexA.X)
                             : x < VertexA.X + (y - VertexA.Y) / (VertexB.Y - VertexA.Y) * (VertexB.X - VertexA.X);
        }
    }
}
