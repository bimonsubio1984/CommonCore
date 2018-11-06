// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.OpenAccess
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates how to open a file when calling file-access functions.</summary>
  public enum OpenAccess
  {
    /// <summary>Read and write access permitted. This is the default.</summary>
    Default = -1,
    /// <summary>Read access permitted.</summary>
    Read = 1,
    /// <summary>Write access permitted.</summary>
    Write = 2,
    /// <summary>Read and write access permitted.</summary>
    ReadWrite = 3,
  }
}
