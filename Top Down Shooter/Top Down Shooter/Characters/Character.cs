using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Top_Down_Shooter
{
    //Base character class
    public abstract class Character : LevelObject
    {
        //The last resort pistol that the player uses when the player has no ammo left at all; it has infinite ammo but is very weak
        //NOTE: This may not be need to be an explicit definition depending on the way we handle multiple guns (Ex. array)
        public Gun BackupGun;

        //The primary and secondary guns
        public Gun PrimaryGun;
        public Gun SecondaryGun;

        // Represents the gun the character is currently holding (may need to be a list or an array later for multiple guns)
        public Gun PlayerGun;

        // The HUD for the player
        public HUD PlayerHUD;

        // Keyboard state
        public KeyboardState keyboardState;

        public Character()
        {
            // Set the player's movement speed
            MoveSpeed = new Vector2(10, 10);
            
            //Set position (test for now)
            ObjectPos = Main.ScreenHalf;

            // Get the player's texture
            ObjectTexture = LoadAssets.CharTest;

            PlayerGun = BackupGun;

            DirectionAnim[(int)Direction.Left] = new Animation(LoadAssets.CharAnimation, new AnimFrame(new Rectangle(10, 154, 18, 29), Vector2.Zero, 200f),
                                                                 new AnimFrame(new Rectangle(35, 153, 17, 30), Vector2.Zero, 200f),
                                                                 new AnimFrame(new Rectangle(59, 154, 20, 29), Vector2.Zero, 200f));

            DirectionAnim[(int)Direction.Up] = new Animation(LoadAssets.CharAnimation, new AnimFrame(new Rectangle(10, 118, 18, 29), Vector2.Zero, 200f),
                                                                 new AnimFrame(new Rectangle(36, 118, 17, 29), Vector2.Zero, 200f),
                                                                 new AnimFrame(new Rectangle(60, 118, 18, 29), new Vector2(-1, 0), 200f));

            DirectionAnim[(int)Direction.Right] = new Animation(DirectionAnim[(int)Direction.Left], SpriteEffects.FlipHorizontally);

            DirectionAnim[(int)Direction.Down] = new Animation(LoadAssets.CharAnimation, new AnimFrame(new Rectangle(62, 82, 16, 30), Vector2.Zero, 200f),
                                                                   new AnimFrame(new Rectangle(37, 83, 17, 29), Vector2.Zero, 200f),
                                                                   new AnimFrame(new Rectangle(13, 82, 15, 30), Vector2.Zero, 200f));


            PowerUp = Powerup.Default;

            MaxHealth = 100;

            // Create a new keyboard state
            keyboardState = new KeyboardState();
        }

        // Method for moving the player
        protected virtual void PlayerMove()
        {
            Vector2 totalmovement = Vector2.Zero;

            // Check if the user can move left
            if (Input.IsKeyHeld(Keys.Left) == true)
            {
                //ObjectPos.X -= MoveSpeed.X;
                totalmovement.X -= MoveSpeed.X;

                PlayerGun.ObjectPos = Helper.CenterGraphic(Direction.Left, this, PlayerGun);

                // Set the direction of the player and the player's gun to left
                ObjectDir = PlayerGun.ObjectDir = Direction.Left;
            }
            // Check if the user can move right
            if (Input.IsKeyHeld(Keys.Right) == true)
            {
                //ObjectPos.X += MoveSpeed.X;
                totalmovement.X += MoveSpeed.X;

                PlayerGun.ObjectPos = Helper.CenterGraphic(Direction.Right, this, PlayerGun);
                
                // Set the direction of the player and the player's gun to right
                ObjectDir = PlayerGun.ObjectDir = Direction.Right;
            }
            // Check if the user can move up
            if (Input.IsKeyHeld(Keys.Up) == true)
            {
                //ObjectPos.Y -= MoveSpeed.Y;
                totalmovement.Y -= MoveSpeed.Y;

                PlayerGun.ObjectPos = Helper.CenterGraphic(Direction.Up, this, PlayerGun);

                // Set the direction of the player and the player's gun to up
                ObjectDir = PlayerGun.ObjectDir = Direction.Up;
            }
            // Check if the user can move down
            if (Input.IsKeyHeld(Keys.Down) == true)
            {
                //ObjectPos.Y += MoveSpeed.Y;
                totalmovement.Y += MoveSpeed.Y;

                PlayerGun.ObjectPos = Helper.CenterGraphic(Direction.Down, this, PlayerGun);

                // Set the direction of the player and the player's gun to down
                ObjectDir = PlayerGun.ObjectDir = Direction.Down;
            }

            CurrentAnim = DirectionAnim[(int)ObjectDir];

            if (totalmovement != Vector2.Zero)
            {
                if (CurrentAnim.IsPlaying == false) CurrentAnim.Play();
                Move(totalmovement);
            }
            else CurrentAnim.Stop();
        }

        //Switches the player's gun; by default, it switches to the primary/secondary gun depending which one is currently equipped
        //However, when the player runs out of ammo for both guns we can force a switch to the backup gun if we wish
        protected void SwitchGun(bool backup = false)
        {
            //Switch to the backup gun
            if (backup == true)
                PlayerGun = BackupGun;
            else
            {
                //Check which gun the player has equipped and check if the next gun exists and has ammo
                if (PlayerGun == PrimaryGun && (SecondaryGun != null && SecondaryGun.HasAmmo == true))
                    PlayerGun = SecondaryGun;
                else if (PlayerGun == SecondaryGun && (PrimaryGun != null && PrimaryGun.HasAmmo == true))
                    PlayerGun = PrimaryGun;
            }
        }

        //Reloads the current gun the player has equipped
        protected void ReloadGun()
        {
            if (Input.IsKeyDown(keyboardState, Keys.R) == true)
            {
                PlayerGun.Reload();
            }
        }

        public void ShootGun()
        {
            // Check if the user is pressing the shoot key
            if (Input.IsKeyHeld(Keys.LeftControl))
            {
                // Try to fire the player's gun
                PlayerGun.Fire();
                /*Untested code*
                 if (PlayerGun.HasAmmo == false)
                 {
                    if (PrimaryGun.HasAmmo == false && SecondaryGun.HasAmmo == false) SwitchGun(true);
                    else SwitchGun();
                 }
                 
                 */
            }
        }

        //Picks up a gun and gives it to the player
        public void PickupGun(Gun gun)
        {
            gun.GunOwner = this;
            gun.Level = this.Level;

            if (PrimaryGun == null) PrimaryGun = gun;
            else if (SecondaryGun == null) SecondaryGun = gun;

            //If the player is using the backup gun (or no gun, which shouldn't ever happen), automatically equip the new gun picked up
            if (PlayerGun == BackupGun || PlayerGun == null) PlayerGun = gun;
        }

        public override void Die()
        {
            base.Die();
        }

        public override void Update()
        {
            //Update animations and such
            base.Update();

            // Move the player if possible
            PlayerMove();

            UpdateCollisionBoxes();

            //Check for manually switching gun
            if (Input.IsKeyDown(keyboardState, Keys.E) == true)
            {
                SwitchGun();
            }

            if (PlayerGun != null)
            {
                ReloadGun();
                ShootGun();
                PlayerGun.Update();
            }

            PlayerHUD.Update();

            //TESTING SOUNDS ONLY; REMOVE LATER
            if (Input.IsKeyDown(keyboardState, Keys.Q) == true)
            {
                SoundManager.PlaySound(LoadAssets.TestSound);
            }

            if (Input.IsKeyHeld(Keys.K) == true)
            {
                TakeDamage(new Hitbox(this, Rectangle.Empty, 2, 0f, 0f));
            }

            //Update the player's Powerup
            UpdatePowerup();

            // Update the keyboard state with the global state
            keyboardState = Keyboard.GetState();
        }

        protected void UpdatePowerup()
        {
            if (PowerUp.PowerupType != Powerup.Powerups.None)
            {
                PowerUp.Update();
                if (PowerUp.PowerupDone == true)
                    PowerUp.Deactivate();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (PlayerGun != null) PlayerGun.Draw(spriteBatch);
        }
    }
}