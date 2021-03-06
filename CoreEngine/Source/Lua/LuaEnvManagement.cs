using System;

using MoonSharp.Interpreter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using CoreEngine.ECS;

namespace CoreEngine.Lua
{
	public static class LuaEnvManagement
    {
        public static void RegisterTypes()
        {
            // Static types or enums
            UserData.RegisterType<Vector2>();
			UserData.RegisterType<Keys>();
            UserData.RegisterType<ColorAPI>();

            // Direct access
            UserData.RegisterType<Transform>();
            UserData.RegisterType<Input>();

            // API
            UserData.RegisterType<EntityAPI>();
            UserData.RegisterType<SceneAPI>();
            UserData.RegisterType<SpriteAPI>();
            UserData.RegisterType<ApplicationAPI>();
        }

        public static void AddAPI_ECS(Script script, Entity en)
        {
			script.Globals["Vector2"] = typeof(Vector2); // Static
			script.Globals["Keys"] = UserData.CreateStatic<Keys>();

            script.Globals.Set("_color", UserData.Create(new ColorAPI()));
            script.Globals.Set("_input", UserData.Create(Application.Instance.Input));
			script.Globals.Set("_scene", UserData.Create(en.Scene.API));
            script.Globals.Set("_entity", UserData.Create(en.API));
            script.Globals.Set("_app", UserData.Create(Application.Instance.API));
        }
    }
}