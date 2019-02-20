using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CoreEngine.ECS
{
    public class Camera : Component
    {
        public float Zoom = 1;
        private Viewport viewport;

        public Camera(float zoom = 1)
        {
            this.viewport = Application.Instance.GraphicsDevice.Viewport;
			Zoom = zoom;
        }

        public Matrix GetTransformation()
		{
			return (
				Matrix.CreateTranslation(new Vector3(-Transform.Position.X, -Transform.Position.Y, 0)) * 
				Matrix.CreateRotationZ(MathHelper.ToRadians(Transform.Rotation)) * 
				Matrix.CreateScale(Zoom, Zoom, 1) * 
				Matrix.CreateTranslation(
					viewport.Width * 0.5f, 
					viewport.Height * 0.5f, 0)
			);
		}
		
		public Vector2 ScreenToWorld(Vector2 pos)
		{
			return new Vector2(Transform.Position.X + (pos.X - viewport.Width / 2) / Zoom, 
							   Transform.Position.Y + (pos.Y - viewport.Height / 2) / Zoom) / Application.WorldToDrawScale;
		}
    }
}