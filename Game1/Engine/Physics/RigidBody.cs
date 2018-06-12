using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public class RigidBody : Component
    {
		public Vector2 Velocity = Vector2.Zero;
        public Vector2 Acceleration = Vector2.Zero;
        public float AVelocity = 0;
        public float AAcceleration = 0;
        public float Mass = 1;
      
		public float MaximumVelocity = 50;
		public float MaximumForce = 100;
		public float MaximumAVelocity = 100;

		public float Density = 1;
		public float SurfaceArea = 1;
        
        public RigidBody()
        {
        }

		public override void Update()
		{
			if (Acceleration.LengthSquared() > MaximumForce)
				Acceleration = MathHelp.SetMagnitude(Acceleration, (float)Math.Sqrt(MaximumForce));
            Velocity += Acceleration;
			if (Velocity.LengthSquared() > MaximumVelocity)
				Velocity = MathHelp.SetMagnitude(Velocity, (float)Math.Sqrt(MaximumVelocity));
            AVelocity += AAcceleration;
            if (AVelocity > MaximumAVelocity)
                AVelocity = MaximumAVelocity;
            
   			Entity.Position += Velocity;
			Entity.Rotation += AVelocity;

			Acceleration = Vector2.Zero;
			AAcceleration = 0;

			base.Update();
		}

		/// <summary>
        /// Adds an acceleration force for the next Physics Update
        /// </summary>
        /// <param name="force"></param>
        public void AddForce(Vector2 force)
        {
            Acceleration += force / Mass;
        }
	}
}
