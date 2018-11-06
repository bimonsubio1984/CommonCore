// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.DueDate
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates when payments are due when calling financial methods.</summary>
  public enum DueDate
  {
    /// <summary>Falls at the end of the date interval</summary>
    EndOfPeriod,
    /// <summary>Falls at the beginning of the date interval</summary>
    BegOfPeriod,
  }
}
