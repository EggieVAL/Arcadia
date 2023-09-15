using System;

namespace Arcadia
{
    internal static class UniversalRandom
    {
        internal static void SetSeed(int seed)
        {
            _random = new Random(seed);
        }

        internal static void SetSeed(long seed)
        {
            _random = new Random(seed.GetHashCode());
        }

        internal static int Next() => _random.Next();

        internal static int Next(int maxValue) => _random.Next(maxValue);

        internal static int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);

        internal static void NextBytes(byte[] buffer) => _random.NextBytes(buffer);

        internal static double NextDouble() => _random.NextDouble();

        internal static double NextInt64() => _random.NextInt64();

        internal static float NextSingle() => _random.NextSingle();

        private static Random _random = new();
    }
}
