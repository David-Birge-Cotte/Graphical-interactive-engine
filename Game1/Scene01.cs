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
        private FlowFieldManager flowFieldM;
		private int _camSpeed = 300;

		public Scene01() : base()
		{
            flowFieldM = new FlowFieldManager();

            camera.Zoom = 0.1f;

            InstantiateEntities();
        }

        private void InstantiateEntities()
        {
            for (int x = 0; x < 200; x++)
            {
                FlowFieldFollowingEntity entity = Instantiate(new FlowFieldFollowingEntity()) as FlowFieldFollowingEntity;
                entity.Position = new Vector2(Noise.Gaussian(-200, 200), Noise.Gaussian(-200, 200));
                entity.FlowField = flowFieldM.GetFlowFromPoint((int)entity.Position.X, (int)entity.Position.Y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // TODO do a camera controller
			CameraMovement((float)gameTime.ElapsedGameTime.TotalSeconds);

            flowFieldM.Time = (float)gameTime.TotalGameTime.TotalSeconds;

            foreach (var en in gameObjects)
            {
                if (!(en is FlowFieldFollowingEntity))
                    continue;

                FlowFieldFollowingEntity ffen = en as FlowFieldFollowingEntity;
                ffen.FlowField = flowFieldM.GetFlowFromPoint((int)en.Position.X, (int)en.Position.Y);
            }

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
    }
}