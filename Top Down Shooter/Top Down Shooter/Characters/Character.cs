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
        // Represents the player texture (will later be changed to animation)
        public Texture2D PlayerTexture;

        // Keyboard state
        public KeyboardState keyboardState;

        public Character()
        {
            // Set the movement speed
            MoveSpeed = new Vector2(10, 10);

            // Create a new keyboard state
            keyboardState = new KeyboardState();

            PlayerTexture = LoadAssets.TestTexture;
        }

        // Method for moving the player
        protected virtual void PlayerMove()
        {
            // Check if the user can move left
            if (Input.IsKeyHeld(Keys.Left) == true)
            {
                ObjectPos.X -= MoveSpeed.X;
            }
            // Check if the user can move right
            if (Input.IsKeyHeld(Keys.Right) == true)
            {
                ObjectPos.X += MoveSpeed.X;
            }
            // Check if the user can move up
            if (Input.IsKeyHeld(Keys.Up) == true)
            {
                ObjectPos.Y -= MoveSpeed.Y;
            }
            // Check if the user can move down
            if (Input.IsKeyHeld(Keys.Down) == true)
            {
                ObjectPos.Y += MoveSpeed.Y;
            }
        }

        public override void Update()
        {
            // Move the player if possible
            PlayerMove();

            // Update the keyboard state with the global state
            keyboardState = Keyboard.GetState();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, ObjectPos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, SetDrawDepth());
        }
    }
}