using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace Game1.Engine
{
    public class Scene
    {
        public Camera camera;
        protected int ChunkSize = 16;
        protected InputManager inputManager;
		protected List<Entity> gameObjects;
		public World world;

        public Scene()
        {
			camera = new Camera(Global.Game.GraphicsDevice.Viewport);
			inputManager = new InputManager();
			gameObjects = new List<Entity>();
			world = new World(Physics.GravityVec);
        }
        
        public virtual void Update(GameTime gameTime)
        {
            // Get Delta Time
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update physics
            world.Step(dt);

            // Update each entity
			for (int i = 0; i < gameObjects.Count; i++)
				gameObjects[i].Update(dt);

            //Post Update
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].PostUpdate(dt);

            // Update for scrollwheel data
            inputManager.Update(gameTime);
        }

        public Entity Instantiate(Entity entity)
        {
            gameObjects.Add(entity);
			entity.ChangeScene(this);
			entity.Initialize();
            return (entity);
        }

        public void Destroy(Entity entity)
        {
            gameObjects.Remove(entity);
			entity.OnDestroy();
        }

        public void Draw(SpriteBatch sprBatch)
        {
            // Draw each sprite
            for (int i = 0; i < gameObjects.Count; i++)
			{
                Sprite spr = gameObjects[i].GetComponent<Sprite>();
                if (spr != null)
                {
					spr.DrawSprite(sprBatch);
                }
			}
        }
    }
}
