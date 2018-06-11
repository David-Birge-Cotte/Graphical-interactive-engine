using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine 
{
    public static class Global
    {
		// Global READ-ONLY properties
		public static Color BackgroundColor { get => Color.Black; }
		public static int WinHeight { get => 768; }
		public static int WinWidth { get => 1536; }
        public static Game Game;
        public static Noise Noiser = new Noise();
        public static Vector2 WorldMousePos { get => Camera.main.ScreenToWorld(Mouse.GetState().Position.ToVector2()); }

        public static Texture2D CreateTexture(int width, int height, Color color)
        {
            GraphicsDevice device = Game.GraphicsDevice;

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
    }
}
