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
            SetProjectileProperties(sourceGun, position, direction, new Vector2(2, 2), 200);

            ObjectTexture = LoadAssets.ProjectileTestTexture;

            //Projectiles have an infinite hitbox duration because they can always damage objects they touch
            hitbox = new Hitbox(this, Helper.CreateRect(ObjectPos, ObjectTexture.Width, ObjectTexture.Height), 2, 0f, Hitbox.InfDuration);
        }


    }
}