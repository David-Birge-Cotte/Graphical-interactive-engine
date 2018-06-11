using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game1.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Physics
{
    public class PhysicsEntity : Entity
    {
        public Vector2 Velocity = Vector2.Zero;
        public Vector2 Acceleration = Vector2.Zero;
        public float AVelocity = 0;
        public float AAcceleration = 0;
        public float Mass = 1;

        public float Density = 1;
        public float SurfaceArea = 1;

        protected float maximumVelocity = 50;
        protected float maximumForce = 100;
        protected float maximumAVelocity = 100;

        public PhysicsEntity() : base()
        {
            AddComponent<Sprite>(new Sprite(8));
            GetComponent<Sprite>().Color = 
                Global.Noiser.RandomGaussianColor();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdatePhysics();
        }

        public virtual void UpdatePhysics()
        {
            if (Acceleration.LengthSquared() > maximumForce)
                Acceleration = MathHelp.Normalized(Acceleration) * maximumForce;
            Velocity += Acceleration;
            Velocity = MathHelp.Limit(Velocity, 
                Vector2.Zero, Vector2.One * maximumVelocity);
            Position += Velocity;
            Acceleration = Vector2.Zero;
            AVelocity += AAcceleration;
            if (AVelocity > maximumAVelocity)
                AVelocity = maximumAVelocity;
            Rotation += AVelocity;
            AAcceleration = 0;
        }

        /// <summary>
        /// Adds an acceleration force for the next Physics Update
        /// </summary>
        /// <param name="force"></param>
        public void AddForce(Vector2 force)
        {
            Acceleration += force / Mass;
        }

        /// <summary>
        /// Steering Behaviour by Craig Reynolds
        /// Steering = DesiredVelocity - Velocity
        /// http://www.red3d.com/cwr/steer/
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Vector2 Seek(Vector2 target)
        {
            Vector2 desiredVelocity;
            Vector2 steering;

            desiredVelocity = Global.WorldMousePos - Position;
            desiredVelocity = MathHelp.SetMagnitude(desiredVelocity, maximumVelocity);
            steering = desiredVelocity - Velocity;
            if (steering.Length() > maximumForce)
                steering = MathHelp.SetMagnitude(steering, maximumForce);
            return (steering);
        }

        public Vector2 Attract(PhysicsEntity other)
        {
            if (this.Equals(other))
                return (Vector2.Zero);

            float strength;
            Vector2 force = other.Position - Position;
            float d = force.LengthSquared();

            d = MathHelp.Limit(d, 0, 10);
            force.Normalize();
            strength = (PhysicsEngine.Gravity * Mass * other.Mass) / d;

            return (force * strength);
        }

        public Vector2 GetFriction(float c = 0.1f)
        {
            Vector2 friction;

            if (Velocity == Vector2.Zero)
                return (Vector2.Zero);

            friction = Velocity;
            friction.Normalize();
            friction *= -c;
            friction = MathHelp.Limit(friction, Vector2.Zero, Velocity);
            return (friction);
        }

        public Vector2 GetGravity()
        {
            return (PhysicsEngine.GravityVec * Mass * 0.1f);
        }

        public Vector2 GetDrag(float c = 0.02f)
        {
            Vector2 drag;

            if (Velocity == Vector2.Zero)
                return (Vector2.Zero);

            drag =
                -0.5f *
                Density *
                Velocity.LengthSquared() *
                SurfaceArea *
                c *
                MathHelp.Normalized(Velocity);

            return (drag);
        }
    }
}
