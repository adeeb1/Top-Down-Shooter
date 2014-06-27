using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    //An interface for objects that can block other objects
    public interface Collideable
    {
        //Checks if the object was touched in the X direction
        bool TouchedX(Rectangle boundingbox);

        //Checks if the object was touched in the Y direction
        bool TouchedY(Rectangle boundingbox);

        //Does something when the object touches another object
        void Touches(LevelObject levelobject);

        //Does something when the object is touched
        void WhenTouched(LevelObject levelobject);

        //Does something when the object touches a tile
        void TouchesTile(Tile tile);

        //For damage collisions
        void DamagedObject(LevelObject levelobject);
    }
}
