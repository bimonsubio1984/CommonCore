// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.MsgBoxResult
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates which button was pressed on a message box, returned by the <see langword="MsgBox" /> function.</summary>
  public enum MsgBoxResult
  {
    /// <summary>
    ///   OK button was pressed. This member is equivalent to the Visual Basic constant <see langword="vbOK" />.
    /// </summary>
    Ok = 1,
    /// <summary>
    /// Cancel button was pressed. This member is equivalent to the Visual Basic constant <see langword="vbCancel" />.</summary>
    Cancel = 2,
    /// <summary>
    /// Abort button was pressed. This member is equivalent to the Visual Basic constant <see langword="vbAbort" />.</summary>
    Abort = 3,
    /// <summary>
    /// Retry button was pressed. This member is equivalent to the Visual Basic constant <see langword="vbRetry" />.</summary>
    Retry = 4,
    /// <summary>
    /// Ignore button was pressed. This member is equivalent to the Visual Basic constant <see langword="vbIgnore" />.</summary>
    Ignore = 5,
    /// <summary>
    /// Yes button was pressed. This member is equivalent to the Visual Basic constant <see langword="vbYes" />.</summary>
    Yes = 6,
    /// <summary>
    /// No button was pressed. This member is equivalent to the Visual Basic constant <see langword="vbNo" />.</summary>
    No = 7,
  }
}
