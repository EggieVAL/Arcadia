using Arcadia.GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObjects.Tiles
{
    public abstract class Tile : RenderableObject
    {
        public bool Breakable { get; protected set; }

        public bool HasHitbox { get; protected set; }

        public int Hardness { get; protected set; }

        public int Ink { get; init; }

        public Tile(Texture2D texture, int gridX, int gridY) : base(texture, GetBounds(gridX, gridY))
        {
            Breakable = true;
            HasHitbox = true;
            Hardness = 0;
        }

        public static Rectangle GetBounds(int gridX, int gridY)
        {
            return new Rectangle(Grid.ConvertToUnits(gridX), Grid.ConvertToUnits(gridY), Grid.Size, Grid.Size);
        }
    }
}
