// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.BooleanType
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
  public sealed class BooleanType
  {
    private BooleanType()
    {
    }

    /// <summary>Returns a <see langword="Boolean" /> value that corresponds to the specified string. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Boolean" /> value.</param>
    /// <returns>The <see langword="Boolean" /> value that corresponds to <paramref name="Value" />.</returns>
    public static bool FromString(string Value)
    {
      if (Value == null)
        Value = "";
      try
      {
        CultureInfo cultureInfo = Utils.GetCultureInfo();
        if (string.Compare(Value, bool.FalseString, true, cultureInfo) == 0)
          return false;
        if (string.Compare(Value, bool.TrueString, true, cultureInfo) == 0)
          return true;
        long i64Value=0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return (ulong) i64Value > 0UL;
        return DoubleType.Parse(Value) != 0.0;
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Boolean"), (Exception) ex);
      }
    }

    /// <summary>Returns a <see langword="Boolean" /> value that corresponds to the specified object.</summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Boolean" /> value.</param>
    /// <returns>The <see langword="Boolean" /> value that corresponds to <paramref name="Value" />.</returns>
    public static bool FromObject(object Value)
    {
      if (Value == null)
        return false;
      IConvertible ValueInterface = Value as IConvertible;
      if (ValueInterface != null)
      {
        switch (ValueInterface.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (bool) Value;
            return ValueInterface.ToBoolean((IFormatProvider) null);
          case TypeCode.Byte:
            if (Value is byte)
              return (byte) Value > (byte) 0;
            return ValueInterface.ToByte((IFormatProvider) null) > (byte) 0;
          case TypeCode.Int16:
            if (Value is short)
              return (uint) (short) Value > 0U;
            return (uint) ValueInterface.ToInt16((IFormatProvider) null) > 0U;
          case TypeCode.Int32:
            if (Value is int)
              return (uint) (int) Value > 0U;
            return (uint) ValueInterface.ToInt32((IFormatProvider) null) > 0U;
          case TypeCode.Int64:
            if (Value is long)
              return (ulong) (long) Value > 0UL;
            return (ulong) ValueInterface.ToInt64((IFormatProvider) null) > 0UL;
          case TypeCode.Single:
            if (Value is float)
              return (double) (float) Value != 0.0;
            return (double) ValueInterface.ToSingle((IFormatProvider) null) != 0.0;
          case TypeCode.Double:
            if (Value is double)
              return (double) Value != 0.0;
            return ValueInterface.ToDouble((IFormatProvider) null) != 0.0;
          case TypeCode.Decimal:
            return BooleanType.DecimalToBoolean(ValueInterface);
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return BooleanType.FromString(str);
            return BooleanType.FromString(ValueInterface.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Boolean"));
    }

    private static bool DecimalToBoolean(IConvertible ValueInterface)
    {
      return Convert.ToBoolean(ValueInterface.ToDecimal((IFormatProvider) null));
    }
  }
}
