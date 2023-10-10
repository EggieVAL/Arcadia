using System;

namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// The <see cref="SimplePolygon"/> class is a representation of a simple polygon. A simple polygon must not have any edges intersecting
    /// with another edge.
    /// </summary>
    public class SimplePolygon : Polygon
    {
        /// <summary>
        /// The area of a simple polygon.
        /// </summary>
        public override float Area
        {
            get
            {
                float area = 0f;
                for (int i = 0; i < NumberOfVertices; ++i)
                {
                    int j = (i + 1) % NumberOfVertices;
                    area += Vertices[i].X * Vertices[j].Y - Vertices[j].X * Vertices[i].Y;
                }
                area = 0.5f * MathF.Abs(area);
                return area;
            }
        }

        /// <summary>
        /// Constructs a simple polygon with the given vertices.
        /// </summary>
        /// <param name="vertices"></param>
        public SimplePolygon(Vertex[] vertices) : base(vertices) { }
    }
}
