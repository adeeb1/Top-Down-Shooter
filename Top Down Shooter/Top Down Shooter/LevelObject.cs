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
        // Enum for dictating the possible directions of the object
        public enum Direction : byte { Left, Up, Right, Down };

        // Object texture
        public Texture2D ObjectTexture;

        // Stores the direction of the object
        public Direction ObjectDir;

        //Level reference
        public BaseLevel Level;

        // Object Position
        public Vector2 ObjectPos;

        //How fast the object moves
        public Vector2 MoveSpeed;

        //The object's hitbox; ALWAYS create a new instance when making a new hitbox
        public Hitbox hitbox;

        //The object's hurtbox; set to null if it doesn't use one
        public Hurtbox hurtbox;

        //Object health (not required to be used)
        public int Health;

        //Tells if the object is dead or not
        protected bool Dead;

        public LevelObject()
        {
            ObjectPos = Vector2.Zero;
            Dead = false;
        }

        //Get if the object is dead or not
        public virtual bool IsDead
        {
            get { return (Dead == true || Health <= 0); }
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

        //The location of the object's "feet"; where the object has collision with tiles
        public Rectangle FeetLoc
        {
            get { return new Rectangle((int)ObjectPos.X, (int)ObjectPos.Y - (ObjectTexture.Height / 2), ObjectTexture.Width, ObjectTexture.Height / 2); }
        }

        //The origin of drawing the object: the bottom-middle of the sprite
        protected Vector2 ObjectOrigin
        {
            get
            {
                Vector2 origin = Vector2.Zero;
                if (ObjectTexture != null) origin = new Vector2(ObjectTexture.Width / 2, ObjectTexture.Height);

                return origin;
            }
        }

        //Sets the draw depth of a level object
        //Objects lower on the screen will be drawn over objects above
        protected virtual float SetDrawDepth()
        {
            return (ObjectPos.Y / 1000f);
        }

        //The location of the object, including its ObjectOrigin
        //We subtract the origin because when drawing, the object is moved left with a positive X origin and up with a positive Y origin
        public Vector2 PosWithOrigin
        {
            get 
            {
                return (ObjectPos - ObjectOrigin); 
            }
        }

        //Calculates the total damage a hitbox deals when touching a hurtbox
        protected virtual int CalculateDamage(Hitbox hitbox)
        {
            //Ensure that we don't add health by obtaining a negative value if the hurtbox's Defense is higher than the hitbox's Damage
            int totaldamage = hitbox.Damage - hurtbox.Defense;
            if (totaldamage < 0) totaldamage = 0;

            return totaldamage;
        }

        //Makes the level object take damage upon contacting with something harmful
        public virtual void TakeDamage(Hitbox hitbox)
        {
            //NOTE: Put a more elaborate damage calculation here that takes power-ups and etc. into account; for now just keep this
            Health -= CalculateDamage(hitbox);

            //No health remaining; the object should die
            if (Health <= 0)
            {
                Die();
            }
        }

        //Instantly kills the level object, whether it lost all its health or touched some hazard that instantly kills
        //This method may do things like starting a death animation, playing a sound or more
        public virtual void Die()
        {
            Dead = true;
        }

        //Interface defaults; override them in the derived classes
        public virtual bool TouchedX(Rectangle boundingbox)
        {
            return false;
        }

        public virtual bool TouchedY(Rectangle boundingbox)
        {
            return false;
        }

        //Default behavior of do nothing
        //For an object like a projectile, you may want the projectile to be removed from the level
        public virtual void Touches(LevelObject levelobject)
        {

        }

        //Default behavior of do nothing
        //For an object like a land mine, you may want it to explode
        public virtual void WhenTouched(LevelObject levelobject)
        {

        }

        //Default behavior; nothing
        public virtual void TouchesTile(Tile tile)
        {

        }

        //Default behavior; do nothing
        //For a standard projectile, you'll want to destroy it
        public virtual void DamagedObject(LevelObject levelobject)
        {

        }

        //Checks if the level object is about to collide with another level object
        //It returns the first LevelObject that is touched
        protected virtual LevelObject Collided()
        {
            //Go through all the level objects
            for (int i = 0; i < Level.levelObjects.Count; i++)
            {
                //Don't check for collisions with itself
                if (this != Level.levelObjects[i])
                {
                    //If X speed is 0, don't bother checking
                    if (MoveSpeed.X != 0f)
                    {
                        if (Level.levelObjects[i].TouchedX(BoundingBox) == true)
                            return Level.levelObjects[i];
                    }

                    //If Y speed is 0, don't bother checking
                    if (MoveSpeed.Y != 0f)
                    {
                        if (Level.levelObjects[i].TouchedY(BoundingBox) == true)
                            return Level.levelObjects[i];
                    }
                }
            }

            return null;
        }

        //Causes a level object to move; there is an option to move taking collision into account, which is set to true by default
        public virtual void Move(Vector2 moveamount, bool collision = true)
        {
            //No collision; simply move
            if (collision == false) ObjectPos += moveamount;
            else
            {
                //First check if the tile we touched will block us or not
                Tile tile = Level.TileEngine.CheckNextTile(FeetLoc, moveamount);

                //Check if we touched a special tile
                if (tile.TileType != Tile.TileTypes.None)
                {
                    TouchesTile(tile);

                    //We touched a block tile, so block movement by exiting
                    if (tile.TileType == Tile.TileTypes.Block)
                        return;
                }

                //Check if we touched anything
                LevelObject objtouched = Collided();

                //We did, so do something and block movement
                if (objtouched != null)
                {
                    Touches(objtouched);
                    objtouched.WhenTouched(this);
                }
                //Otherwise move
                else ObjectPos += moveamount;
            }
        }

        //Updates the hitboxes and hurtboxes of the object
        protected void UpdateCollisionBoxes()
        {
            if (hitbox != null) hitbox.Update();
            if (hurtbox != null) hurtbox.Update();
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (ObjectTexture != null)
                spriteBatch.Draw(ObjectTexture, ObjectPos, null, Color.White, 0f, ObjectOrigin, 1f, SpriteEffects.None, SetDrawDepth());

            //Draw debug info
            DebugDraw(spriteBatch);
        }

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            if (Debug.HitboxDraw == true && hitbox != null)
                hitbox.Draw(spriteBatch);

            if (Debug.HurtboxDraw == true && hurtbox != null)
                hurtbox.Draw(spriteBatch);
        }
    }
}
