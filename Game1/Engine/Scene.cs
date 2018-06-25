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
        protected UIManager UIManager;

		/// <summary>
		/// Initializes a new scene
		/// </summary>
		public Scene()
		{
			Camera = new Camera(Global.Game.GraphicsDevice.Viewport);
			inputManager = new InputManager();
			gameObjects = new List<Entity>();
            UIManager = new UIManager(this);
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
            UIManager.Update(dt);
            #endregion

            #region PostUpdate function
            // Scene entities
            for (int i = 0; i < gameObjects.Count; i++)
				gameObjects[i].PostUpdate(dt);
            UIManager.PostUpdate(dt);
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

		/// <summary>
		/// Draw the scene to the screen. Called by Game
		/// </summary>
		/// <param name="sprBatch"></param>
		public void DrawScene(SpriteBatch sprBatch)
		{
            // Draw Game Objects in scene
            sprBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend, null, null, null, null,
                Camera.main.GetTransformation());
            DrawEntities(sprBatch); // Draw the scene
            sprBatch.End();

            // Draw UI on top
            sprBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend, null, null, null, null, null);
            UIManager.Draw(sprBatch); // Draw the UI
            sprBatch.End();
		}

        private void DrawEntities(SpriteBatch sprBatch)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                Sprite spr = gameObjects[i].GetComponent<Sprite>();
                if (spr != null)
                    spr.DrawSprite(sprBatch);
            }
        }
	}
}
