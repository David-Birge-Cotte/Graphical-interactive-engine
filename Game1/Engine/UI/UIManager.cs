using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Engine
{
    public class UIManager
    { 
        public List<UIElement> UIElements;
        public bool IsMouseDown;

        private Scene _scene;

        public UIManager(Scene scene)
        {
            _scene = scene;
            UIElements = new List<UIElement>();
        }

        public UIElement Instantiate(UIElement uiElement)
        {
            UIElements.Add(uiElement);
            uiElement.Manager = this;
            uiElement.ChangeScene(_scene);
            uiElement.Initialize();
            return (uiElement);
        }

        public void Destroy(UIElement uiElement)
        {
            if (!UIElements.Contains(uiElement))
                throw new Exception($"uiElement {uiElement.Name} is not in the UIElements list<UIElement>");
            UIElements.Remove(uiElement);
            uiElement.OnDestroy();
        }

        public void Update(float dt)
        {
            // Test Mouse collisions with UI elements
            for (int i = 0; i < UIElements.Count; i++)
            {
                if(Collision.PointIntersect(Mouse.GetState().Position.ToVector2(), UIElements[i].boxCollider))
                {
                    if (!IsMouseDown && Mouse.GetState().LeftButton == ButtonState.Pressed)
                        UIElements[i].OnMouseDown();
                    else if (Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        if (IsMouseDown)
                            UIElements[i].OnMouseUp();
                        else
                            UIElements[i].OnMouseHover();
                    }    
                }
                else
                    UIElements[i].OnStopHover(); // is called every frame. Should be optimised
            }

            // Update Mouse Information
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                IsMouseDown = true;
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
                IsMouseDown = false;

            // Update all the UI elements
            for (int i = 0; i < UIElements.Count; i++)
                UIElements[i].Update(dt);
        }

        public void PostUpdate(float dt)
        {
            for (int i = 0; i < UIElements.Count; i++)
                UIElements[i].PostUpdate(dt);
        }

		public void Draw(SpriteBatch sprBatch)
        {
            for (int i = 0; i < UIElements.Count; i++)
            {
                Sprite spr = UIElements[i].GetComponent<Sprite>();
                if (spr != null)
                    spr.DrawSprite(sprBatch);
            }
        }
    }
}
