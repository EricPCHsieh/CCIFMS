using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace CCIFMS
{
  public partial class MainForm : Form
  {
    #region private member
    private ThreadedBindingList<FMSbaseObj> fMSObjs;
    BackgroundWorker backgroundworker;
    #endregion

    public MainForm()
    {
      InitializeComponent();
      RichTextBoxAppender.SetRichTextBox(richTextBoxLog, "RichTextBoxAppender");
      Loghelper.IfneedTestLog = Loghelper.CheckIfneedTestLog();
      LoadAppSetting();
    }

    #region config functions
    private string GetConfigFilePath()
    {
      var _tmoconfigfn = ConfigurationManager.AppSettings["TMOConfigXMLFileName"];
      if (_tmoconfigfn == "")
      {
        _tmoconfigfn = "TMOConfigurations.xml";
      }
      return Path.Combine(System.IO.Directory.GetCurrentDirectory(), _tmoconfigfn);
    }
    private XmlDocument LoadConfigDocument()
    {
      XmlDocument doc = null;
      try
      {
        doc = new XmlDocument();
        doc.Load((string)GetConfigFilePath());
      }
      catch (Exception e)
      {
        Loghelper.Write(LogLevel.Error, "No configuration file found." + e.Message);
      }
      return doc;
    }
    private ThreadedBindingList<FMSbaseObj> LoadSettingtoList()
    {
      ThreadedBindingList<FMSbaseObj> result = new ThreadedBindingList<FMSbaseObj>();
      XmlDocument doc;
      XmlNode node;
      try
      {// load config document for current assembly
        doc = LoadConfigDocument();
        // retrieve TMOConfigurations
        node = doc.SelectSingleNode("//TMOConfigurations");
        if (node != null)
        {
          foreach (XmlElement selectNode in node.SelectNodes("//TMOConfiguration"))
          {
            try
            {
              var _maxt = 1;
              if (!int.TryParse(selectNode.GetAttribute("MaxProcessThreads"), out _maxt))
              {
                _maxt = 1;
              }
              FMSbaseObj o = FMSbaseObj.CreateFMSObj(selectNode.GetAttribute("Id"), selectNode.GetAttribute("InputUNC"),
                                    selectNode.GetAttribute("OutputUNC"), selectNode.GetAttribute("StoreProcedure"), _maxt, selectNode.GetAttribute("Classname"));
              result.Add(o);
            }
            catch (Exception ex)
            {
              Loghelper.Write(LogLevel.Error, ex.Message);
            }
          }
        }
      }
      catch (Exception e)
      {
        Loghelper.Write(LogLevel.Error, e.Message);
      }
      finally
      {
        node = null;
        doc = null;
      }

      return result;
    }
    private void SaveListtoSetting()
    {
      XmlDocument doc = LoadConfigDocument();
      // retrieve TMOConfigurations
      XmlNode node = doc.SelectSingleNode("//TMOConfigurations");
      if (node == null)
      {
        node = doc.CreateElement("TMOConfigurations");
        doc.AppendChild(node);
      }
      else
      {
        node.RemoveAll();
      }
      foreach (var fmsObj in fMSObjs)
      {
        var elem = doc.CreateElement("TMOConfiguration");
        elem.SetAttribute("Id", fmsObj.Id);
        elem.SetAttribute("InputUNC", fmsObj.InputUNC);
        elem.SetAttribute("OutputUNC", fmsObj.OutputUNC);
        elem.SetAttribute("StoreProcedure", fmsObj.ProcedureName);
        elem.SetAttribute("MaxProcessThreads", fmsObj.MaxProcessThreads.ToString());
        elem.SetAttribute("Classname", fmsObj.FMSOObjClassName.ToString());
        node.AppendChild(elem);
      }
      doc.Save(GetConfigFilePath());
    }
    private void LoadAppSetting()
    {
      var _inputreadertrytimes = 0;
      if (!int.TryParse(ConfigurationManager.AppSettings["InputReaderTryTime"], out _inputreadertrytimes))
      {
        MessageBox.Show("Unable to reade [InputReaderTryTime] in setting! TryTimes is set to 15 as default", "CCIFMS",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
        _inputreadertrytimes = 15;
      }
      var _inputreadertryinterval = 0;
      if (!int.TryParse(ConfigurationManager.AppSettings["InputReaderTryInterval"], out _inputreadertryinterval))
      {
        MessageBox.Show("Unable to reade [InputReaderTryInterval] in setting! TryTimes is set to 100 as default", "CCIFMS",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
        _inputreadertryinterval = 100;
      }
      CommUtil.InputFileTryTimes = _inputreadertrytimes;
      CommUtil.InputFileTryInterval = _inputreadertryinterval;
    }
    #endregion

    #region FMSobj operation
    private void KickMonitorUNC(string ObjID)
    {
      foreach (var obj in fMSObjs)
      {
        if (obj.Id == ObjID)
        {
          obj.StartMonitorFolder();
        }
      }
    }
    private void StopMonitorUNC(string ObjID)
    {
      foreach (var obj in fMSObjs)
      {
        if (obj.Id == ObjID)
        {
          obj.StopMonitorFolder();
        }
      }
    }
    #endregion

    #region Form control event
    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (((DataGridView)sender).Columns[e.ColumnIndex].DataPropertyName.ToLower() == "scanfilethreadaction")
      {
        var _sAction = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        var _obj = ((FMSbaseObj)((DataGridView)sender).Rows[e.RowIndex].DataBoundItem);
        if (_sAction.ToUpper() == "START")
        {
          //string _id = ((System.Windows.Forms.DataGridView)(sender)).Rows[e.RowIndex].Cells["Id"].Value.ToString();
          KickMonitorUNC(_obj.Id);
          ((DataGridView)sender).Rows[e.RowIndex].ReadOnly = true;
        }
        else if (_sAction.ToUpper() == "STOP")
        {
          StopMonitorUNC(_obj.Id);
          ((DataGridView)sender).Rows[e.RowIndex].ReadOnly = false;
        }
      }
    }
    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (((DataGridView)sender).Columns[e.ColumnIndex].DataPropertyName.ToLower() == "ScanFileThreadAction")
      {
        var cell = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex];
        if (cell.Value.ToString() != "")
        {
          cell.Style.BackColor = Color.Red;
        }
      }
    }
    private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
    {
      if (e.RowIndex >= ((DataGridView)sender).Rows.Count - 1)
      {
        return;
      }

      if (((DataGridView)sender).Rows[e.RowIndex].Cells["ColumnScanAction"].Value.ToString() == "Stop")
      {
        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
      }
      else
      {
        ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
      }
    }
    private void loadingSettingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      fMSObjs = LoadSettingtoList();
      dataGridView1.AutoGenerateColumns = false;
      dataGridView1.DataSource = fMSObjs;
    }
    private void generateTestingFilesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (dataGridView1.SelectedCells.Count > 0)
      {
        var _obj = ((FMSbaseObj)(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].DataBoundItem));
        if (backgroundworker == null)
        {
          backgroundworker = new BackgroundWorker();
          backgroundworker.WorkerReportsProgress = true;
          backgroundworker.WorkerSupportsCancellation = true;
          backgroundworker.DoWork += new DoWorkEventHandler(backgroundworker_DoWork);
          //backgroundworker.ProgressChanged += new ProgressChangedEventHandler(backgroundworker_ProgressChanged);
          backgroundworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundworker_RunWorkerCompleted);
          backgroundworker.RunWorkerAsync(_obj.InputUNC);
        }
        //GenerateTestingFiles(_obj.InputUNC, 1000);
      }
    }
    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {

    }
    private void saveSettingToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveListtoSetting();
      //fMSObjs[0].MaxProcessThreads += 1;
    }
    private void timer_refreshGrid_Tick(object sender, EventArgs e)
    {
      dataGridView1.Refresh();
    }
    #endregion

    #region testing functions
    private void backgroundworker_DoWork(object sender, DoWorkEventArgs e)
    {
      GenerateTestingFiles(e.Argument.ToString(), 1000);
    }
    private void backgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      try
      {
        if (e.Error != null)
        {
          // Handle failure.
        }
      }
      finally
      {
        // Use wkr outer instead of casting.
        backgroundworker.Dispose();
        backgroundworker = null;
      }
    }
    private void GenerateTestingFiles(string targetUNC, int fileamount)
    {
      string _content = "{0};12A_A;BASEBAND_1;";
      string _sn = "";
      for (int i = 1; i <= fileamount; i++)
      {
        _sn = i.ToString().PadLeft(9, "0".ToCharArray()[0]);
        File.WriteAllText(Path.Combine(targetUNC, (_sn + ".txt")), String.Format(_content, _sn));
      }
    }
    #endregion

    #region Action function
    private void startAll()
    {
      if (fMSObjs==null || fMSObjs.Count<=0)
      {
        return;
      }
      foreach (var obj in fMSObjs)
      {
        if (obj.ScanFileThreadAction == "Start")
        {
          obj.StartMonitorFolder();
        }
      }
    }

    private void stopAll()
    {
      if (fMSObjs == null || fMSObjs.Count <= 0)
      {
        return;
      }
      foreach (var obj in fMSObjs)
      {
        if (obj.ScanFileThreadAction == "Stop")
        {
          obj.StopMonitorFolder();
        }
      }
    }
    #endregion

    private void MainStripMenuItem_StartAll_Click(object sender, EventArgs e)
    {
      startAll();
    }

    private void MainStripMenuItem_StopAll_Click(object sender, EventArgs e)
    {
      stopAll();
    }

    private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (fMSObjs == null)
      {
        MessageBox.Show("Please [Load Setting] first!");
        return;
      }
      using (NewItemForm f = new NewItemForm())
      {
        if (f.ShowDialog() == DialogResult.OK)
        {
          FMSbaseObj o = FMSbaseObj.CreateFMSObj(f.Id, f.InputUNC,f.OutputUNC, f.StoreProcedure, f.MaxThread, f.FMSClassName);
          fMSObjs.Add(o);
        }
      }
    }
  }
}
