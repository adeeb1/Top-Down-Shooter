using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    public class Projectile1 : Projectile
    {
        public Projectile1(Gun sourceGun, Vector2 position, Direction direction)
        {
            // Set the projectile's properties
            SetProjectileProperties(sourceGun, position, direction, new Vector2(10, 10), 200);

            ObjectTexture = LoadAssets.ProjectileTestTexture;
        }


    }
}