﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				return (SetMagnitude(val, high));
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
