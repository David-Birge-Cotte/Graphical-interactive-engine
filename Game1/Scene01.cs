using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Game1.Engine;
using Game1.GameObjects; 

namespace Game1
{
    class Scene01 : Scene
    {
        private int entityNb = 100;
		private List<Entity> entities;
        
        public Scene01() : base()
        {
            camera = new Camera(Global.Game.GraphicsDevice.Viewport, Vector2.Zero, 0, 0.5f);
			entities = new List<Entity>();

			RandomFlies();
        }

        public void RandomFlies()
        {
            for (int i = 0; i < entityNb; i++)
            {
				entities.Add(Instantiate(new SteeringEntity()));
				entities[i].Position = new Vector2(Noise.Gaussian(-Global.WinWidth / 2, Global.WinWidth / 2) * 1.5f,
				                                   Noise.Gaussian(-Global.WinHeight / 2, Global.WinHeight / 2) * 1.5f);
				entities[i].GetComponent<RigidBody>().Mass = Noise.Gaussian(10, 60);
				float scale = entities[i].GetComponent<RigidBody>().Mass;
                entities[i].Scale = new Vector2(scale, scale);
            }
        }

        public override void Update(GameTime gameTime)
        {
            /*foreach (Entity phyEn in gameObjects)
            {
                PhysicsEntity en = phyEn as PhysicsEntity;
                if (en != null)
                    en.AddForce(Vector2.UnitX + -Vector2.UnitY * 0.25f);
            }

			if(Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				Vector2 mousePos = Camera.main.ScreenToWorld(Mouse.GetState().Position.ToVector2());
				Console.WriteLine($"MousePos = {mousePos}");

				foreach (var en in entities)
				{
					if (en.GetComponent<BoxCollider>() != null
					    && Collision.IsColliding(mousePos, en.GetComponent<BoxCollider>()))
						Console.WriteLine($"Collision with {en.ID}");
				}
			}

			float speed = 1;
			Vector2 oldPos = entities[1].Position;

			if(InputManager.Right)         
				entities[1].Position += new Vector2(speed, 0);
			if (InputManager.Left)
                entities[1].Position += new Vector2(-speed, 0);
			if (InputManager.Up)
				entities[1].Position += new Vector2(0, -speed);
			if (InputManager.Down)
				entities[1].Position += new Vector2(0, speed);

			if (Collision.IsColliding(entities[0].GetComponent<BoxCollider>(), entities[1].GetComponent<BoxCollider>()))
                entities[1].Position = oldPos;*/
            
            base.Update(gameTime);
        }
    }
}