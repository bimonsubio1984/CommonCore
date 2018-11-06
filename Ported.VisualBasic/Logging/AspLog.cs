// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Logging.AspLog
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System.Diagnostics;
using System.Security.Permissions;
using System.Web;

namespace Ported.VisualBasic.Logging
{
  /// <summary>Provides a property and methods for writing event and exception information to the application's log listeners.</summary>
  //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
  public class AspLog : Log
  {
    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.Logging.AspLog" /> class. </summary>
    public AspLog()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.Logging.AspLog" /> class. </summary>
    /// <param name="name">
    /// <see cref="T:System.String" />. The name to give to the <see cref="P:Ported.VisualBasic.Logging.Log.TraceSource" /> property object.</param>
    public AspLog(string name)
      : base(name)
    {
    }

    /// <summary>Creates a new <see cref="T:Ported.VisualBasic.Logging.FileLogTraceListener" /> and adds it to the <see cref="P:System.Diagnostics.TraceSource.Listeners" /> collection.</summary>
    protected internal override void InitializeWithDefaultsSinceNoConfigExists()
    {
      this.TraceSource.Listeners.Add((TraceListener) new WebPageTraceListener());
      this.TraceSource.Switch.Level = SourceLevels.Information;
    }
  }
}
*/