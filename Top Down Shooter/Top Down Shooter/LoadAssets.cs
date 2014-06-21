using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Top_Down_Shooter
{
    //Load all assets here
    //All sound files MUST be 16-bit .wav files, and all music files MUST be .wma files since this is being built for Windows 8
    //All music files MUST contain both the .xnb and the .wav file to play (it can't load with just the .wav and it can't play with just the .xnb)!
    public static class LoadAssets
    {
        //Graphics
        public static Texture2D TestTexture;
        public static Texture2D CursorTexture;
        public static Texture2D EnemyTestTexture;
        public static Texture2D GunTestTexture;
        public static Texture2D ProjectileTestTexture;
        public static SpriteFont MenuFont;

        //Debug graphics
        public static Texture2D ScalableBox;

        //Sounds
        public static SoundEffect TestSound;

        //Music
        public static Song TestSong;

        //Static variables here
        static LoadAssets()
        {
            
        }

        //Load all content
        public static void LoadContent(ContentManager Content)
        {
            LoadGraphics(Content);
            LoadMusic(Content);
            LoadSounds(Content);
        }

        //Load all graphics
        public static void LoadGraphics(ContentManager Content)
        {
            TestTexture = Content.Load<Texture2D>("Graphics\\test");
            CursorTexture = Content.Load<Texture2D>("Graphics/cursor");
            EnemyTestTexture = Content.Load<Texture2D>("Graphics\\enemytest");
            GunTestTexture = Content.Load<Texture2D>("Graphics\\guntest");
            ProjectileTestTexture = Content.Load<Texture2D>("Graphics\\projectiletest");
            MenuFont = Content.Load<SpriteFont>("Graphics/MenuFont");

            ScalableBox = Content.Load<Texture2D>("Graphics/ScalableBox");
        }

        //Load all music
        public static void LoadMusic(ContentManager Content)
        {
            TestSong = Content.Load<Song>("Music\\Mario Party - Peaceful Mushroom Village");
        }

        //Load all sounds
        public static void LoadSounds(ContentManager Content)
        {
            TestSound = Content.Load<SoundEffect>("Sounds/test");
        }
    }
}
