using System;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using SharpDX;
using SharpDX.XAudio2;
using SharpDX.Windows;

namespace Top_Down_Shooter
{
    //A sound manager that contains methods for playing sounds as well as containing global sound and music volumes
    public static class SoundManager
    {
        public static float SoundVolume;
        public static float MusicVolume;

        static SoundManager()
        {
            //Initialize them to the middle value of .5f
            SoundVolume = MusicVolume = .5f;

            MediaPlayer.Volume = .5f;
        }

        //Plays a sound with the default pitch and pan values
        public static void PlaySound(SoundEffect sound)
        {
            if (sound != null)
            {
                sound.Play(SoundVolume, 0f, 0f);
            }
        }

        //Plays a song, stopping the previous song first - you can choose to not loop the song if you wish, but the default will be set to loop
        public static void PlaySong(Song song, bool loop = true)
        {
            if (song != null)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = loop;
            }
        }
    }
}
