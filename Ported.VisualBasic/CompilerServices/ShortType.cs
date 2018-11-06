// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.ShortType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated as of Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class ShortType
  {
    private ShortType()
    {
    }

    /// <summary>Returns a <see langword="Short" /> value that corresponds to the specified string. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Short" /> value.</param>
    /// <returns>The <see langword="Short" /> value that corresponds to <paramref name="Value" />.</returns>
    public static short FromString(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value=0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((short) i64Value);
        return checked ((short) Math.Round(DoubleType.Parse(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Short"), (Exception) ex);
      }
    }

    /// <summary>Returns a <see langword="Short" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Short" /> value.</param>
    /// <returns>The <see langword="Short" /> value that corresponds to <paramref name="Value" />.</returns>
    public static short FromObject(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible ValueInterface = Value as IConvertible;
      if (ValueInterface != null)
      {
        switch (ValueInterface.GetTypeCode())
        {
          case TypeCode.Boolean:
            return (short) -(ValueInterface.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.Byte:
            if (Value is byte)
              return (short) (byte) Value;
            return (short) ValueInterface.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (short) Value;
            return ValueInterface.ToInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return checked ((short) (int) Value);
            return checked ((short) ValueInterface.ToInt32((IFormatProvider) null));
          case TypeCode.Int64:
            if (Value is long)
              return checked ((short) (long) Value);
            return checked ((short) ValueInterface.ToInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((short) Math.Round((double) (float) Value));
            return checked ((short) Math.Round((double) ValueInterface.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((short) Math.Round((double) Value));
            return checked ((short) Math.Round(ValueInterface.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            return ShortType.DecimalToShort(ValueInterface);
          case TypeCode.String:
            return ShortType.FromString(ValueInterface.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Short"));
    }

    private static short DecimalToShort(IConvertible ValueInterface)
    {
      return Convert.ToInt16(ValueInterface.ToDecimal((IFormatProvider) null));
    }
  }
}
