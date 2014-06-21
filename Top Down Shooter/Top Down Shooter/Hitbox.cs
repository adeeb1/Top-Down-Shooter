using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //A hitbox that is attached to objects that deal damage
    //This is a generic hitbox; there may be derived ones that are more specific (throw, knock down, or anything else we may need)
    public class Hitbox
    {
        //An infinite duration for a hitbox, -1
        public const float InfDuration = -1f;

        //The bounding box of the hitbox
        public Rectangle Bounds;

        //The damage the hitbox deals
        public int Damage;

        //A force on the hitbox that will move the object it contacts
        public Vector2 ImpactForce;

        //The delay for the hitbox
        protected float Delay;
        protected float StartDelay;

        //The duration of the hitbox
        protected float Duration;
        protected float StartDuration;

        //The hurtboxes that this hitbox hit; this prevents it from hitting the same hurtbox again
        /*NOTE: This MAY not be needed in the majority of cases since bullets will disappear upon hitting something, but in the event that
                there's a hitbox that doesn't disappear upon contact (Ex: bullet that goes through enemies), this will be required*/
        private List<Hurtbox> Hurtboxes;

        //Constructor
        public Hitbox()
        {
            Hurtboxes = new List<Hurtbox>();

            Delay = Duration = StartDelay = StartDuration = 0f;
        }

        //Set initial hitbox properties, like duration and delay
        protected void SetHitboxProperties(float delay, float duration)
        {
            Delay = delay;
            Duration = duration;

            StartDelay = Main.activeTime + Delay;
            StartDuration = Main.activeTime + Duration;
        }

        //Checks if the hitbox can hit
        public bool CanHit
        {
            get { return (Main.activeTime >= StartDelay && ((Main.activeTime < StartDuration) || StartDuration == InfDuration)); }
        }

        //Checks if a hitbox touches a hurtbox
        //It could not have hit the hurtbox previously
        public bool CanHitObject(Hurtbox objecthurtbox)
        {
            return (Hurtboxes.Contains(objecthurtbox) == false && Bounds.Intersects(objecthurtbox.Bounds));
        }

        //Occurs when the hitbox collides with a hurtbox; add the hurtbox to the list of hurtboxes that were hit
        public void SetHurtbox(Hurtbox objecthurtbox)
        {
            Hurtboxes.Add(objecthurtbox);
        }

        //Draw the hitbox (debug info only)
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LoadAssets.ScalableBox, new Vector2(Bounds.X, Bounds.Y), null, Color.Red, 0f, Vector2.Zero, new Vector2(Bounds.Width, Bounds.Height), SpriteEffects.None, .999f);
        }
    }
}
