using Arcadia.GameObjects.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcadia.GameObjects.Characters
{
    public class CharacterStats
    {
        float _maxHP;
        float _HP;

        float _maxMP;
        float _MP;

        float _maxSP;
        float _SP;

        public int VIT { get; set; }
        public int STR { get; set; }
        public int DEF { get; set; }
        public int MAG { get; set; }
        public int AGI { get; set; }
        public int DEX { get; set; }
        public int INT { get; set; }
        public int SLA { get; set; }

        public CharacterStats()
        {
            VIT = 5;
            STR = 5;
            DEF = 5;
            MAG = 5;
            AGI = 5;
            DEX = 5;
            INT = 5;
            SLA = 5;
            InitPointStats();
        }

        public CharacterStats(PlayerClass playerClass)
        {
            switch (playerClass)
            {
                case PlayerClass.Warrior:
                    VIT = 10;
                    STR = 10;
                    DEF = 10;
                    MAG = 1;
                    AGI = 1;
                    DEX = 5;
                    INT = 1;
                    SLA = 5;
                    break;
                case PlayerClass.Hunter:
                    VIT = 1;
                    STR = 5;
                    DEF = 1;
                    MAG = 1;
                    AGI = 10;
                    DEX = 10;
                    INT = 10;
                    SLA = 5;
                    break;
                case PlayerClass.Rogue:
                    VIT = 1;
                    STR = 1;
                    DEF = 5;
                    MAG = 1;
                    AGI = 10;
                    DEX = 10;
                    INT = 5;
                    SLA = 10;
                    break;
                case PlayerClass.Mage:
                    VIT = 5;
                    STR = 1;
                    DEF = 10;
                    MAG = 10;
                    AGI = 1;
                    DEX = 1;
                    INT = 5;
                    SLA = 10;
                    break;
                default:
                    VIT = 5;
                    STR = 5;
                    DEF = 5;
                    MAG = 5;
                    AGI = 5;
                    DEX = 5;
                    INT = 5;
                    SLA = 5;
                    break;
            }
            InitPointStats();
        }

        public void InitPointStats()
        {
            _maxHP = 50 + (VIT * 5);
            _maxMP = 50 + (MAG * 5);
            _maxSP = 50 + (AGI * 5);
            _HP = _maxHP;
            _MP = _maxMP;
            _SP = _maxSP;
        }
    }
}
