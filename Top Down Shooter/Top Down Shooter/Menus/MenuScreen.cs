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
    //Base menu screen class
    public abstract class MenuScreen
    {
        // Stores the list of menu options on the screen
        public List<String> MenuOptions;

        // Stores the position of each menu option on the screen
        public List<Vector2> OptionPositions;

        // Stores the option the user is currently positioned on
        public int SelectedOption;

        // Stores the texture for the user's cursor
        public Texture2D CursorTexture;

        // Stores the X distance of the cursor from the menu option (the Y position should be the same as the menu option) 
        public int CursorXDiff;

        public SpriteFont MenuFont;

        // Keyboard state
        public KeyboardState keyboardState;

        public MenuScreen()
        {
            MenuOptions = new List<String>();
            OptionPositions = new List<Vector2>();

            CursorTexture = LoadAssets.CursorTexture;
            CursorXDiff = 50;

            MenuFont = LoadAssets.MenuFont;

            // Reset the user's input
            ResetInput();
        }

        public void ResetInput()
        {
            // Create a new keyboard state with the Enter, Up, Down, Left, and Right keys already pressed
            keyboardState = new KeyboardState(Keys.Enter, Keys.Up, Keys.Down, Keys.Left, Keys.Right);
        }

        // Method for moving the cursor
        protected virtual void CursorMove()
        {
            // Get the index value of the last item in the MenuOptions list
            int LastMenuOption = (MenuOptions.Count - 1);

            // Check if the user can move the cursor left
            if (Input.IsKeyDown(keyboardState, Keys.Left) == true && SelectedOption > 0)
            {
                SelectedOption -= 1;
            }
            // Check if the user can move right
            if (Input.IsKeyDown(keyboardState, Keys.Right) == true && SelectedOption < LastMenuOption)
            {
                SelectedOption += 1;
            }
            // Check if the user can move up
            if (Input.IsKeyDown(keyboardState, Keys.Up) == true && SelectedOption > 0)
            {
                SelectedOption -= 1;
            }
            // Check if the user can move down
            if (Input.IsKeyDown(keyboardState, Keys.Down) == true && SelectedOption < LastMenuOption)
            {
                SelectedOption += 1;
            }
        }

        protected virtual void PickOption(Main main)
        {
            // Nothing here for the base class
        }

        public virtual void Update(Main main)
        {
            // Move the cursor if possible
            CursorMove();
            
            // Check if the user pressed the "Enter" key
            if (Input.IsKeyDown(keyboardState, Keys.Enter))
            {
                // Pick the selected option
                PickOption(main);
            }

            // Update the keyboard state with the global state
            keyboardState = Keyboard.GetState();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Get the selected option
            Vector2 TheOption = OptionPositions[SelectedOption];
            
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                spriteBatch.DrawString(MenuFont, MenuOptions[i], OptionPositions[i], Color.Black);
            }
            
            spriteBatch.Draw(CursorTexture, new Rectangle((int)(TheOption.X - CursorXDiff), (int)TheOption.Y, CursorTexture.Width, CursorTexture.Height), Color.White);
        }


    }
}