namespace Arcadia.Graphics.Shapes
{
    /// <summary>
    /// The <see cref="Vertex"/> class is a representation of a vertex. A vertex is in the form of (x, y), where x and y are some coordinate
    /// value in units.
    /// </summary>
    public sealed class Vertex
    {
        /// <summary>
        /// The x-coordinate in units.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y-coordinate in units.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Constructs a vertex (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x-coordinate in units.</param>
        /// <param name="y">The y-coordinate in units.</param>
        public Vertex(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
