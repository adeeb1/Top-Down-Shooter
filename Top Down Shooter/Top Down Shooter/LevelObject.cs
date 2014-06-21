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
    public abstract class LevelObject
    {
        // Enum for dictating the possible directions of the object
        public enum Direction : byte { Left, Up, Right, Down };

        // Object texture
        public Texture2D ObjectTexture;

        // Stores the direction of the object
        public Direction ObjectDir;

        // Object Position
        public Vector2 ObjectPos;

        //How fast the object moves
        public Vector2 MoveSpeed;

        //Object health (not required to be used)
        public int Health;

        public LevelObject()
        {

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

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
