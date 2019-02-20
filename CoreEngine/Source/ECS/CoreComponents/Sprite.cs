using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CoreEngine.Lua;

namespace CoreEngine.ECS
{
	public class Sprite : Component
	{
		// Public
		public Texture2D Image;
		public Color Color;
		public float SortingOrder = 1;
		public Vector2 Origin;
        private Vector2 imgRatio;
		public SpriteAPI API;

        #region Constructors
        public Sprite(Texture2D image = null, float sortingOrder = 0)
		{
			Application.Instance.scene.Sprites.Add(this);
			Color = Color.White;
            ChangeImage(image);
			API = new SpriteAPI(this);
		}

		public Sprite(int size)
		{
			Application.Instance.scene.Sprites.Add(this);
            Color = Color.White;
            ChangeImage(CreateTexture(size, size, Color.White));
			API = new SpriteAPI(this);
		}
		#endregion

        public void ChangeImage(Texture2D image)
        {
            if (image == null)
                image = CreateTexture(64, 64, Color);
            Image = image;
            imgRatio = ImageRatio(image);
            SetDefaultOrigin();
        }

		public void DrawSprite(SpriteBatch sprBatch)
		{
			Rectangle destRect;

            int wtds = Application.WorldToDrawScale;
            destRect = new Rectangle(
                new Point((int)(Entity.Transform.Position.X * wtds), 
                            (int)(Entity.Transform.Position.Y * wtds)),
                new Point((int)(Entity.Transform.Scale.X * wtds * imgRatio.X), 
                            (int)(Entity.Transform.Scale.Y * wtds * imgRatio.Y)));

			// Draw to the SpriteBatch
			sprBatch.Draw(
				Image,
				destRect,
				null,
				Color,
				Entity.Transform.Rotation,
				Origin,
				SpriteEffects.None, 
				SortingOrder);
		}

		private Vector2 SetDefaultOrigin()
		{
			Origin.X = Image.Width / 2f;
			Origin.Y = Image.Height / 2f;
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
			GraphicsDevice device = Application.Graphics.GraphicsDevice;

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