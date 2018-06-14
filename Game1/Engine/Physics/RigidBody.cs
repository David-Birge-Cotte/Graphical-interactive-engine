using System;
using Microsoft.Xna.Framework;

using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Common;

public enum Shape
{
	Rectangle,
    Circle
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
      
		private Vector2 _size { get => Entity.Scale; }
		private World _world;
		private Shape _shape;
		private Body _physicsBody;
		private BodyType _bodyType;

		private Vector2 _currentSize;
        
		public RigidBody(World world, Shape shape = Shape.Rectangle, BodyType bodyType = BodyType.Dynamic)
        {
			_world = world;
			_shape = shape;
			_bodyType = bodyType;
        }

		public override void Initialize()
		{
			_currentSize = _size;
			_physicsBody = new Body();
			CreateFixtureShape();
            _physicsBody.SetFriction(0.3f);
            _physicsBody.SetRestitution(0.1f);
			BodyType = _bodyType;

			_world.Add(_physicsBody);
         
			Position = Entity.Position;
			Rotation = Entity.Rotation;
            
			base.Initialize();
		}

		public override void Update()
		{
			if (_physicsBody == null || Entity == null)
				return;

			Entity.Position = this.Position;
			Entity.Rotation = this.Rotation;

			if (_currentSize != _size)
            {
				_physicsBody.Remove(_physicsBody.FixtureList[0]);
				CreateFixtureShape();
            }

			base.Update();
		}

		private void CreateFixtureShape()
		{
			Vertices vertices; 
			PolygonShape shape;

			if (_shape == Shape.Rectangle)
				vertices = PolygonTools.CreateRectangle(_size.X / 2, _size.Y / 2);
			else
				vertices = PolygonTools.CreateCircle(_size.X / 2, 16);

			shape = new PolygonShape(vertices, 1);
			_physicsBody.Add(new Fixture(shape));
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

		public override void OnDestroy()
		{
			if(_physicsBody.World != null)
			    _world.Remove(_physicsBody);
			_physicsBody = null;
			base.OnDestroy();
		}
	}
}
