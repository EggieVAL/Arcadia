using System;

namespace Arcadia.Graphics.Shapes
{
    public enum ShapeType
    {
        Ellipse,
        Circle,
        SimplePolygon,
        Square,
        Rectangle
    }

    public sealed class Vertex
    {
        public float X { get; set; }

        public float Y { get; set; }

        public Vertex(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public sealed class Edge
    {
        public Vertex VertexA { get; set; }

        public Vertex VertexB { get; set; }

        public float Length => MathF.Sqrt(MathF.Pow(VertexB.X - VertexA.X, 2) + MathF.Pow(VertexB.Y - VertexA.Y, 2));

        public Edge(Vertex vertexA, Vertex vertexB)
        {
            VertexA = vertexA;
            VertexB = vertexB;
        }

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

    public interface Shape
    {
        ShapeType ShapeType { get; }

        float Area { get; }

        float Perimeter { get; }

        bool ContainsPoint(float x, float y, bool inclusive);

        bool IsPointOnEdge(float x, float y);
    }
}
