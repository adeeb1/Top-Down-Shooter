using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private Character1 player;
        private Enemy1 enemy;

        // Stack of MenuScreen objects
        private Stack<MenuScreen> MenuScreens;

        // Keeps track of the current game state
        private GameState gameState;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        static Main()
        {
            activeTime = 0f;
        }

        protected override void OnDeactivated(object sender, System.EventArgs args)
        {
            // Code pausing here
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            
            player = new Character1();
            enemy = new Enemy1(LoadAssets.EnemyTestTexture, new Vector2(400, 80));

            // Create a new stack of MenuScreen objects
            MenuScreens = new Stack<MenuScreen>();
            
            // Add the Title Screen to the MenuScreen
            AddScreen(new TitleScreen());
            
            // Set the game state to indicate the player is viewing a screen
            gameState = GameState.Screen;
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadAssets.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            
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
                    player.Update();
                    enemy.Update();

                    break;
                case GameState.Paused: // Don't update any in-game objects
                    break;
            }

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
                    enemy.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    
                    break;
                case GameState.Paused: // Don't update any in-game objects
                    enemy.Draw(spriteBatch);
                    player.Draw(spriteBatch);

                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
