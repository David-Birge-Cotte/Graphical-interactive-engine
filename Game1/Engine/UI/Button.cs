using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public class Button : UIElement
	{
        public Color baseColor = Color.White;

		public Button(Vector2 pos) : base()
		{
			Position = pos;
		}

		public override void Update(float dt)
		{
			base.Update(dt);
		}

        public override void OnMouseHover()
        {
            sprite.Color = Color.DarkGray;
            base.OnMouseHover();
        }

        public override void OnMouseDown()
        {
            sprite.Color = Color.DarkRed;
            base.OnMouseDown();
        }

        public override void OnMouseUp()
        {
            base.OnMouseUp();
        }

        public override void OnStopHover()
        {
            sprite.Color = baseColor;
            base.OnStopHover();
        }
    }
}
