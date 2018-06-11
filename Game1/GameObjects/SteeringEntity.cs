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
    class SteeringEntity : PhysicsEntity
    {
        public SteeringEntity() : base()
        {
            Position = new Vector2(Global.Noiser.Gaussian(-Global.WinWidth / 2, Global.WinWidth / 2) * 1.5f,
                Global.Noiser.Gaussian(-Global.WinHeight / 2, Global.WinHeight / 2) * 1.5f);

            Mass = Global.Noiser.Gaussian(40, 50);
            Scale = new Vector2(Mass, Mass);

            maximumForce = 10;
            maximumVelocity = 20;
        }

        public override void Update(GameTime gameTime)
        {
            float gameTimeMult = (float)gameTime.ElapsedGameTime.TotalSeconds;

            AddForce(Seek(Global.WorldMousePos));

            base.Update(gameTime);
        }
    }
}
