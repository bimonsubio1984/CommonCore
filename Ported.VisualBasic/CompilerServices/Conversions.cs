// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.Conversions
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll


using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>Provides methods that perform various type conversions.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class Conversions
  {
    private Conversions()
    {
    }

    /// <summary>Converts a string to a <see cref="T:System.Boolean" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>A <see langword="Boolean" /> value. Returns <see langword="False" /> if the string is null; otherwise, <see langword="True" />.</returns>
    public static bool ToBoolean(string Value)
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
        return Conversions.ParseDouble(Value) != 0.0;
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Boolean"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see cref="T:System.Boolean" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>A <see langword="Boolean" /> value. Returns <see langword="False" /> if the object is null; otherwise, <see langword="True" />.</returns>
    public static bool ToBoolean(object Value)
    {
      if (Value == null)
        return false;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (bool) Value;
            return convertible.ToBoolean((IFormatProvider) null);
          case TypeCode.SByte:
            if (Value is sbyte)
              return (uint) (sbyte) Value > 0U;
            return (uint) convertible.ToSByte((IFormatProvider) null) > 0U;
          case TypeCode.Byte:
            if (Value is byte)
              return (byte) Value > (byte) 0;
            return convertible.ToByte((IFormatProvider) null) > (byte) 0;
          case TypeCode.Int16:
            if (Value is short)
              return (uint) (short) Value > 0U;
            return (uint) convertible.ToInt16((IFormatProvider) null) > 0U;
          case TypeCode.UInt16:
            if (Value is ushort)
              return (ushort) Value > (ushort) 0;
            return convertible.ToUInt16((IFormatProvider) null) > (ushort) 0;
          case TypeCode.Int32:
            if (Value is int)
              return (uint) (int) Value > 0U;
            return (uint) convertible.ToInt32((IFormatProvider) null) > 0U;
          case TypeCode.UInt32:
            if (Value is uint)
              return (uint) Value > 0U;
            return convertible.ToUInt32((IFormatProvider) null) > 0U;
          case TypeCode.Int64:
            if (Value is long)
              return (ulong) (long) Value > 0UL;
            return (ulong) convertible.ToInt64((IFormatProvider) null) > 0UL;
          case TypeCode.UInt64:
            if (Value is ulong)
              return (ulong) Value > 0UL;
            return convertible.ToUInt64((IFormatProvider) null) > 0UL;
          case TypeCode.Single:
            if (Value is float)
              return (double) (float) Value != 0.0;
            return (double) convertible.ToSingle((IFormatProvider) null) != 0.0;
          case TypeCode.Double:
            if (Value is double)
              return (double) Value != 0.0;
            return convertible.ToDouble((IFormatProvider) null) != 0.0;
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToBoolean((IFormatProvider) null);
            return Convert.ToBoolean(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToBoolean(str);
            return Conversions.ToBoolean(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Boolean"));
    }

    /// <summary>Converts a string to a <see cref="T:System.Byte" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Byte" /> value of the string.</returns>
    public static byte ToByte(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((byte) i64Value);
        return checked ((byte) Math.Round(Conversions.ParseDouble(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Byte"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see cref="T:System.Byte" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Byte" /> value of the object.</returns>
    public static byte ToByte(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (byte) -((bool) Value ? 1 : 0);
            return (byte) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return checked ((byte) (sbyte) Value);
            return checked ((byte) convertible.ToSByte((IFormatProvider) null));
          case TypeCode.Byte:
            if (Value is byte)
              return (byte) Value;
            return convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return checked ((byte) (short) Value);
            return checked ((byte) convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            if (Value is ushort)
              return checked ((byte) (ushort) Value);
            return checked ((byte) convertible.ToUInt16((IFormatProvider) null));
          case TypeCode.Int32:
            if (Value is int)
              return checked ((byte) (int) Value);
            return checked ((byte) convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            if (Value is uint)
              return checked ((byte) (uint) Value);
            return checked ((byte) convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
            if (Value is long)
              return checked ((byte) (long) Value);
            return checked ((byte) convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            if (Value is ulong)
              return checked ((byte) (ulong) Value);
            return checked ((byte) convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((byte) Math.Round((double) (float) Value));
            return checked ((byte) Math.Round((double) convertible.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((byte) Math.Round((double) Value));
            return checked ((byte) Math.Round(convertible.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToByte((IFormatProvider) null);
            return Convert.ToByte(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToByte(str);
            return Conversions.ToByte(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Byte"));
    }

    /// <summary>Converts a string to an <see cref="T:System.SByte" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="SByte" /> value of the string.</returns>
    [CLSCompliant(false)]
    public static sbyte ToSByte(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((sbyte) i64Value);
        return checked ((sbyte) Math.Round(Conversions.ParseDouble(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "SByte"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to an <see cref="T:System.SByte" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="SByte" /> value of the object.</returns>
    [CLSCompliant(false)]
    public static sbyte ToSByte(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (sbyte) -((bool) Value ? 1 : 0);
            return (sbyte) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return (sbyte) Value;
            return convertible.ToSByte((IFormatProvider) null);
          case TypeCode.Byte:
            if (Value is byte)
              return checked ((sbyte) (byte) Value);
            return checked ((sbyte) convertible.ToByte((IFormatProvider) null));
          case TypeCode.Int16:
            if (Value is short)
              return checked ((sbyte) (short) Value);
            return checked ((sbyte) convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            if (Value is ushort)
              return checked ((sbyte) (ushort) Value);
            return checked ((sbyte) convertible.ToUInt16((IFormatProvider) null));
          case TypeCode.Int32:
            if (Value is int)
              return checked ((sbyte) (int) Value);
            return checked ((sbyte) convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            if (Value is uint)
              return checked ((sbyte) (uint) Value);
            return checked ((sbyte) convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
            if (Value is long)
              return checked ((sbyte) (long) Value);
            return checked ((sbyte) convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            if (Value is ulong)
              return checked ((sbyte) (ulong) Value);
            return checked ((sbyte) convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((sbyte) Math.Round((double) (float) Value));
            return checked ((sbyte) Math.Round((double) convertible.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((sbyte) Math.Round((double) Value));
            return checked ((sbyte) Math.Round(convertible.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToSByte((IFormatProvider) null);
            return Convert.ToSByte(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToSByte(str);
            return Conversions.ToSByte(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "SByte"));
    }

    /// <summary>Converts a string to a <see langword="Short" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Short" /> value of the string.</returns>
    public static short ToShort(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((short) i64Value);
        return checked ((short) Math.Round(Conversions.ParseDouble(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Short"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see langword="Short" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Short" /> value of the object.</returns>
    public static short ToShort(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (short) -((bool) Value ? 1 : 0);
            return (short) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return (short) (sbyte) Value;
            return (short) convertible.ToSByte((IFormatProvider) null);
          case TypeCode.Byte:
            if (Value is byte)
              return (short) (byte) Value;
            return (short) convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (short) Value;
            return convertible.ToInt16((IFormatProvider) null);
          case TypeCode.UInt16:
            if (Value is ushort)
              return checked ((short) (ushort) Value);
            return checked ((short) convertible.ToUInt16((IFormatProvider) null));
          case TypeCode.Int32:
            if (Value is int)
              return checked ((short) (int) Value);
            return checked ((short) convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            if (Value is uint)
              return checked ((short) (uint) Value);
            return checked ((short) convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
            if (Value is long)
              return checked ((short) (long) Value);
            return checked ((short) convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            if (Value is ulong)
              return checked ((short) (ulong) Value);
            return checked ((short) convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((short) Math.Round((double) (float) Value));
            return checked ((short) Math.Round((double) convertible.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((short) Math.Round((double) Value));
            return checked ((short) Math.Round(convertible.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToInt16((IFormatProvider) null);
            return Convert.ToInt16(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToShort(str);
            return Conversions.ToShort(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Short"));
    }

    /// <summary>Converts a string to a <see langword="Ushort" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Ushort" /> value of the string.</returns>
    [CLSCompliant(false)]
    public static ushort ToUShort(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((ushort) i64Value);
        return checked ((ushort) Math.Round(Conversions.ParseDouble(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "UShort"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see langword="Ushort" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Ushort" /> value of the object.</returns>
    [CLSCompliant(false)]
    public static ushort ToUShort(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (ushort) -((bool) Value ? 1 : 0);
            return (ushort) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return checked ((ushort) (sbyte) Value);
            return checked ((ushort) convertible.ToSByte((IFormatProvider) null));
          case TypeCode.Byte:
            if (Value is byte)
              return (ushort) (byte) Value;
            return (ushort) convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return checked ((ushort) (short) Value);
            return checked ((ushort) convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            if (Value is ushort)
              return (ushort) Value;
            return convertible.ToUInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return checked ((ushort) (int) Value);
            return checked ((ushort) convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            if (Value is uint)
              return checked ((ushort) (uint) Value);
            return checked ((ushort) convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
            if (Value is long)
              return checked ((ushort) (long) Value);
            return checked ((ushort) convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            if (Value is ulong)
              return checked ((ushort) (ulong) Value);
            return checked ((ushort) convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((ushort) Math.Round((double) (float) Value));
            return checked ((ushort) Math.Round((double) convertible.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((ushort) Math.Round((double) Value));
            return checked ((ushort) Math.Round(convertible.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToUInt16((IFormatProvider) null);
            return Convert.ToUInt16(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToUShort(str);
            return Conversions.ToUShort(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "UShort"));
    }

    /// <summary>Converts a string to an integer value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="int" /> value of the string.</returns>
    public static int ToInteger(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((int) i64Value);
        return checked ((int) Math.Round(Conversions.ParseDouble(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Integer"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to an integer value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="int" /> value of the object.</returns>
    public static int ToInteger(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return -((bool) Value ? 1 : 0);
            return -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return (int) (sbyte) Value;
            return (int) convertible.ToSByte((IFormatProvider) null);
          case TypeCode.Byte:
            if (Value is byte)
              return (int) (byte) Value;
            return (int) convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (int) (short) Value;
            return (int) convertible.ToInt16((IFormatProvider) null);
          case TypeCode.UInt16:
            if (Value is ushort)
              return (int) (ushort) Value;
            return (int) convertible.ToUInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return (int) Value;
            return convertible.ToInt32((IFormatProvider) null);
          case TypeCode.UInt32:
            if (Value is uint)
              return checked ((int) (uint) Value);
            return checked ((int) convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
            if (Value is long)
              return checked ((int) (long) Value);
            return checked ((int) convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            if (Value is ulong)
              return checked ((int) (ulong) Value);
            return checked ((int) convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((int) Math.Round((double) (float) Value));
            return checked ((int) Math.Round((double) convertible.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((int) Math.Round((double) Value));
            return checked ((int) Math.Round(convertible.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToInt32((IFormatProvider) null);
            return Convert.ToInt32(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToInteger(str);
            return Conversions.ToInteger(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Integer"));
    }

    /// <summary>Converts a string to a <see langword="Uint" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Uint" /> value of the string.</returns>
    [CLSCompliant(false)]
    public static uint ToUInteger(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return checked ((uint) i64Value);
        return checked ((uint) Math.Round(Conversions.ParseDouble(Value)));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "UInteger"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see langword="Uint" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Uint" /> value of the object.</returns>
    [CLSCompliant(false)]
    public static uint ToUInteger(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (uint) -((bool) Value ? 1 : 0);
            return (uint) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return checked ((uint) (sbyte) Value);
            return checked ((uint) convertible.ToSByte((IFormatProvider) null));
          case TypeCode.Byte:
            if (Value is byte)
              return (uint) (byte) Value;
            return (uint) convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return checked ((uint) (short) Value);
            return checked ((uint) convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            if (Value is ushort)
              return (uint) (ushort) Value;
            return (uint) convertible.ToUInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return checked ((uint) (int) Value);
            return checked ((uint) convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            if (Value is uint)
              return (uint) Value;
            return convertible.ToUInt32((IFormatProvider) null);
          case TypeCode.Int64:
            if (Value is long)
              return checked ((uint) (long) Value);
            return checked ((uint) convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            if (Value is ulong)
              return checked ((uint) (ulong) Value);
            return checked ((uint) convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((uint) Math.Round((double) (float) Value));
            return checked ((uint) Math.Round((double) convertible.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((uint) Math.Round((double) Value));
            return checked ((uint) Math.Round(convertible.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToUInt32((IFormatProvider) null);
            return Convert.ToUInt32(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToUInteger(str);
            return Conversions.ToUInteger(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "UInteger"));
    }

    /// <summary>Converts a string to a <see langword="Long" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Long" /> value of the string.</returns>
    public static long ToLong(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return i64Value;
        return Convert.ToInt64(Conversions.ParseDecimal(Value, (NumberFormatInfo) null));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Long"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see langword="Long" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Long" /> value of the object.</returns>
    public static long ToLong(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (long) -((bool) Value ? 1 : 0);
            return (long) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return (long) (sbyte) Value;
            return (long) convertible.ToSByte((IFormatProvider) null);
          case TypeCode.Byte:
            if (Value is byte)
              return (long) (byte) Value;
            return (long) convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (long) (short) Value;
            return (long) convertible.ToInt16((IFormatProvider) null);
          case TypeCode.UInt16:
            if (Value is ushort)
              return (long) (ushort) Value;
            return (long) convertible.ToUInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return (long) (int) Value;
            return (long) convertible.ToInt32((IFormatProvider) null);
          case TypeCode.UInt32:
            if (Value is uint)
              return (long) (uint) Value;
            return (long) convertible.ToUInt32((IFormatProvider) null);
          case TypeCode.Int64:
            if (Value is long)
              return (long) Value;
            return convertible.ToInt64((IFormatProvider) null);
          case TypeCode.UInt64:
            if (Value is ulong)
              return checked ((long) (ulong) Value);
            return checked ((long) convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return checked ((long) Math.Round((double) (float) Value));
            return checked ((long) Math.Round((double) convertible.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((long) Math.Round((double) Value));
            return checked ((long) Math.Round(convertible.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToInt64((IFormatProvider) null);
            return Convert.ToInt64(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToLong(str);
            return Conversions.ToLong(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Long"));
    }

    /// <summary>Converts a string to a <see langword="Ulong" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Ulong" /> value of the string.</returns>
    [CLSCompliant(false)]
    public static ulong ToULong(string Value)
    {
      if (Value == null)
        return 0;
      try
      {
        ulong ui64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref ui64Value))
          return ui64Value;
        return Convert.ToUInt64(Conversions.ParseDecimal(Value, (NumberFormatInfo) null));
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "ULong"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see langword="Ulong" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Ulong" /> value of the object.</returns>
    [CLSCompliant(false)]
    public static ulong ToULong(object Value)
    {
      if (Value == null)
        return 0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (ulong) -((bool) Value ? 1 : 0);
            return (ulong) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return checked ((ulong) (sbyte) Value);
            return checked ((ulong) convertible.ToSByte((IFormatProvider) null));
          case TypeCode.Byte:
            if (Value is byte)
              return (ulong) (byte) Value;
            return (ulong) convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return checked ((ulong) (short) Value);
            return checked ((ulong) convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            if (Value is ushort)
              return (ulong) (ushort) Value;
            return (ulong) convertible.ToUInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return checked ((ulong) (int) Value);
            return checked ((ulong) convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            if (Value is uint)
              return (ulong) (uint) Value;
            return (ulong) convertible.ToUInt32((IFormatProvider) null);
          case TypeCode.Int64:
            if (Value is long)
              return checked ((ulong) (long) Value);
            return checked ((ulong) convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            if (Value is ulong)
              return (ulong) Value;
            return convertible.ToUInt64((IFormatProvider) null);
          case TypeCode.Single:
            if (Value is float)
              return checked ((ulong) Math.Round((double) (float) Value));
            return checked ((ulong) Math.Round((double) convertible.ToSingle((IFormatProvider) null)));
          case TypeCode.Double:
            if (Value is double)
              return checked ((ulong) Math.Round((double) Value));
            return checked ((ulong) Math.Round(convertible.ToDouble((IFormatProvider) null)));
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToUInt64((IFormatProvider) null);
            return Convert.ToUInt64(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToULong(str);
            return Conversions.ToULong(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "ULong"));
    }

    /// <summary>Converts a <see cref="T:System.Boolean" /> value to a <see cref="T:System.Decimal" /> value.</summary>
    /// <param name="Value">A Boolean value to convert.</param>
    /// <returns>The <see langword="Decimal" /> value of the Boolean value.</returns>
    public static Decimal ToDecimal(bool Value)
    {
      if (Value)
        return Decimal.MinusOne;
      return Decimal.Zero;
    }

    /// <summary>Converts a string to a <see cref="T:System.Decimal" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Decimal" /> value of the string.</returns>
    public static Decimal ToDecimal(string Value)
    {
      return Conversions.ToDecimal(Value, (NumberFormatInfo) null);
    }

    internal static Decimal ToDecimal(string Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return Decimal.Zero;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return new Decimal(i64Value);
        return Conversions.ParseDecimal(Value, NumberFormat);
      }
      catch (OverflowException ex)
      {
        throw ExceptionUtils.VbMakeException(6);
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Decimal"));
      }
    }

    /// <summary>Converts an object to a <see cref="T:System.Decimal" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Decimal" /> value of the object.</returns>
    public static Decimal ToDecimal(object Value)
    {
      return Conversions.ToDecimal(Value, (NumberFormatInfo) null);
    }

    internal static Decimal ToDecimal(object Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return Decimal.Zero;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return Conversions.ToDecimal((bool) Value);
            return Conversions.ToDecimal(convertible.ToBoolean((IFormatProvider) null));
          case TypeCode.SByte:
            if (Value is sbyte)
              return new Decimal((int) (sbyte) Value);
            return new Decimal((int) convertible.ToSByte((IFormatProvider) null));
          case TypeCode.Byte:
            if (Value is byte)
              return new Decimal((int) (byte) Value);
            return new Decimal((int) convertible.ToByte((IFormatProvider) null));
          case TypeCode.Int16:
            if (Value is short)
              return new Decimal((int) (short) Value);
            return new Decimal((int) convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            if (Value is ushort)
              return new Decimal((int) (ushort) Value);
            return new Decimal((int) convertible.ToUInt16((IFormatProvider) null));
          case TypeCode.Int32:
            if (Value is int)
              return new Decimal((int) Value);
            return new Decimal(convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            if (Value is uint)
              return new Decimal((uint) Value);
            return new Decimal(convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
            if (Value is long)
              return new Decimal((long) Value);
            return new Decimal(convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            if (Value is ulong)
              return new Decimal((ulong) Value);
            return new Decimal(convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            if (Value is float)
              return new Decimal((float) Value);
            return new Decimal(convertible.ToSingle((IFormatProvider) null));
          case TypeCode.Double:
            if (Value is double)
              return new Decimal((double) Value);
            return new Decimal(convertible.ToDouble((IFormatProvider) null));
          case TypeCode.Decimal:
            return convertible.ToDecimal((IFormatProvider) null);
          case TypeCode.String:
            return Conversions.ToDecimal(convertible.ToString((IFormatProvider) null), NumberFormat);
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Decimal"));
    }

    private static Decimal ParseDecimal(string Value, NumberFormatInfo NumberFormat)
    {
      CultureInfo cultureInfo = Utils.GetCultureInfo();
      if (NumberFormat == null)
        NumberFormat = cultureInfo.NumberFormat;
      NumberFormatInfo normalizedNumberFormat = Conversions.GetNormalizedNumberFormat(NumberFormat);
      Value = Utils.ToHalfwidthNumbers(Value, cultureInfo);
      try
      {
        return Decimal.Parse(Value, NumberStyles.Any, (IFormatProvider) normalizedNumberFormat);
      }
      catch (FormatException ex) when (NumberFormat != normalizedNumberFormat)
      {
        return Decimal.Parse(Value, NumberStyles.Any, (IFormatProvider) NumberFormat);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static NumberFormatInfo GetNormalizedNumberFormat(NumberFormatInfo InNumberFormat)
    {
      NumberFormatInfo numberFormatInfo1 = InNumberFormat;
      if (numberFormatInfo1.CurrencyDecimalSeparator != null && numberFormatInfo1.NumberDecimalSeparator != null && (numberFormatInfo1.CurrencyGroupSeparator != null && numberFormatInfo1.NumberGroupSeparator != null) && (numberFormatInfo1.CurrencyDecimalSeparator.Length == 1 && numberFormatInfo1.NumberDecimalSeparator.Length == 1 && (numberFormatInfo1.CurrencyGroupSeparator.Length == 1 && numberFormatInfo1.NumberGroupSeparator.Length == 1)) && ((int) numberFormatInfo1.CurrencyDecimalSeparator[0] == (int) numberFormatInfo1.NumberDecimalSeparator[0] && (int) numberFormatInfo1.CurrencyGroupSeparator[0] == (int) numberFormatInfo1.NumberGroupSeparator[0] && numberFormatInfo1.CurrencyDecimalDigits == numberFormatInfo1.NumberDecimalDigits))
        return InNumberFormat;
      NumberFormatInfo numberFormatInfo2 = InNumberFormat;
      if (numberFormatInfo2.CurrencyDecimalSeparator != null && numberFormatInfo2.NumberDecimalSeparator != null && (numberFormatInfo2.CurrencyDecimalSeparator.Length == numberFormatInfo2.NumberDecimalSeparator.Length && numberFormatInfo2.CurrencyGroupSeparator != null) && (numberFormatInfo2.NumberGroupSeparator != null && numberFormatInfo2.CurrencyGroupSeparator.Length == numberFormatInfo2.NumberGroupSeparator.Length))
      {
        int num1 = 0;
        int num2 = checked (numberFormatInfo2.CurrencyDecimalSeparator.Length - 1);
        int index1 = num1;
        while (index1 <= num2)
        {
          if ((int) numberFormatInfo2.CurrencyDecimalSeparator[index1] == (int) numberFormatInfo2.NumberDecimalSeparator[index1])
            checked { ++index1; }
          else
            goto label_13;
        }
        int num3 = 0;
        int num4 = checked (numberFormatInfo2.CurrencyGroupSeparator.Length - 1);
        int index2 = num3;
        while (index2 <= num4)
        {
          if ((int) numberFormatInfo2.CurrencyGroupSeparator[index2] == (int) numberFormatInfo2.NumberGroupSeparator[index2])
            checked { ++index2; }
          else
            goto label_13;
        }
        return InNumberFormat;
      }
label_13:
      NumberFormatInfo numberFormatInfo3 = (NumberFormatInfo) InNumberFormat.Clone();
      NumberFormatInfo numberFormatInfo4 = numberFormatInfo3;
      numberFormatInfo4.CurrencyDecimalSeparator = numberFormatInfo4.NumberDecimalSeparator;
      numberFormatInfo4.CurrencyGroupSeparator = numberFormatInfo4.NumberGroupSeparator;
      numberFormatInfo4.CurrencyDecimalDigits = numberFormatInfo4.NumberDecimalDigits;
      return numberFormatInfo3;
    }

    /// <summary>Converts a <see cref="T:System.String" /> to a <see cref="T:System.Single" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Single" /> value of the string.</returns>
    public static float ToSingle(string Value)
    {
      return Conversions.ToSingle(Value, (NumberFormatInfo) null);
    }

    internal static float ToSingle(string Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return 0.0f;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return (float) i64Value;
        double d = Conversions.ParseDouble(Value, NumberFormat);
        if ((d < -3.40282346638529E+38 || d > 3.40282346638529E+38) && !double.IsInfinity(d))
          throw new OverflowException();
        return (float) d;
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Single"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see cref="T:System.Single" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Single" /> value of the object.</returns>
    public static float ToSingle(object Value)
    {
      return Conversions.ToSingle(Value, (NumberFormatInfo) null);
    }

    internal static float ToSingle(object Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return 0.0f;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (float) -((bool) Value ? 1 : 0);
            return (float) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return (float) (sbyte) Value;
            return (float) convertible.ToSByte((IFormatProvider) null);
          case TypeCode.Byte:
            if (Value is byte)
              return (float) (byte) Value;
            return (float) convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (float) (short) Value;
            return (float) convertible.ToInt16((IFormatProvider) null);
          case TypeCode.UInt16:
            if (Value is ushort)
              return (float) (ushort) Value;
            return (float) convertible.ToUInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return (float) (int) Value;
            return (float) convertible.ToInt32((IFormatProvider) null);
          case TypeCode.UInt32:
            if (Value is uint)
              return (float) (uint) Value;
            return (float) convertible.ToUInt32((IFormatProvider) null);
          case TypeCode.Int64:
            if (Value is long)
              return (float) (long) Value;
            return (float) convertible.ToInt64((IFormatProvider) null);
          case TypeCode.UInt64:
            if (Value is ulong)
              return (float) (ulong) Value;
            return (float) convertible.ToUInt64((IFormatProvider) null);
          case TypeCode.Single:
            if (Value is float)
              return (float) Value;
            return convertible.ToSingle((IFormatProvider) null);
          case TypeCode.Double:
            if (Value is double)
              return (float) (double) Value;
            return (float) convertible.ToDouble((IFormatProvider) null);
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToSingle((IFormatProvider) null);
            return Convert.ToSingle(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            return Conversions.ToSingle(convertible.ToString((IFormatProvider) null), NumberFormat);
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Single"));
    }

    /// <summary>Converts a string to a <see cref="T:System.Double" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Double" /> value of the string.</returns>
    public static double ToDouble(string Value)
    {
      return Conversions.ToDouble(Value, (NumberFormatInfo) null);
    }

    internal static double ToDouble(string Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return 0.0;
      try
      {
        long i64Value = 0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return (double) i64Value;
        return Conversions.ParseDouble(Value, NumberFormat);
      }
      catch (FormatException ex)
      {
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Double"), (Exception) ex);
      }
    }

    /// <summary>Converts an object to a <see cref="T:System.Double" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Double" /> value of the object.</returns>
    public static double ToDouble(object Value)
    {
      return Conversions.ToDouble(Value, (NumberFormatInfo) null);
    }

    internal static double ToDouble(object Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return 0.0;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            if (Value is bool)
              return (double) -((bool) Value ? 1 : 0);
            return (double) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
          case TypeCode.SByte:
            if (Value is sbyte)
              return (double) (sbyte) Value;
            return (double) convertible.ToSByte((IFormatProvider) null);
          case TypeCode.Byte:
            if (Value is byte)
              return (double) (byte) Value;
            return (double) convertible.ToByte((IFormatProvider) null);
          case TypeCode.Int16:
            if (Value is short)
              return (double) (short) Value;
            return (double) convertible.ToInt16((IFormatProvider) null);
          case TypeCode.UInt16:
            if (Value is ushort)
              return (double) (ushort) Value;
            return (double) convertible.ToUInt16((IFormatProvider) null);
          case TypeCode.Int32:
            if (Value is int)
              return (double) (int) Value;
            return (double) convertible.ToInt32((IFormatProvider) null);
          case TypeCode.UInt32:
            if (Value is uint)
              return (double) (uint) Value;
            return (double) convertible.ToUInt32((IFormatProvider) null);
          case TypeCode.Int64:
            if (Value is long)
              return (double) (long) Value;
            return (double) convertible.ToInt64((IFormatProvider) null);
          case TypeCode.UInt64:
            if (Value is ulong)
              return (double) (ulong) Value;
            return (double) convertible.ToUInt64((IFormatProvider) null);
          case TypeCode.Single:
            if (Value is float)
              return (double) (float) Value;
            return (double) convertible.ToSingle((IFormatProvider) null);
          case TypeCode.Double:
            if (Value is double)
              return (double) Value;
            return convertible.ToDouble((IFormatProvider) null);
          case TypeCode.Decimal:
            if (Value is Decimal)
              return convertible.ToDouble((IFormatProvider) null);
            return Convert.ToDouble(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            return Conversions.ToDouble(convertible.ToString((IFormatProvider) null), NumberFormat);
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Double"));
    }

    private static double ParseDouble(string Value)
    {
      return Conversions.ParseDouble(Value, (NumberFormatInfo) null);
    }

    internal static bool TryParseDouble(string Value, ref double Result)
    {
      CultureInfo cultureInfo = Utils.GetCultureInfo();
      NumberFormatInfo numberFormat = cultureInfo.NumberFormat;
      NumberFormatInfo normalizedNumberFormat = Conversions.GetNormalizedNumberFormat(numberFormat);
      Value = Utils.ToHalfwidthNumbers(Value, cultureInfo);
      if (numberFormat == normalizedNumberFormat)
        return double.TryParse(Value, NumberStyles.Any, (IFormatProvider) normalizedNumberFormat, out Result);
      try
      {
        Result = double.Parse(Value, NumberStyles.Any, (IFormatProvider) normalizedNumberFormat);
        return true;
      }
      catch (FormatException ex1)
      {
        try
        {
          return double.TryParse(Value, NumberStyles.Any, (IFormatProvider) numberFormat, out Result);
        }
        catch (ArgumentException ex2)
        {
          return false;
        }
      }
      catch (StackOverflowException ex)
      {
        throw ex;
      }
      catch (OutOfMemoryException ex)
      {
        throw ex;
      }
      catch (ThreadAbortException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private static double ParseDouble(string Value, NumberFormatInfo NumberFormat)
    {
      CultureInfo cultureInfo = Utils.GetCultureInfo();
      if (NumberFormat == null)
        NumberFormat = cultureInfo.NumberFormat;
      NumberFormatInfo normalizedNumberFormat = Conversions.GetNormalizedNumberFormat(NumberFormat);
      Value = Utils.ToHalfwidthNumbers(Value, cultureInfo);
      try
      {
        return double.Parse(Value, NumberStyles.Any, (IFormatProvider) normalizedNumberFormat);
      }
      catch (FormatException ex) when (NumberFormat != normalizedNumberFormat)
      {
        return double.Parse(Value, NumberStyles.Any, (IFormatProvider) NumberFormat);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Converts a string to a <see cref="T:System.DateTime" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="DateTime" /> value of the string.</returns>
    public static DateTime ToDate(string Value)
    {
      DateTime Result= DateTime.MinValue;
      if (Conversions.TryParseDate(Value, ref Result))
        return Result;
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromStringTo", Strings.Left(Value, 32), "Date"));
    }

    /// <summary>Converts an object to a <see cref="T:System.DateTime" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="DateTime" /> value of the object.</returns>
    public static DateTime ToDate(object Value)
    {
      if (Value != null)
      {
        IConvertible convertible = Value as IConvertible;
        if (convertible != null)
        {
          switch (convertible.GetTypeCode())
          {
            case TypeCode.DateTime:
              if (Value is DateTime)
                return (DateTime) Value;
              return convertible.ToDateTime((IFormatProvider) null);
            case TypeCode.String:
              string str = Value as string;
              if (str != null)
                return Conversions.ToDate(str);
              return Conversions.ToDate(convertible.ToString((IFormatProvider) null));
          }
        }
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Date"));
      }
      DateTime dateTime= DateTime.MinValue;
      return dateTime;
    }

    internal static bool TryParseDate(string Value, ref DateTime Result)
    {
      CultureInfo cultureInfo = Utils.GetCultureInfo();
      return DateTime.TryParse(Utils.ToHalfwidthNumbers(Value, cultureInfo), (IFormatProvider) cultureInfo, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.NoCurrentDateDefault, out Result);
    }

    /// <summary>Converts a string to a <see cref="T:System.Char" /> value.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>The <see langword="Char" /> value of the string.</returns>
    public static char ToChar(string Value)
    {
      if (Value == null || Value.Length == 0)
        return char.MinValue;
      return Value[0];
    }

    /// <summary>Converts an object to a <see cref="T:System.Char" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="Char" /> value of the object.</returns>
    public static char ToChar(object Value)
    {
      if (Value == null)
        return char.MinValue;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Char:
            if (Value is char)
              return (char) Value;
            return convertible.ToChar((IFormatProvider) null);
          case TypeCode.String:
            string str = Value as string;
            if (str != null)
              return Conversions.ToChar(str);
            return Conversions.ToChar(convertible.ToString((IFormatProvider) null));
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Char"));
    }

    /// <summary>Converts a string to a one-dimensional <see cref="T:System.Char" /> array.</summary>
    /// <param name="Value">The string to convert.</param>
    /// <returns>A one-dimensional <see langword="Char" /> array.</returns>
    public static char[] ToCharArrayRankOne(string Value)
    {
      if (Value == null)
        Value = "";
      return Value.ToCharArray();
    }

    /// <summary>Converts an object to a one-dimensional <see cref="T:System.Char" /> array.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>A one-dimensional <see langword="Char" /> array.</returns>
    public static char[] ToCharArrayRankOne(object Value)
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

    /// <summary>Converts a <see cref="T:System.Boolean" /> value to a <see cref="T:System.String" />.</summary>
    /// <param name="Value">The <see langword="Boolean" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Boolean" /> value.</returns>
    public static string ToString(bool Value)
    {
      if (Value)
        return bool.TrueString;
      return bool.FalseString;
    }

    /// <summary>Converts a <see cref="T:System.Byte" /> value to a <see cref="T:System.String" />.</summary>
    /// <param name="Value">The <see langword="Byte" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Byte" /> value.</returns>
    public static string ToString(byte Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Converts a <see cref="T:System.Char" /> value to a <see cref="T:System.String" />.</summary>
    /// <param name="Value">The <see langword="Char" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Char" /> value.</returns>
    public static string ToString(char Value)
    {
      return Value.ToString();
    }

    /// <summary>Converts a <see cref="T:System.Char" /> array to a string.</summary>
    /// <param name="Value">The <see langword="Char" /> array to convert.</param>
    /// <returns>The string representation of the specified array.</returns>
    public static string FromCharArray(char[] Value)
    {
      return new string(Value);
    }

    /// <summary>Converts a <see cref="T:System.Char" /> value to a string, given a byte count.</summary>
    /// <param name="Value">The <see langword="Char" /> value to convert.</param>
    /// <param name="Count">The byte count of the <see langword="Char" /> value.</param>
    /// <returns>The string representation of the specified value.</returns>
    public static string FromCharAndCount(char Value, int Count)
    {
      return new string(Value, Count);
    }

    /// <summary>Converts a subset of a <see cref="T:System.Char" /> array to a string.</summary>
    /// <param name="Value">The <see langword="Char" /> array to convert.</param>
    /// <param name="StartIndex">Zero-based index of the start position.</param>
    /// <param name="Length">Length of the subset in bytes.</param>
    /// <returns>The string representation of the specified array from the start position to the specified length.</returns>
    public static string FromCharArraySubset(char[] Value, int StartIndex, int Length)
    {
      return new string(Value, StartIndex, Length);
    }

    /// <summary>Converts a <see langword="Short" /> value to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="Short" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Short" /> value.</returns>
    public static string ToString(short Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Converts an integer value to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="int" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="int" /> value.</returns>
    public static string ToString(int Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Converts a <see langword="uint" /> value to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="Uint" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Uint" /> value.</returns>
    [CLSCompliant(false)]
    public static string ToString(uint Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Converts a <see langword="Long" /> value to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="Long" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Long" /> value.</returns>
    public static string ToString(long Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Converts a <see langword="Ulong" /> value to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="Ulong" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Ulong" /> value.</returns>
    [CLSCompliant(false)]
    public static string ToString(ulong Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Converts a <see cref="T:System.Single" /> value (a single-precision floating point number) to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="Single" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Single" /> value.</returns>
    public static string ToString(float Value)
    {
      return Conversions.ToString(Value, (NumberFormatInfo) null);
    }

    /// <summary>Converts a <see cref="T:System.Double" /> value to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="Double" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Double" /> value.</returns>
    public static string ToString(double Value)
    {
      return Conversions.ToString(Value, (NumberFormatInfo) null);
    }

    /// <summary>Converts a <see cref="T:System.Single" /> value to a <see cref="T:System.String" /> value, using the specified number format.</summary>
    /// <param name="Value">The <see langword="Single" /> value to convert.</param>
    /// <param name="NumberFormat">The number format to use, according to <see cref="T:System.Globalization.NumberFormatInfo" />.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Single" /> value.</returns>
    public static string ToString(float Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString((string) null, (IFormatProvider) NumberFormat);
    }

    /// <summary>Converts a <see cref="T:System.Double" /> value to a <see cref="T:System.String" /> value, using the specified number format.</summary>
    /// <param name="Value">The <see langword="Double" /> value to convert.</param>
    /// <param name="NumberFormat">The number format to use, according to <see cref="T:System.Globalization.NumberFormatInfo" />.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Double" /> value.</returns>
    public static string ToString(double Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString("G", (IFormatProvider) NumberFormat);
    }

    /// <summary>Converts a <see cref="T:System.DateTime" /> value to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="DateTime" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="DateTime" /> value.</returns>
    public static string ToString(DateTime Value)
    {
      long ticks = Value.TimeOfDay.Ticks;
      if (ticks == Value.Ticks || Value.Year == 1899 && Value.Month == 12 && Value.Day == 30)
        return Value.ToString("T", (IFormatProvider) null);
      if (ticks == 0L)
        return Value.ToString("d", (IFormatProvider) null);
      return Value.ToString("G", (IFormatProvider) null);
    }

    /// <summary>Converts a <see cref="T:System.Decimal" /> value to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The <see langword="Decimal" /> value to convert.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Decimal" /> value.</returns>
    public static string ToString(Decimal Value)
    {
      return Conversions.ToString(Value, (NumberFormatInfo) null);
    }

    /// <summary>Converts a <see cref="T:System.Decimal" /> value to a <see cref="T:System.String" /> value, using the specified number format.</summary>
    /// <param name="Value">The <see langword="decimal" /> value to convert.</param>
    /// <param name="NumberFormat">The number format to use, according to <see cref="T:System.Globalization.NumberFormatInfo" />.</param>
    /// <returns>The <see langword="String" /> representation of the <see langword="Decimal" /> value.</returns>
    public static string ToString(Decimal Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString("G", (IFormatProvider) NumberFormat);
    }

    /// <summary>Converts an object to a <see cref="T:System.String" /> value.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <returns>The <see langword="String" /> representation of the object.</returns>
    public static string ToString(object Value)
    {
      if (Value == null)
        return (string) null;
      string str = Value as string;
      if (str != null)
        return str;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            return Conversions.ToString(convertible.ToBoolean((IFormatProvider) null));
          case TypeCode.Char:
            return Conversions.ToString(convertible.ToChar((IFormatProvider) null));
          case TypeCode.SByte:
            return Conversions.ToString((int) convertible.ToSByte((IFormatProvider) null));
          case TypeCode.Byte:
            return Conversions.ToString(convertible.ToByte((IFormatProvider) null));
          case TypeCode.Int16:
            return Conversions.ToString((int) convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            return Conversions.ToString((uint) convertible.ToUInt16((IFormatProvider) null));
          case TypeCode.Int32:
            return Conversions.ToString(convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            return Conversions.ToString(convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
            return Conversions.ToString(convertible.ToInt64((IFormatProvider) null));
          case TypeCode.UInt64:
            return Conversions.ToString(convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.Single:
            return Conversions.ToString(convertible.ToSingle((IFormatProvider) null));
          case TypeCode.Double:
            return Conversions.ToString(convertible.ToDouble((IFormatProvider) null));
          case TypeCode.Decimal:
            return Conversions.ToString(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.DateTime:
            return Conversions.ToString(convertible.ToDateTime((IFormatProvider) null));
          case TypeCode.String:
            return convertible.ToString((IFormatProvider) null);
        }
      }
      else
      {
        char[] chArray = Value as char[];
        if (chArray != null)
          return new string(chArray);
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "String"));
    }

    /// <summary>Converts an object to a generic type <paramref name="T" />.</summary>
    /// <param name="Value">The object to convert.</param>
    /// <typeparam name="T">The type to convert <paramref name="Value" /> to.</typeparam>
    /// <returns>A structure or object of generic type <paramref name="T" />.</returns>
    public static T ToGenericParameter<T>(object Value)
    {
      if (Value == null)
        return default (T);
      switch (Symbols.GetTypeCode(typeof (T)))
      {
        case TypeCode.Boolean:
            return (T)Convert.ChangeType(Conversions.ToBoolean(Value), typeof(T));
        case TypeCode.Char:
          return (T)Convert.ChangeType(Conversions.ToChar(Value), typeof(T));
                case TypeCode.SByte:
          return (T)Convert.ChangeType(Conversions.ToSByte(Value), typeof(T));
                case TypeCode.Byte:
          return (T)Convert.ChangeType(Conversions.ToByte(Value), typeof(T));
                case TypeCode.Int16:
          return (T)Convert.ChangeType(Conversions.ToShort(Value), typeof(T));
                case TypeCode.UInt16:
          return (T)Convert.ChangeType(Conversions.ToUShort(Value), typeof(T));
                case TypeCode.Int32:
          return (T)Convert.ChangeType(Conversions.ToInteger(Value), typeof(T));
                case TypeCode.UInt32:
          return (T)Convert.ChangeType(Conversions.ToUInteger(Value), typeof(T));
                case TypeCode.Int64:
          return (T)Convert.ChangeType(Conversions.ToLong(Value), typeof(T));
                case TypeCode.UInt64:
          return (T)Convert.ChangeType(Conversions.ToULong(Value), typeof(T));
                case TypeCode.Single:
          return (T)Convert.ChangeType(Conversions.ToSingle(Value), typeof(T));
                case TypeCode.Double:
          return (T)Convert.ChangeType(Conversions.ToDouble(Value), typeof(T));
                case TypeCode.Decimal:
          return (T)Convert.ChangeType(Conversions.ToDecimal(Value), typeof(T));
                case TypeCode.DateTime:
          return (T)Convert.ChangeType(Conversions.ToDate(Value), typeof(T));
                case TypeCode.String:
          return (T)Convert.ChangeType(Conversions.ToString(Value), typeof(T));
                default:
          return (T) Value;
      }
    }

    private static object CastSByteEnum(sbyte Expression, Type TargetType)
    {
      if (Symbols.IsEnum(TargetType))
        return Enum.ToObject(TargetType, Expression);
      return (object) Expression;
    }

    private static object CastByteEnum(byte Expression, Type TargetType)
    {
      if (Symbols.IsEnum(TargetType))
        return Enum.ToObject(TargetType, Expression);
      return (object) Expression;
    }

    private static object CastInt16Enum(short Expression, Type TargetType)
    {
      if (Symbols.IsEnum(TargetType))
        return Enum.ToObject(TargetType, Expression);
      return (object) Expression;
    }

    private static object CastUInt16Enum(ushort Expression, Type TargetType)
    {
      if (Symbols.IsEnum(TargetType))
        return Enum.ToObject(TargetType, Expression);
      return (object) Expression;
    }

    private static object CastInt32Enum(int Expression, Type TargetType)
    {
      if (Symbols.IsEnum(TargetType))
        return Enum.ToObject(TargetType, Expression);
      return (object) Expression;
    }

    private static object CastUInt32Enum(uint Expression, Type TargetType)
    {
      if (Symbols.IsEnum(TargetType))
        return Enum.ToObject(TargetType, Expression);
      return (object) Expression;
    }

    private static object CastInt64Enum(long Expression, Type TargetType)
    {
      if (Symbols.IsEnum(TargetType))
        return Enum.ToObject(TargetType, Expression);
      return (object) Expression;
    }

    private static object CastUInt64Enum(ulong Expression, Type TargetType)
    {
      if (Symbols.IsEnum(TargetType))
        return Enum.ToObject(TargetType, Expression);
      return (object) Expression;
    }

    internal static object ForceValueCopy(object Expression, Type TargetType)
    {
      IConvertible convertible = Expression as IConvertible;
      if (convertible == null)
        return Expression;
      switch (convertible.GetTypeCode())
      {
        case TypeCode.Boolean:
          return (object) convertible.ToBoolean((IFormatProvider) null);
        case TypeCode.Char:
          return (object) convertible.ToChar((IFormatProvider) null);
        case TypeCode.SByte:
          return Conversions.CastSByteEnum(convertible.ToSByte((IFormatProvider) null), TargetType);
        case TypeCode.Byte:
          return Conversions.CastByteEnum(convertible.ToByte((IFormatProvider) null), TargetType);
        case TypeCode.Int16:
          return Conversions.CastInt16Enum(convertible.ToInt16((IFormatProvider) null), TargetType);
        case TypeCode.UInt16:
          return Conversions.CastUInt16Enum(convertible.ToUInt16((IFormatProvider) null), TargetType);
        case TypeCode.Int32:
          return Conversions.CastInt32Enum(convertible.ToInt32((IFormatProvider) null), TargetType);
        case TypeCode.UInt32:
          return Conversions.CastUInt32Enum(convertible.ToUInt32((IFormatProvider) null), TargetType);
        case TypeCode.Int64:
          return Conversions.CastInt64Enum(convertible.ToInt64((IFormatProvider) null), TargetType);
        case TypeCode.UInt64:
          return Conversions.CastUInt64Enum(convertible.ToUInt64((IFormatProvider) null), TargetType);
        case TypeCode.Single:
          return (object) convertible.ToSingle((IFormatProvider) null);
        case TypeCode.Double:
          return (object) convertible.ToDouble((IFormatProvider) null);
        case TypeCode.Decimal:
          return (object) convertible.ToDecimal((IFormatProvider) null);
        case TypeCode.DateTime:
          return (object) convertible.ToDateTime((IFormatProvider) null);
        default:
          return Expression;
      }
    }

    private static object ChangeIntrinsicType(object Expression, Type TargetType)
    {
      switch (Symbols.GetTypeCode(TargetType))
      {
        case TypeCode.Boolean:
          return (object) Conversions.ToBoolean(Expression);
        case TypeCode.Char:
          return (object) Conversions.ToChar(Expression);
        case TypeCode.SByte:
          return Conversions.CastSByteEnum(Conversions.ToSByte(Expression), TargetType);
        case TypeCode.Byte:
          return Conversions.CastByteEnum(Conversions.ToByte(Expression), TargetType);
        case TypeCode.Int16:
          return Conversions.CastInt16Enum(Conversions.ToShort(Expression), TargetType);
        case TypeCode.UInt16:
          return Conversions.CastUInt16Enum(Conversions.ToUShort(Expression), TargetType);
        case TypeCode.Int32:
          return Conversions.CastInt32Enum(Conversions.ToInteger(Expression), TargetType);
        case TypeCode.UInt32:
          return Conversions.CastUInt32Enum(Conversions.ToUInteger(Expression), TargetType);
        case TypeCode.Int64:
          return Conversions.CastInt64Enum(Conversions.ToLong(Expression), TargetType);
        case TypeCode.UInt64:
          return Conversions.CastUInt64Enum(Conversions.ToULong(Expression), TargetType);
        case TypeCode.Single:
          return (object) Conversions.ToSingle(Expression);
        case TypeCode.Double:
          return (object) Conversions.ToDouble(Expression);
        case TypeCode.Decimal:
          return (object) Conversions.ToDecimal(Expression);
        case TypeCode.DateTime:
          return (object) Conversions.ToDate(Expression);
        case TypeCode.String:
          return (object) Conversions.ToString(Expression);
        default:
          throw new Exception();
      }
    }

    /// <summary>Converts an object to the specified type.</summary>
    /// <param name="Expression">The object to convert.</param>
    /// <param name="TargetType">The type to which to convert the object.</param>
    /// <returns>An object of the specified target type.</returns>
    public static object ChangeType(object Expression, Type TargetType)
    {
      if (TargetType == null)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (TargetType)
        }));
      if (Expression == null)
      {
        if (!Symbols.IsValueType(TargetType))
          return (object) null;
        //new ReflectionPermission(ReflectionPermissionFlag.NoFlags).Demand();
        return Activator.CreateInstance(TargetType);
      }
      Type type = Expression.GetType();
      if (TargetType.IsByRef)
        TargetType = TargetType.GetElementType();
      if (TargetType == type || Symbols.IsRootObjectType(TargetType))
        return Expression;
      if (Symbols.IsIntrinsicType(Symbols.GetTypeCode(TargetType)) && Symbols.IsIntrinsicType(Symbols.GetTypeCode(type)))
        return Conversions.ChangeIntrinsicType(Expression, TargetType);
      if (TargetType.IsInstanceOfType(Expression))
        return Expression;
      if (Symbols.IsCharArrayRankOne(TargetType) && Symbols.IsStringType(type))
        return (object) Conversions.ToCharArrayRankOne((string) Expression);
      if (Symbols.IsStringType(TargetType) && Symbols.IsCharArrayRankOne(type))
        return (object) new string((char[]) Expression);
      if (ConversionResolution.ClassifyPredefinedConversion(TargetType, type) == ConversionResolution.ConversionClass.None && (Symbols.IsClassOrValueType(type) || Symbols.IsClassOrValueType(TargetType)) && (!Symbols.IsIntrinsicType(type) || !Symbols.IsIntrinsicType(TargetType)))
      {
        Symbols.Method OperatorMethod = (Symbols.Method) null;
        ConversionResolution.ConversionClass conversionClass = ConversionResolution.ClassifyUserDefinedConversion(TargetType, type, ref OperatorMethod);
        if ((object) OperatorMethod != null)
          return Conversions.ChangeType(new Symbols.Container(OperatorMethod.DeclaringType).InvokeMethod(OperatorMethod, new object[1]
          {
            Expression
          }, (bool[]) null, BindingFlags.InvokeMethod), TargetType);
        if (conversionClass == ConversionResolution.ConversionClass.Ambiguous)
          throw new InvalidCastException(Utils.GetResourceString("AmbiguousCast2", Utils.VBFriendlyName(type), Utils.VBFriendlyName(TargetType)));
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(type), Utils.VBFriendlyName(TargetType)));
    }
  }
}

