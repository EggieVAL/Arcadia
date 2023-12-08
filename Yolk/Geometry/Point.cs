using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.Geometry
{
    [DataContract]
    public struct Point : IEquatable<Point>
    {
        [DataMember]
        public int X;

        [DataMember]
        public int Y;

        [DataMember]
        public bool Inclusive;

        public Point(int x, int y, bool inclusive)
        {
            X = x;
            Y = y;
            Inclusive = inclusive;
        }

        public Point(int x, int y) : this(x, y, true)
        {
        }

        public Point(int value, bool inclusive) : this(value, value, inclusive)
        {
        }

        public Point(int value) : this(value, true)
        {
        }

        public static Point Origin => new(0);

        public static explicit operator Microsoft.Xna.Framework.Point(Point point)
        {
            return new Microsoft.Xna.Framework.Point(point.X, point.Y);
        }

        public static implicit operator PointF(Point point)
        {
            return new PointF(point.X, point.Y, point.Inclusive);
        }

        public static explicit operator System.Drawing.Point(Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }

        public static explicit operator System.Drawing.PointF(Point point)
        {
            return new System.Drawing.PointF(point.X, point.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator +(Point addend1, Point addend2)
        {
            return new Point(
                addend1.X + addend2.X,
                addend1.Y + addend2.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator +(Point addend1, int addend2)
        {
            return addend1 + new Point(addend2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator +(int addend1, Point addend2)
        {
            return addend2 + addend1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator -(Point minuend, Point subtrahend)
        {
            return new Point(
                minuend.X - subtrahend.X,
                minuend.Y - subtrahend.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator -(Point minuend, int subtrahend)
        {
            return minuend - new Point(subtrahend);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator -(int minuend, Point subtrahend)
        {
            return new Point(minuend) - subtrahend;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator -(Point point)
        {
            return Origin - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator *(Point multiplicand, Point multiplier)
        {
            return new Point(
                multiplicand.X * multiplier.X,
                multiplicand.Y * multiplier.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator *(Point multiplicand, int multiplier)
        {
            return multiplicand * new Point(multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator *(int multiplicand, Point multiplier)
        {
            return multiplier * multiplicand;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator /(Point dividend, Point divisor)
        {
            return new Point(
                dividend.X / divisor.X,
                dividend.Y / divisor.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator /(Point dividend, int divisor)
        {
            return dividend / new Point(divisor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point operator /(int dividend, Point divisor)
        {
            return new Point(dividend) / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Point point, Point other)
        {
            return point.X == other.X
                && point.Y == other.Y
                && point.Inclusive == other.Inclusive;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Point point, Point other)
        {
            return !(point == other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Add(Point addend1, Point addend2)
        {
            return addend1 + addend2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CrossProduct(Point point, Point other)
        {
            return (point.X * other.Y) - (point.Y * other.X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Point point, Point other)
        {
            return MathF.Sqrt(DistanceSquared(point, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DistanceSquared(Point point, Point other)
        {
            Point difference = point - other;
            return DotProduct(difference, difference);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Divide(Point dividend, Point divisor)
        {
            return dividend / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DotProduct(Point point, Point other)
        {
            return (point.X * other.Y) + (point.Y * other.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Lerp(Point point, Point other, int amount)
        {
            return point + ((other - point) * amount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Multiply(Point multiplicand, Point multiplier)
        {
            return multiplicand * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Subtract(Point minuend, Point subtrahend)
        {
            return minuend - subtrahend;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Negate(Point point)
        {
            return -point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ReflectOverXAxis(Point point)
        {
            return new Point(point.X, -point.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point ReflectOverYAxis(Point point)
        {
            return new Point(-point.X, point.Y);
        }

        public readonly Point ClosedPoint()
        {
            return new Point(X, Y, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Point other) && Equals(other);
        }

        public readonly bool Equals(Point other)
        {
            return this == other;
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + X.GetHashCode();
                hash = (hash * 7) + Y.GetHashCode();
                hash = (hash * 7) + Inclusive.GetHashCode();
                return hash;
            }
        }

        public readonly bool IsIdentical(Point other)
        {
            return this == other;
        }

        public readonly Point OpenPoint()
        {
            return new Point(X, Y, false);
        }

        public readonly PointF ToPointF()
        {
            return this;
        }

        public override readonly string ToString()
        {
            return $"{{X:{X}, Y:{Y}}}";
        }

        public readonly System.Drawing.Point ToSystemPoint()
        {
            return (System.Drawing.Point) this;
        }

        public readonly System.Drawing.PointF ToSystemPointF()
        {
            return (System.Drawing.PointF) this;
        }

        public readonly System.Numerics.Vector2 ToSystemVector()
        {
            return new System.Numerics.Vector2(X, Y);
        }

        public readonly Vector ToVector()
        {
            return new Vector(X, Y);
        }

        public readonly Microsoft.Xna.Framework.Point ToXnaPoint()
        {
            return (Microsoft.Xna.Framework.Point) this;
        }

        public readonly Microsoft.Xna.Framework.Vector2 ToXnaVector()
        {
            return new Microsoft.Xna.Framework.Vector2(X, Y);
        }

        public void Translate(Point delta)
        {
            Translate(delta.X, delta.Y);
        }

        public void Translate(int dx, int dy)
        {
            unchecked
            {
                X += dx;
                Y += dy;
            }
        }
    }
}
