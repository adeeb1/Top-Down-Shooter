using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //A hurtbox; this is enclosed around objects that can take damage and be harmed
    //When a hitbox collides with this, the object this belongs to takes damage
    public sealed class Hurtbox
    {
        //Infinite duration for invincibility, -1
        public const float InfInvincibility = -1f;

        //The object this hurtbox is attached to (NOTE: May not actually be needed)
        public LevelObject levelObject;
        
        //The bounding box for the rectangle
        public Rectangle Bounds;

        //Invincibility duration
        private float InvDuration;

        public Hurtbox()
        {
            InvDuration = 0f;
        }

        //Whether the hurtbox is invincible or not
        public bool IsInvincible
        {
            get { return (InvDuration == InfInvincibility || Main.activeTime < InvDuration); }
        }

        public void SetInvincibility(float duration)
        {
            InvDuration = (Main.activeTime + duration);
        }

        //Draw the hurtbox (debug info only)
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LoadAssets.ScalableBox, new Vector2(Bounds.X, Bounds.Y), null, Color.Blue, 0f, Vector2.Zero, new Vector2(Bounds.Width, Bounds.Height), SpriteEffects.None, .999f);
        }
    }
}
