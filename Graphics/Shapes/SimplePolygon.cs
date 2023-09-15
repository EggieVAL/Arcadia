using System;

namespace Arcadia.Graphics.Shapes
{
    public class SimplePolygon : Polygon
    {
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

        public SimplePolygon(Vertex[] vertices) : base(vertices) { }
    }
}
