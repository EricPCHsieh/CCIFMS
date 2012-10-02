using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Amib.Threading;

namespace CCIFMS
{
  abstract class FMSbaseObj : INotifyPropertyChanged, IDisposable
  {
    #region backmanager field
    private string _id;
    private string _inputUNC;
    private string _outputUNC;
    private string _procedureName;
    private string _FMSObjStatus;
    private int _maxProcessThreads;
    #endregion

    #region private member
    private ThreadSafeList<string> _processingFileList;
    private SmartThreadPool _sMTPool;
    private IWorkItemsGroup _processFileWIG;
    private FileSystemWatcher _fSWatcher;
    #endregion

    #region Properties

    public string Id
    {
      get
      {
        return _id;
      }
      set
      {
        _id = value;
        NotifyPropertyChanged("Id");
      }
    }
    public string InputUNC
    {
      get
      {
        return _inputUNC;
      }
      set
      {
        _inputUNC = value;
        NotifyPropertyChanged("InputUNC");
      }
    }
    public string OutputUNC
    {
      get
      {
        return _outputUNC;
      }
      set
      {
        _outputUNC = value;
        NotifyPropertyChanged("OutputUNC");
      }
    }
    public string FMSOObjClassName
    {
      get;
      set;
    }
    public string ProcedureName
    {
      get
      {
        return _procedureName;
      }
      set
      {
        _procedureName = value;
        NotifyPropertyChanged("ProcedureName");
      }
    }
    public int FilesInProcessing
    {
      get
      {
        if ((_processingFileList != null))
        {
          return _processingFileList.Count;
        }
        else
        {
          return 0;
        }
      }
    }
    public string FMSObjStatus
    {
      get
      {
        return _FMSObjStatus;
      }
      set
      {
        _FMSObjStatus = value;
        NotifyPropertyChanged("FMSObjStatus");
      }
    }
    public string ScanFileThreadAction
    {
      get
      {
        if (FMSObjStatus == "Start")
        {
          return "Stop";
        }
        return "Start";
      }
    }
    public int MaxProcessThreads
    {
      get
      {
        return ((_maxProcessThreads < 1) ? 1 : _maxProcessThreads);
      }
      set
      {
        if (value > 1)
        {
          _maxProcessThreads = value;
        }
        else
        {
          _maxProcessThreads = 1;
        }
        if (_processFileWIG != null)
        {
          _sMTPool.MaxThreads = _maxProcessThreads;
          _sMTPool.Concurrency = _maxProcessThreads;
          _processFileWIG.Concurrency = _maxProcessThreads;
        }
        NotifyPropertyChanged("MaxProcessThreads");
      }
    }
    public int CurrentProcessThreads
    {
      get
      {
        if (_sMTPool != null)
        {
          return _sMTPool.InUseThreads;
        }
        else
        {
          return 0;
        }
      }
    }
    #endregion
    
    #region static method
    // Factory method
    public static FMSbaseObj CreateFMSObj(string id, string inputunc, string outputunc, string procedureName, int maxProcessThreads,string fmsObjclassname)
    {
      //Get the current assembly object
      Assembly a = typeof(FMSbaseObj).Assembly;
      Type t = a.GetType("CCIFMS."+fmsObjclassname);
      FMSbaseObj o = Activator.CreateInstance(t) as FMSbaseObj;
      o.Initialise(id, inputunc, outputunc, procedureName, maxProcessThreads, fmsObjclassname);
      return o;
    }
    #endregion

    #region constructor
    public FMSbaseObj()
    {
    }
    //public FMSbaseObj(string id, string inputunc, string outputunc, string procedureName, int maxProcessThreads, string fmsObjclassname)
    //{
    //  Id = id;
    //  this.InputUNC = inputunc;
    //  this.OutputUNC = outputunc;
    //  this.ProcedureName = procedureName;
    //  this.MaxProcessThreads = maxProcessThreads;
    //  this.FMSOObjClassName = fmsObjclassname;
    //}
    #endregion

