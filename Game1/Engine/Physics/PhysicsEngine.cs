using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine.Physics
{
    public static class PhysicsEngine
    {
        public static float Gravity = 9.81f;
        public static Vector2 GravityVec = new Vector2(0, Gravity);

        public static Vector2 GetGravityAttraction(PhysicsEntity entity, PhysicsEntity attractor)
        { 
            float strength;
            Vector2 force = attractor.Position - entity.Position;
            float d = force.LengthSquared();

            if (d < 10)
                d = 10;

            if (entity.Equals(attractor))
                return (Vector2.Zero);
            force.Normalize();
            strength = (Gravity * entity.Mass * attractor.Mass) / d;

            return (force * strength);
        }
    }
}
