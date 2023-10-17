using Arcadia.GameWorld;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.GameObjects.Characters
{
    /// <summary>
    /// The <see cref="Player"/> class is a representation of a player. A player is a controllable character that can interact
    /// with a world it is in.
    /// </summary>
    public sealed class Player : Character
    {
        /// <summary>
        /// Constructs a player.
        /// </summary>
        /// <param name="texture">The texture of a player.</param>
        /// <param name="bounds">The bounds of a player.</param>
        /// <param name="world">The world a player is in.</param>
        public Player(Texture2D texture, Rectangle bounds, World world) : base(texture, bounds, world)
        {
        }

        /// <summary>
        /// Updates the player's movements based on keyboard inputs.
        /// </summary>
        /// <param name="gameTime"></param>
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
