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
            MenuOptions.Add("Start Game");
            MenuOptions.Add("Options");
            
            OptionPositions.Add(new Vector2(50, 50));
            OptionPositions.Add(new Vector2(50, 100));
        }

        protected override void PickOption(Main main)
        {
            switch (SelectedOption)
            {
                case 0: // Start Game
                    // Start the game
                    main.ChangeGameState(GameState.InGame);
                    break;
                case 1: // Options
                    main.AddScreen(new OptionScreen());
                    break;
            }
        }


    }
}