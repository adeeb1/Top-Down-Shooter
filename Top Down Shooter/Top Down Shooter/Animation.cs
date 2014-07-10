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

        //States whether the animation is playing or not; it's set to play by default
        protected bool Playing;

        public Animation(Texture2D spritesheet, params AnimFrame[] frames)
        {
            SpriteSheet = spritesheet;
            Frames = new List<AnimFrame>(frames);
            for (int i = 0; i < Frames.Count; i++) Frames[i].Anim = this;

            CurFrame = 0;
            CurrentFrame.ResetFrame();
            Playing = true;
        }

        //A copy constructor, allowing a duplicate of the same animation except with it flipped a certain way
        public Animation(Animation oldanimation, SpriteEffects flipped)
        {
            this.SpriteSheet = oldanimation.SpriteSheet;
            this.Frames = new List<AnimFrame>();

            for (int i = 0; i < oldanimation.Frames.Count; i++)
                this.Frames.Add(new AnimFrame(oldanimation.Frames[i], this, flipped));

            this.CurFrame = oldanimation.CurFrame;
            this.Playing = oldanimation.Playing;
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

        //Tells whether the animation is playing or not
        public bool IsPlaying
        {
            get { return Playing; }
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

        //Starts the animation up again
        public void Play()
        {
            Playing = true;
        }

        //Stops the animation and resets it
        public void Stop()
        {
            Playing = false;
            Reset();
        }

        //Pauses the animation
        public void Pause()
        {
            Playing = false;
            CurrentFrame.PauseFrame();
        }

        //Resumes the animation
        public void Resume()
        {
            Play();
            CurrentFrame.ResumeFrame();
        }

        //Resets the animation back to the start
        public void Reset()
        {
            CurFrame = 0;
        }

        //Restarts the animation; there is an option for just restarting the current frame
        public void Restart(bool entireanimation = true)
        {
            //Stop the animation and restart it
            if (entireanimation == true)
            {
                Stop();
                Play();
            }
            
            CurrentFrame.ResetFrame();
        }

        public void Update()
        {
            if (Playing == true && CurrentFrame.FrameComplete == true)
                NextFrame();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawlocation, float depth)
        {
            CurrentFrame.Draw(spriteBatch, drawlocation, depth);
        }
    }
}
