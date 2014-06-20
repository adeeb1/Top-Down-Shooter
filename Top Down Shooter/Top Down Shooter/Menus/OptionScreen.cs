using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    // Title screen class
    public class OptionScreen : MenuScreen
    {
        public OptionScreen()
        {
            MenuOptions.Add("Stuff");
            MenuOptions.Add("Back");

            OptionPositions.Add(new Vector2(200, 50));
            OptionPositions.Add(new Vector2(200, 100));
        }

        protected override void PickOption(Main main)
        {
            switch (SelectedOption)
            {
                case 0: // Stuff
                    // Do nothing for now

                    break;
                case 1: // Back
                    main.RemoveScreen();
                    break;
            }
        }


    }
}