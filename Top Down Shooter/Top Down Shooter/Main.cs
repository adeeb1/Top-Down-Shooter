using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Top_Down_Shooter
{
    // Global enum to represent the game state
    public enum GameState : byte { Screen, InGame, Paused }

    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Time the game is active
        public static float activeTime;

        //Level reference
        private BaseLevel Level;

        // Stack of MenuScreen objects
        private Stack<MenuScreen> MenuScreens;

        // Keeps track of the current game state
        private GameState gameState;

        // Screen size of the game
        public static readonly Vector2 ScreenSize;

        // Half the screen size of the game
        public static readonly Vector2 ScreenHalf;

        // The scale factor that converts actual screen coordinates to game screen coordinates
        public static Vector2 ResolutionScaleFactor;
        
        public Main()
        {   
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            Content.RootDirectory = "Content";

            // Get the resolution scale factor
            ResolutionScaleFactor = new Vector2((ScreenSize.X / Window.ClientBounds.Width), (ScreenSize.Y / Window.ClientBounds.Height));

            // Show the mouse on the game screen
            IsMouseVisible = true;

            // Enable the tap gesture
            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        static Main()
        {
            activeTime = 0f;

            // Get the screen size
            ScreenSize = new Vector2(640, 384);

            // Get half the screen size
            ScreenHalf = (ScreenSize / 2);
        }

        protected override void OnDeactivated(object sender, System.EventArgs args)
        {
            // Code pausing here
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            
            Level = new BaseLevel();
            Level.AddObject(new Character1());
            Level.AddObject(new Enemy1(LoadAssets.EnemyTestTexture, new Vector2(400, 80)));

            // Create a new stack of MenuScreen objects
            MenuScreens = new Stack<MenuScreen>();
            
            // Add the Title Screen to the MenuScreen
            AddScreen(new TitleScreen());
            
            // Set the game state to indicate the player is viewing a screen
            gameState = GameState.Screen;

            // Handle the ClientSizeChanged event for the game window
            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadAssets.LoadContent(Content);
            SoundManager.PlaySong(LoadAssets.TestSong);
        }

        protected override void UnloadContent()
        {
            
        }

        public GameState GetGameState
        {
            get { return gameState; }
        }

        public void AddScreen(MenuScreen screen)
        {
            MenuScreens.Push(screen);
        }

        public MenuScreen GetCurrentScreen()
        {
            // Return the next screen if one exists; otherwise, return null to indicate that there are no screens left
            return ((MenuScreens.Count > 0) ? MenuScreens.Peek() : null);
        }

        public void RemoveScreen()
        {
            // Check if another screen exists
            if (MenuScreens.Count > 0)
            {
                // Remove the next screen
                MenuScreens.Pop();

                // Check if the current screen exists
                if (GetCurrentScreen() != null)
                {
                    // Reset the input of the current screen
                    MenuScreens.Peek().ResetInput();
                }
            }
        }

        public void ChangeGameState(GameState state)
        {
            // Set the game state to the specified state
            gameState = state;
        }

        protected override void Update(GameTime gameTime)
        {
            //Update active time
            activeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check which game state the player is in
            switch (gameState)
            {
                case GameState.Screen: // Update the current screen
                    GetCurrentScreen().Update(this);

                    break;
                case GameState.InGame: // Update the in-game objects
                    Level.Update(this);

                    //player.Update();
                    //enemy.Update();
                    //player.PlayerGun.Update();
                    //
                    //for (int i = 0; i < player.PlayerGun.GunProjectiles.Count; i++)
                    //{
                    //    player.PlayerGun.GunProjectiles[i].Update();
                    //}

                    break;
                case GameState.Paused: // Don't update any in-game objects
                    //TEMPORARY
                    Level.Update(this);
                    break;
            }

            Debug.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null);

            // Check which game state the player is in
            switch (gameState)
            {
                case GameState.Screen: // Draw the current screen
                    GetCurrentScreen().Draw(spriteBatch);
                    
                    break;
                case GameState.InGame: // Draw the in-game objects
                    Level.Draw(spriteBatch);
                    //enemy.Draw(spriteBatch);
                    //player.Draw(spriteBatch);
                    //player.PlayerGun.Draw(spriteBatch);
                    //
                    //for (int i = 0; i < player.PlayerGun.GunProjectiles.Count; i++)
                    //{
                    //    player.PlayerGun.GunProjectiles[i].Draw(spriteBatch);
                    //}

                    break;
                case GameState.Paused: // Don't update any in-game objects
                    Level.Draw(spriteBatch);
                    //enemy.Draw(spriteBatch);
                    //player.Draw(spriteBatch);
                    //player.PlayerGun.Draw(spriteBatch);
                    //
                    //for (int i = 0; i < player.PlayerGun.GunProjectiles.Count; i++)
                    //{
                    //    player.PlayerGun.GunProjectiles[i].Draw(spriteBatch);
                    //}

                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            // Readjust the resolution scale factor based on the new screen resolution/size

            // TODO: This is not perfect. It doesn't properly take into account the snapping of the game
            // TODO: This is not perfect. It doesn't properly take into account the snapping of the game
            // TODO: This is not perfect. It doesn't properly take into account the snapping of the game
            ResolutionScaleFactor = new Vector2((ScreenSize.X / Window.ClientBounds.Width), (ScreenSize.Y / Window.ClientBounds.Height));
        }


    }
}