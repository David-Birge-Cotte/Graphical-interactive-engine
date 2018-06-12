using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine
{
    public class Scene
    {
        public Camera camera;
        protected InputManager inputManager;
		protected List<Entity> gameObjects;

        public Scene()
        {
			inputManager = new InputManager();
			gameObjects = new List<Entity>();
        }
        
        public virtual void Update(GameTime gameTime)
        {         
            // Update each entity
			for (int i = 0; i < gameObjects.Count; i++)
				gameObjects[i].Update(gameTime);

            // Update for scrollwheel data
			inputManager.Update(gameTime);
        }

        public Entity Instantiate(Entity entity)
        {
            gameObjects.Add(entity);
            return (entity);
        }

        public void Destroy(Entity entity)
        {
            gameObjects.Remove(entity);
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
