using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcadia.GameObjects.Characters;
using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcadia.GameObjects.Characters
{
    public class Mob : Character
    {
        CharacterStats stats;
        public Mob(Texture2D[] texture, Rectangle bounds, World world) : base(texture, bounds, world)
        {
            stats = new CharacterStats();
        }

        public void InitStats(int vit, int str, int def, int mag, int agi, int dex, int Int, int sla)
        {
            stats.VIT = vit;
            stats.STR = str;
            stats.DEF = def;
            stats.MAG = mag;
            stats.AGI = agi;
            stats.DEX = dex;
            stats.INT = Int;
            stats.SLA = sla;
            stats.InitPointStats();
        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = gameTime.ElapsedGameTime.Milliseconds;
            float currentX = X;
            float currentY = Y;
            float nextX = (X += VelocityX * elapsedTime);
            float nextY = (Y += VelocityY * elapsedTime);

            VelocityX = 0.1f;
            VelocityY += 0.0015265f * Grid.Size;


            base.Update(gameTime);
        }

    }
}