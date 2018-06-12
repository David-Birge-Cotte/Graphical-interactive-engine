using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1.Engine
{
	public class Sprite : Component
	{
		// Public
		public Texture2D Image;
		public Color Color;
		public float SortingOrder;
		public Vector2 Origin;

		private Rectangle ScreenRect
		{
			get
			{
				return new Rectangle(
					(int)Entity.Position.X, (int)Entity.Position.Y,
					(int)Entity.Scale.X, (int)Entity.Scale.Y);
			}
		}

		#region Constructors
		public Sprite(Texture2D image = null, Color color = new Color(), int size = 16, float sortingOrder = 0)
		{
			if (image != null)
				Image = image;
			else
				Image = CreateTexture(size, size, Color.White);
			Color = Color.White;
			SortingOrder = 0;
			SetDefaultOrigin();
		}

		public Sprite(int size)
		{
			Image = CreateTexture(size, size, Color.White);
			Color = Color.White;
			SortingOrder = 0;
			SetDefaultOrigin();
		}
		#endregion

		public void DrawSprite(SpriteBatch sprBatch)
		{
			sprBatch.Draw(
				Image,
				ScreenRect,
				new Rectangle(0, 0, Image.Width, Image.Height),
				Color,
				MathHelper.ToRadians(Entity.Rotation),
				Origin,
				SpriteEffects.None, 0);
		}

		private Vector2 SetDefaultOrigin()
		{
			Origin.X = Image.Width / 2f;
			Origin.Y = Image.Width / 2f;
			return (Origin);
		}

		#region Static Sprite Methods
		public static Texture2D CreateTexture(int width, int height, Color color)
		{
			// Get the graphics device used by the current game
			GraphicsDevice device = Global.Game.GraphicsDevice;

			//initialize a texture
			Texture2D texture = new Texture2D(device, width, height);

			//the array holds the color for each pixel in the texture
			Color[] data = new Color[width * height];
			for (int pixel = 0; pixel < data.Length; pixel++)
			{
				//the function applies the color according to the specified pixel
				data[pixel] = color;
			}

			//set the color
			texture.SetData(data);

			return texture;
		}
        #endregion
	}
}
