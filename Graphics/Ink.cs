namespace Arcadia.Graphics
{
    /// <summary>
    /// Some different types of <see cref="Ink"/>s.
    /// </summary>
    public enum Ink
    {
        /// <summary>
        /// Ignore this ink when performing algorithms.
        /// </summary>
        Ignore = -1,

        /// <summary>
        /// An ink that's transparent.
        /// </summary>
        Transparent = 0,

        /// <summary>
        /// The default ink.
        /// </summary>
        Default = 1
    }
}
