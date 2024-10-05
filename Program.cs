using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScenarioBasedVideoUploader
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport( "user32.dll" )]
        private static extern bool SetProcessDPIAware();
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if( Environment.OSVersion.Version.Major >= 6 )
                SetProcessDPIAware();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            
            try
            {
                Application.Run( new MainForm() );
            }
            catch( Exception e )
            {
                MessageBox.Show( e.Message, "Error" );
            }
        }
    }
}