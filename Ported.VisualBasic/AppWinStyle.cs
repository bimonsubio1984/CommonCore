// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.AppWinStyle
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates the window style to use for the invoked program when calling the <see langword="Shell" /> function.</summary>
  public enum AppWinStyle : short
  {
    /// <summary>Window is hidden and focus is passed to the hidden window. This member is equivalent to the Visual Basic constant <see langword="vbHide" />.</summary>
    Hide = 0,
    /// <summary>Window has focus and is restored to its original size and position. This member is equivalent to the Visual Basic constant <see langword="vbNormalFocus" />.</summary>
    NormalFocus = 1,
    /// <summary>Window is displayed as an icon with focus. This member is equivalent to the Visual Basic constant <see langword="vbMinimizedFocus" />.</summary>
    MinimizedFocus = 2,
    /// <summary>Window is maximized with focus. This member is equivalent to the Visual Basic constant <see langword="vbMaximizedFocus" />.</summary>
    MaximizedFocus = 3,
    /// <summary>Window is restored to its most recent size and position. The currently active window remains active. This member is equivalent to the Visual Basic constant <see langword="vbNormalNoFocus" />.</summary>
    NormalNoFocus = 4,
    /// <summary>Window is displayed as an icon. The currently active window remains active. This member is equivalent to the Visual Basic constant <see langword="vbMinimizedNoFocus" />.</summary>
    MinimizedNoFocus = 6,
  }
}
