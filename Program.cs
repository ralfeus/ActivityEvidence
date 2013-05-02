using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using R.Helpers;

namespace ActivityEvidence
{
	public class Program
	{
		public static Mutex ProgramMutex;

		[STAThread]
		static void Main() 
		{
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			ProgramMutex = new Mutex(false, "ActivityEvidence");
			if (ProgramMutex.WaitOne(0, true))
			{
                //EventLog.WriteEntry("ActivityEvidence", "Starting Main form", EventLogEntryType.Information);
				Application.Run(new MainForm());
				if (ProgramMutex.WaitOne(0, true))
                    ProgramMutex.ReleaseMutex();
			}
			else 
			{
				IntPtr prevAppInst = FindWindow(null, "Activity Evidence recorder");
				ShowWindow(prevAppInst, 1);
				SetForegroundWindow(prevAppInst);
			}
		}

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ErrorHandler.Log(e.Exception);
        }

		[DllImport("user32.dll")]
		private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		private extern static IntPtr SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		private extern static bool ShowWindow(IntPtr hWnd, int cmdShow);

	}
}