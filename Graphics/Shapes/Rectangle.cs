namespace Arcadia.Graphics.Shapes
{
    public class Rectangle : Polygon
    {
        public override ShapeType ShapeType => ShapeType.Rectangle;

        public override float Area => Width * Height;

        public float Width { get; set; }

        public float Height { get; set; }

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
    }
}
