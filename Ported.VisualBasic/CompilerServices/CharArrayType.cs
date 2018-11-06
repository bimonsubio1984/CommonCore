// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.CharArrayType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated as of Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class CharArrayType
  {
    private CharArrayType()
    {
    }

    /// <summary>Returns a <see langword="CharArray" /> value that corresponds to the specified string. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="CharArray" /> value.</param>
    /// <returns>The <see langword="CharArray" /> value that corresponds to <paramref name="Value" />.</returns>
    public static char[] FromString(string Value)
    {
      if (Value == null)
        Value = "";
      return Value.ToCharArray();
    }

    /// <summary>Returns a <see langword="CharArray" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="CharArray" /> value.</param>
    /// <returns>The <see langword="CharArray" /> value that corresponds to <paramref name="Value" />.</returns>
    public static char[] FromObject(object Value)
    {
      if (Value == null)
        return "".ToCharArray();
      char[] chArray = Value as char[];
      if (chArray != null && chArray.Rank == 1)
        return chArray;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null && convertible.GetTypeCode() == TypeCode.String)
        return convertible.ToString((IFormatProvider) null).ToCharArray();
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Char()"));
    }
  }
}
