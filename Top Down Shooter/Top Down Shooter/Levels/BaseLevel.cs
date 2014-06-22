using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //The base class for a level; it contains all the level objects
    //This class may be instantiated instead of used purely as a base class
    public class BaseLevel
    {
        public List<LevelObject> levelObjects;

        public BaseLevel()
        {
            levelObjects = new List<LevelObject>();
        }

        public void AddObject(LevelObject levelobject)
        {
            levelObjects.Add(levelobject);
        }
    }
}
