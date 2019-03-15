using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MoonSharp.Interpreter;
using CoreEngine.Lua;

namespace CoreEngine.ECS
{
    public class Entity
    {
        public string Name;
        public Guid ID;
        public Transform Transform;
        public Entity Parent;
        public List<Entity> Children;
        public List<Component> Components;
        public bool IsActive = true;
        public Scene Scene;
        public EntityAPI API;

        public static Entity Instantiate(Scene scene, string name = "entity", Entity parent = null)
        {
            Entity en;

            if(parent == null)
                parent = scene.RootEntity; // Make it at the scene's root

            en = new Entity(name);
            parent.Children.Add(en);
            en.Parent = parent;
            en.Transform.SetParent(parent.Transform);
            en.Scene = scene;
            return en;
        }

        public Entity(string name = "")
        {
            ID = new Guid();
            Name = name;
            Transform = new Transform();
            Children = new List<Entity>();
            Components = new List<Component>();
            API = new EntityAPI(this);
        }

        public void Update(float dt)
		{
            if (!IsActive) // will not update self or children
                return;

			for (int i = 0; i < Components.Count; i++)
				Components[i].Update(dt);
            
            for (int i = 0; i < Children.Count; i++)
                Children[i].Update(dt);
		}

		public void PostUpdate(float dt)
		{
            if (!IsActive)
                return;
    
			for (int i = 0; i < Components.Count; i++)
				Components[i].PostUpdate(dt);

            for (int i = 0; i < Children.Count; i++)
                Children[i].PostUpdate(dt);
		}

        #region Component modifiers

        public T AddComponent<T>(T component) where T : Component
		{
			Components.Add(component);
			component.Initialize(this);
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

        public string DebugInfos()
        {
            string debugInfo;
            debugInfo = $@"
            name: {this.Name}, 
            id: {this.ID},
            position: {this.Transform.LocalPosition.X}, {this.Transform.LocalPosition.Y}
            components: {this.Components.Count}";

            return debugInfo;
        }
    }

    public class Transform
    {
        public Vector2 Position
        {
            get
            {
                if (ParentTransform == null)
                    return LocalPosition;

                Vector2 p = LocalPosition;
                float s = (float)Math.Sin(ParentTransform.Rotation);
                float c = (float)Math.Cos(ParentTransform.Rotation);

                float xnew = p.X * c - p.Y * s;
                float ynew = p.X * s + p.Y * c;

                p.X = xnew + ParentTransform.Position.X;
                p.Y = ynew + ParentTransform.Position.Y;

                return (p);
            }
            set
            {
                if (ParentTransform == null)
                    LocalPosition = value;
                else
                    LocalPosition = value - ParentTransform.Position;
            }
        }
        public float Rotation
        {
            get 
            { 
                if (ParentTransform == null)
                    return LocalRotation; 
                return (ParentTransform.Rotation + LocalRotation);
                
            }
            set
            {
                if (ParentTransform == null)
                    LocalRotation = value;
                else
                    LocalRotation = value - ParentTransform.Rotation;
            }
        }
        public Vector2 Scale 
        {
            get 
            {
                if (ParentTransform == null)
                    return LocalScale;
                
                return (ParentTransform.Scale * LocalScale); 
                
            }
            set
            {
                if (ParentTransform == null)
                    LocalScale = value;
                LocalScale = value - ParentTransform.Scale;
            }
        }
        public Vector2 LocalPosition;
        public float LocalRotation;
        public Vector2 LocalScale;
        public Transform ParentTransform { get; private set; }
        public Transform()
        {
            LocalPosition = Vector2.Zero;
            LocalRotation = 0;
            LocalScale = Vector2.One;
        }

        public void SetParent(Transform parentTransform)
        {
            Vector2 pos = Position; // store old global position
            ParentTransform = parentTransform;
            Position = pos; // recalculate global position
        }
    }
}