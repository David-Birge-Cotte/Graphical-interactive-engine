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
		public float SortingOrder = 1;
		public Vector2 Origin;
		public bool scaleWithCamera = true;

        private Vector2 imgRatio;

        #region Constructors
        public Sprite(Texture2D image = null, float sortingOrder = 0)
		{
			if (image == null)
                image = CreateTexture(16, 16, Color.White);
			Color = Color.White;
            ChangeImage(image);
		}

		public Sprite(int size)
		{
            Color = Color.White;
            ChangeImage(CreateTexture(size, size, Color.White));
		}
		#endregion

        public void ChangeImage(Texture2D image)
        {
            if (image == null)
                image = CreateTexture(16, 16, Color.White);
            Image = image;
            imgRatio = ImageRatio(image);
            SetDefaultOrigin();
        }

		public void DrawSprite(SpriteBatch sprBatch)
		{
			Rectangle destRect;

            if (scaleWithCamera)
			{
				// For classic sprites
				int wtds = Global.WorldToDrawScale;
				destRect = new Rectangle(
					new Point((int)(Entity.Position.X * wtds), (int)(Entity.Position.Y * wtds)),
					new Point((int)(Entity.Scale.X * wtds * imgRatio.X), (int)(Entity.Scale.Y * wtds * imgRatio.Y)));
			}  
			else
			{
				// For UI sprites
				destRect = new Rectangle(
					new Point((int)(Entity.Position.X), (int)(Entity.Position.Y)),
					new Point((int)(Entity.Scale.X), (int)(Entity.Scale.Y)));
			}

			// Draw to the SpriteBatch
			sprBatch.Draw(
				Image,
				destRect,
				null,
				Color,
				Entity.Rotation,
				Origin,
				SpriteEffects.None, 
				SortingOrder);
		}

		private Vector2 SetDefaultOrigin()
		{
			Origin.X = Image.Width / 2f;
			Origin.Y = Image.Width / 2f;
			return (Origin);
		}

		#region Static Sprite Methods
        public Vector2 ImageRatio(Texture2D spr)
        {
            if (spr.Width > spr.Height)
                return new Vector2((float)spr.Width / (float)spr.Height, 1);
            else
                return new Vector2(1, (float)spr.Height / (float)spr.Width);
        }

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
