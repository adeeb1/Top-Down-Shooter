using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    public class WalkingEnemy : Enemy
    {
        // When walking towards the player, sets the point at which the enemy should stop moving
        private Vector2 DistanceFromPlayer;

        public WalkingEnemy(Texture2D graphic, Vector2 location)
        {
            ObjectTexture = graphic;
            Health = 1;

            hurtbox = new Hurtbox(this, Helper.CreateRect(ObjectPos, ObjectTexture.Width, ObjectTexture.Height), 0);

            ObjectPos = location;

            MoveSpeed = new Vector2(2, 2);
            DistanceFromPlayer = new Vector2(0, 0);
        }

        public override void Update()
        {
            // Check if the enemy is active
            if (IsActive == true)
            {
                base.Update();

                // Move the enemy towards the player
                MoveTowardsPlayer(DistanceFromPlayer);
            }
        }

        // TODO: Maybe put in the base Enemy class
        private void MoveTowardsPlayer(Vector2 Radius)
        {
            // Check if the player is to the left of the enemy on the screen. Factor in the radius
            if (Level.Player.ObjectPos.X < (ObjectPos.X - ObjectTexture.Width - Radius.X))
            {
                Move(new Vector2(-MoveSpeed.X, 0), true);
            }
            // Check if the player is to the right of the enemy on the screen. Factor in the radius
            else if (Level.Player.ObjectPos.X > (ObjectPos.X + ObjectTexture.Width + Radius.X))
            {
                Move(new Vector2(MoveSpeed.X, 0), true);
            }

            // Check if the player is above the enemy on the screen. Factor in the radius
            if (Level.Player.ObjectPos.Y < (ObjectPos.Y - ObjectTexture.Height - Radius.Y))
            {
                Move(new Vector2(0, -MoveSpeed.Y), true);
            }
            // Check if the player is below the enemy on the screen. Factor in the radius
            else if (Level.Player.ObjectPos.Y > (ObjectPos.Y + ObjectTexture.Height + Radius.Y))
            {
                Move(new Vector2(0, MoveSpeed.Y), true);
            }
        }


    }
}
