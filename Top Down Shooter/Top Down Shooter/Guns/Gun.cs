using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Top_Down_Shooter
{
    public abstract class Gun : LevelObject
    {
        // Stores all the projectiles created by the gun
        //public List<Projectile> GunProjectiles;

        // Stores a reference to the LevelObject (player or enemy)
        public LevelObject GunOwner;

        // Stores the max ammo for the gun
        public int MaxAmmo;

        // Stores the ammo left for the gun
        public int AmmoLeft;

        // Delay time between shots
        public float ShootDelayTime;

        // Stores the next time (in game time) the user can shoot a projectile
        float NextShootTime;

        public Gun()
        {
            //GunProjectiles = new List<Projectile>();
        }

        public void SetGunProperties(Vector2 position, Direction direction, int maxAmmo, int ammoLeft, float shootDelayTime)
        {
            // Position and Direction
            ObjectPos = position;
            ObjectDir = direction;
            
            // MaxAmmo and AmmoLeft
            MaxAmmo = maxAmmo;
            AmmoLeft = ammoLeft;

            // ShootDelayTime
            ShootDelayTime = shootDelayTime;
        }

        public void Fire()
        {
            // Check if the user is holding the Left Control key and has some ammo left
            if (Main.activeTime >= NextShootTime)
            {
                // Check if the user has any ammo left
                if (AmmoLeft > 0)
                {
                    // Create a new projectile
                    GunOwner.Level.AddObject(new Projectile1(this, ObjectPos, ObjectDir));

                    // Subtract the quantity of ammo left by 1
                    AmmoLeft -= 1;

                    // Store the next time the user can shoot a projectile
                    NextShootTime = (Main.activeTime + ShootDelayTime);
                }
                else // The user has no ammo left
                {
                    // Switch the user's gun to the default gun with infinite ammo, and then process the shot
                    
                }
            }
        }

        //public override void Update()
        //{
        //    for (int i = 0; i < GunProjectiles.Count; i++)
        //    {
        //        GunProjectiles[i].Update();
        //    }
        //}

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Add in rotation so that when facing up or down, the gun is rotated 90 degrees
            // TODO: Add in rotation so that when facing up or down, the gun is rotated 90 degrees
            // TODO: Add in rotation so that when facing up or down, the gun is rotated 90 degrees

            spriteBatch.Draw(ObjectTexture, ObjectPos, null, Color.White, 0f, ObjectOrigin, 1f, SpriteEffects.None, SetDrawDepth());

            //for (int i = 0; i < GunProjectiles.Count; i++)
            //{
            //    GunProjectiles[i].Draw(spriteBatch);
            //}
        }


    }
}