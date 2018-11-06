// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CallType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

namespace Ported.VisualBasic
{
  /// <summary>Indicates the type of procedure being invoked when calling the <see langword="CallByName" /> function.</summary>
  public enum CallType
  {
    /// <summary>A method is being invoked.  This member is equivalent to the Visual Basic constant <see langword="vbMethod" />.</summary>
    Method = 1,
    /// <summary>A property value is being retrieved.  This member is equivalent to the Visual Basic constant <see langword="vbGet" />.</summary>
    Get = 2,
    /// <summary>An Object property value is being determined. This member is equivalent to the Visual Basic constant <see langword="vbLet" />.</summary>
    Let = 4,
    /// <summary>A property value is being determined.  This member is equivalent to the Visual Basic constant <see langword="vbSet" />.</summary>
    Set = 8,
  }
}
