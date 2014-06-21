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
        // Represents the gun the character is currently holding (may need to be a list or an array later for multiple guns)
        public Gun PlayerGun;
        
        // Keyboard state
        public KeyboardState keyboardState;

        public Character()
        {
            // Set the player's movement speed
            MoveSpeed = new Vector2(10, 10);

            // Get the player's texture
            ObjectTexture = LoadAssets.TestTexture;

            // Create a new keyboard state
            keyboardState = new KeyboardState();
        }

        // Method for moving the player
        protected virtual void PlayerMove()
        {
            // Check if the user can move left
            if (Input.IsKeyHeld(Keys.Left) == true)
            {
                ObjectPos.X -= MoveSpeed.X;

                PlayerGun.ObjectPos = Helper.CenterGraphic(Direction.Left, this, PlayerGun);

                // Set the direction of the player and the player's gun to left
                ObjectDir = PlayerGun.ObjectDir = Direction.Left;
            }
            // Check if the user can move right
            if (Input.IsKeyHeld(Keys.Right) == true)
            {
                ObjectPos.X += MoveSpeed.X;

                PlayerGun.ObjectPos = Helper.CenterGraphic(Direction.Right, this, PlayerGun);
                
                // Set the direction of the player and the player's gun to right
                ObjectDir = PlayerGun.ObjectDir = Direction.Right;
            }
            // Check if the user can move up
            if (Input.IsKeyHeld(Keys.Up) == true)
            {
                ObjectPos.Y -= MoveSpeed.Y;

                PlayerGun.ObjectPos = Helper.CenterGraphic(Direction.Up, this, PlayerGun);

                // Set the direction of the player and the player's gun to up
                ObjectDir = PlayerGun.ObjectDir = Direction.Up;
            }
            // Check if the user can move down
            if (Input.IsKeyHeld(Keys.Down) == true)
            {
                ObjectPos.Y += MoveSpeed.Y;

                PlayerGun.ObjectPos = Helper.CenterGraphic(Direction.Down, this, PlayerGun);

                // Set the direction of the player and the player's gun to down
                ObjectDir = PlayerGun.ObjectDir = Direction.Down;
            }
        }

        public void ShootGun()
        {
            // Check if the user is pressing the shoot key
            if (Input.IsKeyHeld(Keys.LeftControl))
            {
                // Try to fire the player's gun
                PlayerGun.Fire();
            }
        }

        public void SwitchGun()
        {
            // TODO: Code switching to another gun slot
        }

        public override void Update()
        {
            // Move the player if possible
            PlayerMove();

            ShootGun();

            //TESTING SOUNDS ONLY; REMOVE LATER
            if (Input.IsKeyDown(keyboardState, Keys.Q) == true)
            {
                SoundManager.PlaySound(LoadAssets.TestSound);
            }

            // Update the keyboard state with the global state
            keyboardState = Keyboard.GetState();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ObjectTexture, ObjectPos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, SetDrawDepth());
        }
    }
}