using System;

namespace Arcadia.GameWorld.Algorithm
{
    internal sealed class UniversalRandom
    {
        internal static void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        internal static void SetSeed(long seed)
        {
            _random = new Random(seed.GetHashCode());
        }

        internal static int Next()
        {
            return _random.Next();
        }

        internal static int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        internal static int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        internal static void NextBytes(byte[] buffer)
        {
            _random.NextBytes(buffer);
        }

        internal static double NextDouble()
        {
            return _random.NextDouble();
        }

        internal static double NextInt64()
        {
            return _random.NextInt64();
        }

        internal static float NextSingle()
        {
            return _random.NextSingle();
        }

        private static Random _random = new Random();
    }
}
