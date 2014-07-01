using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //The base Pickup class
    //A Pickup is any object that does something when a player or enemy touches it
    //These can be power ups or anything else, like a secret bonus stage
    public abstract class Pickup : LevelObject
    {
        //The object this Pickup is affecting
        public LevelObject PickupOwner;

        //Tells if this is a player Pickup
        public bool PlayerPickup;

        //Tells if this is an enemy Pickup
        public bool EnemyPickup;

        public Pickup()
        {
            ObjectTexture = LoadAssets.TestTexture;
            PickupOwner = null;

            PlayerPickup = true;
            EnemyPickup = false;
        }
    }
}
