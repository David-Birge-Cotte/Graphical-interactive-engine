using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public static class Noise
	{
		static Random generator;
		public static FastNoise FNoise;
		
		static Noise()
		{
			generator = new Random();
			FNoise = new FastNoise();
		}

		public static int Generate(int high)
		{
			return (generator.Next(high));
		}

		public static float Generate(float high)
		{
			return (generator.Next((int)(high * 1000)) / 1000f);
		}

		public static int Generate(int low, int high)
		{
			return (generator.Next(low, high));
		}

		public static float Generate(float low, float high)
		{
			return (generator.Next((int)(low * 10000), (int)(high * 10000)) / 10000f);
		}

		public static int[] Generate(int low, int high, int size)
		{
			int[] tab = new int[size];
			for (int i = 0; i < size; i++)
				tab[i] = Generate(low, high);
			return (tab);
		}

		public static int Gaussian(int low, int high, int numberOfSteps = 5, int deviation = 1, int mean = 0)
		{
			int ret = 0; ;
			for (int i = 0; i < numberOfSteps; i++)
				ret += Generate(low, high);
			ret /= numberOfSteps;
			ret *= deviation;
			ret += mean;
			return (ret);
		}

		public static int Gaussian(float low, float high, int numberOfSteps = 5, int deviation = 1, int mean = 0)
		{
			int ret = 0; ;
			for (int i = 0; i < numberOfSteps; i++)
				ret += (int)Generate(low, high);
			ret /= numberOfSteps;
			ret *= deviation;
			ret += mean;
			return (ret);
		}

		public static int[] GaussianArray(int low, int high, int size, int numberOfSteps = 5, int deviation = 1, int mean = 0)
		{
			int[] tab = new int[size];
			for (int i = 0; i < size; i++)
				tab[i] = Gaussian(low, high, numberOfSteps, deviation, mean);
			return (tab);
		}

		public static int MonteCarlo(int low, int high)
		{
			int a, b;
			do
			{
				a = Generate(low, high);
				b = Generate(low, high);
			} while (a < b);
			return a;
		}

		public static int[] MonteCarlo(int low, int high, int size)
		{
			int[] tab = new int[size];
			for (int i = 0; i < size; i++)
				tab[i] = MonteCarlo(low, high);
			return (tab);
		}

		public static Color RandomColor()
		{
			return new Color(Generate(255), Generate(255), Generate(255), Generate(255));
		}
		
		public static Color RandomGaussianColor(bool alpha = false)
		{
			if(alpha)
				return new Color(Gaussian(0, 255), Gaussian(0, 255), Gaussian(0, 255), Gaussian(0, 255));
			return new Color(Gaussian(0, 255), Gaussian(0, 255), Gaussian(0, 255), 255);
		}
	}
}
