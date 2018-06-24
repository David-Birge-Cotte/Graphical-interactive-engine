using System;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
    public class UIElement : Entity
    {
        protected Sprite sprite;

        public UIElement() : base ()
        {
            sprite = AddComponent(new Sprite());
            sprite.scaleWithZoom = false; // UI is fixed on the screen
            sprite.SortingOrder = 0; // in front
        }
    }
}
