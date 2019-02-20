using System;
using CoreEngine.ECS;
using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;

namespace CoreEngine.Lua
{
    public class SceneAPI
    {
        [MoonSharpHidden]
        public Scene Scene;

        [MoonSharpHidden]
        public SceneAPI(Scene scene)
        {
            Scene = scene;
        }

        public EntityAPI Instantiate(string name = "", EntityAPI parent = null)
        {
            if(parent == null)
                parent = Scene.RootEntity.API;
            var entity = parent.AddChild(name);
            return entity;
        }
    }

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

        public SpriteAPI AddSpriteComponent()
        {
            var spr = Entity.AddComponent(new Sprite());
            return spr.API;
        }

        public void AddScriptComponent(string luaFile)
        {
            var script = FileLoader.LoadFile(luaFile, "my-scene");
            Entity.AddComponent(new Scriptable(script));
        }

        public EntityAPI AddChild(string name = "")
        {
            var e = Entity.Instantiate(name, Entity);
            
            return e.API;
        }
    }

    	public class SpriteAPI
        {
            [MoonSharpHidden]
            public Sprite Sprite;
            
            [MoonSharpHidden]
            public SpriteAPI(Sprite spr)
            {
                Sprite = spr;
            }

            public void RandomizeColor()
            {
                Sprite.Color = Noise.RandomGaussianColor();
            }
        }
}