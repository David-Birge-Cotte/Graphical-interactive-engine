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
        public static Vector2 WorldMousePos { get => Camera.main.ScreenToWorld(Mouse.GetState().Position.ToVector2()); }
    }
}
