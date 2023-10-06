using Arcadia.GameWorld;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.GameObjects.Characters
{
    public sealed class Player : Character
    {
        public Player(Texture2D texture, Rectangle bounds, World world) : base(texture, bounds, world)
        {
        }

        public override void Update(GameTime gameTime)
        {
            VelocityX = 0f;
            VelocityY += 0.0015265f * Grid.Size;

            if (KeyListener.IsKeyPressed(Keys.A))
            {
                VelocityX = -0.2f;
            }
            if (KeyListener.IsKeyPressed(Keys.D))
            {
                VelocityX = 0.2f;
            }
            if (KeyListener.IsKeyPressed(Keys.LeftShift))
            {
                VelocityX *= 2;
            }
            
            base.Update(gameTime);
        }
    }
}
