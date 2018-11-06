// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.UICancelOption
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic.FileIO
{
  /// <summary>Specifies whether an exception is thrown if the user clicks Cancel during an operation.</summary>
  public enum UICancelOption
  {
    /// <summary>Do nothing when the user clicks Cancel.</summary>
    DoNothing = 2,
    /// <summary>Throw an exception when the user clicks Cancel.</summary>
    ThrowException = 3,
  }
}
