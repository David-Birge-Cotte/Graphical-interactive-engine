using System;

namespace Game1.Engine
{
	public class CircleCollider : Collider
    {
		public float Radius;

		public CircleCollider(float radius = 1)
        {
			Radius = radius;
        }
    }
}
