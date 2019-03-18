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
        private SpriteBatch spriteBatch;
        public string Name;
        public Entity RootEntity { get; private set; }
        public Camera ActiveCamera { get; set; }
        public List<Sprite> Sprites = new List<Sprite>();
        public bool IsActive = true;
        public SceneAPI API;

        public static Scene LaunchScene(string sceneName = "default-scene")
        {
            var scene = new Scene();

            scene.Name = sceneName;
            Application.Instance.Scenes.Add(scene);
            scene.Initialize();
            scene.LoadContent();
            return (scene);
        }

        public Scene()
        {
            API = new SceneAPI(this);
        }

        public void Initialize()
        {
            RootEntity = new Entity("root");
            RootEntity.Scene = this;

            // Put a default camera on the root entity
            ActiveCamera = RootEntity.AddComponent(new Camera());

            string sceneScript = FileLoader.LoadScene(Name);
            RootEntity.AddComponent(new Scriptable(sceneScript)); // Load scene script
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(Application.Instance.GraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            if(!IsActive)
                return;

            // Get Delta Time
			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update
            RootEntity.Update(dt);
            RootEntity.PostUpdate(dt);
        }

        public void Draw()
        {
            // Do not render if there is no camera
            if (ActiveCamera == null)
                return;

            // Draw Game Objects in scene
            spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null, //SamplerState.PointClamp, // filtering type for pixel art or smooth HD
                null, null, null, // default values
                ActiveCamera.GetTransformation()); // draw from camera point of view
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