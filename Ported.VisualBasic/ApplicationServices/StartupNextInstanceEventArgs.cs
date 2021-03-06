﻿// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>Provides data for the <see langword="My.Application.StartupNextInstance" /> event. </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public class StartupNextInstanceEventArgs : EventArgs
  {
    private bool m_BringToForeground;
    private ReadOnlyCollection<string> m_CommandLine;

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs" /> class.</summary>
    /// <param name="args">A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> object that contains the command-line arguments of the subsequent application instance.</param>
    /// <param name="bringToForegroundFlag">A <see cref="T:System.Boolean" /> that indicates whether the first application instance should be brought to the foreground upon exiting the exception handler.</param>
    public StartupNextInstanceEventArgs(ReadOnlyCollection<string> args, bool bringToForegroundFlag)
    {
      if (args == null)
        args = new ReadOnlyCollection<string>((IList<string>) null);
      this.m_CommandLine = args;
      this.m_BringToForeground = bringToForegroundFlag;
    }

    /// <summary>Indicates whether the first application instance should be brought to the foreground upon exiting the exception handler.</summary>
    /// <returns>A <see cref="T:System.Boolean" /> that indicates whether the first application instance should be brought to the foreground upon exiting the exception handler.</returns>
    public bool BringToForeground
    {
      get
      {
        return this.m_BringToForeground;
      }
      set
      {
        this.m_BringToForeground = value;
      }
    }

    /// <summary>Gets the command-line arguments of the subsequent application instance.</summary>
    /// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> object that contains the command-line arguments of the subsequent application instance.</returns>
    public ReadOnlyCollection<string> CommandLine
    {
      get
      {
        return this.m_CommandLine;
      }
    }
  }
}
