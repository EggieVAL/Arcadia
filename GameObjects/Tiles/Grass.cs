using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObjects.Tiles
{
    public sealed class Grass : Tile
    {
        public Grass(Texture2D texture, int tileX, int tileY) : base(texture, tileX, tileY)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
