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
		public float SortingOrder = 0.5f; // 0 front, 1 back
		public Vector2 Origin;
        private Vector2 imgRatio;
		public SpriteAPI API;

        #region Constructors
        public Sprite(Scene scene, Texture2D image = null, float sortingOrder = 0)
		{
			scene.Sprites.Add(this);
			Color = Color.White;
            ChangeImage(image, Application.PixelsPerUnit);
			API = new SpriteAPI(this);
		}

		public Sprite(Scene scene, int size)
		{
			scene.Sprites.Add(this);
            Color = Color.White;
            ChangeImage(CreateTexture(size, size, Color.White), Application.PixelsPerUnit);
			API = new SpriteAPI(this);
		}
		#endregion

        public void ChangeImage(Texture2D image, int ppu = 1)
        {
            if (image == null)
                image = CreateTexture(ppu, ppu, Color);
            Image = image;
            imgRatio = ImageRatio(image);
            SetDefaultOrigin();
        }

		public void DrawSprite(SpriteBatch sprBatch)
		{
			Rectangle destRect;

            int ppu = Application.PixelsPerUnit;
            destRect = new Rectangle(
                new Point((int)(Entity.Transform.Position.X * ppu), 
                            (int)(Entity.Transform.Position.Y * ppu)),
                new Point((int)(Entity.Transform.Scale.X * Image.Width),
                            (int)(Entity.Transform.Scale.Y * Image.Height)));

			// Draw to the SpriteBatch
			sprBatch.Draw(
				Image,
				destRect,
				null,
				Color,
				(float)(Math.PI * Entity.Transform.Rotation / 180f),
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

		public void ChangeTextureData(Color[] pixels, int height, int width)
		{
			Image = new Texture2D(Application.Graphics.GraphicsDevice, 
					height, width);
			Image.SetData(pixels);
			imgRatio = ImageRatio(Image);
			SetDefaultOrigin();
		}

		#region Static Sprite Methods
        public static Vector2 ImageRatio(Texture2D spr)
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
			Color[] pixels = new Color[width * height];
			for (int i = 0; i < pixels.Length; i++)
			{
				//the function applies the color according to the specified pixel
				pixels[i] = color;
			}

			//set the color
			texture.SetData(pixels);

			return texture;
		}
		#endregion
	}
}