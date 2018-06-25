using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
	public class UIElement : Entity
	{
        public UIManager Manager;
		protected Sprite sprite;
        public BoxCollider boxCollider { get; protected set; }

        public UIElement() : base ()
		{
            sprite = AddComponent(new Sprite());
			sprite.scaleWithZoom = false; // UI is fixed on the screen
			sprite.SortingOrder = 0; // in front

            boxCollider = AddComponent(new BoxCollider());
        }

        /// <summary>
        /// Called when the mouse is above the UI element
        /// </summary>
        public virtual void OnMouseHover()
        {

        }

        /// <summary>
        /// Called when the mouse clicks on the UI element
        /// </summary>
        public virtual void OnMouseDown()
        {

        }

        /// <summary>
        /// Called when the mouse is released from the UI element
        /// </summary>
        public virtual void OnMouseUp()
        {

        }

        /// <summary>
        /// Called when the mouse is leaving the UI element
        /// </summary>
        public virtual void OnStopHover()
        {

        }
    }
}
