using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //An animation for animating LevelObjects, backgrounds, and more
    //It takes in a spritesheet and an array of individual frames, drawing each at a specific position on the spritesheet
    //NOTE: Support for reversing animations will be added if necessary
    public class Animation
    {
        //The spritesheet to draw from
        public Texture2D SpriteSheet;

        //The frames to draw
        protected List<AnimFrame> Frames;

        //The current frame
        protected int CurFrame;

        public Animation(Texture2D spritesheet, params AnimFrame[] frames)
        {
            SpriteSheet = spritesheet;
            Frames = new List<AnimFrame>(frames);
            for (int i = 0; i < Frames.Count; i++) Frames[i].Anim = this;

            CurFrame = 0;
            CurrentFrame.ResetFrame();
        }

        //The default origin all sprites are to be drawn; the bottom-middle of the sprite
        public static Vector2 DefaultOrigin(Texture2D Graphic)
        {
            return new Vector2(Graphic.Width / 2, Graphic.Height);
        }

        //The default origin all sprites are to be drawn; the bottom-middle of the sprite
        //This overload takes in a width and height
        public static Vector2 DefaultOrigin(Vector2 GraphicSize)
        {
            return new Vector2((int)(GraphicSize.X / 2), GraphicSize.Y);
        }

        protected AnimFrame CurrentFrame
        {
            get { return Frames[CurFrame]; }
        }

        protected void NextFrame()
        {
            CurFrame = (CurFrame + 1) % Frames.Count;
            Frames[CurFrame].ResetFrame();
        }

        public void Update()
        {
            if (CurrentFrame.FrameComplete == true)
                NextFrame();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawlocation, float depth)
        {
            CurrentFrame.Draw(spriteBatch, drawlocation, depth);
        }
    }
}
