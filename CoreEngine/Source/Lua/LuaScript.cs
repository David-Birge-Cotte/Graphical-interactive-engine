using System;
using MoonSharp.Interpreter;

namespace CoreEngine.Lua
{
    public class LuaScript
    {
        public Script Script;
        public DynValue FuncInitialize;
		public DynValue FuncUpdate;

        public LuaScript()
        {
            Script = new Script();
        }
    }
}