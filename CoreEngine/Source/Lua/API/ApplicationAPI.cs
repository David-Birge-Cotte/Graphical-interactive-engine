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
    }
}