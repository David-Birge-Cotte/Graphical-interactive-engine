using System;
using CoreEngine.ECS;
using Microsoft.Xna.Framework;
using MoonSharp.Interpreter;

namespace CoreEngine.Lua
{
    public class ApplicationAPI
    {
        [MoonSharpHidden]
        public Application Application;

        [MoonSharpHidden]
        public ApplicationAPI(Application app)
        {
            Application = app;
        }

        public SceneAPI LaunchScene(string sceneName)
        {
            var scene = Scene.LaunchScene(sceneName);
            return (scene.API);
        }

        public Vector2 GetScreenSize()
        {
            return (new Vector2
                (Application.Graphics.PreferredBackBufferWidth, 
                Application.Graphics.PreferredBackBufferHeight));
        }

        public int GetPixelsPerUnit()
        {
            return Application.PixelsPerUnit;
        }
    }
}