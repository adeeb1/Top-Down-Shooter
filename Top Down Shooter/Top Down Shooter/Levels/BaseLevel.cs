﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Top_Down_Shooter
{
    //The base class for a level; it contains all the level objects
    //This class may be instantiated instead of used purely as a base class
    public class BaseLevel
    {
        private KeyboardState LevelKeyboard;
        public List<LevelObject> levelObjects;

        public BaseLevel()
        {
            levelObjects = new List<LevelObject>();

            LevelKeyboard = new KeyboardState(Keys.Enter);
        }

        public void AddObject(LevelObject levelobject)
        {
            if (levelobject != null)
            {
                levelObjects.Add(levelobject);

                levelObjects[levelObjects.Count - 1].Level = this;
            }
        }

        private void HitboxCollision(int index)
        {
            //Check for collisions
            for (int i = 0; i < levelObjects.Count; i++)
            {
                //An object can't hit itself, so don't bother checking for that
                if (i != index)
                {
                    //If an object has a hitbox and touched the hurtbox of the object we're checking for, make that object take damage
                    if (levelObjects[i].hitbox != null && levelObjects[i].hitbox.CanHitObject(levelObjects[index].hurtbox) == true)
                    {
                        levelObjects[index].TakeDamage(levelObjects[i].hitbox);
                        
                        //If the object gained invincibility after taking damage, break out of the loop since there's no way it can get hurt now
                        if (levelObjects[index].hurtbox.IsInvincible == true) break;
                    }
                }
            }
        }

        public void Update(Main main)
        {
            //Check for pausing/unpausing the game
            if (Input.IsKeyDown(LevelKeyboard, Keys.Enter) == true)
            {
                if (main.GetGameState == GameState.InGame)
                    main.ChangeGameState(GameState.Paused);
                else main.ChangeGameState(GameState.InGame);
            }

            if (main.GetGameState == GameState.InGame)
            {
                //Update all level objects
                for (int i = 0; i < levelObjects.Count; i++)
                {
                    //Check if the object has a hurtbox and can get hurt
                    if (levelObjects[i].hurtbox != null && levelObjects[i].hurtbox.CanTakeDamage() == true)
                    {
                        //Check if any object's hitbox hit this one's hurtbox and take any necessary actions
                        HitboxCollision(i);
                    }

                    //Check if the object is dead and remove it if so
                    if (levelObjects[i].IsDead == true)
                    {
                        levelObjects.RemoveAt(i);
                        i--;
                    }
                    //Otherwise update the object
                    else
                    {
                        //Update the object
                        levelObjects[i].Update();
                    }
                }
            }

            LevelKeyboard = Keyboard.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < levelObjects.Count; i++)
                levelObjects[i].Draw(spriteBatch);
        }
    }
}