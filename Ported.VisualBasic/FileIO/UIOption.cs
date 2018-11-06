// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.UIOption
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.FileIO
{
  /// <summary>Specifies which dialog boxes to show when copying, deleting, or moving files or directories.</summary>
  public enum UIOption
  {
    /// <summary>Only show error dialog boxes and hide progress dialog boxes. Default.</summary>
    OnlyErrorDialogs = 2,
    /// <summary>Show progress dialog box and any error dialog boxes.</summary>
    AllDialogs = 3,
  }
}
