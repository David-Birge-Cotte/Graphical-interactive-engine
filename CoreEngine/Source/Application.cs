using System;
using System.IO;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CoreEngine.ECS;

namespace CoreEngine
{
    public class Application : Game
    {
        // References
        public static Application Instance { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static int WorldToDrawScale = 32;
        public static ContentLoader ContentLoader;
        // Scene
        public Scene scene;

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

            Window.Title = windowTitle;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            Graphics = new GraphicsDeviceManager(this);
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Lua.LuaEnvManagement.RegisterTypes();

            if (fullscreen) 
                SetFullScreen();
            else 
                SetWindowed(width, height);

            scene = new Scene(this);
        }

        protected override void Initialize()
        {
            ContentLoader = new ContentLoader();
            scene.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            scene.LoadContent();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            scene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            scene.Draw();
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
