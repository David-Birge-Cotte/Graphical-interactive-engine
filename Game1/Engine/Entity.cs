using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Game1.Engine;

namespace Game1.Engine
{
    public class Entity
    {
        public ulong ID;
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public List<Component> Components;

        public Rectangle GetRectangle()
        {
            return new Rectangle(
                (int)Position.X, (int)Position.Y, 
                (int)Scale.X, (int)Scale.Y);
        }

        public Entity()
        {
            Components = new List<Component>();
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0;
        }

        public Entity(Vector2 position, float rotation, Vector2 scale)
        {
            Components = new List<Component>();
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public Entity(Vector2 position, float rotation, Vector2 scale, Component component)
        {
            Components = new List<Component>();
            Position = position;
            Rotation = rotation;
            Scale = scale;
            AddComponent(component);
        }

        public Entity(Component component)
        {
            Components = new List<Component>();
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0;
            AddComponent(component);
        }

        public void AddComponent<T>(T t) where T : Component
        {
            Components.Add(t);
        }

        public void RemoveComponent(Component component)
        {
           if (Components.Remove(component) == false)
            {
                Console.WriteLine("ERROR: Can't remove component {1} on {2}", 
                    this.ID, component.ToString());
            }
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in Components)
            {
                if (component.GetType() == typeof(T))
                    return (component as T);
            }
            return (null); 
        }

		public virtual void Update(GameTime gameTime)
		{
			
		}
    }
}