    #region abstract method
    internal abstract string[] ReadFilelines(string processFN);
    internal abstract string GetSNNumber(string processFN);
    internal abstract string[] GetOutputValues(string snnumber, string[] fileContent);
    
    #endregion

    #region private method
    private SmartThreadPool GetThreadPool()
    {
      if (_sMTPool == null)
      {
        _sMTPool = new SmartThreadPool();
        //_sMTPool.STPStartInfo.EnableLocalPerformanceCounters = false;
        _sMTPool.Name = "SMTPool # " + this.Id;
      }
      return _sMTPool;
    }
    #endregion

    #region public method
    public virtual void Initialise(string id, string inputunc, string outputunc, string procedureName, int maxProcessThreads, string fmsObjclassname)
    {
      Id = id;
      this.InputUNC = inputunc;
      this.OutputUNC = outputunc;
      this.ProcedureName = procedureName;
      this.MaxProcessThreads = maxProcessThreads;
      this.FMSOObjClassName = fmsObjclassname;
    }
    public void StartMonitorFolder()
    {
      if (_processingFileList == null)
      {
        _processingFileList = new ThreadSafeList<string>();
      }
      if (_fSWatcher == null)
      {
        PrcessExistingFiles();
        try
        {
          _fSWatcher = new FileSystemWatcher();
          _fSWatcher.Path = this.InputUNC;
          _fSWatcher.Created += new FileSystemEventHandler(FSWatcher_Created);
          _fSWatcher.EnableRaisingEvents = true;
          // re-config NotifyFilter & IncludeSubdirectories to saving resource
          _fSWatcher.IncludeSubdirectories = false;
          _fSWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
          this.FMSObjStatus = "Start";
        }
        catch (Exception ex)
        {
          Loghelper.Write(LogLevel.Error, ex.Message);
          _fSWatcher = null;
        }
        finally
        {
        }

      }
    }
    public void StopMonitorFolder()
    {
      if (_fSWatcher != null)
      {
        _fSWatcher.EnableRaisingEvents = false;
        _fSWatcher.Created -= new FileSystemEventHandler(FSWatcher_Created);
        _fSWatcher = null;
        this.FMSObjStatus = "Stop";
      }
      if ((_processFileWIG != null))
      {
        _processFileWIG.Cancel(true);
        _processFileWIG = null;
      }

      if (_sMTPool != null)
      {
        _sMTPool.Shutdown();
        _sMTPool = null;
      }
      if ((_processingFileList != null))
      {
        _processingFileList = null;
      }
    }
    #endregion

    #region protect method
    protected void Add2ProcessFileList(string FN)
    {
      if (!_processingFileList.Contains(FN))
      {
        this._processingFileList.Add(FN);
      }
    }
    protected void RemoveFromProcessFileList(string FN)
    {
      if (_processingFileList.Contains(FN))
      {
        _processingFileList.Remove(FN);
      }
    }
    #endregion

    #region FWS Eventhandler
    private void FSWatcher_Created(object sender, FileSystemEventArgs e)
    {
      if (!_processingFileList.Contains(e.FullPath))
      {
        //Add2ProcessFileList(e.FullPath);
        //CreateProcessThread(e.FullPath);
      }
      else
      {
        Loghelper.Write(LogLevel.Warning, e.FullPath + ": File exist in ProcessList!!");
      }
      CreateProcessThread(e.FullPath);
    }
    #endregion

