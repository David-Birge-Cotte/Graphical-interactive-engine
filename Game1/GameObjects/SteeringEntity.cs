using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game1.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.GameObjects
{
	class SteeringEntity : Entity
    {
		RigidBody rb2d;
		Sprite sprite;

        public SteeringEntity() : base()
        {
			Position = new Vector2(Noise.Gaussian(-Global.WinWidth / 2, Global.WinWidth / 2) * 1.5f,
			                       Noise.Gaussian(-Global.WinHeight / 2, Global.WinHeight / 2) * 1.5f);
			sprite = AddComponent(new Sprite());
			sprite.Color = Noise.RandomGaussianColor();
			rb2d = AddComponent(new RigidBody());


			rb2d.Mass = Noise.Gaussian(40, 50);
			Scale = new Vector2(rb2d.Mass, rb2d.Mass);
            
			rb2d.MaximumForce = 10;
			rb2d.MaximumVelocity = 100;
        }
        
        public override void Update(GameTime gameTime)
        {
            float gameTimeMult = (float)gameTime.ElapsedGameTime.TotalSeconds;

			//rb2d.AddForce(new Vector2(-1, 0));
			rb2d.AddForce(Physics.Seek(rb2d, Global.WorldMousePos));

            base.Update(gameTime);
        }
    }
}
