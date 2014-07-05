using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    //The camera that follows the player; the player must always be centered unless he/she is near the edge of the camera bounds
    public sealed class Camera
    {
        //Level reference
        public BaseLevel Level;

        //Specifies if the Camera is active; when inactive (Ex. cutscene), it won't follow the player
        public bool Active;

        //The location of the camera; this is always the top-left of the screen
        public Vector2 CameraLocation;

        //The limits of the camera; it shouldn't go out of bounds
        private Rectangle CameraBounds;

        public Camera(BaseLevel level)
        {
            Level = level;

            //The camera starts out at 0
            CameraLocation = Vector2.Zero;

            //The camera bounds; the end of the Tile Engine
            CameraBounds = new Rectangle(0, 0, TileEngine[TileEngine.MaxTilesX, 0].Right, TileEngine[0, TileEngine.MaxTilesY].Bottom);
        }

        //A constructor which specifies where the camera should start looking at
        public Camera(BaseLevel level, Vector2 startingloc) : this(level)
        {
            //Keep the camera relative to the center of the screen
            CameraLocation = startingloc;
        }

        //A constructor which specifies where the camera should start looking at relative to the player's starting location
        //This constructor makes it so the camera is always centered on the player at the start (if in bounds)
        public Camera(Vector2 playerloc, BaseLevel level) : this(level, playerloc - Main.ScreenHalf)
        {

        }

        //Easy-access Player reference
        private Character Player
        {
            get { return Level.Player; }
        }

        //Easy-access TileEngine reference
        private TileEngine TileEngine
        {
            get { return Level.TileEngine; }
        }

        public Vector2 CameraOffset
        {
            get { return -CameraLocation; }
        }

        //The movement bounds before the camera will move
        private Rectangle CameraScrollRange
        {
            get 
            {
                Vector2 centerloc = Main.ScreenHalf + CameraLocation;

                return new Rectangle((int)centerloc.X - 10, (int)centerloc.Y - 10, 20, 20);
            }
        }

        //Camera speed
        private Vector2 CameraSpeed
        {
            get { return Player.MoveSpeed; }
        }

        //Check if the camera should scroll left
        private bool CheckScrollLeft()
        {
            return (Player.ObjectPos.X < CameraScrollRange.Left);
        }

        //Check if the camera should scroll right
        private bool CheckScrollRight()
        {
            return (Player.ObjectPos.X > CameraScrollRange.Right);
        }

        //Check if the camera should scroll up
        private bool CheckScrollUp()
        {
            return (Player.ObjectPos.Y < CameraScrollRange.Top);
        }

        //Check if the camera should scroll down
        private bool CheckScrollDown()
        {
            return (Player.ObjectPos.Y > CameraScrollRange.Bottom);
        }

        public void Update()
        {
            //Check for scrolling up or down
            if (CheckScrollLeft() == true)
                CameraLocation.X -= CameraSpeed.X;
            else if (CheckScrollRight() == true)
                CameraLocation.X += CameraSpeed.X;
            if (CheckScrollUp() == true)
                CameraLocation.Y -= CameraSpeed.Y;
            else if (CheckScrollDown() == true)
                CameraLocation.Y += CameraSpeed.Y;

            //Check if the camera is out of bounds in the X direction
            if (CameraLocation.X < CameraBounds.Left) CameraLocation.X = CameraBounds.Left;
            else if ((CameraLocation.X + Main.ScreenSize.X) > CameraBounds.Right) CameraLocation.X = (CameraBounds.Right - Main.ScreenSize.X);
            
            //Check if the camera is out of bounds in the Y direction
            if (CameraLocation.Y < CameraBounds.Top) CameraLocation.Y = CameraBounds.Top;
            else if ((CameraLocation.Y + Main.ScreenSize.Y) > CameraBounds.Bottom) CameraLocation.Y = (CameraBounds.Bottom - Main.ScreenSize.Y);
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //
        //}
    }
}
