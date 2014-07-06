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

        //Object animation
        public Animation ObjectAnim;

        // Stores the direction of the object
        public Direction ObjectDir;

        //Level reference
        public BaseLevel Level;

        //The powerup this object has
        public Powerup PowerUp;

        // Object Position
        public Vector2 ObjectPos;

        //How fast the object moves
        public Vector2 MoveSpeed;

        //The object's hitbox; ALWAYS create a new instance when making a new hitbox
        public Hitbox hitbox;

        //The object's hurtbox; set to null if it doesn't use one
        public Hurtbox hurtbox;

        //Object health (not required to be used)
        public int MaxHealth;
        public int Health;

        //Tells if the object is dead or not
        protected bool Dead;

        public LevelObject()
        {
            ObjectPos = Vector2.Zero;
            Dead = false;

            MaxHealth = 10;
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
            get { return new Rectangle((int)ObjectPos.X - (ObjectTexture.Width / 2), (int)ObjectPos.Y - (ObjectTexture.Height / 2), ObjectTexture.Width, ObjectTexture.Height / 2); }
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

        //The location to draw the object, which takes the camera's position into account
        protected Vector2 DrawLocation
        {
            get { return ObjectPos + Level.LevelCam.CameraOffset; }
        }

        //Sets the draw depth of a level object
        //Objects lower on the screen will be drawn over objects above
        protected virtual float SetDrawDepth()
        {
            float depth = ((ObjectPos.Y + Level.LevelCam.CameraOffset.Y) / 1000f);
            if (depth <= 0) depth = .001f;

            return depth;
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

        //The Powerup the LevelObject has
        public virtual Powerup GetPowerup
        {
            get { return PowerUp; }
        }

        //The owner of the collision check; for projectiles, this is the object that shot the bullets
        public virtual LevelObject CollisionOwner
        {
            get { return this; }
        }

        //Heals the LevelObject for a certain amount
        public virtual void Heal(int healamount)
        {
            Health += healamount;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        //Calculates the total damage a hitbox deals when touching a hurtbox
        protected virtual int CalculateDamage(Hitbox hitbox)
        {
            //Ensure that we don't add health by obtaining a negative value if the hurtbox's Defense is higher than the hitbox's Damage
            //Take powerups into account
            int totaldamage = (hitbox.Damage + hitbox.hitboxOwner.GetPowerup.AdditionalDamage()) - (hurtbox.Defense + hurtbox.hurtboxOwner.PowerUp.AdditionalDefense());
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
                Health = 0;
                Die();
            }
            //Disable Powerups that disappear on hit
            else
            {
                //Deactivate a Powerup if it disappears on-hit
                if (PowerUp.Duration == Powerup.HitlongDuration) PowerUp.Deactivate();
            }
        }

        //Instantly kills the level object, whether it lost all its health or touched some hazard that instantly kills
        //This method may do things like starting a death animation, playing a sound or more
        public virtual void Die()
        {
            Dead = true;
        }

        public virtual bool HasCollision
        {
            get { return false; }
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
        protected virtual LevelObject Collided(Rectangle boundingbox)
        {
            //Go through all the level objects
            /*To improve efficiency, have a separate list of objects that contains only LevelObjects with collision don't do this 
              unless performance becomes an issue*/
            for (int i = 0; i < Level.levelObjects.Count; i++)
            {
                //Don't check for collisions with itself or if the object doesn't have a hurtbox
                if (this != Level.levelObjects[i] && Level.levelObjects[i].hurtbox != null)
                {
                    //If X speed is 0, don't bother checking
                    if (MoveSpeed.X != 0f)
                    {
                        if (Level.levelObjects[i].TouchedX(boundingbox) == true)
                            return Level.levelObjects[i];
                    }

                    //If Y speed is 0, don't bother checking
                    if (MoveSpeed.Y != 0f)
                    {
                        if (Level.levelObjects[i].TouchedY(boundingbox) == true)
                            return Level.levelObjects[i];
                    }
                }
            }

            return null;
        }

        //Move in increments
        protected void MoveIncrements(int maxmove, Vector2 increment, int inc)
        {
            for (int move = 0; move != maxmove; move += inc)
            {
                //First check if the tilea we touched will block us or not
                List<Tile> tiles = Level.TileEngine.CheckCollidingTiles(FeetLoc, increment);

                bool blocktile = false;

                //Go through all the tiles and touch them
                for (int i = 0; i < tiles.Count; i++)
                {
                    //Check if we touched a special tile
                    if (tiles[i].TileType != Tile.TileTypes.None)
                    {
                        TouchesTile(tiles[i]);

                        //We touched a block tile; track it and continue
                        /*NOTE: This causes a slight oddity in that if there was a tile type that reacted to you moving onto it and didn't track who
                                touched it, it will activate again even if you didn't actually move anywhere*/
                        if (tiles[i].TileType == Tile.TileTypes.Block)
                            blocktile = true;
                    }
                }

                //We touched a block tile, so exit
                if (blocktile == true) return;

                //Update the bounding box
                Rectangle boundingbox = BoundingBox;
                boundingbox.X += (int)increment.X;
                boundingbox.Y += (int)increment.Y;

                //Check if we touched anything
                LevelObject objtouched = Collided(boundingbox);

                //We did, so do something and block movement
                //NOTE: Change this to account for objects you do touch and can go through but don't actually hurt, such as Powerups
                if (objtouched != null)
                {
                    Touches(objtouched);
                    objtouched.WhenTouched(this);

                    //Block movement and end the loop if the object has collision, otherwise move and continue
                    if (objtouched.HasCollision == true)
                    {
                        break;
                    }
                    else ObjectPos += increment;
                }
                //Otherwise move
                else
                {
                    ObjectPos += increment;
                    UpdateCollisionBoxes();
                }
            }
        }

        //Causes a level object to move; there is an option to move taking collision into account, which is set to true by default
        public virtual void Move(Vector2 moveamount, bool collision = true)
        {
            //No collision; simply move
            if (collision == false) ObjectPos += moveamount;
            else
            {
                //See how much to move
                int incx = moveamount.X < 0 ? -1 : 1;
                int incy = moveamount.Y < 0 ? -1 : 1;

                //Move in the X first, then the Y
                MoveIncrements((int)moveamount.X, new Vector2(incx, 0), incx);
                MoveIncrements((int)moveamount.Y, new Vector2(0, incy), incy);
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
            if (ObjectAnim != null) ObjectAnim.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //if (ObjectTexture != null)
            //    spriteBatch.Draw(ObjectTexture, DrawLocation, null, Color.White, 0f, ObjectOrigin, 1f, SpriteEffects.None, SetDrawDepth());
            if (ObjectAnim != null)
                ObjectAnim.Draw(spriteBatch, DrawLocation, SetDrawDepth());

            //Draw debug info
            DebugDraw(spriteBatch);
        }

        public void DebugDraw(SpriteBatch spriteBatch)
        {
            if (Debug.HitboxDraw == true && hitbox != null)
                hitbox.Draw(spriteBatch);

            if (Debug.HurtboxDraw == true && hurtbox != null)
                hurtbox.Draw(spriteBatch);

            if (Debug.FeetLocDraw == true)
            {
                Rectangle feetloc = FeetLoc;
                spriteBatch.Draw(LoadAssets.ScalableBox, new Vector2(feetloc.X, feetloc.Y) + Level.LevelCam.CameraOffset, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(feetloc.Width, feetloc.Height), SpriteEffects.None, .999f);
            }
        }
    }
}
