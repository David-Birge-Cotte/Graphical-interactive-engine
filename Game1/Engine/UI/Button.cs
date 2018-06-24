using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine
{
	public class Button : UIElement
	{
		private BoxCollider _boxCollider;
		protected bool isMouseDown = false;

		public Button(Vector2 pos) : base()
		{
			Position = pos;

			// Add a box collider so we can click
			_boxCollider = AddComponent(new BoxCollider()); 
		}

		public override void Update(float dt)
		{
			// Test if click down
			if (!isMouseDown && Mouse.GetState().LeftButton == ButtonState.Pressed
				&& Collision.PointIntersect(Mouse.GetState().Position.ToVector2(), _boxCollider))
					OnMouseDown();
			
			// Test if click up
			if (Mouse.GetState().LeftButton == ButtonState.Released
				&& Collision.PointIntersect(Mouse.GetState().Position.ToVector2(), _boxCollider))
					OnMouseUp();

			base.Update(dt);
		}

		/// <summary>
		/// Called when the mouse clicks on the Button
		/// </summary>
		public virtual void OnMouseDown()
		{
			isMouseDown = true;
		}

		/// <summary>
		/// Called when the mouse is released from the Button
		/// </summary>
		public virtual void OnMouseUp()
		{
			isMouseDown = false;
		}
	}
}
