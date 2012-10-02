using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace CCIFMS
{
  public enum LogLevel
  {
    Debug = 0,
    Error = 1,
    Fatal = 2,
    Info = 3,
    Warning = 4
  }

  /// <summary>
  /// Write out messages using the logging provider.
  /// </summary>
  static class Loghelper
  {
    #region Members
    private static readonly ILog _logger = log4net.LogManager.GetLogger("stdlog");
    private static Dictionary<LogLevel, Action<string>> _actions;
    public static bool IfneedTestLog = false;
    #endregion

    /// <summary>
    /// Static instance of the log manager.
    /// </summary>
    static Loghelper()
    {
      //XmlConfigurator.Configure();
      _actions = new Dictionary<LogLevel, Action<string>>();
      _actions.Add(LogLevel.Debug, WriteDebug);
      _actions.Add(LogLevel.Error, WriteError);
      _actions.Add(LogLevel.Fatal, WriteFatal);
      _actions.Add(LogLevel.Info, WriteInfo);
      _actions.Add(LogLevel.Warning, WriteWarning);
    }

    /// <summary>
    /// Write the message to the appropriate log based on the relevant log level.
    /// </summary>
    /// <param name="level">The log level to be used.</param>
    /// <param name="message">The message to be written.</param>
    /// <exception cref="ArgumentNullException">Thrown if the message is empty.</exception>
    public static void Write(LogLevel level, string message)
    {
      if (!string.IsNullOrEmpty(message))
      {
        if (level > LogLevel.Warning || level < LogLevel.Debug)
          throw new ArgumentOutOfRangeException("level");

        // Now call the appropriate log level message.
        _actions[level](message);
      }
    }

    #region Action methods
    private static void WriteDebug(string message)
    {
      if (_logger.IsDebugEnabled)
        _logger.Debug(message);
    }

    private static void WriteError(string message)
    {
      if (_logger.IsErrorEnabled)
        _logger.Error(message);
    }

    private static void WriteFatal(string message)
    {
      if (_logger.IsFatalEnabled)
        _logger.Fatal(message);
    }

    private static void WriteInfo(string message)
    {
      if (_logger.IsInfoEnabled)
        _logger.Info(message);
    }

    private static void WriteWarning(string message)
    {
      if (_logger.IsWarnEnabled)
        _logger.Warn(message);
    }
    #endregion

    public static void SetThreadContext(string FMSObjId, string processFN, int tryTimes, int _processTimeofReadfile, int _processTimeofProcedure, int _processTimeofOutput)
    {
      log4net.ThreadContext.Properties["FMSObjID"] = FMSObjId;
      log4net.ThreadContext.Properties["FileName"] = processFN;
      log4net.ThreadContext.Properties["tryTimes"] = tryTimes;
      log4net.ThreadContext.Properties["ExecuteTime"] = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss,fffff");
      log4net.ThreadContext.Properties["processTimeofReadfile"] = _processTimeofReadfile;
      log4net.ThreadContext.Properties["processTimeofProcedure"] = _processTimeofProcedure;
      log4net.ThreadContext.Properties["processTimeofOutput"] = _processTimeofOutput;
    }

    public static bool CheckIfneedTestLog()
    {
      var result = false;
      IAppender[] appenders = LogManager.GetRepository().GetAppenders();
      foreach (IAppender appender in appenders)
      {
        if (appender.Name=="ADONetAppender")
        {
          result = true;
          break;
        }
      }
      return result;
    }
  }
}
