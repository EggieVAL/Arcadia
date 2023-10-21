using System;

namespace Arcadia.Graphics.Shapes
{
    public class Collision
    {
        public Shape Shape { get; set; }

        public Collision(Shape shape)
        {
            Shape = shape;
        }

        public bool IsColliding(Collision collision)
        {
            if (Shape is Rectangle && collision.Shape is Rectangle)
            {
                Rectangle rect1 = (Rectangle)Shape;
                Rectangle rect2 = (Rectangle)collision.Shape;

                // Check for collision by comparing positions and dimensions
                if (rect1.GetLeft() < rect2.GetRight() &&
                    rect1.GetRight() > rect2.GetLeft() &&
                    rect1.GetTop() < rect2.GetBottom() &&
                    rect1.GetBottom() > rect2.GetTop())
                {
                    // Rectangles are colliding
                    return true;
                }
            }

                // Handle collision detection for other shape types here

                return false; // Default to not colliding
        }



    }
}

