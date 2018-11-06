// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Devices.WebClientExtended
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.Net;

namespace Ported.VisualBasic.Devices
{
  internal class WebClientExtended : WebClient
  {
    private int m_Timeout;
    private bool m_UseNonPassiveFtp;

    public WebClientExtended()
    {
      this.m_Timeout = 100000;
    }

    public int Timeout
    {
      set
      {
        this.m_Timeout = value;
      }
    }

    public bool UseNonPassiveFtp
    {
      set
      {
        this.m_UseNonPassiveFtp = value;
      }
    }

    protected override WebRequest GetWebRequest(Uri address)
    {
      WebRequest webRequest = base.GetWebRequest(address);
      if (webRequest != null)
      {
        webRequest.Timeout = this.m_Timeout;
        if (this.m_UseNonPassiveFtp)
        {
          FtpWebRequest ftpWebRequest = webRequest as FtpWebRequest;
          if (ftpWebRequest != null)
            ftpWebRequest.UsePassive = false;
        }
        HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
        if (httpWebRequest != null)
          httpWebRequest.AllowAutoRedirect = false;
      }
      return webRequest;
    }
  }
}
