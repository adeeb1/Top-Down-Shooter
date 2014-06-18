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
    public class TitleScreen : MenuScreen
    {
        public TitleScreen()
        {
            MenuOptions.Add("Test");
            MenuOptions.Add("Test 2");
            
            OptionPositions.Add(new Vector2(50, 50));
            OptionPositions.Add(new Vector2(50, 100));
        }


    }
}