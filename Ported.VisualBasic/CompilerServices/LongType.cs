// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.LongType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Globalization;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated as of Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class LongType
  {
    private LongType()
    {
    }

    /// <summary>Returns a <see langword="Long" /> value that corresponds to the specified string. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Long" /> value.</param>
    /// <returns>The <see langword="Long" /> value that corresponds to <paramref name="Value" />.</returns>
    public static long FromString(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value=0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return i64Value;
        return Convert.ToInt64(DecimalType.Parse(Value, (NumberFormatInfo) null));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Long"), (Exception) ex);
      }
    }

    /// <summary>Returns a <see langword="Long" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Long" /> value.</param>
    /// <returns>The <see langword="Long" /> value that corresponds to <paramref name="Value" />.</returns>
    public static long FromObject(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible ValueInterface = Value as IConvertible;
      if (ValueInterface != null)
      {
        switch (ValueInterface.GetTypeCode())
        {
          case TypeCode.Boolean:
            return (long) -(ValueInterface.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.Byte:
            if (Value is byte)
              return (long) (byte) Value;
            return (long) ValueInterface.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (long) (short) Value;
            return (long) ValueInterface.ToInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return (long) (int) Value;
            return (long) ValueInterface.ToInt32((IFormatProvider) null);
          case TypeCode.Int64:
            if (Value is long)
              return (long) Value;
            return ValueInterface.ToInt64((IFormatProvider) null);
          case TypeCode.Single:
            if (Value is float)
              return checked ((long) Math.Round((double) (float) Value));
            return checked ((long) Math.Round((double) ValueInterface.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((long) Math.Round((double) Value));
            return checked ((long) Math.Round(ValueInterface.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            return LongType.DecimalToLong(ValueInterface);
          case TypeCode.String:
            return LongType.FromString(ValueInterface.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Long"));
    }

    private static long DecimalToLong(IConvertible ValueInterface)
    {
      return Convert.ToInt64(ValueInterface.ToDecimal((IFormatProvider) null));
    }
  }
}
