namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// The <see cref="Polygon"/> class is an abstraction of a polygon.
    /// </summary>
    public abstract class Polygon : Shape
    {
        /// <summary>
        /// The area of a polygon.
        /// </summary>
        public abstract float Area { get; }

        /// <summary>
        /// The perimeter of a polygon.
        /// </summary>
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

        /// <summary>
        /// The vertices of a polygon.
        /// </summary>
        public Vertex[] Vertices { get; set; }

        /// <summary>
        /// The edges of a polygon.
        /// </summary>
        public Edge[] Edges { get; set; }

        /// <summary>
        /// The number of vertices a polygon has.
        /// </summary>
        public int NumberOfVertices => Vertices.Length;

        /// <summary>
        /// The number of edges a polygon has.
        /// </summary>
        public int NumberOfEdges => Edges.Length;

        /// <summary>
        /// Constructs a polygon with the given vertices.
        /// </summary>
        /// <param name="vertices"></param>
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

        /// <summary>
        /// Determines whether the polygon contain the point (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <param name="inclusive">Should the edge of the polygon be included in the set of points it contains?</param>
        /// <returns><c>true</c> if the polygon contains (<paramref name="x"/>, <paramref name="y"/>); otherwise <c>false</c>.</returns>
        public bool ContainsPoint(float x, float y, bool inclusive)
        {
            int numberOfEdgesIntersected = 0;
            foreach (Edge edge in Edges)
            {
                numberOfEdgesIntersected += edge.RayIntersectsEdge(x, y, inclusive) ? 1 : 0;
            }
            return numberOfEdgesIntersected % 2 == 1;
        }

        /// <summary>
        /// Determines whether the given point <paramref name="x"/>, <paramref name="y"/>) is on any edge of a polygon.
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        /// <returns><c>true</c> if <paramref name="x"/>, <paramref name="y"/>) is on any edge; otherwise <c>false</c>.</returns>
        public bool IsPointOnEdge(float x, float y)
        {
            return ContainsPoint(x, y, true) && !ContainsPoint(x, y, false);
        }

        /// <summary>
        /// Gets the bounding box of a polygon. The bounding box is the smallest rectangle that contains the polygon.
        /// </summary>
        /// <param name="min">The minimum point of the bounding box; the top left point.</param>
        /// <param name="max">The maximum point of the bounding box; the bottom right point.</param>
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
