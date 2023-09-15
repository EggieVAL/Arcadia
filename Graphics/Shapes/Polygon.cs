namespace Arcadia.Graphics.Shapes
{
    public abstract class Polygon : Shape
    {
        public abstract float Area { get; }

        public float Perimeter
        {
            get
            {
                float perimeter = 0f;
                foreach (Edge edge in Edges)
                {
                    perimeter += edge.Length;
                }
                return perimeter;
            }
        }

        public Vertex[] Vertices { get; set; }

        public Edge[] Edges { get; set; }

        public int NumberOfVertices => Vertices.Length;

        public int NumberOfEdges => Edges.Length;

        public Polygon(Vertex[] vertices)
        {
            Vertices = vertices;
            Edges = new Edge[NumberOfVertices];

            for (int i = 0; i < NumberOfEdges; ++i)
            {
                int a = i;
                int b = (a + 1) % NumberOfEdges;

                Edges[i] = new Edge(vertices[a], vertices[b]);
            }
        }

        public bool ContainsPoint(float x, float y, bool inclusive)
        {
            int numberOfEdgesIntersected = 0;
            foreach (Edge edge in Edges)
            {
                numberOfEdgesIntersected += edge.RayIntersectsEdge(x, y, inclusive) ? 1 : 0;
            }
            return numberOfEdgesIntersected % 2 == 1;
        }

        public bool IsPointOnEdge(float x, float y)
        {
            return ContainsPoint(x, y, true) && !ContainsPoint(x, y, false);
        }

        public void GetBoundingBox(out Vertex min, out Vertex max)
        {
            min = new Vertex(float.MaxValue, float.MaxValue);
            max = new Vertex(float.MinValue, float.MinValue);

            foreach (Vertex vertex in Vertices)
            {
                if (vertex.X < min.X)
                {
                    min.X = vertex.X;
                }
                else if (vertex.X > max.X)
                {
                    max.X = vertex.X;
                }

                if (vertex.Y < min.Y)
                {
                    min.Y = vertex.Y;
                }
                else if (vertex.Y > max.Y)
                {
                    max.Y = vertex.Y;
                }
            }
        }
    }
}
