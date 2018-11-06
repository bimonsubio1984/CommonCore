// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.MyServices.Internal.ProgressDialog
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll


/*
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Ported.VisualBasic.MyServices.Internal
{
  internal class ProgressDialog : Form
  {
    private bool m_Closing;
    private bool m_Canceled;
    private ManualResetEvent m_FormClosableSemaphore;
    private bool m_CloseDialogInvoked;
    private const int WS_THICKFRAME = 262144;
    private const int BORDER_SIZE = 20;
    [AccessedThroughProperty("LabelInfo")]
    private Label _LabelInfo;
    [AccessedThroughProperty("ProgressBarWork")]
    private ProgressBar _ProgressBarWork;
    [AccessedThroughProperty("ButtonCloseDialog")]
    private Button _ButtonCloseDialog;
    private IContainer components;

    public event ProgressDialog.UserHitCancelEventHandler UserHitCancel;

    public ProgressDialog()
    {
      this.Shown += new EventHandler(this.ProgressDialog_Activated);
      this.FormClosing += new FormClosingEventHandler(this.ProgressDialog_FormClosing);
      this.Resize += new EventHandler(this.ProgressDialog_Resize);
      this.m_Canceled = false;
      this.m_FormClosableSemaphore = new ManualResetEvent(false);
      this.InitializeComponent();
    }

    public void Increment(int incrementAmount)
    {
      this.ProgressBarWork.Increment(incrementAmount);
    }

    public void CloseDialog()
    {
      this.m_CloseDialogInvoked = true;
      this.Close();
    }

    public void ShowProgressDialog()
    {
      try
      {
        if (this.m_Closing)
          return;
        int num = (int) this.ShowDialog();
      }
      finally
      {
        this.FormClosableSemaphore.Set();
      }
    }

    public string LabelText
    {
      get
      {
        return this.LabelInfo.Text;
      }
      set
      {
        this.LabelInfo.Text = value;
      }
    }

    public ManualResetEvent FormClosableSemaphore
    {
      get
      {
        return this.m_FormClosableSemaphore;
      }
    }

    public void IndicateClosing()
    {
      this.m_Closing = true;
    }

    public bool UserCanceledTheDialog
    {
      get
      {
        return this.m_Canceled;
      }
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.Style |= 262144;
        return createParams;
      }
    }

    private void ButtonCloseDialog_Click(object sender, EventArgs e)
    {
            throw new System.Exception("Not implemented!");
      
        }

    private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
            throw new System.Exception("Not implemented!");
          
    }

    private void ProgressDialog_Resize(object sender, EventArgs e)
    {
      this.LabelInfo.MaximumSize = new Size(checked (this.ClientSize.Width - 20), 0);
    }

    private void ProgressDialog_Activated(object sender, EventArgs e)
    {
      this.m_FormClosableSemaphore.Set();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    internal virtual Label LabelInfo
    {
      get
      {
        return this._LabelInfo;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        this._LabelInfo = value;
      }
    }

    internal virtual ProgressBar ProgressBarWork
    {
      get
      {
        return this._ProgressBarWork;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        this._ProgressBarWork = value;
      }
    }

    internal virtual Button ButtonCloseDialog
    {
      get
      {
        return this._ButtonCloseDialog;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        if (this._ButtonCloseDialog != null)
          this._ButtonCloseDialog.Click -= new EventHandler(this.ButtonCloseDialog_Click);
        this._ButtonCloseDialog = value;
        if (this._ButtonCloseDialog == null)
          return;
        this._ButtonCloseDialog.Click += new EventHandler(this.ButtonCloseDialog_Click);
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProgressDialog));
      this.LabelInfo = new Label();
      this.ProgressBarWork = new ProgressBar();
      this.ButtonCloseDialog = new Button();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.LabelInfo, "LabelInfo", CultureInfo.CurrentUICulture);
      this.LabelInfo.MaximumSize = new Size(300, 0);
      this.LabelInfo.Name = "LabelInfo";
      componentResourceManager.ApplyResources((object) this.ProgressBarWork, "ProgressBarWork", CultureInfo.CurrentUICulture);
      this.ProgressBarWork.Name = "ProgressBarWork";
      componentResourceManager.ApplyResources((object) this.ButtonCloseDialog, "ButtonCloseDialog", CultureInfo.CurrentUICulture);
      this.ButtonCloseDialog.Name = "ButtonCloseDialog";
      componentResourceManager.ApplyResources((object) this, "$this", CultureInfo.CurrentUICulture);
      this.Controls.Add((Control) this.ButtonCloseDialog);
      this.Controls.Add((Control) this.ProgressBarWork);
      this.Controls.Add((Control) this.LabelInfo);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ProgressDialog);
      this.ShowInTaskbar = false;
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public delegate void UserHitCancelEventHandler();
  }
}

*/