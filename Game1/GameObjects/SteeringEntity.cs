using System;
using System.Collections.Generic;
using Game1.Engine;
using Game1.Engine.MachineLearning;
using Microsoft.Xna.Framework;

namespace Game1.GameObjects
{
	class SteeringEntity : Entity
    {
        public NeuralNetwork Brain;
		public RigidBody Rb2d;
		Sprite sprite;
        
		float maxForce = 50;
		float maxSpeed = 10;

        public float energy = 10;

        public SteeringEntity() : base()
        {

        }

		public override void Initialize()
		{
            // 2 input neurons -> 1 layer of 2 hidden neurons -> 2 output neurons
            Brain = new NeuralNetwork(2, 2, 2);

            Position = new Vector2(
                Noise.Gaussian(-Global.WinWidth / 2, Global.WinWidth / 2), 
                Noise.Gaussian(-Global.WinHeight / 2, Global.WinHeight / 2)) / 10f;

            sprite = AddComponent(new Sprite());
            sprite.Color = Noise.RandomGaussianColor();

			Rb2d = AddComponent(new RigidBody(Scene.World, BodyShape.Rectangle));
			Rb2d.Mass = 1;
            Rb2d.FixedRotation = true;

			Rb2d.IgnoreGravity = true;

			base.Initialize();
		}

		public override void Update(float dt)
        {
            energy -= dt;

            base.Update(dt);
        }

        public void BrainMovement(Entity closestFood)
        {
            Vector2 dir = closestFood.Position - Position;
            Vector2 firstInputs = dir;
            
            float[] inputs = { firstInputs.X, firstInputs.Y };
            float[] outputs = Brain.FeedForward(inputs);
            Vector2 outputMvr = new Vector2(outputs[0], outputs[1]);

            outputMvr.Normalize();
            Rb2d.AddForce(SeekDesired(outputMvr));
        }


        /// <summary>
        /// OLD VERSION 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
		public Vector2 Seek(Vector2 target)
        {
            Vector2 desiredVelocity;
            Vector2 steering;

			desiredVelocity = target - Rb2d.Position;
			desiredVelocity = desiredVelocity.SetMagnitude(maxSpeed);

			steering = desiredVelocity - Rb2d.Velocity;
			steering = steering.Limit(maxForce);
            return (steering);
        }

        public Vector2 SeekDesired(Vector2 desiredVelocity)
        {
            Vector2 steering;

            desiredVelocity = desiredVelocity.SetMagnitude(maxSpeed);
            steering = desiredVelocity - Rb2d.Velocity;
            steering = steering.Limit(maxForce);
            return (steering);
        }
    }
}
