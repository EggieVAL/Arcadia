using System;

namespace Arcadia.Graphics.Shapes
{
    public interface Shape
    {
        float Area { get; }

        float Perimeter { get; }

        bool ContainsPoint(float x, float y, bool inclusive);

        bool IsPointOnEdge(float x, float y);
    }
}
