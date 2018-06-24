using System;

using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public static class MathHelp
	{
		public static int Limit(int val, int low, int high)
		{
			if (val < low)
				return (low);
			else if (val > high)
				return (high);
			return (val);
		}

		public static float Limit(float val, float low, float high)
		{
			if (val < low)
				return (low);
			else if (val > high)
				return (high);
			return (val);
		}

		public static Vector2 Limit(this Vector2 val, float high)
		{
			if (val.Length() > high)
				return (val.SetMagnitude(high));
			return (val);
		}

		public static Vector2 Normalized(Vector2 vector)
		{
			vector.Normalize();
			return (vector);
		}

		public static Vector2 SetMagnitude(this Vector2 vector, float mag)
		{
			vector.Normalize();
			vector *= mag;
			return (vector);
		}

		public static Vector2 FromAngle(float angle)
		{
			return (new Vector2(
				(float)Math.Cos(angle), 
				(float)Math.Sin(angle)));
		}

		public static Vector2 ScalarProjection(Vector2 a, Vector2 b)
		{
			Vector2.Normalize(b);
			b *= Vector2.Dot(a, b);
			return (b);
		}

		public static int Map(int inputStart, int inputEnd, int outputStart, int outputEnd, int input)
		{
			int slope = 1 * (outputEnd - outputStart) / (inputEnd - inputStart);
			return (outputStart + slope * (input - inputStart));
		}

		public static float Map(float inputStart, float inputEnd, float outputStart, float outputEnd, float input)
		{
			float slope = 1.0f * (outputEnd - outputStart) / (inputEnd - inputStart);
			return (outputStart + slope * (input - inputStart));
		}

		public static Vector2 GetCarthFromPolar(float r, float a)
		{
			return new Vector2(
				(float)(r * Math.Sin(a)),
				(float)(r * Math.Cos(a))
				);
		}
	}
}
