using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    public class MachineGun : Gun
    {
        public MachineGun(Vector2 position, Direction direction, int maxAmmo, int ammoLeft, float shootDelayTime)
        {
            // Set the gun's properties
            SetGunProperties(position, direction, maxAmmo, ammoLeft, shootDelayTime);
            
            // Set the gun's texture
            ObjectTexture = LoadAssets.GunTestTexture;
        }


    }
}