using Arcadia.GameWorld;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.GameObject.Characters
{
    /// <summary>
    /// A <c>Player</c> is a player character that is controlled by a real person
    /// using a keyboard, mouse, or controller.
    /// </summary>
    public sealed class Player : Character
    {
        /// <summary>
        /// Constructs a player with a texture in some bounding box in some world.
        /// </summary>
        /// <param name="texture">The texture of a player.</param>
        /// <param name="bounds">The bounds of a player.</param>
        /// <param name="world">The world a player is in.</param>
        public Player(Texture2D texture, Rectangle bounds, World world) : base(texture, bounds, world)
        {
        }

        /// <summary>
        /// Updates the state of a player according to key input(s).
        /// </summary>
        /// <param name="gameTime">The time state of the game.</param>
        public override void Update(GameTime gameTime)
        {
            VelocityX = 0f;
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
