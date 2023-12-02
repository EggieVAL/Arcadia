namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// The <see cref="Rectangle"/> class is a representation of a rectangle. A rectangle is a type of polygon that has four edges: the two
    /// edges parallel to each other are the same length, the other two edges can have a different length. The edges The edges form a 90
    /// degree angle.
    /// </summary>
    public class Rectangle : Polygon
    {
        /// <summary>
        /// The area of a rectangle.
        /// </summary>
        public override float Area => Width * Height;

        /// <summary>
        /// The width of a rectangle.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// The height of a rectangle.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Constructs a rectangle with some <paramref name="width"/> and <paramref name="height"/> at a <paramref name="center"/> point.
        /// </summary>
        /// <param name="width">The width of a rectangle in units.</param>
        /// <param name="height">The height of a rectangle in units.</param>
        /// <param name="center">The center point of a rectangle.</param>
        public Rectangle(float width, float height, Vertex center) : base(GetVertices(width, height, center))
        {
            Width = width;
            Height = height;
        }


        private static Vertex[] GetVertices(float width, float height, Vertex center)
        {
            Vertex[] vertices = new Vertex[4];

            float halfOfWidth = width / 2;
            float halfOfHeight = height / 2;

            float minX = center.X - halfOfWidth;
            float minY = center.Y - halfOfHeight;
            float maxX = minX + width;
            float maxY = minY + height;

            vertices[0] = new Vertex(minX, minY);
            vertices[1] = new Vertex(maxX, minY);
            vertices[2] = new Vertex(maxX, maxY);
            vertices[3] = new Vertex(minX, maxY);

            return vertices;
        }
        public float GetLeft()
        {
            // Calculate the left edge based on the vertices
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
            // Calculate the right edge based on the vertices
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
            // Calculate the top edge based on the vertices
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
            // Calculate the bottom edge based on the vertices
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
