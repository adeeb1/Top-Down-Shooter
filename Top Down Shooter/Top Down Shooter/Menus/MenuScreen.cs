using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

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

        // The font of each menu
        public SpriteFont MenuFont;

        // Keyboard state
        public KeyboardState keyboardState;

        // Mouse state
        public MouseState mouseState;

        // Touch state
        public TouchPanelState touchState;

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

            // Check if the player moved the selection cursor left
            if (Input.IsKeyDown(keyboardState, Keys.Left) == true)
            {
                // Change the setting by -1
                ChangeOption(-1);
            }
            // Check if the player moved the selection cursor right
            if (Input.IsKeyDown(keyboardState, Keys.Right) == true)
            {
                // Change the setting by 1
                ChangeOption(1);
            }
            // Check if the player moved the selection cursor up
            if (Input.IsKeyDown(keyboardState, Keys.Up) == true && SelectedOption > 0)
            {
                SelectedOption -= 1;
            }
            // Check if the player moved the selection cursor down
            if (Input.IsKeyDown(keyboardState, Keys.Down) == true && SelectedOption < LastMenuOption)
            {
                SelectedOption += 1;
            }
        }

        // Method for automatically moving the selected option based on the mouse position
        protected virtual void MouseMove()
        {
            // Loop through all of the menu options
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                // Check if the mouse is within the bounds of the option's rectangle
                if (Input.IsMouseInRect(GetOptionRect(i)) == true)
                {
                    // Set the selected option to the current option in the loop
                    SelectedOption = i;

                    // Break out of the loop
                    break;
                }
            }
        }

        protected virtual void TouchSelect(Main main)
        {
            // Loop through all of the menu options
            for (int i = 0; i < MenuOptions.Count; i++)
            {
                // Check if the mouse is within the bounds of the option's rectangle
                if (Input.IsTapInRect(GetOptionRect(i)) == true)
                {
                    // Set the selected option to the current option in the loop
                    SelectedOption = i;

                    // Try to pick the option immediately
                    PickOption(main);

                    // Break out of the loop
                    break;
                }
            }
        }

        protected virtual void PickOption(Main main)
        {
            // Nothing here for the base class
        }

        // Occurs when the user changes an option by pressing the Left or Right arrow keys
        protected virtual void ChangeOption(int change)
        {
            // Nothing here for the base class
        }

        protected Rectangle GetOptionRect(int OptionIndex)
        {
            // Measure the menu option
            Vector2 OptionLength = MenuFont.MeasureString(MenuOptions[OptionIndex]);

            // Create a new rectangle
            return new Rectangle((int)OptionPositions[OptionIndex].X, (int)OptionPositions[OptionIndex].Y,
                                 (int)OptionLength.X, (int)OptionLength.Y);
        }

        public virtual void Update(Main main)
        {
            touchState = TouchPanel.GetState(main.Window);

            // Move the cursor if possible
            CursorMove();

            // If the player moves the mouse, select the option that is at the mouse position
            MouseMove();

            // If the player taps on the screen, select the option at the touch position and pick it immediately after
            TouchSelect(main);

            // Check if the user pressed the "Enter" key or pressed the left mouse button
            if (Input.IsKeyDown(keyboardState, Keys.Enter) || (Input.IsLeftMouseButtonDown(mouseState) && Input.IsMouseInRect(GetOptionRect(SelectedOption))))
            {
                // Pick the selected option
                PickOption(main);
            }

            // Update the keyboard, mouse, and touch states with the global state
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            touchState = TouchPanel.GetState(main.Window);
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

            if (Debug.OptionRectDraw == true)
            {
                for (int i = 0; i < MenuOptions.Count; i++)
                {
                    spriteBatch.Draw(LoadAssets.ScalableBox, GetOptionRect(i), null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, .999f);
                }
            }
        }


    }
}