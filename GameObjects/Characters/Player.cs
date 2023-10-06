using Arcadia.GameObjects.Characters;
using Arcadia.GameWorld;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.GameObject.Characters
{
    public sealed class Player : Character
    {
        PlayerClass Class { get; set; }
        CharacterStats Stats { get; set; }
        public Player(PlayerClass playerClass, Texture2D texture, Rectangle bounds, World world) : base(texture, bounds, world)
        {
            Class = playerClass;
            Stats = new CharacterStats(playerClass);
        }

        public override void Update(GameTime gameTime)
        {
            VelocityX = 0f;
            VelocityY += 0.0015265f * Grid.Size;

            if (KeyListener.IsKeyPressed(Keys.A))
            {
                VelocityX = -0.2f + Stats.AGI * 0.05f;
            }
            if (KeyListener.IsKeyPressed(Keys.D))
            {
                VelocityX = 0.2f + Stats.AGI * 0.05f;
            }
            if (KeyListener.IsKeyPressed(Keys.LeftShift))
            {
                VelocityX *= 2 + Stats.AGI * 0.1f;
            }

            
            base.Update(gameTime);
        }
    }

    public enum PlayerClass
    {
        Warrior = 0,
        Hunter = 1,
        Rogue = 2,
        Mage = 3
    }
}
