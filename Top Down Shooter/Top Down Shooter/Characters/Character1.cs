using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    public sealed class Character1 : Character
    {
        public Character1()
        {
            MoveSpeed = new Vector2(5, 5);

            Health = 20;

            PlayerGun = new MachineGun(ObjectPos, ObjectDir, int.MaxValue, int.MaxValue, 200);

            PlayerGun.GunOwner = this;
        }


    }
}
