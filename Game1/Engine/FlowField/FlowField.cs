using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public class FlowFieldManager
	{
		public List<FlowField> FlowFields;
		public float Time = 0;
		private int _chunkSize = 16;

		public FlowFieldManager()
		{
			FlowFields = new List<FlowField>();
		}

		public FlowField AddChunk(FlowField flowField)
		{
			FlowFields.Add(flowField);
			return (flowField);
		}

		public FlowField GetFlowFromPoint(int x, int y)
		{
			int x_ = x / _chunkSize;
			int y_ = y / _chunkSize;

			for (int i = 0; i < FlowFields.Count; i++)
			{
				if (x_ == FlowFields[i].x && y_ == FlowFields[i].y)
				{
					if(FlowFields[i].Time != Time)
						FlowFields[i].SetTimeAndGenerate(Time);                 
					return FlowFields[i];
				}
			}
			return AddChunk(new FlowField(_chunkSize, x_, y_));
		}
	}

	public class FlowField
	{
		public int x, y;
		public Vector2[,] Grid;
		public float Time;
		public int Size { get => _size; }

		private int _size;
		private float _resolution;


		/// <summary>
		/// Generate a new Flowfield
		/// </summary>
		/// <param name="size">chunksize</param>
		/// <param name="pos">position in the list</param>
		/// <param name="resolution">zoom factor</param>
		public FlowField(int size, int posx, int posy, float resolution = 0.1f)
		{
			_size = size;
			_resolution = resolution;
			Grid = new Vector2[_size, _size];
			x = posx; y = posy;

			GeneratePerlinFlowField(_size, x, y);
		}

		public void SetTimeAndGenerate(float t)
		{
			Time = t;
			GeneratePerlinFlowField(_size, x, y);
		}
		
		public void GeneratePerlinFlowField(int size = 16, int posx = 0, int posy = 0)
		{
			for (int x = 0; x < _size; x++)
			{
				for (int y = 0; y < _size; y++)
				{
					Grid[x, y] = MathHelp.FromAngle(
						MathHelp.Map(0, 1, 0, (float)Math.PI * 2, Noise.FNoise.GetPerlin((x + posx * size) / _resolution, (y + posy * size) / _resolution, Time / _resolution)));
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
