using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObject.Tiles
{
    /// <summary>
    ///     The <c>ATile</c> class is an abstraction of a game tile.
    /// </summary>
    public abstract class ATile : ARenderableObject
    {
        /// <summary>
        ///     Constructs a tile of some texture at some (x, y) position.
        /// </summary>
        /// <param name="texture">The texture of a tile.</param>
        /// <param name="unitx">The x-coordinate in units.</param>
        /// <param name="unity">The y-coordinate in units.</param>
        public ATile(Texture2D texture, int unitx, int unity)
            : base(texture, new Rectangle(unitx, unity, Grid.Size, Grid.Size))
        {
        }
    }
}
