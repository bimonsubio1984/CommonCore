// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.DateFormat
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates how to display dates when calling the <see langword="FormatDateTime" /> function.</summary>
  public enum DateFormat
  {
    /// <summary>For real numbers, displays a date and time. If the number has no fractional part, displays only a date. If the number has no integer part, displays time only. Date and time display is determined by your computer's regional settings. This member is equivalent to the Visual Basic constant <see langword="vbGeneralDate" />.</summary>
    GeneralDate,
    /// <summary>Displays a date using the long-date format specified in your computer's regional settings. This member is equivalent to the Visual Basic constant <see langword="vbLongDate" />.</summary>
    LongDate,
    /// <summary>Displays a date using the short-date format specified in your computer's regional settings. This member is equivalent to the Visual Basic constant <see langword="vbShortDate" />.</summary>
    ShortDate,
    /// <summary>Displays a time using the long-time format specified in your computer's regional settings. This member is equivalent to the Visual Basic constant <see langword="vbLongTime" />.</summary>
    LongTime,
    /// <summary>Displays a time using the short-time format specified in your computer's regional settings. This member is equivalent to the Visual Basic constant <see langword="vbShortTime" />.</summary>
    ShortTime,
  }
}
