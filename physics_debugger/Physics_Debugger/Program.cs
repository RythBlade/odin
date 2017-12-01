using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace physics_debugger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main());
            Application.Run(new Renderer.Form1());

            //Renderer.Renderer newRenderer = new Renderer.Renderer();

            //newRenderer.Initialise(null, IntPtr.Zero, 0, 0);

            //Renderer.Form1 form = new Renderer.Form1();
        }
    }
}
