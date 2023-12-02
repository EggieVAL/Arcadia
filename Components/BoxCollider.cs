using Arcadia.Graphics.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcadia.Components
{
    public class BoxCollider : Collider
    {
        public BoxCollider(Rectangle rect) : base(rect)
        {

        }

        public override bool IsColliding(Collider collision)
        {
            if (collision.Shape is Rectangle)
            {
                Rectangle shape = (Rectangle)Shape;
                Rectangle other = (Rectangle)collision.Shape;
                // Check for collision by comparing positions and dimensions
                if (shape.GetLeft() < other.GetRight() &&
                    shape.GetRight() > other.GetLeft() &&
                    shape.GetTop() < other.GetBottom() &&
                    shape.GetBottom() > other.GetTop())
                {
                    // Rectangles are colliding
                    return true;
                }
            }
            else if (Shape is Circle && collision.Shape is Circle)
            {
                Circle circle1 = (Circle)Shape;
                Circle circle2 = (Circle)collision.Shape;

                // Calculate the distance between the centers of the circles
                float distance = MathF.Sqrt(
                    (circle2.Center.X - circle1.Center.X) * (circle2.Center.X - circle1.Center.X) +
                    (circle2.Center.Y - circle1.Center.Y) * (circle2.Center.Y - circle1.Center.Y)
                );

                // Check for collision by comparing the distance to the sum of the radii
                if (distance < (circle1.Radius + circle2.Radius))
                {
                    // Circles are colliding
                    return true;
                }
            }
            else if (Shape is Ellipse && collision.Shape is Ellipse)
            {
                Ellipse ellipse1 = (Ellipse)Shape;
                Ellipse ellipse2 = (Ellipse)collision.Shape;

                // Calculate the distance between the centers of the ellipses
                float distance = MathF.Sqrt(
                    ((ellipse2.Center.X - ellipse1.Center.X) / ellipse1.RadiusX) * ((ellipse2.Center.X - ellipse1.Center.X) / ellipse1.RadiusX) +
                    ((ellipse2.Center.Y - ellipse1.Center.Y) / ellipse1.RadiusY) * ((ellipse2.Center.Y - ellipse1.Center.Y) / ellipse1.RadiusY)
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
