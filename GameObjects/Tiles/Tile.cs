using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObject.Tiles
{
    /// <summary>
    /// The <c>Tile</c> class is an abstraction of a game tile.
    /// </summary>
    public abstract class Tile : RenderableObject
    {
        /// <summary>
        /// Whether the tile is breakable.
        /// </summary>
        public bool Breakable { get; protected set; }

        /// <summary>
        /// Whether the tile has a hitbox; for collisions.
        /// </summary>
        public bool HasHitbox { get; protected set; }

        /// <summary>
        /// The hardness of the tile.
        /// </summary>
        public int Hardness { get; protected set; }

        /// <summary>
        /// Constructs a game tile with some texture at some position in the grid space.
        /// </summary>
        /// <param name="texture">The texture of a tile.</param>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="TileY">The y-coordinate in the grid space.</param>
        public Tile(Texture2D texture, int tileX, int TileY) : base(texture,
            new Rectangle(tileX * Grid.Size, TileY * Grid.Size, Grid.Size, Grid.Size))
        {
            Breakable = true;
            HasHitbox = true;
            Hardness = 0;
        }
    }
}
