namespace CCIFMS
{
  partial class NewItemForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.txt_Storeprocedure = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txt_OutputUNC = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txt_InputUNC = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txt_ID = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.nUD_Max = new System.Windows.Forms.NumericUpDown();
      this.errorProviderNeItem = new System.Windows.Forms.ErrorProvider(this.components);
      this.cmb_Classname = new System.Windows.Forms.ComboBox();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nUD_Max)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.errorProviderNeItem)).BeginInit();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cmb_Classname);
      this.groupBox1.Controls.Add(this.nUD_Max);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Controls.Add(this.label5);
      this.groupBox1.Controls.Add(this.txt_Storeprocedure);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.txt_OutputUNC);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.txt_InputUNC);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.txt_ID);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F);
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(475, 208);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Item Information";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(14, 173);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(35, 13);
      this.label6.TabIndex = 7;
      this.label6.Text = "Max:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(14, 147);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(76, 13);
      this.label5.TabIndex = 5;
      this.label5.Text = "ClassName:";
      // 
      // txt_Storeprocedure
      // 
      this.txt_Storeprocedure.AcceptsReturn = true;
      this.txt_Storeprocedure.AcceptsTab = true;
      this.txt_Storeprocedure.Location = new System.Drawing.Point(100, 114);
      this.txt_Storeprocedure.Name = "txt_Storeprocedure";
      this.txt_Storeprocedure.Size = new System.Drawing.Size(348, 21);
      this.txt_Storeprocedure.TabIndex = 4;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(14, 117);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(70, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Procedure:";
      // 
      // txt_OutputUNC
      // 
      this.txt_OutputUNC.AcceptsReturn = true;
      this.txt_OutputUNC.AcceptsTab = true;
      this.txt_OutputUNC.Location = new System.Drawing.Point(100, 83);
      this.txt_OutputUNC.Name = "txt_OutputUNC";
      this.txt_OutputUNC.Size = new System.Drawing.Size(348, 21);
      this.txt_OutputUNC.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(14, 87);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(75, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "OutputUNC:";
      // 
      // txt_InputUNC
      // 
      this.txt_InputUNC.AcceptsReturn = true;
      this.txt_InputUNC.AcceptsTab = true;
      this.txt_InputUNC.Location = new System.Drawing.Point(100, 53);
      this.txt_InputUNC.Name = "txt_InputUNC";
      this.txt_InputUNC.Size = new System.Drawing.Size(348, 21);
      this.txt_InputUNC.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(67, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "InputUNC:";
      // 
      // txt_ID
      // 
      this.txt_ID.AcceptsReturn = true;
      this.txt_ID.AcceptsTab = true;
      this.txt_ID.Location = new System.Drawing.Point(100, 23);
      this.txt_ID.Name = "txt_ID";
      this.txt_ID.Size = new System.Drawing.Size(73, 21);
      this.txt_ID.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(14, 26);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(26, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "ID:";
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(17, 215);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(87, 25);
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(110, 215);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(87, 25);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // nUD_Max
      // 
      this.nUD_Max.Location = new System.Drawing.Point(100, 171);
      this.nUD_Max.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nUD_Max.Name = "nUD_Max";
      this.nUD_Max.Size = new System.Drawing.Size(60, 21);
      this.nUD_Max.TabIndex = 6;
      this.nUD_Max.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // errorProviderNeItem
      // 
      this.errorProviderNeItem.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
      this.errorProviderNeItem.ContainerControl = this;
      // 
      // cmb_Classname
      // 
      this.cmb_Classname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmb_Classname.FormattingEnabled = true;
      this.cmb_Classname.Location = new System.Drawing.Point(100, 144);
      this.cmb_Classname.Name = "cmb_Classname";
      this.cmb_Classname.Size = new System.Drawing.Size(347, 21);
      this.cmb_Classname.TabIndex = 5;
      // 
      // NewItemForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(475, 251);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.groupBox1);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "NewItemForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "NewItemForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewItemForm_FormClosing);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nUD_Max)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.errorProviderNeItem)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txt_Storeprocedure;
    private System.Windows.Forms.TextBox txt_OutputUNC;
    private System.Windows.Forms.TextBox txt_InputUNC;
    private System.Windows.Forms.TextBox txt_ID;
    private System.Windows.Forms.NumericUpDown nUD_Max;
    private System.Windows.Forms.ErrorProvider errorProviderNeItem;
    private System.Windows.Forms.ComboBox cmb_Classname;

  }
}