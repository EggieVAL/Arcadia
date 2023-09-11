using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObject.Tiles
{
    /// <summary>
    /// The <c>Dirt</c> class is a representation of a dirt tile.
    /// </summary>
    public sealed class Dirt : Tile
    {
        /// <summary>
        /// Constructs a grass tile at some (x, y) position.
        /// </summary>
        /// <param name="texture">The texture of a dirt tile.</param>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        public Dirt(Texture2D texture, int tileX, int tileY) : base(texture, tileX, tileY)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
