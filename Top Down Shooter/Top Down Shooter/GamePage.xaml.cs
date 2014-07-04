using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MonoGame.Framework;
using Windows.ApplicationModel.Activation;
using Windows.System;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Microsoft.Xna.Framework;

namespace Top_Down_Shooter
{
    public sealed partial class GamePage : SwapChainBackgroundPanel
    {
        readonly Main _game;

        public GamePage(LaunchActivatedEventArgs args)
        {
            this.InitializeComponent();

            // Create the game.
            _game = XamlGame<Main>.Create(args, Window.Current.CoreWindow, this);

            // Set the GamePage of Main to the current class
            _game.GamePage = this;
        }

        private void SwapChainBackgroundPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // Create a new Title Screen
            TitleScreen screen = new TitleScreen(_game.GamePage, _game);

            // Add the Title Screen to the MenuScreens Stack
            _game.AddScreen(screen);
        }

    }
}
