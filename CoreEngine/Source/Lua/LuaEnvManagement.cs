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
			UserData.RegisterType<KeyboardState>();
			UserData.RegisterType<Keys>();
            UserData.RegisterType<Color>(); // should do an API

            // Direct access
            UserData.RegisterType<Transform>();

            // API
            UserData.RegisterType<EntityAPI>();
            UserData.RegisterType<SceneAPI>();
            UserData.RegisterType<SpriteAPI>();
        }

        public static void AddAPI_ECS(Script script, Entity en)
        {
			script.Globals["Vector2"] = typeof(Vector2); // Static
			script.Globals["Keys"] = UserData.CreateStatic<Keys>();

            script.Globals.Set("entity", UserData.Create(en.API));
			script.Globals.Set("scene", UserData.Create(Application.Instance.scene.API));
        }
    }
}