using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
    public static class Collision
    { 
		public static bool PointIntersect(Vector2 pos, BoxCollider col)
		{
			return (pos.X > col.LeftBorder && pos.X < col.RightBorder
                    && pos.Y > col.TopBorder && pos.Y < col.BottomBorder);
		}

		public static bool PointIntersect(Vector2 pos, CircleCollider col)
		{
			return (Vector2.DistanceSquared(pos, col.Position) <= (col.Radius*col.Radius));
		}
              
		public static bool AABBIntersect(BoxCollider col1, BoxCollider col2)
		{
			return (!(col1.LeftBorder >= col2.RightBorder
                      || col1.RightBorder <= col2.LeftBorder
                      || col1.TopBorder >= col2.BottomBorder
                      || col1.BottomBorder <= col2.TopBorder));
		}

		public static bool CircleIntersect(CircleCollider col1, CircleCollider col2)
		{
			return (Vector2.DistanceSquared(col1.Position, col2.Position)
			        <= ((col1.Radius + col2.Radius) * (col1.Radius + col2.Radius)));
		}
    }
}
