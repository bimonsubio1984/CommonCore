// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.OpenMode
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates how to open a file when calling file-access functions.</summary>
  public enum OpenMode
  {
    /// <summary>File opened for read access.</summary>
    Input = 1,
    /// <summary>File opened for write access.</summary>
    Output = 2,
    /// <summary>File opened for random access.</summary>
    Random = 4,
    /// <summary>File opened to append to it. Default.</summary>
    Append = 8,
    /// <summary>File opened for binary access.</summary>
    Binary = 32, // 0x00000020
  }
}
