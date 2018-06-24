using System;
using Game1.Engine;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects
{
	public class FlowFieldFollowingEntity : Entity
    {
  		RigidBody rb2d;
        Sprite sprite;

		float _maxForce = 50;
        float _maxSpeed = 30;

		public FlowField FlowField;

        public FlowFieldFollowingEntity()
        {
        }

		public override void Initialize()
        {
            // Adding a sprite
            sprite = AddComponent(new Sprite());
            // Settings its color to a randomized but pleasing simple color
            sprite.Color = Noise.RandomGaussianColor();
            // Adding a physics body
            rb2d = AddComponent(new RigidBody(Scene.World));
            // Generating a random mass and its square root
            int mass = Noise.Gaussian(1, 5);
            // Changing the entity scale and physics mass
            Scale = new Vector2(mass, mass);
            rb2d.Mass = mass;
            // ignore gravity forces
            rb2d.IgnoreGravity = true;

            base.Initialize();
        }

        public override void Update(float dt)
        {
            Vector2 desiredVelocity = GetNoiseFieldVelocity();
            desiredVelocity = desiredVelocity.SetMagnitude(_maxSpeed);
            rb2d.AddForce(SteeringTo(desiredVelocity));

            base.Update(dt);
        }

        private Vector2 GetNoiseFieldVelocity()
        {
            int x = (int)rb2d.Position.X % FlowField.Size, y = (int)rb2d.Position.Y % FlowField.Size;
            return FlowField.Grid[Math.Abs(x), Math.Abs(y)];
        }

        /// <summary>
        /// Returns a steering force towards a position
        /// </summary>
        public Vector2 SteeringTo(Vector2 desiredVelocity)
        {
            Vector2 steering;
            
            steering = desiredVelocity - rb2d.Velocity;
            steering = steering.Limit(_maxForce);

			return (steering);
        }
    }
}
