using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Game1.Engine
{
    public class Noise
    {
        Random generator;
        public FastNoise FNoise;
        
        public Noise()
        {
            generator = new Random();
            FNoise = new FastNoise();
        }

        public int Generate(int high)
        {
            return (generator.Next(high));
        }

        public int Generate(int low, int high)
        {
            return (generator.Next(low, high));
        }

        public int[] Generate(int low, int high, int size)
        {
            int[] tab = new int[size];
            for (int i = 0; i < size; i++)
                tab[i] = Generate(low, high);
            return (tab);
        }

        public int Gaussian(int low, int high, int numberOfSteps = 5, int deviation = 1, int mean = 0)
        {
            int ret = 0; ;
            for (int i = 0; i < numberOfSteps; i++)
                ret += Generate(low, high);
            ret /= numberOfSteps;
            ret *= deviation;
            ret += mean;
            return (ret);
        }

        public int[] GaussianArray(int low, int high, int size, int numberOfSteps = 5, int deviation = 1, int mean = 0)
        {
            int[] tab = new int[size];
            for (int i = 0; i < size; i++)
                tab[i] = Gaussian(low, high, numberOfSteps, deviation, mean);
            return (tab);
        }

        public int MonteCarlo(int low, int high)
        {
            int a, b;
            do
            {
                a = Generate(low, high);
                b = Generate(low, high);
            } while (a < b);
            return a;
        }

        public int[] MonteCarlo(int low, int high, int size)
        {
            int[] tab = new int[size];
            for (int i = 0; i < size; i++)
                tab[i] = MonteCarlo(low, high);
            return (tab);
        }

        public Vector2 RandomVector2()
        {
            return new Vector2(Generate(100) - 50, Generate(100) - 50);
        }

        public Color RandomColor()
        {
            return new Color(Generate(255), Generate(255), Generate(255), Generate(255));
        }

        public Color RandomGaussianColor()
        {
            return new Color(Gaussian(0, 255), Gaussian(0, 255), Gaussian(0, 255), Gaussian(0, 255));
        }
    }
}
