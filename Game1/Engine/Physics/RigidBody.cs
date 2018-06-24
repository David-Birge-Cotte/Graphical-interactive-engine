using System;
using Microsoft.Xna.Framework;

using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;

public enum BodyShape
{
	Rectangle,
	Circle,
	None
}

namespace Game1.Engine
{
	public class RigidBody : Component
	{      
		public Vector2 Velocity
		{
			get => _physicsBody.LinearVelocity; set => _physicsBody.LinearVelocity = value;
		}
		public float Mass
		{
			get => _physicsBody.Mass; set => _physicsBody.Mass = value;
		}
		public BodyType BodyType
		{
			get => _physicsBody.BodyType; set => _physicsBody.BodyType = value;
		}
		public bool IgnoreGravity
		{
			get => _physicsBody.IgnoreGravity; set => _physicsBody.IgnoreGravity = value;
		}
		public Vector2 Position
		{
			get => _physicsBody.Position; set => _physicsBody.Position = value;
		}
		public float Rotation
		{
			get => _physicsBody.Rotation; set => _physicsBody.Rotation = value;
		}
		private World _world;
		private BodyShape _shape;
		private Body _physicsBody;
		private BodyType _bodyType;

		private Vector2 _currentSize;
		private Vector2 _lastEntityPos;
		
		public RigidBody(World world, BodyShape shape = BodyShape.None, BodyType bodyType = BodyType.Dynamic)
		{
			_world = world;
			_shape = shape;
			_bodyType = bodyType;
		}

		public override void Initialize()
		{
			_currentSize = Entity.Scale;
			_physicsBody = new Body();
			AddFixture();
			_physicsBody.SetFriction(0.3f);
			_physicsBody.SetRestitution(0.1f);
			BodyType = _bodyType;

			_world.Add(_physicsBody);
		 
			Position = Entity.Position;
			Rotation = Entity.Rotation;
			
			base.Initialize();
		}

		public override void Update(float dt)
		{
			// We should be able to move use Entity.Position in our code
			if (_lastEntityPos != Entity.Position)
				this.Position = Entity.Position;

			// We should be able to change Entity.Scale too
			if (_currentSize != Entity.Scale)
			{
				ResetFixture(); // Recreate the fixture
				_currentSize = Entity.Scale;
			}
				
			// Update the position and rotation based on physics
			Entity.Position = this.Position;
			Entity.Rotation = this.Rotation;

			// Save the current position
			_lastEntityPos = Entity.Position;

			base.Update(dt);
		}

		/// <summary>
		/// Removes the current fixture and puts another one
		/// </summary>
		public void ResetFixture()
		{
			RemoveFixture();
			AddFixture();
		}

		/// <summary>
		/// Adds a new fixture to the body
		/// </summary>
		public void AddFixture()
		{
			if (_shape == BodyShape.None)
				return;
			_physicsBody.Add(CreateFixture());
		}
		
		/// <summary>
		/// Removes the current fixture
		/// </summary>
		public void RemoveFixture()
		{
			if (_physicsBody.FixtureList.Count == 0)
				return;
			_physicsBody.Remove(_physicsBody.FixtureList[0]);
		}

		/// <summary>
		/// Returns a new fixture based on Entity and rigidbody parameters
		/// </summary>
		private Fixture CreateFixture()
		{
			Vertices vertices; 
			PolygonShape shape;

			if (_shape == BodyShape.Rectangle)
				vertices = PolygonTools.CreateRectangle(Entity.Scale.X / 2, Entity.Scale.Y / 2);
			else if (_shape == BodyShape.Circle)
				vertices = PolygonTools.CreateCircle(Entity.Scale.X / 2, 16);
			else
				return null;

			shape = new PolygonShape(vertices, 1);
			return(new Fixture(shape));
		}

		/// <summary>
		/// Called when the entity is beeing removed from the scene
		/// Removes the physics body from the world
		/// </summary>
		public override void OnDestroy()
		{
			if (_physicsBody.World != null)
				_world.Remove(_physicsBody);
			_physicsBody = null;
			base.OnDestroy();
		}

		public void AddForce(Vector2 force)
		{
			_physicsBody.ApplyForce(force);
		}

		public void ApplyTorque(float torque)
		{
			_physicsBody.ApplyTorque(torque);
		}

		public void SetAngularVelocity(float av)
		{
			_physicsBody.AngularVelocity = av;
		}      
	}
}
