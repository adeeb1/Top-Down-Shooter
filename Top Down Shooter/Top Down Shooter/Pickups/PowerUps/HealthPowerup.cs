using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    //A Powerup that grants health to the object
    //These powerups usually apply immediately
    public sealed class HealthPowerup : Powerup
    {
        public HealthPowerup(Vector2 objectpos, int health) : base(objectpos)
        {
            Health = health;
        }

        protected override void Activate()
        {
            base.Activate();

            PickupOwner.Heal(Health);
        }
    }
}
