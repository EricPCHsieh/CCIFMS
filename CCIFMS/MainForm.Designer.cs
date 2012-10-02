using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace CCIFMS
{
  partial class MainForm
  {
    /// <summary>
    /// 設計工具所需的變數。
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// 清除任何使用中的資源。
    /// </summary>
    /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form 設計工具產生的程式碼

    /// <summary>
    /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
    /// 修改這個方法的內容。
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.ColumnScanAction = new System.Windows.Forms.DataGridViewLinkColumn();
      this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnFilesInProcessList = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnInputUNC = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnOutputUNC = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnStoreProcedure = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnFMSOObjClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnMaxProcessThreads = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ColumnCurrentProcessThreads = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
      this.MainStripMenuItem_Action = new System.Windows.Forms.ToolStripMenuItem();
      this.MainStripMenuItem_StartAll = new System.Windows.Forms.ToolStripMenuItem();
      this.MainStripMenuItem_StopAll = new System.Windows.Forms.ToolStripMenuItem();
      this.MainStripMenuItem_Setting = new System.Windows.Forms.ToolStripMenuItem();
      this.loadingSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.testingFuncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.generateTestingFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.addItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
      this.timer_refreshGrid = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.MainMenuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToOrderColumns = true;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnScanAction,
            this.ColumnId,
            this.ColumnFilesInProcessList,
            this.ColumnInputUNC,
            this.ColumnOutputUNC,
            this.ColumnStoreProcedure,
            this.ColumnFMSOObjClassName,
            this.ColumnMaxProcessThreads,
            this.ColumnCurrentProcessThreads});
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dataGridView1.Location = new System.Drawing.Point(0, 24);
      this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dataGridView1.RowTemplate.Height = 24;
      this.dataGridView1.Size = new System.Drawing.Size(877, 140);
      this.dataGridView1.TabIndex = 1;
      this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
      this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
      this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
      // 
      // ColumnScanAction
      // 
      this.ColumnScanAction.DataPropertyName = "ScanFileThreadAction";
      this.ColumnScanAction.Frozen = true;
      this.ColumnScanAction.HeaderText = "Action";
      this.ColumnScanAction.Name = "ColumnScanAction";
      this.ColumnScanAction.Text = "";
      this.ColumnScanAction.Width = 50;
      // 
      // ColumnId
      // 
      this.ColumnId.DataPropertyName = "Id";
      this.ColumnId.Frozen = true;
      this.ColumnId.HeaderText = "ID";
      this.ColumnId.Name = "ColumnId";
      this.ColumnId.Width = 40;
      // 
      // ColumnFilesInProcessList
      // 
      this.ColumnFilesInProcessList.DataPropertyName = "FilesInProcessing";
      this.ColumnFilesInProcessList.Frozen = true;
      this.ColumnFilesInProcessList.HeaderText = "PF";
      this.ColumnFilesInProcessList.Name = "ColumnFilesInProcessList";
      this.ColumnFilesInProcessList.ReadOnly = true;
      this.ColumnFilesInProcessList.Width = 40;
      // 
      // ColumnInputUNC
      // 
      this.ColumnInputUNC.DataPropertyName = "InputUNC";
      this.ColumnInputUNC.Frozen = true;
      this.ColumnInputUNC.HeaderText = "InputUNC";
      this.ColumnInputUNC.Name = "ColumnInputUNC";
      // 
      // ColumnOutputUNC
      // 
      this.ColumnOutputUNC.DataPropertyName = "OutputUNC";
      this.ColumnOutputUNC.Frozen = true;
      this.ColumnOutputUNC.HeaderText = "OutputUNC";
      this.ColumnOutputUNC.Name = "ColumnOutputUNC";
      // 
      // ColumnStoreProcedure
      // 
      this.ColumnStoreProcedure.DataPropertyName = "ProcedureName";
      this.ColumnStoreProcedure.Frozen = true;
      this.ColumnStoreProcedure.HeaderText = "Procedure";
      this.ColumnStoreProcedure.Name = "ColumnStoreProcedure";
      // 
      // ColumnFMSOObjClassName
      // 
      this.ColumnFMSOObjClassName.DataPropertyName = "FMSOObjClassName";
      this.ColumnFMSOObjClassName.HeaderText = "ClassName";
      this.ColumnFMSOObjClassName.Name = "ColumnFMSOObjClassName";
      // 
      // ColumnMaxProcessThreads
      // 
      this.ColumnMaxProcessThreads.DataPropertyName = "MaxProcessThreads";
      this.ColumnMaxProcessThreads.HeaderText = "Max";
      this.ColumnMaxProcessThreads.Name = "ColumnMaxProcessThreads";
      this.ColumnMaxProcessThreads.Width = 50;
      // 
      // ColumnCurrentProcessThreads
      // 
      this.ColumnCurrentProcessThreads.DataPropertyName = "CurrentProcessThreads";
      this.ColumnCurrentProcessThreads.HeaderText = "Cur";
      this.ColumnCurrentProcessThreads.Name = "ColumnCurrentProcessThreads";
      this.ColumnCurrentProcessThreads.ReadOnly = true;
      this.ColumnCurrentProcessThreads.Width = 50;
      // 
      // MainMenuStrip
      // 
      this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainStripMenuItem_Action,
            this.MainStripMenuItem_Setting,
            this.testingFuncToolStripMenuItem,
            this.addItemToolStripMenuItem});
      this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
      this.MainMenuStrip.Name = "MainMenuStrip";
      this.MainMenuStrip.Size = new System.Drawing.Size(877, 24);
      this.MainMenuStrip.TabIndex = 5;
      this.MainMenuStrip.Text = "menuStrip1";
      // 
      // MainStripMenuItem_Action
      // 
      this.MainStripMenuItem_Action.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainStripMenuItem_StartAll,
            this.MainStripMenuItem_StopAll});
      this.MainStripMenuItem_Action.Name = "MainStripMenuItem_Action";
      this.MainStripMenuItem_Action.Size = new System.Drawing.Size(49, 20);
      this.MainStripMenuItem_Action.Text = "Action";
      // 
      // MainStripMenuItem_StartAll
      // 
      this.MainStripMenuItem_StartAll.Name = "MainStripMenuItem_StartAll";
      this.MainStripMenuItem_StartAll.Size = new System.Drawing.Size(112, 22);
      this.MainStripMenuItem_StartAll.Text = "Start All";
      this.MainStripMenuItem_StartAll.Click += new System.EventHandler(this.MainStripMenuItem_StartAll_Click);
      // 
      // MainStripMenuItem_StopAll
      // 
      this.MainStripMenuItem_StopAll.Name = "MainStripMenuItem_StopAll";
      this.MainStripMenuItem_StopAll.Size = new System.Drawing.Size(112, 22);
      this.MainStripMenuItem_StopAll.Text = "Stop All";
      this.MainStripMenuItem_StopAll.Click += new System.EventHandler(this.MainStripMenuItem_StopAll_Click);
      // 
      // MainStripMenuItem_Setting
      // 
      this.MainStripMenuItem_Setting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadingSettingToolStripMenuItem,
            this.saveSettingToolStripMenuItem});
      this.MainStripMenuItem_Setting.Name = "MainStripMenuItem_Setting";
      this.MainStripMenuItem_Setting.Size = new System.Drawing.Size(53, 20);
      this.MainStripMenuItem_Setting.Text = "Setting";
      // 
      // loadingSettingToolStripMenuItem
      // 
      this.loadingSettingToolStripMenuItem.Name = "loadingSettingToolStripMenuItem";
      this.loadingSettingToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.loadingSettingToolStripMenuItem.Text = "Load Setting";
      this.loadingSettingToolStripMenuItem.Click += new System.EventHandler(this.loadingSettingToolStripMenuItem_Click);
      // 
      // saveSettingToolStripMenuItem
      // 
      this.saveSettingToolStripMenuItem.Name = "saveSettingToolStripMenuItem";
      this.saveSettingToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.saveSettingToolStripMenuItem.Text = "Save Setting";
      this.saveSettingToolStripMenuItem.Click += new System.EventHandler(this.saveSettingToolStripMenuItem_Click);
      // 
      // testingFuncToolStripMenuItem
      // 
      this.testingFuncToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateTestingFilesToolStripMenuItem});
      this.testingFuncToolStripMenuItem.Enabled = false;
      this.testingFuncToolStripMenuItem.Name = "testingFuncToolStripMenuItem";
      this.testingFuncToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
      this.testingFuncToolStripMenuItem.Text = "Testing func";
      // 
      // generateTestingFilesToolStripMenuItem
      // 
      this.generateTestingFilesToolStripMenuItem.Name = "generateTestingFilesToolStripMenuItem";
      this.generateTestingFilesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
      this.generateTestingFilesToolStripMenuItem.Text = "Generate Testing files";
      this.generateTestingFilesToolStripMenuItem.Click += new System.EventHandler(this.generateTestingFilesToolStripMenuItem_Click);
      // 
      // addItemToolStripMenuItem
      // 
      this.addItemToolStripMenuItem.Name = "addItemToolStripMenuItem";
      this.addItemToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
      this.addItemToolStripMenuItem.Text = "Add Item";
      this.addItemToolStripMenuItem.Click += new System.EventHandler(this.addItemToolStripMenuItem_Click);
      // 
      // richTextBoxLog
      // 
      this.richTextBoxLog.DetectUrls = false;
      this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.richTextBoxLog.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.richTextBoxLog.Location = new System.Drawing.Point(0, 164);
      this.richTextBoxLog.Name = "richTextBoxLog";
      this.richTextBoxLog.ReadOnly = true;
      this.richTextBoxLog.Size = new System.Drawing.Size(877, 210);
      this.richTextBoxLog.TabIndex = 7;
      this.richTextBoxLog.Text = "";
      this.richTextBoxLog.WordWrap = false;
      // 
      // timer_refreshGrid
      // 
      this.timer_refreshGrid.Enabled = true;
      this.timer_refreshGrid.Interval = 1000;
      this.timer_refreshGrid.Tick += new System.EventHandler(this.timer_refreshGrid_Tick);
      // 
      // MainForm
      // 
      this.ClientSize = new System.Drawing.Size(877, 374);
      this.Controls.Add(this.richTextBoxLog);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.MainMenuStrip);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.Name = "MainForm";
      this.Text = "CCI SFIS TMO Service...";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.MainMenuStrip.ResumeLayout(false);
      this.MainMenuStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private DataGridView dataGridView1;
    private MenuStrip MainMenuStrip;
    private ToolStripMenuItem MainStripMenuItem_Setting;
    private ToolStripMenuItem loadingSettingToolStripMenuItem;
    private ToolStripMenuItem testingFuncToolStripMenuItem;
    private ToolStripMenuItem generateTestingFilesToolStripMenuItem;
    private ToolStripMenuItem saveSettingToolStripMenuItem;
    private RichTextBox richTextBoxLog;
    private Timer timer_refreshGrid;
    private DataGridViewLinkColumn ColumnScanAction;
    private DataGridViewTextBoxColumn ColumnId;
    private DataGridViewTextBoxColumn ColumnFilesInProcessList;
    private DataGridViewTextBoxColumn ColumnInputUNC;
    private DataGridViewTextBoxColumn ColumnOutputUNC;
    private DataGridViewTextBoxColumn ColumnStoreProcedure;
    private DataGridViewTextBoxColumn ColumnFMSOObjClassName;
    private DataGridViewTextBoxColumn ColumnMaxProcessThreads;
    private DataGridViewTextBoxColumn ColumnCurrentProcessThreads;
    private ToolStripMenuItem MainStripMenuItem_Action;
    private ToolStripMenuItem MainStripMenuItem_StartAll;
    private ToolStripMenuItem MainStripMenuItem_StopAll;
    private ToolStripMenuItem addItemToolStripMenuItem;
  }
}

