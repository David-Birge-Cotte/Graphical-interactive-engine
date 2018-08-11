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
        private FlowFieldManager flowFieldM;
		private int _camSpeed = 300;

        private Entity en;

		public Scene01() : base()
		{
            flowFieldM = new FlowFieldManager();
            Camera.Zoom = 0.1f;

            InstantiateEntities();

            //en = Instantiate(new Entity());
            //en.AddComponent(new RigidBody(World, BodyShape.Rectangle)).IgnoreGravity = true; 
            //en.AddComponent(new Sprite(Global.ContentLoader.Images[0].Texture2D));
            Console.WriteLine();
            Global.ContentLoader.Images[0].PrintInfos();

        }

        private void InstantiateEntities()
        {
            for (int x = 0; x < 500; x++)
            {
                FlowFieldFollowingEntity entity = Instantiate(new FlowFieldFollowingEntity()) as FlowFieldFollowingEntity;
                entity.Position = new Vector2(Noise.Gaussian(-200, 200), Noise.Gaussian(-200, 200));
                entity.FlowField = flowFieldM.GetFlowFromPoint((int)entity.Position.X, (int)entity.Position.Y);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Quick Quit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Global.Game.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                en.GetComponent<Sprite>().ChangeImage(null);

            // TODO do a camera controller
            CameraMovement((float)gameTime.ElapsedGameTime.TotalSeconds);

            flowFieldM.Time = (float)(int)gameTime.TotalGameTime.TotalSeconds;
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
				Camera.Move(-Vector2.UnitY * deltaTime * _camSpeed);
            if (InputManager.Down)
				Camera.Move(Vector2.UnitY * deltaTime * _camSpeed);
            if (InputManager.Left)
				Camera.Move(-Vector2.UnitX * deltaTime * _camSpeed);
            if (InputManager.Right)
				Camera.Move(Vector2.UnitX * deltaTime * _camSpeed);

            if (InputManager.IsScrolling)
                Camera.Zoom += InputManager.ScrollValue * 0.0001f * Camera.Zoom;
		}
    }
}