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
    // TODO: THE PLAYER'S OWN PROJECTILES CAN HIT HIMSELF - FIX!!!
    // TODO: THE PLAYER'S OWN PROJECTILES CAN HIT HIMSELF - FIX!!!
    // TODO: THE PLAYER'S OWN PROJECTILES CAN HIT HIMSELF - FIX!!!
    public class Projectile : LevelObject
    {
        // Stores a reference to the Gun the shot the projectile
        public Gun SourceGun;

        // Delay time between when the user fires a projectile and when the projectile becomes active
        // Ex: Shotguns will have more projectile delay than a machine gun
        public float ActiveDelayTime;

        // Stores the time (in game time) the projectile will be active
        public float ActiveTime;

        // Checks whether or not the projectile is active
        private bool IsActive;

        public Projectile()
        {
            Health = 1;
        }

        public override Powerup GetPowerup
        {
            get { return SourceGun.GunOwner.PowerUp; }
        }

        public override LevelObject CollisionOwner
        {
            get { return SourceGun.GunOwner; }
        }

        public void SetProjectileProperties(Gun sourceGun, Vector2 position, Direction direction, Vector2 moveSpeed, float activeDelayTime)
        {
            // Store the gun that shot the projectile
            SourceGun = sourceGun;
            
            // Set the position of the projectile
            ObjectPos = position;

            // Set the direction of the projectile
            ObjectDir = direction;

            // Set the movement speed of the projectile
            MoveSpeed = moveSpeed;

            // Set the active delay time of the projectile
            ActiveDelayTime = activeDelayTime;

            // Set when the projectile will become active
            ActiveTime = (Main.activeTime + ActiveDelayTime);
        }

        public override void Touches(LevelObject levelobject)
        {
            Die();
        }

        public override void WhenTouched(LevelObject levelobject)
        {
            Die();
        }

        public override void TouchesTile(Tile tile)
        {
            //If the tile is out of bounds or is a block tile, destroy the projectile
            if (tile.TileType == Tile.TileTypes.Block || tile.IndexX < 0)
                Die();
        }

        public override void DamagedObject(LevelObject levelobject)
        {
            Die();
        }

        private void CheckActivateProjectile()
        {
            // Check if the projectile was not previously active
            if (IsActive == false)
            {
                // Set the position to the projectile to the center of the source gun's position
                ObjectPos = Helper.CenterGraphic(SourceGun.ObjectDir, SourceGun, this);

                // Set the direction of the projectile to the source gun's direction
                ObjectDir = SourceGun.ObjectDir;

                // Set the projectile to be active
                IsActive = true;
            }
        }

        public override void Update()
        {
            // Check if the projectile should be active
            if (Main.activeTime >= ActiveTime)
            {
                // Check if we need to activate the projectile, and activate it if so
                CheckActivateProjectile();

                Vector2 moveamount = Vector2.Zero;

                // Adjust the position of the projectile based on the direction that it's facing
                switch (ObjectDir)
                {
                    case Direction.Left:
                        moveamount.X -= MoveSpeed.X;

                        break;
                    case Direction.Right:
                        moveamount.X += MoveSpeed.X;

                        break;
                    case Direction.Up:
                        moveamount.Y -= MoveSpeed.Y;

                        break;
                    case Direction.Down:
                        moveamount.Y += MoveSpeed.Y;

                        break;
                }

                Move(moveamount);

                UpdateCollisionBoxes();
            }

            // TODO: Add collision logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive == true)
            {
                spriteBatch.Draw(ObjectTexture, ObjectPos - SourceGun.GunOwner.Level.LevelCam.CameraLocation, null, Color.White, 0f, ObjectOrigin, 1f, SpriteEffects.None, SetDrawDepth());
                if (hitbox != null) hitbox.Draw(spriteBatch);
            }
        }
    }
}