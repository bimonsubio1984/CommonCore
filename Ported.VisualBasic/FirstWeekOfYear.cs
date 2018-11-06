// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FirstWeekOfYear
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates the first week of the year to use when calling date-related functions.</summary>
  public enum FirstWeekOfYear
  {
    /// <summary>The weekspecified in your system settings as the first week of the year. This member is equivalent to the Visual Basic constant <see langword="vbUseSystem" />.</summary>
    System,
    /// <summary>The week in which January 1 occurs (default). This member is equivalent to the Visual Basic constant <see langword="vbFirstJan1" />.</summary>
    Jan1,
    /// <summary>The first week that has at least four days in the new year. This member is equivalent to the Visual Basic constant <see langword="vbFirstFourDays" />.</summary>
    FirstFourDays,
    /// <summary>The first full week of the year. This member is equivalent to the Visual Basic constant <see langword="vbFirstFullWeek" />.</summary>
    FirstFullWeek,
  }
}
