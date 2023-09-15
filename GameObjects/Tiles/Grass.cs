using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcadia.GameObject.Tiles
{
    public sealed class Grass : Tile
    {
        public Grass(Texture2D texture, int gridX, int gridY) : base(texture, gridX, gridY)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
