using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game1.Engine;
using Game1.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.GameObjects
{
    public class TestPhysics : PhysicsEntity
    {
        public float AccelerationDrag = 1;

        public TestPhysics() : base ()
        {
            Position = new Vector2(Global.Noiser.Gaussian(-Global.WinWidth / 2, Global.WinWidth / 2) * 1.5f,
                Global.Noiser.Gaussian(-Global.WinHeight / 2, Global.WinHeight / 2) * 1.5f);

            Mass = Global.Noiser.Gaussian(10, 60);
            float scale = Mass;
            Scale = new Vector2(scale, scale);
        }

        public override void Update(GameTime gameTime)
        {
            float gameTimeMult = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.F))
                AddForce(new Vector2(1.5f, 50f));

            AAcceleration = Global.Noiser.Gaussian(-10, 10) * gameTimeMult * 2;
            AddForce(GetGravity());
            AddForce(GetDrag());

            if (Math.Abs(AVelocity) > 0)
                AAcceleration -= MathHelp.Limit(AVelocity, -gameTimeMult, gameTimeMult);

            WallCollisions();

            base.Update(gameTime);
        }

        private void WallCollisions()
        {
            float width = Global.WinWidth / Camera.main.Zoom;
            float height = Global.WinHeight / Camera.main.Zoom;
            float slowingForce = -MathHelp.Limit(Velocity.LengthSquared(), 0, 150);

            if (Position.X >= width / 2 - Scale.X)
            {
                Position.X = width / 2 - Scale.X;
                Velocity.X *= -1;
                AddForce(Vector2.UnitX * -slowingForce);
            }
            else if (Position.X <= -width / 2)
            {
                Velocity.X *= -1;
                Position.X = -width / 2;
                AddForce(Vector2.UnitX * slowingForce);
            }

            if (Position.Y >= height / 2 - Scale.Y)
            {
                Velocity.Y *= -1;
                Position.Y = height / 2 - Scale.Y;
                AddForce(Vector2.UnitY * -slowingForce);
            }
            else if (Position.Y <= -height / 2)
            {
                Velocity.Y *= -1;
                Position.Y = -height / 2;
                AddForce(Vector2.UnitY * slowingForce);
            }
        }
    }
}
