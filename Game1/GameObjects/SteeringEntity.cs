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

        public SteeringEntity() : base()
        {

        }

		public override void Initialize()
		{
			Position = new Vector2(Noise.Gaussian(-10, 10), Noise.Gaussian(-5, 0));

            sprite = AddComponent(new Sprite());
            sprite.Color = Noise.RandomGaussianColor();
            
			int mass = Noise.Gaussian(1, 5);
			Scale = new Vector2(mass / 10f, mass / 10f);

			rb2d = AddComponent(new RigidBody(Scene.world));
			rb2d.Mass = mass;

			rb2d.IgnoreGravity = true;

			base.Initialize();
		}

		public override void Update(GameTime gameTime)
        {
            float gameTimeMult = (float)gameTime.ElapsedGameTime.TotalSeconds;


			rb2d.AddForce(Seek(rb2d, Global.WorldMousePos));

            base.Update(gameTime);
        }


		public Vector2 Seek(RigidBody rigid, Vector2 target)
        {
            Vector2 desiredVelocity;
            Vector2 steering;

			desiredVelocity = target - rigid.Entity.Position;

			/*float d = desiredVelocity.Length();
            if (d < 100)
            {
                float m = MathHelp.Map(d, 0, 100, 0, rigid.MaximumVelocity);
                desiredVelocity.SetMagnitude(m);
            }
            else*/
				desiredVelocity.SetMagnitude(100);
			
            steering = desiredVelocity - rigid.Velocity;
			steering.Limit(50);
            return (steering);
        }
    }
}
