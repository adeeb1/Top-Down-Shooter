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

        //The object this hitbox is attached to
        public LevelObject hitboxOwner;

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

        //Constructor
        public Hitbox()
        {
            Delay = Duration = StartDelay = StartDuration = 0f;
        }

        public Hitbox(LevelObject hitboxowner, Rectangle bounds, int damage, float delay, float duration) : this()
        {
            hitboxOwner = hitboxowner;
            Bounds = bounds;
            Damage = damage;

            SetHitboxProperties(delay, duration);
        }

        public Hitbox(LevelObject hitboxowner, Rectangle bounds, int damage, float delay, float duration, Vector2 impactforce) : this(hitboxowner, bounds, damage, delay, duration)
        {
            ImpactForce = impactforce;
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
            get { return (Main.activeTime >= StartDelay && ((Main.activeTime < StartDuration) || Duration == InfDuration)); }
        }

        //Checks if a hitbox touches a hurtbox
        public bool CanHitObject(Hurtbox objecthurtbox)
        {
            return (CanHit == true && Bounds.Intersects(objecthurtbox.Bounds));
        }

        //Update the location of the hurtbox to be on its owner
        public void Update()
        {
            Vector2 ownerloc = hitboxOwner.PosWithOrigin;

            Bounds.X = (int)ownerloc.X;
            Bounds.Y = (int)ownerloc.Y;
        }

        //Draw the hitbox (debug info only)
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(LoadAssets.ScalableBox, new Vector2(Bounds.X, Bounds.Y) - hitboxOwner.Level.LevelCam.CameraLocation, null, Color.Red, 0f, Vector2.Zero, new Vector2(Bounds.Width, Bounds.Height), SpriteEffects.None, .999f);
        }
    }
}
