using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Threading;
using log4net;
using System.Data.SQLite;

namespace CCIFMS
{
  public class CommUtil
  {
    public static int InputFileTryTimes = 15;
    public static int InputFileTryInterval = 100;

    public static string GetProcessFileName(string FolderPath)
    {
      string result = "";
      List<FileInfo> fslist = GetProcessFiles(FolderPath);
      if (fslist.Count > 0)
      {
        result = fslist[0].FullName;
      }
      fslist = null;
      return result;
    }
    public static List<FileInfo> GetProcessFiles(string FolderPath)
    {
      FileInfo[] files = new DirectoryInfo(@FolderPath).GetFiles();
      List<FileInfo> fslist = new List<FileInfo>(files);
      // sorting files by time
      fslist.Sort(new Comparison<FileInfo>(delegate(FileInfo a, FileInfo b)
      {
        return a.CreationTime.CompareTo(b.CreationTime);
      }));
      //FileInfo f = fslist[0].FullName ;
      return fslist;
    }
    public static bool WritetoFile(string outputfilename, string[] outputValues)
    {
      try
      {
        using (var swOutput = File.CreateText(outputfilename))
        {
          for (int i = 0; i < outputValues.Length; i++)
          {
            swOutput.WriteLine(outputValues[i]);
          }
          swOutput.Flush();
          swOutput.Close();
        }
        return true;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    #region check FileLock
    public static bool IsFileLocked(IOException exception)
    {
      int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
      return errorCode == 32 || errorCode == 33;
    }

    public static bool CheckFileLock(string fileName,ref int TryTimes)
    {
      return CheckFileLock(fileName, ref TryTimes, CommUtil.InputFileTryInterval);
    }
    public static bool CheckFileLock(string fileName,ref int TryTimes, int tryintervalms)
    {
      var _numberOfTries = TryTimes;
      TryTimes = 0;
      while (true)
      {
        TryTimes++;
        try
        {
          using(var fs=File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete))
          {
            fs.Close();  
          }          
          return true;
        }
        catch (IOException e)
        {
          if (!IsFileLocked(e))
          {
            throw;
          }
          if (++TryTimes > _numberOfTries)
          {
            throw new Exception("The file is locked too long: " + e.Message, e);
          }
          Thread.Sleep(tryintervalms);
        }
      }
    }

    #endregion
  }
}
