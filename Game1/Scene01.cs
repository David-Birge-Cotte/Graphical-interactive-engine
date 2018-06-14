using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Dynamics;

using Game1.Engine;
using Game1.GameObjects;

namespace Game1
{
    class Scene01 : Scene
    {
        private int entityNb = 20;

		public Scene01() : base()
		{
			//RandomFlies();
			camera.Zoom = 0.1f;

			Instantiate(new Entity());
			gameObjects[0].Position = new Vector2(0, 10);
			gameObjects[0].Scale = new Vector2(50, 1);
			gameObjects[0].AddComponent(new Sprite());
			gameObjects[0].AddComponent(new RigidBody(world, Shape.Rectangle, BodyType.Kinematic));
			//gameObjects[lastNb].GetComponent<RigidBody>().IgnoreGravity = true;

            
			Instantiate(new SimplePhysicsEntity());
			gameObjects[1].GetComponent<RigidBody>().Position = new Vector2(0, -5);
			Instantiate(new SimplePhysicsEntity());         
        }

        public void RandomFlies()
        {
            for (int i = 0; i < entityNb; i++)
            {
				Instantiate(new SteeringEntity());
            }
        }

        public override void Update(GameTime gameTime)
        {
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			if(InputManager.Space)
			{
				Instantiate(new SimplePhysicsEntity());
				gameObjects[gameObjects.Count - 1].GetComponent<RigidBody>().Position = Global.WorldMousePos;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Q))
				gameObjects[0].GetComponent<RigidBody>().SetAngularVelocity(-deltaTime * 10);
			else if (Keyboard.GetState().IsKeyDown(Keys.D))
				gameObjects[0].GetComponent<RigidBody>().SetAngularVelocity(deltaTime * 10);
			else
				gameObjects[0].GetComponent<RigidBody>().SetAngularVelocity(0);

			int camSpeed = 300;

			if (InputManager.Up)
				camera.Move(-Vector2.UnitY * deltaTime * camSpeed);
			if (InputManager.Down)
				camera.Move(Vector2.UnitY * deltaTime * camSpeed);
			if (InputManager.Left)
				camera.Move(-Vector2.UnitX * deltaTime * camSpeed);
			if (InputManager.Right)
				camera.Move(Vector2.UnitX * deltaTime * camSpeed);

			if (InputManager.IsScrolling)
				camera.Zoom += InputManager.ScrollValue * 0.0001f;
         
			base.Update(gameTime);
        }
    }
}