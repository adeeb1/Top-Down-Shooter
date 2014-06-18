using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Top_Down_Shooter
{
    // Class that handles key input
    public static class Input
    {
        public static bool IsKeyDown(KeyboardState KeyboardState, Keys Key)
        {
            // Return true if the specified key is pressed and held; otherwise, return false
            return (Keyboard.GetState().IsKeyDown(Key) && KeyboardState.IsKeyUp(Key));
        }

        public static bool IsKeyHeld(Keys Key)
        {
            return (Keyboard.GetState().IsKeyDown(Key));
        }



    }
}
