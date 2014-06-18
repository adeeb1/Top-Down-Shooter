using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    public sealed class Enemy1 : Enemy
    {
        public Enemy1(Texture2D graphic, Vector2 location)
        {
            EnemyTexture = graphic;

            ObjectPos = location;
        }
    }
}
