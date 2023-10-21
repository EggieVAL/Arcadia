namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// The <see cref="Square"/> class is a representation of a square. A square is a type of polygon that has four edges of equal lengths.
    /// These edges form a 90 degree angle.
    /// </summary>
    public class Square : Polygon
    {
        /// <summary>
        /// The area of a square.
        /// </summary>
        public override float Area => Length * Length;

        /// <summary>
        /// The length of each edge.
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// Constructs a square with some <paramref name="length"/> at a <paramref name="center"/> point.
        /// </summary>
        /// <param name="length">The length of a square in units.</param>
        /// <param name="center">The center point of a square.</param>
        public Square(float length, Vertex center) : base(GetVertices(length, center))
        {
            Length = length;
        }

        private static Vertex[] GetVertices(float length, Vertex center)
        {
            Vertex[] vertices = new Vertex[4];

            float halfOfLength = length / 2;

            float minX = center.X - halfOfLength;
            float minY = center.Y - halfOfLength;
            float maxX = minX + length;
            float maxY = minY + length;

            vertices[0] = new Vertex(minX, minY);
            vertices[1] = new Vertex(maxX, minY);
            vertices[2] = new Vertex(maxX, maxY);
            vertices[3] = new Vertex(minX, maxY);

            return vertices;
        }
        public float GetLeft()
        {
            float minX = float.MaxValue;
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.X < minX)
                {
                    minX = vertex.X;
                }
            }
            return minX;
        }

        public float GetRight()
        {
            float maxX = float.MinValue;
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.X > maxX)
                {
                    maxX = vertex.X;
                }
            }
            return maxX;
        }

        public float GetTop()
        {
            float minY = float.MaxValue;
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.Y < minY)
                {
                    minY = vertex.Y;
                }
            }
            return minY;
        }

        public float GetBottom()
        {
            float maxY = float.MinValue;
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.Y > maxY)
                {
                    maxY = vertex.Y;
                }
            }
            return maxY;
        }

    }
}
