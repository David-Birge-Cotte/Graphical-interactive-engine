using System;

using Game1.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.GameObjects
{   
	public class SimplePhysicsEntity : Entity
    {
		RigidBody rb2d;
        Sprite sprite;

        public SimplePhysicsEntity()
        {
        }

		public override void Initialize()
        {         
            sprite = AddComponent(new Sprite());
            sprite.Color = Noise.RandomGaussianColor(true);
            
			int mass = Noise.Gaussian(1, 5);
			Scale = new Vector2(mass / 2f, mass / 2f);

            rb2d = AddComponent(new RigidBody(Scene.world));
			rb2d.Mass = mass;

            base.Initialize();
        }

		public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
         
			Scale -= new Vector2(deltaTime * 0.2f);
			if (Scale.LengthSquared() < 0.1f)
			{
				Scene.Destroy(this);
			}
				

			base.Update(gameTime);
        }
    }
}
