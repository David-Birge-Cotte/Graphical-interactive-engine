using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using CoreEngine.ECS;

namespace CoreEngine
{
    public class Input
    {
        KeyboardState oldState;
        private float scrollSinceStartup;

        // Update should be called after gameplay Update() and PostUpdate()
        public void Update(GameTime gameTime)
		{
			scrollSinceStartup = Mouse.GetState().ScrollWheelValue;
            oldState = Keyboard.GetState();
		}

        #region Keyboard
        public bool IsKeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return (oldState.IsKeyUp(key) && Keyboard.GetState().IsKeyDown(key));
        }

        public bool IsKeyReleased(Keys key)
        {
            return (oldState.IsKeyUp(key) && Keyboard.GetState().IsKeyDown(key));
        }
        #endregion

        #region Mouse
		public float ScrollValue
		{
			get => Mouse.GetState().ScrollWheelValue - scrollSinceStartup;
		}
		public bool IsScrolling
		{
			get => (Math.Abs(ScrollValue) > 0);
		}

        public Vector2 GetMousePosition(Scene scene)
        {
            var mousePos = Mouse.GetState().Position.ToVector2();
            var cam = scene.ActiveCamera; // hum..

            if (cam != null)
                return (cam.ScreenToWorld(mousePos));
            else
                return (mousePos);
        }
        #endregion
    }
}