using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine
{
	public static class Physics
	{
		public static float Gravity = 9.81f;
		public static Vector2 GravityVec { get { return new Vector2(0, Gravity); } }

		#region Forces      
		public static Vector2 GetGravity(RigidBody rigid)
        {
            return (GravityVec * rigid.Mass * 0.1f);
        }

		public static Vector2 Attract(RigidBody rb2d, RigidBody other)
        {
            if (rb2d.Equals(other))
                return (Vector2.Zero);

            float strength;
            Vector2 force = other.Entity.Position - rb2d.Entity.Position;
            float d = force.LengthSquared();

            d = MathHelp.Limit(d, 0, 10);
            force.Normalize();
            strength = (Physics.Gravity * rb2d.Mass * other.Mass) / d;

            return (force * strength);
        }

		public static Vector2 GetGravityAttraction(RigidBody rigid, RigidBody attractor)
        {
            if (rigid.Equals(attractor))
                return (Vector2.Zero);

            float strength;
            Vector2 force = attractor.Entity.Position - rigid.Entity.Position;
            float d = force.LengthSquared();

            if (d < 10) // TODO find a cleaner solution
                d = 10;

            force.Normalize();
            strength = (Gravity * rigid.Mass * attractor.Mass) / d;

            return (force * strength);
        }
      
		public static Vector2 GetFriction(RigidBody rigid, float c = 0.1f)
		{
			Vector2 friction;

			if (rigid.Velocity == Vector2.Zero)
				return (Vector2.Zero);

			friction = rigid.Velocity;
			friction.Normalize();
			friction *= -c;
			friction = MathHelp.Limit(friction, rigid.Velocity.Length());
			return (friction);
		}      

		public static Vector2 GetDrag(RigidBody rigid, float c = 0.02f)
		{
			Vector2 drag;

			if (rigid.Velocity == Vector2.Zero)
				return (Vector2.Zero);

			drag = -0.5f *
					 /*rigid.Density*/ 1 *
					 rigid.Velocity.LengthSquared() *
					 /*rigid.SurfaceArea*/ 1 *
					 c *
					 MathHelp.Normalized(rigid.Velocity);
			return (drag);
		}
        #endregion
	}
}
