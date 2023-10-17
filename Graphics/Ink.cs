namespace Arcadia.Graphics
{
    /// <summary>
    /// Some different types of <see cref="Ink"/>s.
    /// </summary>
    public static class Ink
    {
        /// <summary>
        /// Ignore this ink when performing algorithms.
        /// </summary>
        public static readonly int Ignore = -1;

        /// <summary>
        /// An ink that's transparent.
        /// </summary>
        public static readonly int Transparent = 0;

        /// <summary>
        /// The default ink.
        /// </summary>
        public static readonly int Default = 1;
    }
}
