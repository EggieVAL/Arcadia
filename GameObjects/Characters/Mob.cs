using System;
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

namespace Arcadia.GameObjects.Characters
{
    public class Mob : Character
    {
        CharacterStats stats;
        public Mob(Texture2D texture, Rectangle bounds, World world) : base(texture, bounds, world)
        {
            stats = new CharacterStats();
        }
        
        public void InitStats(int vit, int str, int def, int man, int agi, int dex, int Int, int sla)
        {
            stats.VIT = vit;
            stats.STR = str;
            stats.DEF = def;
            stats.MAN = man;
            stats.AGI = agi;
            stats.DEX = dex;
            stats.INT = Int;
            stats.SLA = sla;
        }

    }
}
