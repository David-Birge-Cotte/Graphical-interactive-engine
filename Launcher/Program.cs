using System;

namespace Launcher
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var app = new CoreEngine.Application("Core Engine", 1080, 720))
                app.Run();
        }
    }
}
