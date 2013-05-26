using System;
using System.Windows;

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
            app.Run(new WinterServer());

        }
    }
#endif
}

