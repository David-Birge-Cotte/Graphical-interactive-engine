using System;
using System.Collections.Generic;

using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Dynamics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine
{
    public class Scene
    {
        public Camera Camera;
        public World World;

        protected int chunkSize = 16;
        protected InputManager inputManager;
		protected List<Entity> gameObjects;
        protected List<UIElement> UIElements;

        /// <summary>
        /// Initializes a new scene
        /// </summary>
        public Scene()
        {
			Camera = new Camera(Global.Game.GraphicsDevice.Viewport);
			inputManager = new InputManager();
			gameObjects = new List<Entity>();
            UIElements = new List<UIElement>();
            World = new World(Physics.GravityVec);
        }
        
        /// <summary>
        /// Calls different Update functions to the physics system, the scene's entities and UI
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            // Get Delta Time
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update physics
            World.Step(dt);

            #region Update function
            // Scene entities
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Update(dt);
            // UI entities
            for (int i = 0; i < UIElements.Count; i++)
                UIElements[i].Update(dt);
            #endregion

            #region PostUpdate function
            // Scene entities
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].PostUpdate(dt);
            // UI entities
            for (int i = 0; i < UIElements.Count; i++)
                UIElements[i].PostUpdate(dt);
            #endregion

            // Update the inputmanager for scrollwheel data
            inputManager.Update(gameTime);
        }

        /// <summary>
        /// Adds an entity to the scene and initialize it
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Instantiate(Entity entity)
        {
            gameObjects.Add(entity);
			entity.ChangeScene(this);
			entity.Initialize();
            return (entity);
        }

        /// <summary>
        /// Adds an UI element to the scene and initialize it
        /// </summary>
        /// <param name="UIElement"></param>
        /// <returns></returns>
        public UIElement Instantiate(UIElement UIElement)
        {
            UIElements.Add(UIElement);
            UIElement.ChangeScene(this);
            UIElement.Initialize();
            return (UIElement);
        }

        /// <summary>
        /// Removes an entity from the scene
        /// </summary>
        /// <param name="entity"></param>
        public void Destroy(Entity entity)
        {
            if (!gameObjects.Contains(entity))
                throw new Exception($"Entity {entity.Name} is not in the gameObjects list<Entity>");
            gameObjects.Remove(entity);
			entity.OnDestroy();
        }

        public void Destroy(UIElement UIElement)
        {
            if (!gameObjects.Contains(UIElement))
                throw new Exception($"UIElement {UIElement.Name} is not in the UIElement list<UIElement>");
            gameObjects.Remove(UIElement);
            UIElement.OnDestroy();
        }

        /// <summary>
        /// Draws the scene to a sprite batch
        /// </summary>
        /// <param name="sprBatch"></param>
        public void DrawScene(SpriteBatch sprBatch)
        {
            for (int i = 0; i < gameObjects.Count; i++)
			{
                Sprite spr = gameObjects[i].GetComponent<Sprite>();
                if (spr != null)
					spr.DrawSprite(sprBatch);
			}
        }

        /// <summary>
        /// Draws the UI to a sprite batch
        /// </summary>
        /// <param name="sprBatch"></param>
        public void DrawUI(SpriteBatch sprBatch)
        {
            for (int i = 0; i < UIElements.Count; i++)
            {
                Sprite spr = UIElements[i].GetComponent<Sprite>();
                if (spr != null)
                    spr.DrawSprite(sprBatch);
            }
        }
    }
}
