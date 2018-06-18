using System;
using System.Collections.Generic;
using Game1.Engine;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects
{
	class SteeringEntity : Entity
    {
		RigidBody rb2d;
		Sprite sprite;
        
		float maxForce = 50;
		float maxSpeed = 1000;

        public SteeringEntity() : base()
        {

        }

		public override void Initialize()
		{
			Position = new Vector2(Noise.Gaussian(-10, 10), Noise.Gaussian(-5, 0));

            sprite = AddComponent(new Sprite());
            sprite.Color = Noise.RandomGaussianColor();
            
			int mass = Noise.Gaussian(1, 5);
			Scale = new Vector2(mass / 2f, mass / 2f);

			rb2d = AddComponent(new RigidBody(Scene.world));
			rb2d.Mass = mass;

			rb2d.IgnoreGravity = true;

			base.Initialize();
		}

		public override void Update(GameTime gameTime)
        {
            float gameTimeMult = (float)gameTime.ElapsedGameTime.TotalSeconds;


			rb2d.AddForce(Seek(Global.WorldMousePos));

            base.Update(gameTime);
        }


		public Vector2 Seek(Vector2 target)
        {
            Vector2 desiredVelocity;
            Vector2 steering;

			desiredVelocity = target - rb2d.Position;
			desiredVelocity = desiredVelocity.SetMagnitude(maxSpeed);

			steering = desiredVelocity - rb2d.Velocity;
			steering = steering.Limit(maxForce);
            return (steering);
        }
    }
}
