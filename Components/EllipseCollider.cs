using Arcadia.Graphics.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcadia.Components
{
    public class EllipseCollider : Collider
    {
        public EllipseCollider(Shape shape) : base(shape)
        {
        }

        public override bool IsColliding(Collider collision)
        {
            if (Shape is Ellipse && collision.Shape is Ellipse)
            {
                Ellipse shape = (Ellipse)Shape;
                Ellipse other = (Ellipse)collision.Shape;

                // Calculate the distance between the centers of the ellipses
                float distance = MathF.Sqrt(
                    ((other.Center.X - shape.Center.X) / shape.RadiusX) * ((other.Center.X - shape.Center.X) / shape.RadiusX) +
                    ((other.Center.Y - shape.Center.Y) / shape.RadiusY) * ((other.Center.Y - shape.Center.Y) / shape.RadiusY)
                );

                // Check for collision by comparing the distance to 1 (the edge of an ellipse)
                if (distance < 1)
                {
                    // Ellipses are colliding
                    return true;
                }
            }
            else if (Shape is SimplePolygon && collision.Shape is SimplePolygon)
            {
                SimplePolygon poly1 = (SimplePolygon)Shape;
                SimplePolygon poly2 = (SimplePolygon)collision.Shape;

                // Check for collision based on bounding boxes (left, right, top, and bottom edges)
                if (poly1.GetLeft() < poly2.GetRight() &&
                    poly1.GetRight() > poly2.GetLeft() &&
                    poly1.GetTop() < poly2.GetBottom() &&
                    poly1.GetBottom() > poly2.GetTop())
                {
                    // SimplePolygons are colliding
                    return true;
                }
            }
            return false;
        }
    }
}
