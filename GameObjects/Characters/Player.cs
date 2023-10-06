using Arcadia.GameObject.Tiles;
using Arcadia.GameObjects.Characters;
using Arcadia.GameWorld;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System;
using System.Diagnostics;

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
            float elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            float currentX = X;
            float currentY = Y;
            float nextX = (X += VelocityX * elapsedTime);
            float nextY = (Y += VelocityY * elapsedTime);

            VelocityX = 0f;
            VelocityY += 0.0015265f * Grid.Size;

            if (KeyListener.IsKeyPressed(Keys.A))
            {
                VelocityX = -(0.2f + Stats.AGI * 0.05f);
            }
            if (KeyListener.IsKeyPressed(Keys.D))
            {
                VelocityX = 0.2f + Stats.AGI * 0.05f;
            }
            if (KeyListener.IsKeyPressed(Keys.LeftShift))
            {
                VelocityX *= 2 + Stats.AGI * 0.1f;
            }
            bool grounded = IsCollidingBelow(currentY, nextY, out List<Tile> tilesCollided);
            if (KeyListener.IsKeyPressed(Keys.Space))
            {
                if(grounded)
                {
                    Debug.WriteLine(true);
                    VelocityY -= (0.75f + Stats.AGI * 0.1f);
                }
                
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
