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
        // Object Position
        public Vector2 ObjectPos;

        public LevelObject()
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
