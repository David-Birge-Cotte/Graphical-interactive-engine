using System;
using MoonSharp.Interpreter;
using CoreEngine.ECS;

namespace CoreEngine.Lua
{
    public class LuaScript
    {
        public Script Script;
        public DynValue FuncInitialize;
		public DynValue FuncUpdate;

        public LuaScript(string script)
        {
            Script = new Script();
            try
			{
				Script.DoString(script);
			}
			catch (InterpreterException ex)
			{
				Console.WriteLine("A Lua syntax error occured! {0}", ex.DecoratedMessage);
			}
            FuncInitialize = Script.Globals.Get("_initialize");
			FuncUpdate = Script.Globals.Get("_update");
        }

        public void AddAPI(Entity entity)
        {
            LuaEnvManagement.AddAPI_ECS(Script, entity);
        }

        public void Initialize()
        {
            if(FuncInitialize.IsNotNil())
			{
				try
				{
					Script.Call(FuncInitialize);
				}
				catch (InterpreterException ex)
				{
					Console.WriteLine("A Lua runtime error occured! {0}", ex.DecoratedMessage);
				}
			}
        }

        public void Update(float dt)
        {
            Script.Globals["dt"] = dt;
			if(FuncUpdate.IsNotNil())
			{
				try
				{
					Script.Call(FuncUpdate);
				}
				catch (InterpreterException ex)
				{
					Console.WriteLine("A Lua runtime error occured! {0}", ex.DecoratedMessage);
				}
			}
        }
    }
}