using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baigiamasis_d
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

            // Kai uždaroma programa
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            Application.Run(new Pradzia());
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            Database_class.Close();
        }
    }
}
