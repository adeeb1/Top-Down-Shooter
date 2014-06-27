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

        //The basic invincibility duration granted when an object takes damage
        public const float HitInvincibility = 200f;

        //The object this hurtbox is attached to
        public LevelObject hurtboxOwner;
        
        //The bounding box for the rectangle
        public Rectangle Bounds;

        //The defense of the Hurtbox - we ideally want to use low numbers, so try to keep this very low
        public int Defense;

        //Invincibility duration
        private float InvDuration;

        public Hurtbox()
        {
            InvDuration = 0f;
            Defense = 0;
            hurtboxOwner = null;
        }

        public Hurtbox(LevelObject hurtboxowner, Rectangle bounds, int defense) : this()
        {
            hurtboxOwner = hurtboxowner;
            Bounds = bounds;
            Defense = defense;
        }

        //Checks if the hurtbox can take damage
        public bool CanTakeDamage()
        {
            return (IsInvincible == false);
        }

        //Whether the hurtbox is invincible or not
        public bool IsInvincible
        {
            get { return (InvDuration == InfInvincibility || Main.activeTime < InvDuration); }
        }

        //Sets invincibility for the hurtbox
        public void SetInvincibility(float duration)
        {
            InvDuration = (Main.activeTime + duration);
        }

        //Update the location of the hurtbox to be on its owner
        public void Update()
        {
            Vector2 ownerloc = hurtboxOwner.PosWithOrigin;

            Bounds.X = (int)ownerloc.X;
            Bounds.Y = (int)ownerloc.Y;
        }

        //Draw the hurtbox (debug info only)
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LoadAssets.ScalableBox, new Vector2(Bounds.X, Bounds.Y), null, Color.Blue, 0f, Vector2.Zero, new Vector2(Bounds.Width, Bounds.Height), SpriteEffects.None, .999f);
        }
    }
}
