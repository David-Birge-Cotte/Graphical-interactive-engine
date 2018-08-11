using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

using Game1.Engine;
using Game1.GameObjects;

namespace Game1
{
    /// <summary>
    /// This is the main type of the program.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Scene mainScene;

        /// <summary>
        /// Initializes the game with compile-time settings
        /// </summary>
        public Game1()
        {
			Global.Game = this;
  
            Content.RootDirectory = "Content";
            IsMouseVisible = Global.IsCursorVisible;
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1d / Global.TargetFrameRate);

            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferMultiSampling = true;
            graphics.IsFullScreen = Global.IsFullscreen;
            graphics.PreferredBackBufferWidth = Global.WinWidth;
            graphics.PreferredBackBufferHeight = Global.WinHeight;
            graphics.ApplyChanges();

            Console.WriteLine($"---------------------------------------------------------------");
            Console.WriteLine($"--- Welcome to the GIE, the graphical interactive engine ! ----");
            Console.WriteLine($"---------------------------------------------------------------");
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // Starts the game scene
            mainScene = new NeuralEntities_test01();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.ContentLoader = new ContentLoader();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Updates the game scene
            mainScene.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clears the screen to a flat color
            GraphicsDevice.Clear(Global.BackgroundColor);
            // Draws the scene to the screen
            mainScene.DrawScene(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
