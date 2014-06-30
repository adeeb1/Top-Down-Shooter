using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    //A Powerup that increases the defense of its object
    public sealed class DefensePowerup : Powerup
    {
        private int Defense;

        public DefensePowerup(Vector2 objectpos, int defense, float duration) : base(objectpos)
        {
            PowerupType = Powerups.Defense;
            Defense = defense;
            Duration = duration;
        }

        public override int AdditionalDefense()
        {
            return Defense;
        }
    }
}
