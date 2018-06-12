using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
    public static class Collision
    {
		public static bool IsColliding(Vector2 pos, Collider col)
		{
			if (col.GetType() == typeof(BoxCollider))
				return (IsBoxPointColliding(pos, col as BoxCollider));
			if (col.GetType() == typeof(CircleCollider))
				return (IsCirclePointColliding(pos, col as CircleCollider));
			return false;
		}

		private static bool IsBoxPointColliding(Vector2 pos, BoxCollider col)
		{
			return (pos.X > col.LeftBorder && pos.X < col.RightBorder
                    && pos.Y > col.TopBorder && pos.Y < col.BottomBorder);
		}

		private static bool IsCirclePointColliding(Vector2 pos, CircleCollider col)
		{
			return (Vector2.DistanceSquared(pos, col.RealPosition) <= (col.Radius*col.Radius));
		}

		public static bool IsColliding(Collider col1, Collider col2)
		{
			if (col1.GetType() == typeof(BoxCollider) && col2.GetType() == typeof(BoxCollider))
				return (IsBoxBoxColliding(col1 as BoxCollider, col2 as BoxCollider));
			if (col1.GetType() == typeof(CircleCollider) && col2.GetType() == typeof(CircleCollider))
				return (IsCircleCircleColliding(col1 as CircleCollider, col2 as CircleCollider));
			return false;
		}

		private static bool IsBoxBoxColliding(BoxCollider col1, BoxCollider col2)
		{
			return (!(col1.LeftBorder >= col2.RightBorder
                      || col1.RightBorder <= col2.LeftBorder
                      || col1.TopBorder >= col2.BottomBorder
                      || col1.BottomBorder <= col2.TopBorder));
		}

		private static bool IsCircleCircleColliding(CircleCollider col1, CircleCollider col2)
		{
			return (Vector2.DistanceSquared(col1.RealPosition, col2.RealPosition)
			        <= ((col1.Radius + col2.Radius) * (col1.Radius + col2.Radius)));
		}
    }
}