    #region Func for Process file thread
    private void PostExecuteProcessFile(IWorkItemResult wir)
    {
      //((ProcessFileState)wir.State).Status = "Finish";
    }
    private void CreateProcessThread(string processFileName)
    {
      if (_processFileWIG == null)
      {
        _processFileWIG = this.GetThreadPool().CreateWorkItemsGroup(MaxProcessThreads);
      }
      _processFileWIG.QueueWorkItem(DoProcessFile, processFileName);
      //_processFileWIG.Start();
    }
    private void PrcessExistingFiles()
    {
      List<FileInfo> fsList;
      try
      {
        fsList = CommUtil.GetProcessFiles(this.InputUNC);
        if (fsList.Count >= 0)
        {
          foreach (var f in fsList)
          {
            if (!_processingFileList.Contains(f.FullName))
            {
              _processingFileList.Add(f.FullName);
              CreateProcessThread(f.FullName);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Loghelper.Write(LogLevel.Error, ex.Message);
      }
      finally
      {
        fsList = null;
      }
    }
    private void DoProcessFile(string processFN)
    {
      Add2ProcessFileList(processFN);
      int _tickstart = Environment.TickCount;
      int _processTimeofReadfile = -1;
      int _processTimeofProcedure = -1;
      int _processTimeofOutput = -1;
      int _processTime = -1;
      int _tryTimes = 1;
      string _errormsg = "";
      if (processFN != "")
      {
        try
        {
          _tryTimes = CommUtil.InputFileTryTimes;
          // check file lock
          CommUtil.CheckFileLock(processFN, ref _tryTimes);
          // read file content
          var textLines = ReadFilelines(processFN);
          // remove file right after read content
          if (File.Exists(processFN))
          {
            File.Delete(processFN);
          }
          else
          {
            throw new Exception("File does not exist before try to delete");
          }
          _processTimeofReadfile = Environment.TickCount - _tickstart;
          _tickstart = Environment.TickCount;
          // get SN#
          var _snnumber = GetSNNumber(processFN);
          //process file content and get output values
          string[] outputValues = GetOutputValues(_snnumber, textLines);
          _processTimeofProcedure = Environment.TickCount - _tickstart;
          _tickstart = Environment.TickCount;
          // get output file name from output parameter "COMMAND1" 1st segment
          var outputFN = outputValues[0].Split(';')[0];
          // remove filename from returned parameters
          outputValues[0] = outputValues[0].Substring(outputValues[0].IndexOf(';') + 1);
          // connect outputvalues into one string
          var _outputvalues_s = String.Join("", outputValues);
          outputValues = new string[] { _outputvalues_s };
          // output to file
          if (outputFN != "")
          {
            if (CommUtil.WritetoFile(Path.Combine(this.OutputUNC, outputFN + ".sf"), outputValues))
            {
              // remove file
              //File.Delete(processFN);
            }
          }
          else
          {
            textLines = null;
            outputValues = null;
            throw new Exception("Output FileName is Empty!!");
          }
          textLines = null;
          outputValues = null;
          _processTimeofOutput = Environment.TickCount - _tickstart;
        }
        catch (Exception ex)
        {
          _errormsg = ":Error:" + ex.Message;
        }
      }
      _processTime = _processTimeofReadfile + _processTimeofProcedure + _processTimeofOutput;
      #region write log
      if (Loghelper.IfneedTestLog)
      {
        Loghelper.SetThreadContext(this.Id, processFN, _tryTimes, _processTimeofOutput, _processTimeofProcedure, _processTimeofOutput);  
      }
      string _log = String.Format("{0}{1}:{2}|{3}|{4}|{5}|{6}", processFN, _errormsg, _tryTimes, _processTimeofReadfile, _processTimeofProcedure, _processTimeofOutput, _processTime);
      var _loglevel = (_errormsg != "") ? LogLevel.Error : ((_tryTimes > 1) ? LogLevel.Warning : LogLevel.Info);
      Loghelper.Write(_loglevel, _log);
      #endregion
      // ** remove file from ProcessingFileList
      RemoveFromProcessFileList(processFN);
      //return null;
    }
    #endregion

    #region INotifyPropertyChanged implement
    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(string name)
    {
      if (PropertyChanged != null)
      {
        //this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        PropertyChanged(this, new PropertyChangedEventArgs(name));
      }
    }
    #endregion

    #region IDisposable implement
    public void Dispose()
    {
      if (_sMTPool != null)
      {
        _sMTPool.Shutdown();
        _sMTPool = null;
      }
      if ((_processingFileList != null))
      {
        _processingFileList = null;
      }
    }
    #endregion
  }
}