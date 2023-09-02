using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.GameObject.Tiles
{
    /// <summary>
    ///     The <c>Grass</c> class is a representation of a grass tile.
    /// </summary>
    public sealed class Grass : ATile
    {
        /// <summary>
        ///     Constructs a grass tile at some (x, y) position.
        /// </summary>
        /// <param name="texture">The texture of a grass tile.</param>
        /// <param name="unitx">The x-coordinate in units.</param>
        /// <param name="unity">The y-coordinate in units.</param>
        public Grass(Texture2D texture, int unitx, int unity)
            : base(texture, unitx, unity)
        {
        }

        public override void Update(GameTime gt)
        {
            throw new NotImplementedException();
        }
    }
}
