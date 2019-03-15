using System;
using System.Linq;

using CoreEngine.ECS;
using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;

namespace CoreEngine.Lua
{
    public class EntityAPI
    {
        [MoonSharpHidden]
        public Entity Entity;

        [MoonSharpHidden]
        public EntityAPI(Entity entity)
        {
            Entity = entity;
        }

        public void Move(float x, float y)
        {
            Entity.Transform.LocalPosition += new Vector2(x, y);
        }

        public void SetPosition(float x, float y)
        {
            Entity.Transform.Position = new Vector2(x, y);
        }

        public Vector2 GetPosition()
        {
            return (Entity.Transform.Position);
        }

        public void Rotate(float rot)
        {
            Entity.Transform.Rotation += rot;
        }

        public void SetRotation(float rot)
        {
            Entity.Transform.Rotation = rot;
        }

        public float GetRotation()
        {
            return Entity.Transform.Rotation;
        }

        public SpriteAPI AddSpriteComponent()
        {
            var spr = Entity.AddComponent(new Sprite(Entity.Scene));
            return spr.API;
        }

        public void AddScriptComponent(string luaFile)
        {
            var script = FileLoader.LoadFile(luaFile, Entity.Scene.Name);
            Entity.AddComponent(new Scriptable(script));
        }

        public EntityAPI AddChild(string name = "")
        {
            var e = Entity.Instantiate(Entity.Scene, name, Entity);
            
            return e.API;
        }

        public EntityAPI GetChild(string name)
        {
            var e = 
                from child in Entity.Children
                where child.Name == name
                select child;
            if(e.FirstOrDefault() == null)
                return null; // TODO think about user error handling in lua
            return (e.FirstOrDefault().API);
        }
    }
}