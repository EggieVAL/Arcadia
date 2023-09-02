using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.GameObject.Characters
{
    /// <summary>
    ///     A <c>Player</c> is a player character that is controlled by a real person
    ///     using a keyboard, mouse, or controller.
    /// </summary>
    public sealed class Player : ACharacter
    {
        /// <summary>
        ///     Constructs a player with some texture in some bounding box.
        /// </summary>
        /// <param name="texture">The texture of a player.</param>
        /// <param name="bounds">The bounds of a player</param>
        public Player(Texture2D texture, Rectangle bounds) : base(texture, bounds)
        {
        }

        /// <summary>
        ///     This <c>Update</c> method updates the state of a player according
        ///     to player input(s).
        /// </summary>
        /// <param name="gt">The time state of the game.</param>
        public override void Update(GameTime gt)
        {
            KeyManager manager = KeyManager.Instance;
            VelX = 0f;

            if (manager.IsKeyDown(Keys.A))
            {
                VelX = -0.2f;
            }
            if (manager.IsKeyDown(Keys.D))
            {
                VelX = 0.2f;
            }
            if (manager.IsKeyDown(Keys.LeftShift))
            {
                VelX *= 2;
            }
            base.Update(gt);
        }
    }
}
