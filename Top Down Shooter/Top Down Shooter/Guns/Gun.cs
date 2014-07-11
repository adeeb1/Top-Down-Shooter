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

        // Stores the max ammo for the gun, excluding a full clip
        public int MaxAmmo;

        // Stores the ammo left in the gun's other clips
        public int TotalAmmo;

        // Stores the size of the gun's clip
        public int ClipSize;

        //The amount of ammo in the gun's current clip
        public int ClipAmmo;

        // Delay time between shots
        public float ShootDelayTime;

        // Stores the next time (in game time) the user can shoot a projectile
        protected float NextShootTime;

        // Delay time for how long it takes the player to reload the gun
        public float ReloadTime;
        protected float PrevReload;

        public Gun()
        {
            //GunProjectiles = new List<Projectile>();
            PrevReload = 0f;
        }

        public void SetGunProperties(Vector2 position, Direction direction, int maxAmmo, int totalAmmo, int clipSize, float shootDelayTime, float reloadTime)
        {
            // Position and Direction
            ObjectPos = position;
            ObjectDir = direction;
            
            // MaxAmmo and AmmoLeft
            MaxAmmo = maxAmmo;
            TotalAmmo = totalAmmo;

            //Set the clip size and the amount of ammo in the current clip
            ClipSize = clipSize;
            ClipAmmo = ClipSize;

            // ShootDelayTime
            ShootDelayTime = shootDelayTime;

            //Reload time
            ReloadTime = reloadTime;
        }

        protected override float SetDrawDepth()
        {
            return ((ObjectPos.Y + GunOwner.Level.LevelCam.CameraOffset.Y) / 1000f);
        }

        //Reloads the gun
        public void Reload()
        {
            //Allow reloading only if the current clip has less ammo than the clip size and there is ammo left in other clips
            if (ClipAmmo < ClipSize && TotalAmmo > 0)
            {
                //The amount to reload; if there isn't enough left in the gun, just reload the remaining amount
                int reloadamount = ClipSize - ClipAmmo;
                if (TotalAmmo < reloadamount) reloadamount = TotalAmmo;

                //Add bullets to the clip and remove them from the total ammo remaining
                ClipAmmo += reloadamount;
                TotalAmmo -= reloadamount;

                PrevReload = Main.activeTime + ReloadTime;

                //Play a reload sound
            }
        }

        public void Fire()
        {
            // Check if the user is holding the Left Control key and has some ammo left
            if (Main.activeTime >= PrevReload && Main.activeTime >= NextShootTime)
            {
                // Check if the user has any ammo left
                if (ClipAmmo > 0)
                {
                    // Create a new projectile
                    GunOwner.Level.AddObject(new Projectile1(this, ObjectPos, ObjectDir));

                    // Subtract the quantity of ammo in the clip by 1
                    ClipAmmo -= 1;

                    // Store the next time the user can shoot a projectile
                    NextShootTime = (Main.activeTime + ShootDelayTime);
                }
                else // The user has no ammo left in the clip
                {
                    //Reload if there's ammo left in the gun
                    if (TotalAmmo > 0) Reload();
                    else
                    {
                        // Switch the user's gun to another gun, otherwise switch to the default gun with infinite ammo, and then process the shot
                    }
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

            spriteBatch.Draw(ObjectTexture, ObjectPos - GunOwner.Level.LevelCam.CameraLocation, null, Color.White, 0f, ObjectOrigin, 1f, SpriteEffects.None, SetDrawDepth());

            //for (int i = 0; i < GunProjectiles.Count; i++)
            //{
            //    GunProjectiles[i].Draw(spriteBatch);
            //}
        }


    }
}