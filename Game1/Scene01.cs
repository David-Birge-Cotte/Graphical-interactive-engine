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
		private int _entityNb = 1;
		private FlowField _flowField;
		private int _camSpeed = 300;

		public Scene01() : base()
		{
			_flowField = new FlowField(100, 0.1f);

			camera.Zoom = 0.1f;

			for (int x = 0; x < _flowField.Size * 10; x++)
			{
				FlowFieldFollowingEntity entity = Instantiate(new FlowFieldFollowingEntity() { FlowField = _flowField }) as FlowFieldFollowingEntity;
				entity.Position = new Vector2(Noise.Gaussian(0, _flowField.Size), Noise.Gaussian(0, _flowField.Size));
			}


			//camera.Position = new Vector2(_flowField.Size / 2f, _flowField.Size / 2f);

			/*
			Instantiate(new Entity());
			gameObjects[0].Position = new Vector2(0, 10);
			gameObjects[0].Scale = new Vector2(50, 1);
			gameObjects[0].AddComponent(new Sprite());
			gameObjects[0].AddComponent(new RigidBody(world, BodyShape.Rectangle, BodyType.Kinematic));
			*/

			/*
			Instantiate(new Entity());
            gameObjects[1].Position = new Vector2(25, 10);
            gameObjects[1].Scale = new Vector2(50, 1);
			gameObjects[1].Rotation = MathHelper.ToRadians(-45);
            gameObjects[1].AddComponent(new Sprite());
            gameObjects[1].AddComponent(new RigidBody(world, BodyShape.Rectangle, BodyType.Kinematic));

			Instantiate(new Entity());
            gameObjects[2].Position = new Vector2(-25, 10);
            gameObjects[2].Scale = new Vector2(50, 1);
            gameObjects[2].Rotation = MathHelper.ToRadians(45);
            gameObjects[2].AddComponent(new Sprite());
            gameObjects[2].AddComponent(new RigidBody(world, BodyShape.Rectangle, BodyType.Kinematic));
            */

			//SpawnRandomFlies();
        }

        

        public override void Update(GameTime gameTime)
        {
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			_flowField.GeneratePerlinFlowField((float)gameTime.TotalGameTime.TotalSeconds);

			if(InputManager.Space)
			{
				Instantiate(new FlowFieldFollowingEntity() { FlowField = _flowField });
				gameObjects[gameObjects.Count - 1].Position = Global.WorldMousePos;
			}

			if(Keyboard.GetState().IsKeyDown(Keys.R))
				camera.Position = new Vector2(_flowField.Size, _flowField.Size);

			CameraMovement(deltaTime);
         
			base.Update(gameTime);
        }

		private void CameraMovement(float deltaTime)
		{
			if (InputManager.Up)
				camera.Move(-Vector2.UnitY * deltaTime * _camSpeed);
            if (InputManager.Down)
				camera.Move(Vector2.UnitY * deltaTime * _camSpeed);
            if (InputManager.Left)
				camera.Move(-Vector2.UnitX * deltaTime * _camSpeed);
            if (InputManager.Right)
				camera.Move(Vector2.UnitX * deltaTime * _camSpeed);

            if (InputManager.IsScrolling)
                camera.Zoom += InputManager.ScrollValue * 0.0001f * camera.Zoom;
		}

		private void SpawnRandomFlies()
        {
            for (int i = 0; i < _entityNb; i++)
            {
                Instantiate(new SteeringEntity());
            }
        }
    }
}