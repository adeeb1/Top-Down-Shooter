using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    //A hitbox that is attached to objects that deal damage
    public class Hitbox
    {
        //The bounding box of the hitbox
        public Rectangle Bounds;

        //The damage the hitbox deals
        public int Damage;

        //A force on the hitbox that will move the object it contacts
        public Vector2 ImpactForce;

        //The hurtboxes that this hitbox hit; this prevents it from hitting the same hurtbox again
        /*NOTE: This MAY not be needed in the majority of cases since bullets will disappear upon hitting something, but in the event that
                there's a hitbox that doesn't disappear upon contact (Ex: bullet that goes through enemies), this will be required*/
        private List<Hurtbox> Hurtboxes;

        //Constructor
        public Hitbox()
        {
            Hurtboxes = new List<Hurtbox>();
        }

        //Checks if a hitbox touches a hurtbox
        //It could not have hit the hurtbox previously
        public bool CanHitObject(Hurtbox objecthurtbox)
        {
            return (Hurtboxes.Contains(objecthurtbox) == false && Bounds.Intersects(objecthurtbox.Bounds));
        }

        //Occurs when the hitbox collides with a hurtbox; add the hurtbox to the list of hurtboxes that were hit
        public void SetHurtbox(Hurtbox objecthurtbox)
        {
            Hurtboxes.Add(objecthurtbox);
        }
    }
}
