using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObjects.Tiles
{
    /// <summary>
    /// The <see cref="Tile"/> class is an abstraction of a game tile. A tile has a unique id, or an ink value.
    /// For example, all dirt tiles have an ink value of 1.
    /// </summary>
    public abstract class Tile : RenderableObject
    {
        /// <summary>
        /// Is the tile breakable?
        /// </summary>
        public bool IsBreakable { get; protected set; }

        /// <summary>
        /// Does the tile have a hitbox?
        /// </summary>
        public bool HasHitbox { get; set; }

        /// <summary>
        /// The hardness value of a tile.
        /// </summary>
        public int Hardness { get; protected set; }

        /// <summary>
        /// The ink value of a tile. A specific tile can be referenced by its ink value. For example,
        /// all dirt tiles have an ink value of 1.
        /// </summary>
        public int Ink { get; init; }

        /// <summary>
        /// Constructs a tile at a position in the grid space.
        /// </summary>
        /// <param name="texture">The texture of a tile.</param>
        /// <param name="tileX">The x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        public Tile(Texture2D texture, int tileX, int tileY) : base(texture, GetBounds(tileX, tileY))
        {
            IsBreakable = true;
            Hardness = 0;
        }

        /// <summary>
        /// Gets the bounds of a tile at a position.
        /// </summary>
        /// <param name="tileX">the x-coordinate in the grid space.</param>
        /// <param name="tileY">The y-coordinate in the grid space.</param>
        /// <returns>Returns the bounds of a tile at a position.</returns>
        public static Rectangle GetBounds(int tileX, int tileY)
        {
            return new Rectangle(Grid.ConvertToUnits(tileX), Grid.ConvertToUnits(tileY), Grid.Size, Grid.Size);
        }
    }
}
