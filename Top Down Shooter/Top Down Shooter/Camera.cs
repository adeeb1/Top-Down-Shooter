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

        //The location of the camera
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

        private Vector2 CameraScrollRange
        {
            get { return (Main.ScreenHalf + CameraLocation); }
        }

        private float HorizontalDifference
        {
            get { return Player.ObjectPos.X - CameraLocation.X; }
        }

        private float VerticalDifference
        {
            get { return Player.ObjectPos.Y - CameraLocation.Y; }
        }

        //Check if the camera should scroll left
        private bool CheckScrollLeft()
        {
            return (Player.ObjectPos.X < CameraScrollRange.X);
        }

        //Check if the camera should scroll right
        private bool CheckScrollRight()
        {
            return (Player.ObjectPos.X > CameraScrollRange.X);
        }

        //Check if the camera should scroll up
        private bool CheckScrollUp()
        {
            return (Player.ObjectPos.Y < CameraScrollRange.Y);
        }

        //Check if the camera should scroll down
        private bool CheckScrollDown()
        {
            return (Player.ObjectPos.Y > CameraScrollRange.Y);
        }

        public void Update()
        {
            //Check for scrolling up or down
            if (CheckScrollLeft() == true) CameraLocation.X += HorizontalDifference;
            else if (CheckScrollRight() == true) CameraLocation.X -= HorizontalDifference;
            if (CheckScrollUp() == true) CameraLocation.Y += VerticalDifference;
            else if (CheckScrollDown() == true) CameraLocation.Y -= VerticalDifference;

            //Check if the camera is out of bounds
            //if (CameraLocation.X < CameraBounds.Left) CameraLocation.X += (CameraBounds.Left - CameraLocation.X);
            //else if (CameraLocation.X > CameraBounds.Right) CameraLocation.X -= (CameraLocation.X - CameraBounds.Right);
            //else if (CameraLocation.Y < CameraBounds.Top) CameraLocation.Y = CameraBounds.Top;
            //else if (CameraLocation.Y > CameraBounds.Bottom) CameraLocation.Y = CameraBounds.Bottom;
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //
        //}
    }
}
