using System;

using Game1.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.GameObjects
{   
	public class SimplePhysicsEntity : Entity
    {
		RigidBody rb2d;
        Sprite sprite;

        public SimplePhysicsEntity()
        {
        }

		public override void Initialize()
        {         
            sprite = AddComponent(new Sprite());
            sprite.Color = Noise.RandomGaussianColor();
            
			int mass = Noise.Gaussian(1, 5);
			Scale = new Vector2(mass / 2f, mass / 2f);

			rb2d = AddComponent(new RigidBody(Scene.World, BodyShape.Rectangle));
			rb2d.Mass = mass;

            base.Initialize();
        }

		public override void Update(float dt)
        {
			// Destroy self if falling outside of the world
			if (Position.Y > 1000)
			{
				Scene.Destroy(this);
				return;
			}         

			base.Update(dt);
        }
    }
}
