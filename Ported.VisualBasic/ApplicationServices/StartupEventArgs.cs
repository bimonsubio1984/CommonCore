// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.StartupEventArgs
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides data for the <see langword="My.Application.Startup" /> event. </summary>
  [ComVisible(false)]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public class StartupEventArgs : CancelEventArgs
  {
    private ReadOnlyCollection<string> m_CommandLine;

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.StartupEventArgs" /> class.</summary>
    /// <param name="args">A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> object that contains the command-line arguments of the application.</param>
    public StartupEventArgs(ReadOnlyCollection<string> args)
    {
      if (args == null)
        args = new ReadOnlyCollection<string>((IList<string>) null);
      this.m_CommandLine = args;
    }

    /// <summary>Gets the command-line arguments of the application.</summary>
    /// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> object that contains the command-line arguments of the application.</returns>
    public ReadOnlyCollection<string> CommandLine
    {
      get
      {
        return this.m_CommandLine;
      }
    }
  }
}
