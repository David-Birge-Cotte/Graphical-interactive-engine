using System;
using System.Threading;

using MoonSharp.Interpreter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CoreEngine.Lua
{
	public class LuaConsoleInterpreter
    {
        Script script;

        public LuaConsoleInterpreter()
        {
            script = new Script();

            // Doing a thread because reading the console is blocking
            ThreadStart childref = new ThreadStart(LuaConsoleThread);
            Thread childThread = new Thread(childref);
            childThread.Start();
        }

        public void LuaConsoleThread()
        {
            while(true)
            {
                try
                {
                    String scriptCode = Console.ReadLine(); // This blocks the thread
                    DynValue res = script.DoString(scriptCode);
                }
                catch (ScriptRuntimeException ex)
                {
                    Console.WriteLine("An error occured! {0}", ex.DecoratedMessage);
                }
            }
        }
    }
}