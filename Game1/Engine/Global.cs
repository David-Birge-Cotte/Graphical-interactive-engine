using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine 
{
    public static class Global
    {
        public static Game Game;
		public static int WinHeight { get => 720; }
		public static int WinWidth { get => 1080; }
        public static Color BackgroundColor = Color.Black;
        public static int WorldToDrawScale = 200;
		public static int TargetFrameRate = 60;
		public static bool IsCursorVisible = true;
		public static bool IsFullscreen = false;
        public static ContentLoader ContentLoader;
        public static GraphicsDevice GraphicsDevice
        {
            get => Game.GraphicsDevice;
        }
        public static Vector2 WorldMousePos 
		{ 
			get => Camera.main.ScreenToWorld(Mouse.GetState().Position.ToVector2()); 
		}
		public static string AppPath
		{
			get => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
		}
	}
}
