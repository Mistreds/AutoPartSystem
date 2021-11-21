using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
namespace AutoPartSystem
{
    public partial class App : System.Windows.Application
    {
        public static string AppData;
        public App()
        {
            
#if DEBUG || DEBUG2
            ConsoleHelper.AllocConsole();
#endif
#if RELEASE
            //this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
#endif

             AppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData)+@"/AutoPartSystem";
            if (!Directory.Exists(AppData)) Directory.CreateDirectory(AppData);
            var Autorization = new Autorization();
            Autorization.Show();
        }
    }
    public class ConsoleHelper
    {
        /// <summary>
        /// Allocates a new console for current process.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        /// <summary>
        /// Frees the console.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
    }
}
