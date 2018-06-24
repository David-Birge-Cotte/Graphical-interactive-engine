using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game1.Engine
{
    public class Entity
    {
		public int ID = Noise.Generate(int.MaxValue);
		public string Name;
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public List<Component> Components;
		public Scene Scene { get; protected set; }
        
        #region Constructors
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
        #endregion

		public virtual void Initialize()
		{
			
		}

        public void ChangeScene(Scene scene)
        {
            // TODO change scene properly
            Scene = scene;
        }

		#region Component Management
		public T AddComponent<T>(T component) where T : Component
        {
			Components.Add(component);
			component.Entity = this;
			component.Initialize();
			return (component);
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

		public bool RemoveComponent<T>() where T : Component
		{
			Component component = GetComponent<T>();
			if (component != null)
			{
				RemoveComponent(component);
				return true;
			}	
			return false;
		}

        public void RemoveComponent(Component component)
        {
			Components.Remove(component);
        }
        #endregion

		public virtual void Update(float dt)
		{
			for (int i = 0; i < Components.Count; i++)
				Components[i].Update(dt);
		}

        public virtual void PostUpdate(float dt)
        {
            for (int i = 0; i < Components.Count; i++)
                Components[i].PostUpdate(dt);
        }

        public virtual void OnDestroy()
		{
			for (int i = 0; i < Components.Count; i++)
			{
				Components[i].OnDestroy();
			}
		}
	}
}
