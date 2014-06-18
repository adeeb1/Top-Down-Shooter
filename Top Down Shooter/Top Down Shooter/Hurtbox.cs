using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    //A hurtbox; this is enclosed around objects that can take damage and be harmed
    //When a hitbox collides with this, the object this belongs to takes damage
    public sealed class Hurtbox
    {
        //The object this hurtbox is attached to (NOTE: May not actually be needed)
        public LevelObject levelObject;

        //The bounding box for the rectangle
        public Rectangle Bounds;

        //Whether the hurtbox is invincible or not
        public bool IsInvincible;
    }
}
