// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.IntegerType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated as of Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class IntegerType
  {
    private IntegerType()
    {
    }

    /// <summary>Returns an <see langword="Integer" /> value that corresponds to the specified string. </summary>
    /// <param name="Value">Required. String to convert to an <see langword="Integer" /> value.</param>
    /// <returns>The <see langword="Integer" /> value that corresponds to <paramref name="Value" />.</returns>
    public static int FromString(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value=0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((int) i64Value);
        return checked ((int) Math.Round(DoubleType.Parse(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Integer"), (Exception) ex);
      }
    }

    /// <summary>Returns an <see langword="Integer" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to an <see langword="Integer" /> value.</param>
    /// <returns>The <see langword="Integer" /> value that corresponds to <paramref name="Value" />.</returns>
    public static int FromObject(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible ValueInterface = Value as IConvertible;
      if (ValueInterface != null)
      {
        switch (ValueInterface.GetTypeCode())
        {
          case TypeCode.Boolean:
            return -(ValueInterface.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.Byte:
            if (Value is byte)
              return (int) (byte) Value;
            return (int) ValueInterface.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (int) (short) Value;
            return (int) ValueInterface.ToInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return (int) Value;
            return ValueInterface.ToInt32((IFormatProvider) null);
          case TypeCode.Int64:
            if (Value is long)
              return checked ((int) (long) Value);
            return checked ((int) ValueInterface.ToInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((int) Math.Round((double) (float) Value));
            return checked ((int) Math.Round((double) ValueInterface.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((int) Math.Round((double) Value));
            return checked ((int) Math.Round(ValueInterface.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            return IntegerType.DecimalToInteger(ValueInterface);
          case TypeCode.String:
            return IntegerType.FromString(ValueInterface.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Integer"));
    }

    private static int DecimalToInteger(IConvertible ValueInterface)
    {
      return Convert.ToInt32(ValueInterface.ToDecimal((IFormatProvider) null));
    }
  }
}
