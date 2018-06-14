using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public class Collider : Component
    {
		public Vector2 Position { get { return (Entity.Position); }}
        
		public Collider()
        {

        }
    }
    
    public class BoxCollider : Collider
    {
        public Vector2 Size;
        public Vector2 RealSize { get { return (Entity.Scale * Size); } }

		public float LeftBorder { get { return (Position.X - (RealSize.X / 2)); } }
		public float RightBorder { get { return (Position.X + (RealSize.X / 2)); } }
		public float TopBorder { get { return (Position.Y - (RealSize.Y / 2)); } }
		public float BottomBorder { get { return (Position.Y + (RealSize.Y / 2)); } }
		public Vector2 TopLeftCorner { get { return new Vector2(LeftBorder, TopBorder); }}
		public Vector2 TopRightCorner { get { return new Vector2(RightBorder, TopBorder); } }
		public Vector2 BottomLeftCorner { get { return new Vector2(LeftBorder, BottomBorder); } }
		public Vector2 BottomRightCorner { get { return new Vector2(RightBorder, BottomBorder); } }

        public BoxCollider()
        {
            Size = Vector2.One;
        }
    }
    
    public class CircleCollider : Collider
    {
        public float Radius;
		public BoxCollider BoundingBox { get { return new BoxCollider() { Size = new Vector2(Radius), Entity = this.Entity }; }}

        public CircleCollider(float radius = 1)
        {
            Radius = radius;
        }
    }
}
