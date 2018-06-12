using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public class Collider : Component
    {
		public Vector2 LocalPosition;
		public Vector2 RealPosition { get { return (Entity.Position + LocalPosition); }}
        
		public Collider()
        {
			LocalPosition = Vector2.Zero;
        }
    }
}
