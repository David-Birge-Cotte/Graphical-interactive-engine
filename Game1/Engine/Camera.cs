using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine
{
	public class Camera
    {
        // singleton to current camera
        public static Camera main;

        // private variables
        private Vector2 _position = Vector2.Zero;
        private float _rotation;
        private float _zoom;
        private Viewport _viewport;
        private float _minZoom = 0.01f;
        private float _maxZoom = 100;

        // public fields
        public Vector2 Position { get => _position; set => _position = value; }
        public float Rotation { get => _rotation; set => _rotation = value; }
        public float Zoom
        { 
            get => _zoom;
            set
            {
                if (value > _maxZoom)
                    value = _maxZoom;
                else if (value < _minZoom)
                    value = _minZoom;
                _zoom = value;
            }
        }

        // constructor
        public Camera(Viewport viewport, Vector2 pos = new Vector2(), float rot = 0, float zoom = 1)
        {
            _viewport = viewport;
            Position = pos;
            Rotation = rot;
            Zoom = zoom;

            if (Camera.main == null)
                Camera.main = this;
        }
        
		public void SetAsMain()
		{
			main = this;
		}

        public void Rotate(float rot)
        {
            Rotation += rot;
        }

        public void Move(Vector2 mvt)
        {
            Position += mvt / Zoom;
        }

        public Matrix GetTransformation()
        {
            return (
                Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) * 
                Matrix.CreateRotationZ(MathHelper.ToRadians(Rotation)) * 
                Matrix.CreateScale(Zoom, Zoom, 1) * 
                Matrix.CreateTranslation(
                    _viewport.Width * 0.5f, 
                    _viewport.Height * 0.5f, 0)
            );
        }
        
		public Vector2 ScreenToWorld(Vector2 pos)
        {
			return new Vector2(Position.X + (pos.X - _viewport.Width / 2) / Zoom, 
			                   Position.Y + (pos.Y - _viewport.Height / 2) / Zoom);
        }
    }
}
