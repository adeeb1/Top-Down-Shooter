using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //A frame of an Animation
    public class AnimFrame
    {
        //The animation this AnimSprite belongs to
        public Animation Anim;

        //The rectangle on the spritesheet to draw the frame
        private Rectangle FrameBox;

        //The offset to draw the frame
        //Note that the values will shift the frame in the opposite direction than expected (positive = left & up, negative = right & down)
        private Vector2 FrameOrigin;

        //The length of the frame
        private float FrameDuration;
        private float PrevDuration;

        //How much time is remaining in the frame when the frame was paused
        private float TimePaused;

        //Whether the frame is flipped or not
        private SpriteEffects Flipped;

        public AnimFrame()
        {
            Anim = null;
            FrameBox = Rectangle.Empty;
            FrameOrigin = Vector2.Zero;
            FrameDuration = PrevDuration = TimePaused = 0f;

            Flipped = SpriteEffects.None;
        }

        public AnimFrame(Rectangle framebox, Vector2 frameorigin, float frameduration) : this()
        {
            FrameBox = framebox;
            FrameOrigin = frameorigin;
            FrameDuration = frameduration;
        }

        public AnimFrame(Rectangle framebox, Vector2 frameorigin, float frameduration, SpriteEffects flipped) : this(framebox, frameorigin, frameduration)
        {
            Flipped = flipped;
        }

        //Copy constructor with the flipped parameter
        public AnimFrame(AnimFrame oldframe, Animation newanimation, SpriteEffects flipped)
        {
            this.Anim = newanimation;

            this.FrameBox = oldframe.FrameBox;
            this.FrameOrigin = oldframe.FrameOrigin;
            this.FrameDuration = oldframe.FrameDuration;
            this.PrevDuration = 0f;

            this.Flipped = flipped;
        }

        public bool FrameComplete
        {
            get { return (Main.activeTime >= PrevDuration); }
        }

        //Resets the frame's duration
        public void ResetFrame()
        {
            PrevDuration = (Main.activeTime + FrameDuration);
        }

        //Pauses the frame
        public void PauseFrame()
        {
            TimePaused = PrevDuration - Main.activeTime;
        }

        //Resumes the frame
        public void ResumeFrame()
        {
            PrevDuration = Main.activeTime + TimePaused;
            TimePaused = 0f;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawlocation, float depth)
        {
            spriteBatch.Draw(Anim.SpriteSheet, drawlocation, FrameBox, Color.White, 0f, Animation.DefaultOrigin(new Vector2(FrameBox.Width, FrameBox.Height)) + FrameOrigin, 1f, Flipped, depth);
        }
    }
}
