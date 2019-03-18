using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using CoreEngine.ECS;
using CoreEngine.Lua;

namespace CoreEngine
{
    public class Application : Game
    {
        // References
        public static Application Instance { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static int PixelsPerUnit = 16;
        public static ContentLoader ContentLoader;
        public ApplicationAPI API;

        public Input Input;
        //public Scene Scene;
        public List<Scene> Scenes = new List<Scene>();

        // Content
        public static string ContentDirectory 
        {
            get 
            {
                var assemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                return Path.Combine(assemblyDirectory, Instance.Content.RootDirectory); 
            }
        }

        public Application(string windowTitle, int width, int height, bool fullscreen = false)
        {
            Instance = this;

            Graphics = new GraphicsDeviceManager(this);
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Input = new Input();
            Content.RootDirectory = "Content";
            LuaEnvManagement.RegisterTypes();
            Window.Title = windowTitle;
            IsMouseVisible = true;
            API = new ApplicationAPI(this);

            if (fullscreen) 
                SetFullScreen();
            else 
                SetWindowed(width, height);

            Scene.LaunchScene("default-scene");
        }

        protected override void Initialize()
        {
            ContentLoader = new ContentLoader();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Scenes.Count; i++)
                Scenes[i].Update(gameTime);

            // default global quit button
            if (Input.IsKeyPressed(Keys.Escape))
                Exit();

            Input.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Application.Instance.GraphicsDevice.Clear(Color.TransparentBlack);
            for (int i = 0; i < Scenes.Count; i++)
                Scenes[i].Draw();
            base.Draw(gameTime);
        }

        public static void SetWindowed(int width, int height)
        {
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();
        }

        public static void SetFullScreen()
        {
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.IsFullScreen = true;         
            Graphics.ApplyChanges();
        }
    }
}
