using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace CCIFMS
{
  static class Program
  {
    /// <summary>
    /// 應用程式的主要進入點。
    /// </summary>
    [STAThread]
    static void Main()
    {
      if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
      //if (!CheckifInProcess())
      {
        MessageBox.Show("Program unable to start because another instance already run up!");
        return;
      }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }

    public static bool CheckifInProcess()
    {
      string programName = Process.GetCurrentProcess().MainModule.ModuleName;

         
      string mutexRange = "Global\\";
      bool _inProcess = false;
      System.Threading.Mutex mutex = new System.Threading.Mutex(true, mutexRange + programName, out _inProcess);
      return _inProcess;
    }   
  }
}
