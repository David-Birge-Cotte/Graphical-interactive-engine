using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CoreEngine.Lua;

namespace CoreEngine.ECS
{
    public class Scene
    {
        private Application app;
        private SpriteBatch spriteBatch;
        public Color ClearColor;
        public Entity RootEntity { get; private set; }
        public Camera ActiveCamera { get; set; }
        public List<Sprite> Sprites = new List<Sprite>();
        public SceneAPI API;

        public Scene(Application app)
        {
            this.app = app;
            ClearColor = Noise.RandomGaussianColor();
            API = new SceneAPI(this);
        }

        public void Initialize()
        {
            RootEntity = new Entity("root");

            string sceneScript = FileLoader.LoadScene("my-scene");
            RootEntity.AddComponent(new Scriptable(sceneScript));
 
            // Put a default camera on the root entity
            var defaultCam = RootEntity.AddComponent(new Camera());
            ActiveCamera = defaultCam;
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(app.GraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            // Get Delta Time
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update
            RootEntity.Update(dt);
            RootEntity.PostUpdate(dt);

            // default quit button
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                app.Exit();
        }

        public void Draw()
        {
            app.GraphicsDevice.Clear(ClearColor);

            if (ActiveCamera == null)
                return;

            // Draw Game Objects in scene
            spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend, null, null, null, null,
                ActiveCamera.GetTransformation());
            DrawEntities(); // Draw the scene
            spriteBatch.End();
        }

        private void DrawEntities()
        {
            for (int i = 0; i < Sprites.Count; i++)
                Sprites[i].DrawSprite(spriteBatch);
        }
    }
}