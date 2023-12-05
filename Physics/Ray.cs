using Arcadia.GameObjects;
using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcadia.Physics
{
    public class Ray
    {
        public Vector2 Start;
        public Vector2 Direction;
        public float Length = 250f;

        public Ray(Vector2 start, Vector2 dir)
        {
            Start = start;
            Direction = dir;
        }
        public Ray(Vector2 start, Vector2 dir, float length)
        {
            Start = start;
            Direction = dir;
            Length = length;
        }

        public bool Intersecting(AABB box)
        {

        }

        public RayHit Cast()
        {
            return new RayHit();
        }

    }

    public struct RayHit
    {
        public Vector2 point;
        public RenderableObject obj;

        public RayHit(Vector2 point, RenderableObject obj)
        {
            this.point = point;
            this.obj = obj;
        }
    }
}

