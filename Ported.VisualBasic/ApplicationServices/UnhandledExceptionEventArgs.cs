// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides data for the <see langword="My.Application.UnhandledException" /> event. </summary>
  [ComVisible(false)]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public class UnhandledExceptionEventArgs : ThreadExceptionEventArgs
  {
    private bool m_ExitApplication;

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs" /> class.</summary>
    /// <param name="exitApplication">A <see cref="T:System.Boolean" /> that indicates whether the application should exit upon exiting the exception handler.</param>
    /// <param name="exception">The <see cref="T:System.Exception" /> that occurred. </param>
    public UnhandledExceptionEventArgs(bool exitApplication, Exception exception)
      : base(exception)
    {
      this.m_ExitApplication = exitApplication;
    }

    /// <summary>Indicates whether the application should exit upon exiting the exception handler.</summary>
    /// <returns>A <see cref="T:System.Boolean" /> that indicates whether the application should exit upon exiting the exception handler.</returns>
    public bool ExitApplication
    {
      get
      {
        return this.m_ExitApplication;
      }
      set
      {
        this.m_ExitApplication = value;
      }
    }
  }
}
