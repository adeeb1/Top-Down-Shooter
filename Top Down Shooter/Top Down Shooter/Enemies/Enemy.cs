using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //Base Enemy class
    public abstract class Enemy : LevelObject
    {
        //Enemy graphic (will be an animation later)
        public Texture2D EnemyTexture;

        public Enemy()
        {

        }

        //Slightly reduce the depth so the player overlaps the enemy if they're in the same exact spot
        protected override float SetDrawDepth()
        {
            return (base.SetDrawDepth() - .0001f);
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EnemyTexture, ObjectPos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, SetDrawDepth());
        }
    }
}
