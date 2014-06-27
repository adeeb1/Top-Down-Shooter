using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Top_Down_Shooter
{
    //A class for helping with debugging, like containing hotkeys for enabling the drawing of hurtboxes
    public static class Debug
    {
        private static KeyboardState DebugKeyboard;

        public static bool HitboxDraw;
        public static bool HurtboxDraw;
        public static bool TileDraw;

        static Debug()
        {
            DebugKeyboard = new KeyboardState(Keys.Left, Keys.Right);

            HitboxDraw = HurtboxDraw = TileDraw = false;
        }

        public static void Update()
        {
            //Debug commands; hold left shift and press an arrow key to enable/disable the drawing of hitboxes or hurtboxes
            if (Input.IsKeyHeld(Keys.LeftShift) == true)
            {
                if (Input.IsKeyDown(DebugKeyboard, Keys.Left) == true)
                {
                    HurtboxDraw = !HurtboxDraw;
                }

                if (Input.IsKeyDown(DebugKeyboard, Keys.Right) == true)
                {
                    HitboxDraw = !HitboxDraw;
                }

                if (Input.IsKeyDown(DebugKeyboard, Keys.T) == true)
                {
                    TileDraw = !TileDraw;
                }
            }

            DebugKeyboard = Keyboard.GetState();
        }
    }
}
