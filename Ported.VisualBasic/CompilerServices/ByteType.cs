// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.ByteType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated as of Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class ByteType
  {
    private ByteType()
    {
    }

    /// <summary>Returns a <see langword="Byte" /> value that corresponds to the specified string. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Byte" /> value.</param>
    /// <returns>The <see langword="Byte" /> value that corresponds to <paramref name="Value" />.</returns>
    public static byte FromString(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value=0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((byte) i64Value);
        return checked ((byte) Math.Round(DoubleType.Parse(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Byte"), (Exception) ex);
      }
    }

    /// <summary>Returns a <see langword="Byte" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Byte" /> value.</param>
    /// <returns>The <see langword="Byte" /> value that corresponds to <paramref name="Value" />.</returns>
    public static byte FromObject(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible ValueInterface = Value as IConvertible;
      if (ValueInterface != null)
      {
        switch (ValueInterface.GetTypeCode())
        {
          case TypeCode.Boolean:
            return (byte) -(ValueInterface.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.Byte:
            if (Value is byte)
              return (byte) Value;
            return ValueInterface.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return checked ((byte) (short) Value);
            return checked ((byte) ValueInterface.ToInt16((IFormatProvider) null));
          case TypeCode.Int32:
            if (Value is int)
              return checked ((byte) (int) Value);
            return checked ((byte) ValueInterface.ToInt32((IFormatProvider) null));
          case TypeCode.Int64:
            if (Value is long)
              return checked ((byte) (long) Value);
            return checked ((byte) ValueInterface.ToInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((byte) Math.Round((double) (float) Value));
            return checked ((byte) Math.Round((double) ValueInterface.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((byte) Math.Round((double) Value));
            return checked ((byte) Math.Round(ValueInterface.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            return ByteType.DecimalToByte(ValueInterface);
          case TypeCode.String:
            return ByteType.FromString(ValueInterface.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Byte"));
    }

    private static byte DecimalToByte(IConvertible ValueInterface)
    {
      return Convert.ToByte(ValueInterface.ToDecimal((IFormatProvider) null));
    }
  }
}
