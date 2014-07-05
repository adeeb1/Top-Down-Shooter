using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Top_Down_Shooter
{
    // TODO: CLEAN UP THIS CLASS!!!!
    public class HUD
    {
        public Character PlayerChar;
        
        private Texture2D BackgroundTexture;
        private Texture2D HealthTexture;

        private SpriteFont HUDFont;
        
        private double HealthBarWidth;
        private double HealthBarHeight;

        private double FontSize;
        private double HUDWidth;
        private double HUDHeight;

        // TODO: NEED A REFERENCE TO Main IN BOTH THE CONSTRUCTOR AND THE UPDATE METHOD
        public HUD(Character player)
        {
            PlayerChar = player;

            BackgroundTexture = LoadAssets.ScalableBox;
            HealthTexture = LoadAssets.ScalableBox;

            // TODO: ADD SCALING TO ALL CONTROLS ON GAMEPAGE TO FACTOR IN RESOLUTION
            // TODO: ADD SCALING TO ALL CONTROLS ON GAMEPAGE TO FACTOR IN RESOLUTION

            HealthBarWidth = (100 / Main.ResolutionScaleFactor.X);
            HealthBarHeight = (10 / Main.ResolutionScaleFactor.Y);

            FontSize = (14 / Main.ResolutionScaleFactor.X);

            HUDFont = LoadAssets.MenuFont;

            HUDWidth = (384 / Main.ResolutionScaleFactor.X);
            //HUDHeight = (300 / Main.ResolutionScaleFactor.Y);
            HUDHeight = 384;
        }

        public void Update(Main main)
        {
            // TODO: DO A CALCULATION SIMILAR TO THE ResolutionScaleFactor, EXCEPT WITH THE ActualHeight OF THE HUD CANVAS
            HUDHeight = 384 / (Main.ScreenSize.Y / main.GamePage.HUD.ActualHeight);

            main.GamePage.HUDBackground.Width = HUDWidth;
            main.GamePage.HUDBackground.Height = HUDHeight;
            
            main.GamePage.OuterHealthBar.Width = HealthBarWidth;
            main.GamePage.OuterHealthBar.Height = HealthBarHeight;

            

            main.GamePage.InnerHealthBar.Width = ((float)PlayerChar.Health / (float)PlayerChar.MaxHealth) * HealthBarWidth;
            main.GamePage.InnerHealthBar.Height = HealthBarHeight;
            main.GamePage.Ammo.FontSize = FontSize;
            main.GamePage.Ammo.Text = "Ammo: " + PlayerChar.PlayerGun.AmmoLeft + " / " + PlayerChar.PlayerGun.MaxAmmo;
        }


    }
}