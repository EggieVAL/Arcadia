namespace Arcadia.Graphics.Shapes
{
    public class Square : Polygon
    {
        public override ShapeType ShapeType => ShapeType.Square;

        public override float Area => Length * Length;

        public float Length { get; set; }

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
    }
}
