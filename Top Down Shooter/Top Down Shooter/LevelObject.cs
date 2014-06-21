using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    // Base level object class
    public abstract class LevelObject : Collideable
    {
        // Object Position
        public Vector2 ObjectPos;

        //How fast the object moves
        public Vector2 MoveSpeed;

        //The object's hurtbox; set to null if it doesn't use one
        public Hurtbox hurtbox;

        //Object health (not required to be used)
        public int Health;

        public LevelObject()
        {

        }

        //The bounding box for movement and damage collisions for level objects
        public Rectangle BoundingBox
        {
            get 
            {
                if (hurtbox != null) return hurtbox.Bounds;
                else return Rectangle.Empty;
            }
        }

        //Sets the draw depth of a level object
        //Objects lower on the screen will be drawn over objects above
        protected virtual float SetDrawDepth()
        {
            return (ObjectPos.Y / 1000f);
        }

        //Makes the level object take damage upon contacting with something harmful
        public virtual void TakeDamage()
        {

        }

        //Instantly kills the level object, whether it lost all its health or touched some hazard that instantly kills
        //This method may do things like starting a death animation, playing a sound or more
        public virtual void Die()
        {

        }

        //Interface defaults; override them in the derived classes
        public bool TouchedX(Rectangle boundingbox)
        {
            return false;
        }

        public bool TouchedY(Rectangle boundingbox)
        {
            return false;
        }

        //Default behavior of do nothing
        //For an object like a projectile, you may want the projectile to be removed from the level
        public void Touches(LevelObject levelobject)
        {

        }

        //Default behavior of do nothing
        //For an object like a land mine, you may want it to explode
        public void OnTouched(LevelObject levelobject)
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Debug.HurtboxDraw == true && hurtbox != null)
                hurtbox.Draw(spriteBatch);
        }
    }
}
