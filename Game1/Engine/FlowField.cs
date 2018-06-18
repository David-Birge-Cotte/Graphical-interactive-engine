using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
    public class FlowField
    {
		public Vector2[,] Grid;
		public int Size { get => _size; }

		private int _size;
		private float _resolution;

		public FlowField(int size = 100, float resolution = 0.1f)
        {
			_size = size;
			_resolution = resolution;
			Grid = new Vector2[_size, _size];

			GeneratePerlinFlowField();
        }
        
		public void GeneratePerlinFlowField(float time = 0)
		{
			for (int x = 0; x < _size; x++)
            {
				for (int y = 0; y < _size; y++)
                {
                    Grid[x, y] = MathHelp.FromAngle(
						MathHelp.Map(0, 1, 0, (float)Math.PI * 2, Noise.FNoise.GetPerlin(x / _resolution, y / _resolution, time / _resolution)));
                }
            }
		}

		private void GenerateSimpleFlowField()
        {
            Grid = new Vector2[_size, _size];
            for (int x = 0; x < _size; x++)
            {
                for (int y = 0; y < _size; y++)
                {
					Grid[x, y] = MathHelp.FromAngle(Noise.FNoise.GetPerlin(x * (float)-Math.PI / 4, y * (float)Math.PI / 4));
                }
            }
        }
    }
}
