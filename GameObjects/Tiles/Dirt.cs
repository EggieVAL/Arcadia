using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObjects.Tiles
{
    public sealed class Dirt : Tile
    {
        public Dirt(Texture2D[] texture, int gridX, int gridY) : base(texture, gridX, gridY)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
