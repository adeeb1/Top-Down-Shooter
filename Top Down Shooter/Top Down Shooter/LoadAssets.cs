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
        public static Texture2D CursorTexture;
        public static SpriteFont MenuFont;

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
            TestTexture = Content.Load<Texture2D>("Graphics/test");
            CursorTexture = Content.Load<Texture2D>("Graphics/cursor");
            MenuFont = Content.Load<SpriteFont>("Graphics/MenuFont");
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
