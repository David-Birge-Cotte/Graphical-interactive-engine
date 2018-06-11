using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Game1.Engine
{
	public class Sprite : Component
    {
        // Public
        public Texture2D Image;
        public Color Color;
		public float SortingOrder;

        public Vector2 Origin = Vector2.Zero;

        public Sprite(Texture2D image = null, Color color = new Color(), float sortingOrder = 0)
        {
            if (image != null)
                Image = image;
            else
                Image = Global.CreateTexture(16, 16, Color.White);
            Color = Color.White;
            SortingOrder = 0;
            SetDefaultOrigin();
        }

        public Sprite(int size)
        {
            Image = Global.CreateTexture(size, size, Color.White);
            Color = Color.White;
            SortingOrder = 0;
            SetDefaultOrigin();
        }

        public Vector2 SetDefaultOrigin()
        {
            Origin.X = Image.Width / 2f;
            Origin.Y = Image.Width / 2f;
            return (Origin);
        }
    }
}
