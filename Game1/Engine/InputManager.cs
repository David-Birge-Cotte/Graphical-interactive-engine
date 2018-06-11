using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine
{
    public class InputManager
    {   
		// Constructor
		public InputManager()
        {
        }

        // Scrolling
		private static float _scrollSinceStartup;
		public static float ScrollValue
        {
            get => Mouse.GetState().ScrollWheelValue - _scrollSinceStartup;
        }
		public static bool Scrolling
        {
            get => (Math.Abs(ScrollValue) > 0);
        }

        // Keyboard
		public static bool Right
        { 
            get => (Keyboard.GetState().IsKeyDown(Keys.Right)
                    || Keyboard.GetState().IsKeyDown(Keys.D));
        }
		public static bool Left
        {
            get => (Keyboard.GetState().IsKeyDown(Keys.Left)
			        || Keyboard.GetState().IsKeyDown(Keys.Q));
        }
		public static bool Up
        {
            get => (Keyboard.GetState().IsKeyDown(Keys.Up)
			        || Keyboard.GetState().IsKeyDown(Keys.Z));
        }
		public static bool Down
        {
            get => (Keyboard.GetState().IsKeyDown(Keys.Down)
                    || Keyboard.GetState().IsKeyDown(Keys.S));
        }
		public static bool Space
        {
			get => (Keyboard.GetState().IsKeyDown(Keys.Space));
        }

        // Update each tick
        public void Update(GameTime gameTime)
        {
            _scrollSinceStartup = Mouse.GetState().ScrollWheelValue;
        }
    }
}
