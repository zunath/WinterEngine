using System;
using System.Windows;
using WinterEngine.Library.Managers;


namespace WinterEngine.Server
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread()]
        static void Main(string[] args)
        {
            Application app = new Application();
            //IModuleManager moduleManager = new ModuleManager();
            MainWindow window = null;
            app.Run(window);

        }
    }
#endif
}

