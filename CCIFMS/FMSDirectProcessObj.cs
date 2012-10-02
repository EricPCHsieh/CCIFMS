using System;
using System.Collections.Generic;
using System.IO;

namespace CCIFMS
{
  class FMSDirectProcessObj : FMSbaseObj
  {
    #region constructor
    public FMSDirectProcessObj()
      : base()
    {
    }
    #endregion

    private string	Get_W_SECTION(DateTime dt)
    {
      var _time = DateTime.Now.ToString("HHmm");
      var s =
        "Select WORK_SECTION WRKSec From SFIS1.C_WORK_DESC_T WHERE START_TIME <=:Time AND END_TIME >:Time AND LINE_NAME ='Default' AND SECTION_NAME = 'Default' AND SHIFT = '1'";
      var parameters = new Dictionary<string, object>();
      parameters.Add("Time", _time);
      var result = OraDBUtil.ExecuteSQL(s, parameters);
      parameters = null;
      return result.ToString();
    }

    /// <summary>
    /// Setup Oracle StoreParcedure Parameters
    /// </summary>
    /// <param name="snNumber">Serial Number </param>
    /// <param name="textLines">All lines read from Text file</param>
    /// <returns>return Dictionary with Oracle Store Procedure Parameter name and value</returns>
    private Dictionary<string, object> SetStoreProcParams(string snNumber, string[] textLines)
    {
      var result = new Dictionary<string, object>();
      var paramstr = textLines[0];
      result.Add("STATION_NUM", "");
      // add snFilename ahead of original value
      result.Add("DATA", snNumber + ";" + paramstr);
      result.Add("ID1", "");
      result.Add("PORT1", "");
      result.Add("DATETIME", DateTime.Now);
      result.Add("MO_DATE",DateTime.Now.ToString("yyyyMMdd"));
      result.Add("W_SECTION", Get_W_SECTION(DateTime.Now));
      result.Add("LINE", "");
      result.Add("SECTION", "");
      result.Add("W_STATION", "");
      result.Add("MYGROUP", "");
      result.Add("NEXTPROC", "");
      result.Add("SN", "");
      for (int i = 1; i <= 10; i++)
      {
        result.Add("COMMAND" + i, "");
      }
      return result;
    }

    /// <summary>
    /// After Execute Store Procedure , call this function get values for writing output file
    /// </summary>
    /// <param name="parameters">returned parameters after execute Oracle Store Procedure</param>
    /// <returns>valus for output file</returns>
    private string[] GetOutputStringtoWrite(Dictionary<string, object> parameters)
    {
      var result = new string[10];
      for (int i = 0; i < 10; i++)
      {
        if (parameters.ContainsKey("COMMAND" + (i + 1)))
        {
          result[i] = parameters["COMMAND" + (i + 1)].ToString();
        }
      }
      return result;
    }

    internal override string[] ReadFilelines(string processFN)
    {
      return File.ReadAllLines(processFN);
    }

    internal override string GetSNNumber(string processFN)
    {
      return Path.GetFileNameWithoutExtension(processFN);
    }

    internal override string[] GetOutputValues(string snnumber, string[] fileContent)
    {
      var returnParams = OraDBUtil.ExecuteStoreProc(this.ProcedureName, SetStoreProcParams(snnumber, fileContent));
      string[] outputValues = GetOutputStringtoWrite(returnParams);
      return outputValues;
    }

    //public void DoProcessFile(string processFN)
    //{
    //  Add2ProcessFileList(processFN);
    //  int _tickstart = Environment.TickCount;
    //  int _processTimeofReadfile = -1;
    //  int _processTimeofProcedure = -1;
    //  int _processTimeofOutput = -1;
    //  int _processTime = -1;
    //  int _tryTimes = 1;
    //  string _errormsg = "";
    //  if (processFN != "")
    //  {
    //    try
    //    {
    //      _tryTimes = 10;
    //      CommUtil.CheckFileLock(processFN, ref _tryTimes);
    //      var textLines = File.ReadAllLines(processFN);
    //      // remove file right after read content
    //      File.Delete(processFN);

    //      _processTimeofReadfile = Environment.TickCount - _tickstart;
    //      _tickstart = Environment.TickCount;
    //      //calling Oracle SP
    //      var _snnumber = Path.GetFileNameWithoutExtension(processFN);
    //      var returnParams = OraDBUtil.ExecuteStoreProc(this.ProcedureName, SetStoreProcParams(_snnumber, textLines));
    //      _processTimeofProcedure = Environment.TickCount - _tickstart;
    //      #region output to file.
    //      _tickstart = Environment.TickCount;
    //      string[] outputValues = GetOutputStringtoWrite(returnParams);
    //      // get output file name from output parameter "COMMAND1" 1st segment
    //      var outputFN = outputValues[0].Split(';')[0];
    //      if (outputFN != "")
    //      {
    //        if (CommUtil.WritetoFile(Path.Combine(this.OutputUNC, outputFN + ".sf"), outputValues))
    //        {
    //          // remove file
    //          //File.Delete(processFN);
    //        }
    //      }
    //      else
    //      {
    //        textLines = null;
    //        returnParams = null;
    //        outputValues = null;
    //        throw new Exception("Output FileName is Empty!!");
    //      }
    //      textLines = null;
    //      returnParams = null;
    //      outputValues = null;
    //      _processTimeofOutput = Environment.TickCount - _tickstart;
    //      #endregion
    //    }
    //    catch (Exception ex)
    //    {
    //      //throw;//new Exception();
    //      _errormsg = ":Error:" + ex.Message;
    //    }
    //  }
    //  _processTime = _processTimeofReadfile + _processTimeofProcedure + _processTimeofOutput;
    //  if (Loghelper.IfneedTestLog)
    //  {
    //    Loghelper.SetThreadContext(this.Id, processFN, _tryTimes, _processTimeofOutput, _processTimeofProcedure, _processTimeofOutput);  
    //  }
    //  string _log = String.Format("{0}{1}:{2}|{3}|{4}|{5}|{6}", processFN, _errormsg, _tryTimes, _processTimeofReadfile, _processTimeofProcedure, _processTimeofOutput, _processTime);
    //  var _loglevel = (_errormsg != "") ? LogLevel.Error : ((_tryTimes > 1) ? LogLevel.Warning : LogLevel.Info);
    //  Loghelper.Write(_loglevel, _log);
    //  // ** remove file from ProcessingFileList
    //  RemoveFromProcessFileList(processFN);
    //  //return null;
    //}
  }
}