using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    // This class provides a set of helper methods
    public static class Helper
    {
        // Centers a gun in relation to the player's position or a projectile in relation to the player's gun's position (and more!)
        // In the former example above, the player is the parent object, and the gun is the child object
        // In the latter example above, the gun is the parent object, and the projectile is the child object
        // This method returns the child object's position
        public static Vector2 CenterGraphic(LevelObject.Direction dir, LevelObject Parent, LevelObject Child)
        {
            // Store the X and Y position of the child graphic, respectively
            int X = 0, Y = 0;

            switch (dir)
            {
                case LevelObject.Direction.Left:
                    X = (int)(Parent.ObjectPos.X - Child.ObjectTexture.Width);
                    Y = (int)(Parent.ObjectPos.Y + (Parent.ObjectTexture.Height / 2) - (Child.ObjectTexture.Height / 2));

                    break;
                case LevelObject.Direction.Right:
                    X = (int)(Parent.ObjectPos.X + Parent.ObjectTexture.Width);
                    Y = (int)(Parent.ObjectPos.Y + (Parent.ObjectTexture.Height / 2) - (Child.ObjectTexture.Height / 2));

                    break;
                case LevelObject.Direction.Up:
                    X = (int)(Parent.ObjectPos.X + (Parent.ObjectTexture.Width / 2) - (Child.ObjectTexture.Width / 2));
                    Y = (int)(Parent.ObjectPos.Y - Child.ObjectTexture.Height);

                    break;
                case LevelObject.Direction.Down:
                    X = (int)(Parent.ObjectPos.X + (Parent.ObjectTexture.Width / 2) - (Child.ObjectTexture.Width / 2));
                    Y = (int)(Parent.ObjectPos.Y + Parent.ObjectTexture.Height);

                    break;
            }

            // Return a new Vector2 with the coordinates for the child object
            return (new Vector2(X, Y));
        }

        public static Rectangle CreateRect(Vector2 objectpos, int width, int height)
        {
            return new Rectangle((int)objectpos.X, (int)objectpos.Y, width, height);
        }
    }
}
