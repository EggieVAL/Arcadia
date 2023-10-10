﻿using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Arcadia.GameObjects.Characters
{
    /// <summary>
    /// The <see cref="Player"/> class is a representation of a player. A player is a controllable character that can interact
    /// with a world it is in.
    /// </summary>
    public sealed class Player : Character
    {
        PlayerClass Class { get; set; }
        CharacterStats Stats { get; set; }
        /// <summary>
        /// Constructs a player.
        /// </summary>
        /// <param name="texture">The texture of a player.</param>
        /// <param name="bounds">The bounds of a player.</param>
        /// <param name="world">The world a player is in.</param>
        public Player(PlayerClass playerClass, Texture2D texture, Rectangle bounds, World world) : base(texture, bounds, world)
        {
            Class = playerClass;
            Stats = new CharacterStats(playerClass);
        }

        /// <summary>
        /// Updates the player's movements based on keyboard inputs.
        /// </summary>
        /// <param name="gameTime"></param>
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
