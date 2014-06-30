using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //The base Powerup class
    //Powerups grant players or enemies bonuses, such as health, increased damage, increased defense, invincibility, or special bullets
    //Powerups can last a certain amount of time, until hit, until death, or until a level is over or a player runs out of lives
    public abstract class Powerup : Pickup
    {
        //Powerup types
        //None exists solely as a default value since we don't want to classify an uninitialized Powerup as something
        public enum Powerups
        {
            None, Health, Damage, Defense, Invincibility, Other
        };

        //Duration for a level-long or continue-long Powerup
        public const float LevelDuration = -3f;

        //Duration for a life-long Powerup
        public const float LifeDuration = -2f;

        //Duration for a hit-long Powerup
        public const float HitlongDuration = -1f;

        //Powerup type
        public Powerups PowerupType;

        //Powerup duration; a duration of 0 means the power-up is instant (Ex: Health)
        public float Duration;
        protected float PrevDuration;

        //Whether the Powerup has taken effect or not
        protected bool Activated;

        public Powerup()
        {
            PickupOwner = null;
            Deactivate();
        }

        public Powerup(Vector2 objectpos) : this()
        {
            ObjectPos = objectpos;

            hurtbox = new Hurtbox(this, Helper.CreateRect(ObjectPos, ObjectTexture.Width, ObjectTexture.Height), 0);
        }

        //Default Powerup
        public static Powerup Default
        {
            get { return new DefaultPowerup(); }
        }

        public override bool IsDead
        {
            get { return (Dead == true); }
        }

        public bool PowerupDone
        {
            get { return (Activated == true && Duration > HitlongDuration && Main.activeTime >= PrevDuration); }
        }

        public override bool TouchedX(Rectangle boundingbox)
        {
            return boundingbox.Intersects(FeetLoc);
        }

        public override bool TouchedY(Rectangle boundingbox)
        {
            return boundingbox.Intersects(FeetLoc);
        }

        //Additional damage the Powerup may deal
        public virtual int AdditionalDamage()
        {
            return 0;
        }

        public virtual int AdditionalDefense()
        {
            return 0;
        }

        //If the Pickup touches something if it moves, we want what happens when the Pickup is touched to happen here
        public override void Touches(LevelObject levelobject)
        {
            WhenTouched(levelobject);
        }

        //When touched, the Pickup is removed from the level and given to the object that touched it, provided the object is a player (or enemy in some cases)
        public override void WhenTouched(LevelObject levelobject)
        {
            //Check if the object is a player
            if (PlayerPickup == true)
            {
                Character character = levelobject as Character;
                if (character != null)
                {
                    Level.levelObjects.Remove(this);

                    PickupOwner = levelobject;
                    PickupOwner.PowerUp = this;
                    if (ShouldActivate() == true) Activate();
                }
            }
            else if (EnemyPickup == true)
            {
                Enemy enemy = levelobject as Enemy;
                if (enemy != null)
                {
                    Level.levelObjects.Remove(this);

                    PickupOwner = levelobject;
                    PickupOwner.PowerUp = this;
                    if (ShouldActivate() == true) Activate();
                }
            }
        }

        //Tells whether the Powerup should activate or not (Ex. automatically or through an input like a button press)
        protected virtual bool ShouldActivate()
        {
            return true;
        }

        //Activates the Powerup
        //Call this on touch or when a key is pressed to activate the powerup
        protected virtual void Activate()
        {
            PrevDuration = Main.activeTime + Duration;

            Activated = true;
        }

        //Deactivates a Powerup
        //Call this when you need to deactivate a Powerup that lasts for a hit, life, or level/continue
        public void Deactivate()
        {
            PowerupType = Powerups.None;
            Activated = false;
            Duration = PrevDuration = 0f;
        }
    }
}
