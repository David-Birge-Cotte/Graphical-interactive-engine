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

        public void Activate(bool b)
        {
            Scene.IsActive = b;
        }

        public EntityAPI Instantiate(string name = "", EntityAPI parent = null)
        {
            if(parent == null)
                parent = Scene.RootEntity.API;
            var entity = parent.AddChild(name);
            return entity;
        }
    }
}