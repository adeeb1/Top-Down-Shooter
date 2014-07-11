using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Windows.UI;
using Windows.UI.Xaml;

namespace Top_Down_Shooter
{
    // TODO: CLEAN UP THIS CLASS!!!!
    public class HUD
    {
        // Reference to GamePage.xaml for less typing
        public GamePage GamePage;

        // Reference to the player
        public Character PlayerChar;

        // Width and Height of the HUD's background rectangle
        private double HUDWidth;
        private double HUDHeight;

        // Width and Height of the Player's health bar
        private double HealthBarWidth;
        private double HealthBarHeight;

        // Font size of text
        private double FontSize;

        // The scale factor that proportionally scales the size of XAML UI elements based on the player's screen resolution
        // This allows the HUD to look the same on all machines, regardless of screen size
        private Vector2 HUDScaleFactor;

        public HUD(Main main, Character player)
        {
            // Get the reference to Main and GamePage
            GamePage = main.GamePage;

            // Get the player reference
            PlayerChar = player;

            // Get the HUD's scale factor by finding the (Defined Screen Size to HUD Canvas Size) ratio 
            HUDScaleFactor = new Vector2((float)(Main.ScreenSize.X / GamePage.HUD.ActualWidth),
                                         (float)(Main.ScreenSize.Y / GamePage.HUD.ActualHeight));
            
            // Get the Width and Height of the HUD's background rectangle
            HUDWidth = GetWidth(GamePage.HUDBackground.Width);
            HUDHeight = GetHeight(GamePage.HUDBackground.Height);

            // Get the Width and Height of the player's health bar
            HealthBarWidth = GetWidth(GamePage.OuterHealthBar.Width);
            HealthBarHeight = GetHeight(GamePage.OuterHealthBar.Height);

            // Get the Font Size of the player's remaining ammo
            FontSize = GetWidth(GamePage.ClipAmmoLeft.FontSize);

            // Apply the scaled values to the XAML UI elements
            ApplyElementDimensions();
            ApplyElementFontSizes();
        }

        public void Update()
        {
            // Update the health bar
            UpdateHealthBar();

            // Update the player's remaining ammo
            GamePage.ClipAmmoLeft.Text = PlayerChar.PlayerGun.ClipAmmo.ToString();
            GamePage.TotalAmmoLeft.Text = PlayerChar.PlayerGun.TotalAmmo.ToString();
        }

        // Gets the scaled Width of a XAML UI element
        private double GetWidth(double width)
        {
            return (width / HUDScaleFactor.X);
        }

        // Gets the scaled Height of a XAML UI element
        private double GetHeight(double height)
        {
            return (height / HUDScaleFactor.Y);
        }

        // Applies the scaled Width and Height properties to the XAML UI elements
        private void ApplyElementDimensions()
        {
            // HUD background rectangle
            GamePage.HUDBackground.Width = HUDWidth;
            GamePage.HUDBackground.Height = HUDHeight;

            // Health bar
            GamePage.OuterHealthBar.Width = GamePage.InnerHealthBar.Width = HealthBarWidth;
            GamePage.OuterHealthBar.Height = GamePage.InnerHealthBar.Height = HealthBarHeight;

            GamePage.GunImage.Width = GetWidth(GamePage.GunImage.ActualWidth);
            GamePage.GunImage.Height = GetHeight(GamePage.GunImage.ActualHeight);
        }

        // Applies the scaled FontSize property to the XAML UI elements
        private void ApplyElementFontSizes()
        {
            // Ammo Left
            GamePage.ClipAmmoLeft.FontSize = FontSize;

            // TODO: THIS NEEDS A SEPARATE VARIABLE
            // TODO: THIS NEEDS A SEPARATE VARIABLE
            GamePage.TotalAmmoLeft.FontSize = GetWidth(GamePage.TotalAmmoLeft.FontSize);
        }

        // Updates the Width of the inner health bar to indicate how much health the player has remaining
        private void UpdateHealthBar()
        {
            GamePage.InnerHealthBar.Width = ((float)PlayerChar.Health / (float)PlayerChar.MaxHealth) * HealthBarWidth;
        }

        public void Remove()
        {
            GamePage.HUD.Visibility = Visibility.Collapsed;
        }


    }
}