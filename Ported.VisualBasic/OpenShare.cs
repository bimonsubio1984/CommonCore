// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.OpenShare
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates how to open a file when calling file-access functions.</summary>
  public enum OpenShare
  {
    /// <summary>
    /// <see langword="LockReadWrite" />. This is the default.</summary>
    Default = -1,
    /// <summary>Other processes cannot read or write to the file.</summary>
    LockReadWrite = 0,
    /// <summary>Other processes cannot write to the file.</summary>
    LockWrite = 1,
    /// <summary>Other processes cannot read the file.</summary>
    LockRead = 2,
    /// <summary>Any process can read or write to the file.</summary>
    Shared = 3,
  }
}
