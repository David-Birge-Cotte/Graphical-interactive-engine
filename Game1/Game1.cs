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
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Scene mainScene; // This is where the game takes place

        /// <summary>
        /// Initializes the game with compile-time settings
        /// </summary>
        public Game1()
        {
			Global.Game = this;
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = Global.IsCursorVisible;
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1d / Global.TargetFrameRate);

			graphics.GraphicsProfile = GraphicsProfile.HiDef; 
            graphics.PreferMultiSampling = true;
			graphics.IsFullScreen = Global.IsFullscreen;
			graphics.PreferredBackBufferWidth = Global.WinWidth;
            graphics.PreferredBackBufferHeight = Global.WinHeight;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Starts the game scene
            mainScene = new Scene01();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // have a loader for loading textures corresponding to the required scene etc..

            // This is just a test to read a file
            /*
            string path = Global.AppPath;
            if (String.Compare(path.Substring(path.Length - 1, 1), "\\") != 0)
                path = path + "\\";
            string fileText = File.ReadAllText(path + "data\\test.txt");
            Console.WriteLine(fileText);
            */
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
