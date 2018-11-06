// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.MyServices.Internal.WebClientCopy
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Ported.VisualBasic.MyServices.Internal
{
  internal class WebClientCopy
  {
    [AccessedThroughProperty("m_WebClient")]
    private WebClient _m_WebClient;
    [AccessedThroughProperty("m_ProgressDialog")]
    private ProgressDialog _m_ProgressDialog;
    private Exception m_ExceptionEncounteredDuringFileTransfer;
    private int m_Percentage;

    public WebClientCopy(WebClient client, ProgressDialog dialog)
    {
      this.m_Percentage = 0;
      this.m_WebClient = client;
      this.m_ProgressDialog = dialog;
    }

    public void DownloadFile(Uri address, string destinationFileName)
    {
      if (this.m_ProgressDialog != null)
      {
        this.m_WebClient.DownloadFileAsync(address, destinationFileName);
        this.m_ProgressDialog.ShowProgressDialog();
      }
      else
        this.m_WebClient.DownloadFile(address, destinationFileName);
      if (this.m_ExceptionEncounteredDuringFileTransfer != null && (this.m_ProgressDialog == null || !this.m_ProgressDialog.UserCanceledTheDialog))
        throw this.m_ExceptionEncounteredDuringFileTransfer;
    }

    public void UploadFile(string sourceFileName, Uri address)
    {
      if (this.m_ProgressDialog != null)
      {
        this.m_WebClient.UploadFileAsync(address, sourceFileName);
        this.m_ProgressDialog.ShowProgressDialog();
      }
      else
        this.m_WebClient.UploadFile(address, sourceFileName);
      if (this.m_ExceptionEncounteredDuringFileTransfer != null && (this.m_ProgressDialog == null || !this.m_ProgressDialog.UserCanceledTheDialog))
        throw this.m_ExceptionEncounteredDuringFileTransfer;
    }

    private void InvokeIncrement(int progressPercentage)
    {
      if (this.m_ProgressDialog == null || !this.m_ProgressDialog.IsHandleCreated)
        return;
      int num = checked (progressPercentage - this.m_Percentage);
      this.m_Percentage = progressPercentage;
      if (num <= 0)
        return;
      this.m_ProgressDialog.BeginInvoke((Delegate) new WebClientCopy.DoIncrement(this.m_ProgressDialog.Increment), (object) num);
    }

    private void CloseProgressDialog()
    {
      if (this.m_ProgressDialog == null)
        return;
      this.m_ProgressDialog.IndicateClosing();
      if (this.m_ProgressDialog.IsHandleCreated)
        this.m_ProgressDialog.BeginInvoke((Delegate) new MethodInvoker(this.m_ProgressDialog.CloseDialog));
      else
        this.m_ProgressDialog.Close();
    }

    private void m_WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
      try
      {
        if (e.Error != null)
          this.m_ExceptionEncounteredDuringFileTransfer = e.Error;
        if (e.Cancelled || e.Error != null)
          return;
        this.InvokeIncrement(100);
      }
      finally
      {
        this.CloseProgressDialog();
      }
    }

    private void m_WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
      this.InvokeIncrement(e.ProgressPercentage);
    }

    private void m_WebClient_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
    {
      try
      {
        if (e.Error != null)
          this.m_ExceptionEncounteredDuringFileTransfer = e.Error;
        if (e.Cancelled || e.Error != null)
          return;
        this.InvokeIncrement(100);
      }
      finally
      {
        this.CloseProgressDialog();
      }
    }

    private void m_WebClient_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
    {
      this.InvokeIncrement(checked ((int) unchecked (checked (e.BytesSent * 100L) / e.TotalBytesToSend)));
    }

    private void m_ProgressDialog_UserCancelledEvent()
    {
      this.m_WebClient.CancelAsync();
    }

    public virtual WebClient m_WebClient
    {
      get
      {
        return this._m_WebClient;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        if (this._m_WebClient != null)
        {
          this._m_WebClient.DownloadProgressChanged -= new DownloadProgressChangedEventHandler(this.m_WebClient_DownloadProgressChanged);
          this._m_WebClient.DownloadFileCompleted -= new AsyncCompletedEventHandler(this.m_WebClient_DownloadFileCompleted);
          this._m_WebClient.UploadProgressChanged -= new UploadProgressChangedEventHandler(this.m_WebClient_UploadProgressChanged);
          this._m_WebClient.UploadFileCompleted -= new UploadFileCompletedEventHandler(this.m_WebClient_UploadFileCompleted);
        }
        this._m_WebClient = value;
        if (this._m_WebClient == null)
          return;
        this._m_WebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.m_WebClient_DownloadProgressChanged);
        this._m_WebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.m_WebClient_DownloadFileCompleted);
        this._m_WebClient.UploadProgressChanged += new UploadProgressChangedEventHandler(this.m_WebClient_UploadProgressChanged);
        this._m_WebClient.UploadFileCompleted += new UploadFileCompletedEventHandler(this.m_WebClient_UploadFileCompleted);
      }
    }

    public virtual ProgressDialog m_ProgressDialog
    {
      get
      {
        return this._m_ProgressDialog;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        if (this._m_ProgressDialog != null)
          this._m_ProgressDialog.UserHitCancel -= new ProgressDialog.UserHitCancelEventHandler(this.m_ProgressDialog_UserCancelledEvent);
        this._m_ProgressDialog = value;
        if (this._m_ProgressDialog == null)
          return;
        this._m_ProgressDialog.UserHitCancel += new ProgressDialog.UserHitCancelEventHandler(this.m_ProgressDialog_UserCancelledEvent);
      }
    }

    private delegate void DoIncrement(int Increment);
  }
}

*/