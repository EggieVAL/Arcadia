using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObject.Tiles
{
    /// <summary>
    /// The <c>Grass</c> class is a representation of a grass tile.
    /// </summary>
    public sealed class Grass : Tile
    {
        /// <summary>
        /// Constructs a grass tile at some (x, y) position.
        /// </summary>
        /// <param name="texture">The texture of a grass tile.</param>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        public Grass(Texture2D texture, int tileX, int tileY) : base(texture, tileX, tileY)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
