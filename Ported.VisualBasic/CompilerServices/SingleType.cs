// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.SingleType
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
  public sealed class SingleType
  {
    private SingleType()
    {
    }

    /// <summary>Returns a <see langword="Single" /> value that corresponds to the specified string. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Single" /> value.</param>
    /// <returns>The <see langword="Single" /> value that corresponds to <paramref name="Value" />.</returns>
    public static float FromString(string Value)
    {
      return SingleType.FromString(Value, (NumberFormatInfo) null);
    }

    /// <summary>Returns a <see langword="Single" /> value that corresponds to the specified string and number format information. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Single" /> value.</param>
    /// <param name="NumberFormat">A <see cref="T:System.Globalization.NumberFormatInfo" /> object that defines how numeric values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="Single" /> value corresponding to <paramref name="Value" />.</returns>
    public static float FromString(string Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return 0.0f;
      try
      {
        long i64Value=0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return (float) i64Value;
        double d = DoubleType.Parse(Value, NumberFormat);
        if ((d < -3.40282346638529E+38 || d > 3.40282346638529E+38) && !double.IsInfinity(d))
          throw new OverflowException();
        return (float) d;
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Single"), (Exception) ex);
      }
    }

    /// <summary>Returns a <see langword="Single" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Single" /> value.</param>
    /// <returns>The <see langword="Single" /> value corresponding to <paramref name="Value" />.</returns>
    public static float FromObject(object Value)
    {
      return SingleType.FromObject(Value, (NumberFormatInfo) null);
    }

    /// <summary>Returns a <see langword="Single" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Single" /> value.</param>
    /// <param name="NumberFormat">A <see cref="T:System.Globalization.NumberFormatInfo" /> object that defines how numeric values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="Single" /> value corresponding to <paramref name="Value" />.</returns>
    public static float FromObject(object Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return 0.0f;
      IConvertible ValueInterface = Value as IConvertible;
      if (ValueInterface != null)
      {
        switch (ValueInterface.GetTypeCode())
        {
          case TypeCode.Boolean:
            return (float) -(ValueInterface.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.Byte:
            if (Value is byte)
              return (float) (byte) Value;
            return (float) ValueInterface.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (float) (short) Value;
            return (float) ValueInterface.ToInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return (float) (int) Value;
            return (float) ValueInterface.ToInt32((IFormatProvider) null);
          case TypeCode.Int64:
            if (Value is long)
              return (float) (long) Value;
            return (float) ValueInterface.ToInt64((IFormatProvider) null);
          case TypeCode.Single:
            if (Value is float)
              return (float) Value;
            return ValueInterface.ToSingle((IFormatProvider) null);
          case TypeCode.Double:
            if (Value is double)
              return (float) (double) Value;
            return (float) ValueInterface.ToDouble((IFormatProvider) null);
          case TypeCode.Decimal:
            return SingleType.DecimalToSingle(ValueInterface);
          case TypeCode.String:
            return SingleType.FromString(ValueInterface.ToString((IFormatProvider) null), NumberFormat);
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Single"));
    }

    private static float DecimalToSingle(IConvertible ValueInterface)
    {
      return Convert.ToSingle(ValueInterface.ToDecimal((IFormatProvider) null));
    }
  }
}
