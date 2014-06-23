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
        private String MusicVolume
        {
            get { return ((SoundManager.MusicVolume * 10)).ToString(); }
        }

        private String SoundVolume
        {
            get { return ((SoundManager.SoundVolume * 10)).ToString(); }
        }

        public OptionScreen()
        {
            MenuOptions.Add("Music Volume: " + MusicVolume);
            MenuOptions.Add("Sound Volume: " + SoundVolume);

            MenuOptions.Add("Back");

            OptionPositions.Add(new Vector2(200, 50));
            OptionPositions.Add(new Vector2(200, 100));
            
            OptionPositions.Add(new Vector2(200, 200));
        }

        protected override void PickOption(Main main)
        {
            // The user selected the "Back" option
            if (SelectedOption == 2)
            {
                // Bring the user back to the previous screen
                main.RemoveScreen();
            }
        }

        protected override void ChangeOption(int change)
        {
            // Check if the selected option is "Music Volume"
            if (SelectedOption == 0)
            {
                // Get the float representation of the change
                float TheChange = ((float)change / 10);

                // Float representation of the new volume value
                float NewVolume = (float)Math.Round((SoundManager.MusicVolume + TheChange), 1);

                // Check if the new volume falls within the accepted values
                if ((NewVolume >= 0f) && (NewVolume <= 1f))
                {
                    // Change the volume
                    SoundManager.SetMusicVolume(NewVolume);

                    // Update the volume on the screen
                    MenuOptions[0] = "Music Volume: " + MusicVolume;
                }
            }
            else if (SelectedOption == 1) // The selected option is "Sound Volume"
            {
                // Get the float representation of the change
                float TheChange = ((float)change / 10);

                // Float representation of the new volume value
                float NewVolume = (float)Math.Round((SoundManager.SoundVolume + TheChange), 2);

                // Check if the new volume falls within the accepted values
                if ((NewVolume >= 0f) && (NewVolume <= 1f))
                {
                    // Change the volume
                    SoundManager.SetSoundVolume(NewVolume);

                    // Update the volume on the screen
                    MenuOptions[1] = "Sound Volume: " + SoundVolume;
                }
            }
        }


    }
}