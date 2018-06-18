using System;
using Game1.Engine;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects
{
	public class FlowFieldFollowingEntity : Entity
    {
  		RigidBody rb2d;
        Sprite sprite;

		float maxForce = 10;
        float maxSpeed = 10;

		public FlowField FlowField;

        public FlowFieldFollowingEntity()
        {
        }

		public override void Initialize()
        {         
            sprite = AddComponent(new Sprite());
            sprite.Color = Noise.RandomGaussianColor();

            int mass = Noise.Gaussian(1, 5);
            Scale = new Vector2(mass / 4f, mass / 4f);

            rb2d = AddComponent(new RigidBody(Scene.world));
            rb2d.Mass = mass;

            rb2d.IgnoreGravity = true;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            float gameTimeMult = (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (rb2d.Position.X < 0)
				rb2d.Position = new Vector2(FlowField.Size - 1, rb2d.Position.Y);
			if (rb2d.Position.X >= FlowField.Size)
				rb2d.Position = new Vector2(0, rb2d.Position.Y);
			if (rb2d.Position.Y < 0)
			    rb2d.Position = new Vector2(rb2d.Position.X, FlowField.Size - 1);
			if (rb2d.Position.Y >= FlowField.Size)
				rb2d.Position = new Vector2(rb2d.Position.X, 0);

            rb2d.AddForce(Seek());


            base.Update(gameTime);
        }


        public Vector2 Seek()
        {
            Vector2 desiredVelocity;
            Vector2 steering;
			int posx = (int)rb2d.Position.X, posy = (int)rb2d.Position.Y;
			
			desiredVelocity = FlowField.Grid[posx, posy];
            desiredVelocity = desiredVelocity.SetMagnitude(maxSpeed);
         
            steering = desiredVelocity - rb2d.Velocity;
            steering = steering.Limit(maxForce);
			return (steering);
        }
    }
}
