using System;
using Arcadia.Graphics.Shapes;

namespace Arcadia.Components
{
    public abstract class Collider
    {

        public bool IsTrigger { get; set; }

        public Shape Shape { get; set; }

        public Collider(Shape shape)
        {
            Shape = shape;
        }

        public abstract bool IsColliding(Collider collision);
    }
}
