using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public class BoxCollider : Collider
    {
		public Vector2 Size;
		public Vector2 RealSize { get { return (Entity.Scale * Size); } }

		public float LeftBorder { get { return (RealPosition.X - (RealSize.X / 2)); } }
        public float RightBorder { get { return (RealPosition.X + (RealSize.X / 2)); } }
        public float TopBorder { get { return (RealPosition.Y - (RealSize.Y / 2)); } }
        public float BottomBorder { get { return (RealPosition.Y + (RealSize.Y / 2)); } }

		public BoxCollider()
		{
			Size = Vector2.One;
		}
    }
}
