using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //Load all assets here
    public static class LoadAssets
    {
        public static Texture2D TestTexture;
        public static Texture2D EnemyTestTexture;

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
            EnemyTestTexture = Content.Load<Texture2D>("Graphics\\enemytest");
        }

        //Load all music
        public static void LoadMusic(ContentManager Content)
        {

        }

        //Load all sounds
        public static void LoadSounds(ContentManager Content)
        {

        }
    }
}
