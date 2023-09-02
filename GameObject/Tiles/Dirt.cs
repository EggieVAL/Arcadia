using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia.GameObject.Tiles
{
    /// <summary>
    ///     The <c>Dirt</c> class is a representation of a dirt tile.
    /// </summary>
    public sealed class Dirt : ATile
    {
        /// <summary>
        ///     Constructs a dirt tile at some (x, y) position.
        /// </summary>
        /// <param name="texture">The texture of a dirt tile.</param>
        /// <param name="unitx">The x-coordinate in units.</param>
        /// <param name="unity">The y-coordinate in units.</param>
        public Dirt(Texture2D texture, int unitx, int unity)
            : base(texture, unitx, unity)
        {
        }

        public override void Update(GameTime gt)
        {
            throw new NotImplementedException();
        }
    }
}
