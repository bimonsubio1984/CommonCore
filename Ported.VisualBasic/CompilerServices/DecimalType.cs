// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.DecimalType
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
  public sealed class DecimalType
  {
    private DecimalType()
    {
    }

    /// <summary>Returns a <see langword="Decimal" /> value that corresponds to the specified <see langword="Boolean" /> value. </summary>
    /// <param name="Value">Required. <see langword="Boolean" /> value to convert to a <see langword="Decimal" /> value.</param>
    /// <returns>The <see langword="Decimal" /> value that corresponds to <paramref name="Value" />.</returns>
    public static Decimal FromBoolean(bool Value)
    {
      if (Value)
        return Decimal.MinusOne;
      return Decimal.Zero;
    }

    /// <summary>Returns a <see langword="Decimal" /> value that corresponds to the specified string. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Decimal" /> value.</param>
    /// <returns>The <see langword="Decimal" /> value that corresponds to <paramref name="Value" />.</returns>
    public static Decimal FromString(string Value)
    {
      return DecimalType.FromString(Value, (NumberFormatInfo) null);
    }

    /// <summary>Returns a <see langword="Decimal" /> value that corresponds to the specified string and number format information. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Decimal" /> value.</param>
    /// <param name="NumberFormat">A <see cref="T:System.Globalization.NumberFormatInfo" /> object that defines how numeric values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="Decimal" /> value that corresponds to <paramref name="Value" />.</returns>
    public static Decimal FromString(string Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return Decimal.Zero;
      try
      {
        long i64Value=0;
        if (Utils.IsHexOrOctValue(Value, ref i64Value))
          return new Decimal(i64Value);
        return DecimalType.Parse(Value, NumberFormat);
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

    /// <summary>Returns a <see langword="Decimal" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Decimal" /> value.</param>
    /// <returns>The <see langword="Decimal" /> value that corresponds to <paramref name="Value" />.</returns>
    public static Decimal FromObject(object Value)
    {
      return DecimalType.FromObject(Value, (NumberFormatInfo) null);
    }

    /// <summary>Returns a <see langword="Decimal" /> value that corresponds to the specified object and number format information. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="Decimal" /> value.</param>
    /// <param name="NumberFormat">A <see cref="T:System.Globalization.NumberFormatInfo" /> object that defines how numeric values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="Decimal" /> value that corresponds to <paramref name="Value" />.</returns>
    public static Decimal FromObject(object Value, NumberFormatInfo NumberFormat)
    {
      if (Value == null)
        return Decimal.Zero;
      IConvertible convertible = Value as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            return DecimalType.FromBoolean(convertible.ToBoolean((IFormatProvider) null));
          case TypeCode.Byte:
            return new Decimal((int) convertible.ToByte((IFormatProvider) null));
          case TypeCode.Int16:
            return new Decimal((int) convertible.ToInt16((IFormatProvider) null));
          case TypeCode.Int32:
            return new Decimal(convertible.ToInt32((IFormatProvider) null));
          case TypeCode.Int64:
            return new Decimal(convertible.ToInt64((IFormatProvider) null));
          case TypeCode.Single:
            return new Decimal(convertible.ToSingle((IFormatProvider) null));
          case TypeCode.Double:
            return new Decimal(convertible.ToDouble((IFormatProvider) null));
          case TypeCode.Decimal:
            return convertible.ToDecimal((IFormatProvider) null);
          case TypeCode.String:
            return DecimalType.FromString(convertible.ToString((IFormatProvider) null), NumberFormat);
        }
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "Decimal"));
    }

    /// <summary>Returns a <see langword="Decimal" /> value that corresponds to the specified string and number format information. </summary>
    /// <param name="Value">Required. String to convert to a <see langword="Decimal" /> value.</param>
    /// <param name="NumberFormat">A <see cref="T:System.Globalization.NumberFormatInfo" /> object that defines how numeric values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="Decimal" /> value that corresponds to <paramref name="Value" />.</returns>
    public static Decimal Parse(string Value, NumberFormatInfo NumberFormat)
    {
      CultureInfo cultureInfo = Utils.GetCultureInfo();
      if (NumberFormat == null)
        NumberFormat = cultureInfo.NumberFormat;
      NumberFormatInfo normalizedNumberFormat = DecimalType.GetNormalizedNumberFormat(NumberFormat);
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

    internal static NumberFormatInfo GetNormalizedNumberFormat(NumberFormatInfo InNumberFormat)
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
  }
}
