using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObject.Tiles
{
    /// <summary>
    ///     The <c>Tile</c> class is an abstraction of a game tile.
    /// </summary>
    public abstract class ATile : ARenderableObject
    {
        /// <summary>
        ///     Whether the tile is breakable.
        /// </summary>
        public bool Breakable { get; protected set; }

        /// <summary>
        ///     Whether the tile has a hitbox; for collisions.
        /// </summary>
        public bool HasHitbox { get; protected set; }

        /// <summary>
        ///     The hardness of the tile.
        /// </summary>
        public int Hardness { get; protected set; }

        /// <summary>
        ///     Constructs a game tile with some texture and at
        ///     some position.
        /// </summary>
        /// <param name="texture">The texture of a tile.</param>
        /// <param name="unitx">The x-coordinate in units.</param>
        /// <param name="unity">The y-coordinate in units.</param>
        public ATile(Texture2D texture, float unitx, float unity)
            : base(texture, new Rectangle((int) unitx, (int) unity,
                Grid.Size, Grid.Size))
        {
            Texture = texture;
            Breakable = true;
            HasHitbox = true;
            Hardness = 0;
        }
    }
}
