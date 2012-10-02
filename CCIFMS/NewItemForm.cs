using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CCIFMS
{
  public partial class NewItemForm : Form
  {
    #region public member

    public string Id
    {
      get;
      set;
    }
    public string InputUNC
    {
      get;
      set;
    }
    public string OutputUNC
    {
      get;
      set;
    }
    public string StoreProcedure
    {
      get;
      set;
    }
    public string FMSClassName
    {
      get;
      set;
    }
    public int MaxThread
    {
      get;
      set;
    }
    #endregion

    private List<string> fmsTypeList;

    private List<string> GetFMSObjList()
    {
      var result = new List<string>();
      Type tF = typeof(FMSbaseObj);
      Assembly a = tF.Assembly;
      foreach (var t in a.GetTypes())
      {
        if (t.IsSubclassOf(typeof(FMSbaseObj)))
        {
          result.Add(t.Name);
        }
      }
      return result;
    }

    private bool ValidateForm()
    {
      bool result = true;
      if (txt_ID.Text.Trim().Length == 0)
      {
        errorProviderNeItem.SetError(txt_ID, "required!");
        result = false;
      }
      else
      {
        errorProviderNeItem.SetError(txt_ID, "");
      }
      if (txt_InputUNC.Text.Trim().Length == 0)
      {
        errorProviderNeItem.SetError(txt_InputUNC, "required!");
        result = false;
      }
      else
      {
        errorProviderNeItem.SetError(txt_InputUNC, "");
      }
      if (txt_OutputUNC.Text.Trim().Length == 0)
      {
        errorProviderNeItem.SetError(txt_OutputUNC, "required!");
        result = false;
      }
      else
      {
        errorProviderNeItem.SetError(txt_OutputUNC, "");
      }
      if (txt_Storeprocedure.Text.Trim().Length == 0)
      {
        errorProviderNeItem.SetError(txt_Storeprocedure, "required!");
        result = false;
      }
      else
      {
        errorProviderNeItem.SetError(txt_Storeprocedure, "");
      }
      if (cmb_Classname.Text.Trim().Length == 0)
      {
        errorProviderNeItem.SetError(cmb_Classname, "required!");
        result = false;
      }
      else
      {
        errorProviderNeItem.SetError(cmb_Classname, "");
      }
      if (nUD_Max.Value < 1)
      {
        errorProviderNeItem.SetError(nUD_Max, "must > 1 !");
        result = false;
      }
      else
      {
        errorProviderNeItem.SetError(nUD_Max, "");
      }
      return result;
    }

    public NewItemForm()
    {
      InitializeComponent();
      fmsTypeList = GetFMSObjList();
      cmb_Classname.Items.AddRange(fmsTypeList.ToArray());
    }

    private void NewItemForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.OK)
      {
        e.Cancel = !ValidateForm();
        this.Id = txt_ID.Text;
        this.InputUNC = txt_InputUNC.Text;
        this.OutputUNC = txt_OutputUNC.Text;
        this.StoreProcedure = txt_Storeprocedure.Text;
        this.FMSClassName = cmb_Classname.Text;
        this.MaxThread = (int)nUD_Max.Value;
      }

    }
  }
}
