using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    //A Powerup that increases the damage its object deals
    public sealed class DamagePowerup : Powerup
    {
        private int Damage;

        public DamagePowerup(Vector2 objectpos, int damage, float duration) : base(objectpos)
        {
            PowerupType = Powerups.Damage;
            Damage = damage;
            Duration = duration;
        }

        public override int AdditionalDamage()
        {
            return Damage;
        }
    }
}
