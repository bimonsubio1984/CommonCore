// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Conversion
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Threading;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="Conversion" /> module contains the procedures used to perform various conversion operations.</summary>
  [StandardModule]
  public sealed class Conversion
  {
    private const int NUMPRS_LEADING_WHITE = 1;
    private const int NUMPRS_TRAILING_WHITE = 2;
    private const int NUMPRS_LEADING_PLUS = 4;
    private const int NUMPRS_TRAILING_PLUS = 8;
    private const int NUMPRS_LEADING_MINUS = 16;
    private const int NUMPRS_TRAILING_MINUS = 32;
    private const int NUMPRS_HEX_OCT = 64;
    private const int NUMPRS_PARENS = 128;
    private const int NUMPRS_DECIMAL = 256;
    private const int NUMPRS_THOUSANDS = 512;
    private const int NUMPRS_CURRENCY = 1024;
    private const int NUMPRS_EXPONENT = 2048;
    private const int NUMPRS_USE_ALL = 4096;
    private const int NUMPRS_STD = 8191;
    private const int NUMPRS_NEG = 65536;
    private const int NUMPRS_INEXACT = 131072;
    private const int VTBIT_EMPTY = 0;
    private const int VTBIT_NULL = 2;
    private const int VTBIT_I2 = 4;
    private const int VTBIT_I4 = 8;
    private const int VTBIT_R4 = 16;
    private const int VTBIT_R8 = 32;
    private const int VTBIT_CY = 64;
    private const int VTBIT_DATE = 128;
    private const int VTBIT_BSTR = 256;
    private const int VTBIT_OBJECT = 512;
    private const int VTBIT_ERROR = 1024;
    private const int VTBIT_BOOL = 2048;
    private const int VTBIT_VARIANT = 4096;
    private const int VTBIT_DATAOBJECT = 8192;
    private const int VTBIT_DECIMAL = 16384;
    private const int VTBIT_BYTE = 131072;
    private const int VTBIT_CHAR = 262144;
    private const int VTBIT_LONG = 1048576;
    private const int MAX_ERR_NUMBER = 65535;
    private const int LOCALE_NOUSEROVERRIDE = -2147483648;
    private const int LCID_US_ENGLISH = 1033;
    private const int PRSFLAGS = 2388;
    private const int VTBITS = 16428;
    private const char TYPE_INDICATOR_INT16 = '%';
    private const char TYPE_INDICATOR_INT32 = '&';
    private const char TYPE_INDICATOR_SINGLE = '!';
    private const char TYPE_INDICATOR_DECIMAL = '@';

    /// <summary>Returns the error message that corresponds to a given error number.</summary>
    /// <returns>The error message that corresponds to the most recent run-time error.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ErrorNumber" /> is out of range.</exception>
    public static string ErrorToString()
    {
      return Information.Err().Description;
    }

    /// <summary>Returns the error message that corresponds to a given error number.</summary>
    /// <param name="ErrorNumber">Optional. Any valid error number.</param>
    /// <returns>The error message that corresponds to a given error number.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="ErrorNumber" /> is out of range.</exception>
    public static string ErrorToString(int ErrorNumber)
    {
      if (ErrorNumber >= (int) ushort.MaxValue)
        throw new ArgumentException(Utils.GetResourceString("MaxErrNumber"));
      if (ErrorNumber > 0)
        ErrorNumber = -2146828288 | ErrorNumber;
      if ((ErrorNumber & 536805376) == 655360)
      {
        ErrorNumber &= (int) ushort.MaxValue;
        return Utils.GetResourceString((vbErrors) ErrorNumber);
      }
      if (ErrorNumber != 0)
        return Utils.GetResourceString(vbErrors.UserDefined);
      return "";
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static short Fix(short Number)
    {
      return Number;
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static int Fix(int Number)
    {
      return Number;
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static long Fix(long Number)
    {
      return Number;
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static double Fix(double Number)
    {
      if (Number >= 0.0)
        return Math.Floor(Number);
      return -Math.Floor(-Number);
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static float Fix(float Number)
    {
      if ((double) Number >= 0.0)
        return (float) Math.Floor((double) Number);
      return (float) -Math.Floor(-(double) Number);
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static Decimal Fix(Decimal Number)
    {
      if (Number < Decimal.Zero)
        return Decimal.Negate(Decimal.Floor(Decimal.Negate(Number)));
      return Decimal.Floor(Number);
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static object Fix(object Number)
    {
      if (Number == null)
        throw new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (Number)
        }));
      IConvertible convertible = Number as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            return (object) convertible.ToInt32((IFormatProvider) null);
          case TypeCode.SByte:
          case TypeCode.Byte:
          case TypeCode.Int16:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
          case TypeCode.Int64:
          case TypeCode.UInt64:
            return Number;
          case TypeCode.Single:
            return (object) Conversion.Fix(convertible.ToSingle((IFormatProvider) null));
          case TypeCode.Double:
            return (object) Conversion.Fix(convertible.ToDouble((IFormatProvider) null));
          case TypeCode.Decimal:
            return (object) Conversion.Fix(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            return (object) Conversion.Fix(Conversions.ToDouble(convertible.ToString((IFormatProvider) null)));
        }
      }
      throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_NotNumericType2", nameof (Number), Number.GetType().FullName)), 13);
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static short Int(short Number)
    {
      return Number;
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static int Int(int Number)
    {
      return Number;
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static long Int(long Number)
    {
      return Number;
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static double Int(double Number)
    {
      return Math.Floor(Number);
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static float Int(float Number)
    {
      return (float) Math.Floor((double) Number);
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static Decimal Int(Decimal Number)
    {
      return Decimal.Floor(Number);
    }

    /// <summary>Return the integer portion of a number.</summary>
    /// <param name="Number">Required. A number of type <see langword="Double" /> or any valid numeric expression. If <paramref name="Number" /> contains <see langword="Nothing" />, <see langword="Nothing" /> is returned.</param>
    /// <returns>Return the integer portion of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">Number is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">Number is not a numeric type.</exception>
    public static object Int(object Number)
    {
      if (Number == null)
        throw new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (Number)
        }));
      IConvertible convertible = Number as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            return (object) convertible.ToInt32((IFormatProvider) null);
          case TypeCode.SByte:
          case TypeCode.Byte:
          case TypeCode.Int16:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
          case TypeCode.Int64:
          case TypeCode.UInt64:
            return Number;
          case TypeCode.Single:
            return (object) Conversion.Int(convertible.ToSingle((IFormatProvider) null));
          case TypeCode.Double:
            return (object) Conversion.Int(convertible.ToDouble((IFormatProvider) null));
          case TypeCode.Decimal:
            return (object) Conversion.Int(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.String:
            return (object) Conversion.Int(Conversions.ToDouble(convertible.ToString((IFormatProvider) null)));
        }
      }
      throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_NotNumericType2", nameof (Number), Number.GetType().FullName)), 13);
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    [CLSCompliant(false)]
    public static string Hex(sbyte Number)
    {
      return Number.ToString("X");
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Hex(byte Number)
    {
      return Number.ToString("X");
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Hex(short Number)
    {
      return Number.ToString("X");
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    [CLSCompliant(false)]
    public static string Hex(ushort Number)
    {
      return Number.ToString("X");
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Hex(int Number)
    {
      return Number.ToString("X");
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    [CLSCompliant(false)]
    public static string Hex(uint Number)
    {
      return Number.ToString("X");
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Hex(long Number)
    {
      return Number.ToString("X");
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    [CLSCompliant(false)]
    public static string Hex(ulong Number)
    {
      return Number.ToString("X");
    }

    /// <summary>Returns a string representing the hexadecimal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the hexadecimal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Hex(object Number)
    {
      if (Number == null)
        throw new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (Number)
        }));
      IConvertible convertible = Number as IConvertible;
      if (convertible != null)
      {
        long int64;
        switch (convertible.GetTypeCode())
        {
          case TypeCode.SByte:
            return Conversion.Hex(convertible.ToSByte((IFormatProvider) null));
          case TypeCode.Byte:
            return Conversion.Hex(convertible.ToByte((IFormatProvider) null));
          case TypeCode.Int16:
            return Conversion.Hex(convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            return Conversion.Hex(convertible.ToUInt16((IFormatProvider) null));
          case TypeCode.Int32:
            return Conversion.Hex(convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            return Conversion.Hex(convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
          case TypeCode.Single:
          case TypeCode.Double:
          case TypeCode.Decimal:
            int64 = convertible.ToInt64((IFormatProvider) null);
            break;
          case TypeCode.UInt64:
            return Conversion.Hex(convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.String:
            try
            {
              int64 = Conversions.ToLong(convertible.ToString((IFormatProvider) null));
              break;
            }
            catch (OverflowException ex)
            {
              return Conversion.Hex(Conversions.ToULong(convertible.ToString((IFormatProvider) null)));
            }
          default:
            goto label_19;
        }
        if (int64 == 0L)
          return "0";
        if (int64 > 0L || int64 < (long) int.MinValue)
          return Conversion.Hex(int64);
        return Conversion.Hex(checked ((int) int64));
      }
label_19:
      throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValueType2", nameof (Number), Utils.VBFriendlyName(Number)));
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    [CLSCompliant(false)]
    public static string Oct(sbyte Number)
    {
      return Utils.OctFromLong((long) Number & (long) byte.MaxValue);
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Oct(byte Number)
    {
      return Utils.OctFromULong((ulong) Number);
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Oct(short Number)
    {
      return Utils.OctFromLong((long) Number & (long) ushort.MaxValue);
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    [CLSCompliant(false)]
    public static string Oct(ushort Number)
    {
      return Utils.OctFromULong((ulong) Number);
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Oct(int Number)
    {
      return Utils.OctFromLong((long) Number & (long) uint.MaxValue);
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    [CLSCompliant(false)]
    public static string Oct(uint Number)
    {
      return Utils.OctFromULong((ulong) Number);
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Oct(long Number)
    {
      return Utils.OctFromLong(Number);
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    [CLSCompliant(false)]
    public static string Oct(ulong Number)
    {
      return Utils.OctFromULong(Number);
    }

    /// <summary>Returns a string representing the octal value of a number.</summary>
    /// <param name="Number">Required. Any valid numeric expression or <see langword="String" /> expression.</param>
    /// <returns>Returns a string representing the octal value of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Oct(object Number)
    {
      if (Number == null)
        throw new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (Number)
        }));
      IConvertible convertible = Number as IConvertible;
      if (convertible != null)
      {
        long int64;
        switch (convertible.GetTypeCode())
        {
          case TypeCode.SByte:
            return Conversion.Oct(convertible.ToSByte((IFormatProvider) null));
          case TypeCode.Byte:
            return Conversion.Oct(convertible.ToByte((IFormatProvider) null));
          case TypeCode.Int16:
            return Conversion.Oct(convertible.ToInt16((IFormatProvider) null));
          case TypeCode.UInt16:
            return Conversion.Oct(convertible.ToUInt16((IFormatProvider) null));
          case TypeCode.Int32:
            return Conversion.Oct(convertible.ToInt32((IFormatProvider) null));
          case TypeCode.UInt32:
            return Conversion.Oct(convertible.ToUInt32((IFormatProvider) null));
          case TypeCode.Int64:
          case TypeCode.Single:
          case TypeCode.Double:
          case TypeCode.Decimal:
            int64 = convertible.ToInt64((IFormatProvider) null);
            break;
          case TypeCode.UInt64:
            return Conversion.Oct(convertible.ToUInt64((IFormatProvider) null));
          case TypeCode.String:
            try
            {
              int64 = Conversions.ToLong(convertible.ToString((IFormatProvider) null));
              break;
            }
            catch (OverflowException ex)
            {
              return Conversion.Oct(Conversions.ToULong(convertible.ToString((IFormatProvider) null)));
            }
          default:
            goto label_19;
        }
        if (int64 == 0L)
          return "0";
        if (int64 > 0L || int64 < (long) int.MinValue)
          return Conversion.Oct(int64);
        return Conversion.Oct(checked ((int) int64));
      }
label_19:
      throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValueType2", nameof (Number), Utils.VBFriendlyName(Number)));
    }

    /// <summary>Returns a <see langword="String" /> representation of a number.</summary>
    /// <param name="Number">Required. An <see langword="Object" /> containing any valid numeric expression.</param>
    /// <returns>Returns a <see langword="String" /> representation of a number.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Number" /> is not specified.</exception>
    /// <exception cref="T:System.InvalidCastException">
    /// <paramref name="Number" /> is not a numeric type.</exception>
    public static string Str(object Number)
    {
      if (Number == null)
        throw new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (Number)
        }));
      IConvertible convertible = Number as IConvertible;
      if (convertible == null)
        throw new InvalidCastException(Utils.GetResourceString("ArgumentNotNumeric1", new string[1]
        {
          nameof (Number)
        }));
      TypeCode typeCode = convertible.GetTypeCode();
      string s;
      switch (typeCode)
      {
        case TypeCode.DBNull:
          return "Null";
        case TypeCode.Boolean:
          return convertible.ToBoolean((IFormatProvider) null) ? "True" : "False";
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          s = Conversions.ToString(Number);
          break;
        default:
          if (typeCode == TypeCode.String)
          {
            try
            {
              s = Conversions.ToString(Conversions.ToDouble(convertible.ToString((IFormatProvider) null)));
              break;
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
            }
          }
          throw new InvalidCastException(Utils.GetResourceString("ArgumentNotNumeric1", new string[1]
          {
            nameof (Number)
          }));
      }
      if (s.Length > 0 && s[0] != '-')
        return " " + Utils.StdFormat(s);
      return Utils.StdFormat(s);
    }

    private static double HexOrOctValue(string InputStr, int i)
    {
      int num1 = 0;
      int length = InputStr.Length;
      char ch1 = InputStr[i];
      checked { ++i; }
      switch (ch1)
      {
        case 'H':
        case 'h':
          long num2=0;
          while (i < length && num1 < 17)
          {
            ch1 = InputStr[i];
            checked { ++i; }
            char ch2 = ch1;
            if (ch2 != '\t' && ch2 != '\n' && (ch2 != '\r' && ch2 != ' ') && ch2 != '　')
            {
              int num3;
              if (ch2 == '0')
              {
                if (num1 != 0)
                  num3 = 0;
                else
                  continue;
              }
              else if (ch2 >= '1' && ch2 <= '9')
                num3 = checked ((int) ch1 - 48);
              else if (ch2 >= 'A' && ch2 <= 'F')
                num3 = checked ((int) ch1 - 55);
              else if (ch2 >= 'a' && ch2 <= 'f')
                num3 = checked ((int) ch1 - 87);
              else
                break;
              num2 = checked (unchecked (num1 != 15) || unchecked (num2 <= 576460752303423487L) ? num2 * 16L : (num2 & 576460752303423487L) * 16L | long.MinValue + (long) num3);
              checked { ++num1; }
            }
          }
          if (num1 == 16)
          {
            checked { ++i; }
            if (i < length)
              ch1 = InputStr[i];
          }
          if (num1 <= 8)
          {
            if (num1 > 4 || ch1 == '&')
            {
              if (num2 > (long) int.MaxValue)
                num2 = checked ((long) int.MinValue + num2 & (long) int.MaxValue);
            }
            else if ((num1 > 2 || ch1 == '%') && num2 > (long) short.MaxValue)
              num2 = checked ((long) short.MinValue + num2 & (long) short.MaxValue);
          }
          switch (ch1)
          {
            case '%':
              num2 = (long) checked ((short) num2);
              break;
            case '&':
              num2 = (long) checked ((int) num2);
              break;
          }
          return (double) num2;
        case 'O':
        case 'o':
          long num4=0;
          while (i < length && num1 < 22)
          {
            ch1 = InputStr[i];
            checked { ++i; }
            char ch2 = ch1;
            if (ch2 != '\t' && ch2 != '\n' && (ch2 != '\r' && ch2 != ' ') && ch2 != '　')
            {
              int num3;
              if (ch2 == '0')
              {
                if (num1 != 0)
                  num3 = 0;
                else
                  continue;
              }
              else if (ch2 >= '1' && ch2 <= '7')
                num3 = checked ((int) ch1 - 48);
              else
                break;
              num4 = checked (unchecked (num4 < 1152921504606846976L) ? num4 * 8L : (num4 & 1152921504606846975L) * 8L | 1152921504606846976L + (long) num3);
              checked { ++num1; }
            }
          }
          if (num1 == 22)
          {
            checked { ++i; }
            if (i < length)
              ch1 = InputStr[i];
          }
          if (num4 <= 4294967296L)
          {
            if (num4 > (long) ushort.MaxValue || ch1 == '&')
            {
              if (num4 > (long) int.MaxValue)
                num4 = checked ((long) int.MinValue + num4 & (long) int.MaxValue);
            }
            else if ((num4 > (long) byte.MaxValue || ch1 == '%') && num4 > (long) short.MaxValue)
              num4 = checked ((long) short.MinValue + num4 & (long) short.MaxValue);
          }
          switch (ch1)
          {
            case '%':
              num4 = (long) checked ((short) num4);
              break;
            case '&':
              num4 = (long) checked ((int) num4);
              break;
          }
          return (double) num4;
        default:
          return 0.0;
      }
    }

    /// <summary>Returns the numbers contained in a string as a numeric value of appropriate type.</summary>
    /// <param name="InputStr">Required. Any valid <see langword="String" /> expression, <see langword="Object" /> variable, or <see langword="Char" /> value. If <paramref name="Expression" /> is of type <see langword="Object" />, its value must be convertible to <see langword="String" /> or an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>Returns the numbers contained in a string as a numeric value of appropriate type.</returns>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="InputStr" /> is too large.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Expression" /> is an <see langword="Object" /> type expression that is not convertible to <see langword="String" />.</exception>
    public static double Val(string InputStr)
    {
      int num1 = InputStr != null ? InputStr.Length : 0;
      int index = 0;
      while (index < num1)
      {
        switch (InputStr[index])
        {
          case '\t':
          case '\n':
          case '\r':
          case ' ':
          case '　':
            checked { ++index; }
            continue;
          default:
            goto label_4;
        }
      }
label_4:
      if (index >= num1)
        return 0.0;
      if (InputStr[index] == '&')
        return Conversion.HexOrOctValue(InputStr, checked (index + 1));
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      double num2 = 0.0;
      char ch1 = InputStr[index];
      switch (ch1)
      {
        case '+':
          checked { ++index; }
          break;
        case '-':
          flag3 = true;
          checked { ++index; }
          break;
      }
      int num3=0;
      double num4 = 0;
      int num5 = 0;
      while (index < num1)
      {
        ch1 = InputStr[index];
        char ch2 = ch1;
        if (ch2 == '\t' || ch2 == '\n' || (ch2 == '\r' || ch2 == ' ') || ch2 == '　')
          checked { ++index; }
        else if (ch2 == '0')
        {
          if (num3 != 0 || flag1)
          {
            num4 = num4 * 10.0 + (double) ch1 - 48.0;
            checked { ++index; }
            checked { ++num3; }
          }
          else
            checked { ++index; }
        }
        else if (ch2 >= '1' && ch2 <= '9')
        {
          num4 = num4 * 10.0 + (double) ch1 - 48.0;
          checked { ++index; }
          checked { ++num3; }
        }
        else if (ch2 == '.')
        {
          checked { ++index; }
          if (!flag1)
          {
            flag1 = true;
            num5 = num3;
          }
          else
            break;
        }
        else
        {
          if (ch2 == 'e' || ch2 == 'E' || (ch2 == 'd' || ch2 == 'D'))
          {
            flag2 = true;
            checked { ++index; }
            break;
          }
          break;
        }
      }
      int num6=0;
      if (flag1)
        num6 = checked (num3 - num5);
      if (flag2)
      {
        bool flag4 = false;
        bool flag5 = false;
        while (index < num1)
        {
          ch1 = InputStr[index];
          char ch2 = ch1;
          if (ch2 == '\t' || ch2 == '\n' || (ch2 == '\r' || ch2 == ' ') || ch2 == '　')
            checked { ++index; }
          else if (ch2 >= '0' && ch2 <= '9')
          {
            num2 = num2 * 10.0 + (double) ch1 - 48.0;
            checked { ++index; }
          }
          else if (ch2 == '+')
          {
            if (!flag4)
            {
              flag4 = true;
              checked { ++index; }
            }
            else
              break;
          }
          else if (ch2 == '-' && !flag4)
          {
            flag4 = true;
            flag5 = true;
            checked { ++index; }
          }
          else
            break;
        }
        if (flag5)
        {
          double num7 = num2 + (double) num6;
          num4 *= Math.Pow(10.0, -num7);
        }
        else
        {
          double y = num2 - (double) num6;
          num4 *= Math.Pow(10.0, y);
        }
      }
      else if (flag1 && num6 != 0)
        num4 /= Math.Pow(10.0, (double) num6);
      if (double.IsInfinity(num4))
        throw ExceptionUtils.VbMakeException(6);
      if (flag3)
        num4 = -num4;
      switch (ch1)
      {
        case '!':
          num4 = num4;
          break;
        case '%':
          if (num6 > 0)
            throw ExceptionUtils.VbMakeException(13);
          num4 = (double) checked ((short) Math.Round(num4));
          break;
        case '&':
          if (num6 > 0)
            throw ExceptionUtils.VbMakeException(13);
          num4 = (double) checked ((int) Math.Round(num4));
          break;
        case '@':
          num4 = Convert.ToDouble(new Decimal(num4));
          break;
      }
      return num4;
    }

    /// <summary>Returns the numbers contained in a string as a numeric value of appropriate type.</summary>
    /// <param name="Expression">Required. Any valid <see langword="String" /> expression, <see langword="Object" /> variable, or <see langword="Char" /> value. If <paramref name="Expression" /> is of type <see langword="Object" />, its value must be convertible to <see langword="String" /> or an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>Returns the numbers contained in a string as a numeric value of appropriate type.</returns>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="InputStr" /> is too large.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Expression" /> is an <see langword="Object" /> type expression that is not convertible to <see langword="String" />.</exception>
    public static int Val(char Expression)
    {
      int num = (int) Expression;
      if (num >= 49 && num <= 57)
        return checked (num - 48);
      return 0;
    }

    /// <summary>Returns the numbers contained in a string as a numeric value of appropriate type.</summary>
    /// <param name="Expression">Required. Any valid <see langword="String" /> expression, <see langword="Object" /> variable, or <see langword="Char" /> value. If <paramref name="Expression" /> is of type <see langword="Object" />, its value must be convertible to <see langword="String" /> or an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>Returns the numbers contained in a string as a numeric value of appropriate type.</returns>
    /// <exception cref="T:System.OverflowException">
    /// <paramref name="InputStr" /> is too large.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Expression" /> is an <see langword="Object" /> type expression that is not convertible to <see langword="String" />.</exception>
    public static double Val(object Expression)
    {
      string InputStr1 = Expression as string;
      if (InputStr1 != null)
        return Conversion.Val(InputStr1);
      if (Expression is char)
        return (double) Conversion.Val((char) Expression);
      if (Versioned.IsNumeric(Expression))
        return Conversions.ToDouble(Expression);
      string InputStr2;
      try
      {
        InputStr2 = Conversions.ToString(Expression);
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
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_InvalidValueType2", nameof (Expression), Utils.VBFriendlyName(Expression))), 438);
      }
      return Conversion.Val(InputStr2);
    }

    /*
    internal static object ParseInputField(object Value, VariantType vtInput)
    {
      string str = Conversions.ToString(Value);
      if (vtInput == VariantType.Empty && (Value == null || Strings.Len(Conversions.ToString(Value)) == 0))
        return (object) null;
      ProjectData projectData = ProjectData.GetProjectData();
      byte[] numprsPtr = projectData.m_numprsPtr;
      byte[] digitArray = projectData.m_DigitArray;
      Array.Copy((Array) BitConverter.GetBytes(Convert.ToInt32(digitArray.Length)), 0, (Array) numprsPtr, 0, 4);
      Array.Copy((Array) BitConverter.GetBytes(Convert.ToInt32(2388)), 0, (Array) numprsPtr, 4, 4);
      if (UnsafeNativeMethods.VarParseNumFromStr(str, 1033, int.MinValue, numprsPtr, digitArray) < 0)
      {
        if (vtInput != VariantType.Empty)
          return (object) 0;
        return (object) str;
      }
      int int32_1 = BitConverter.ToInt32(numprsPtr, 8);
      int int32_2 = BitConverter.ToInt32(numprsPtr, 12);
      int int32_3 = BitConverter.ToInt32(numprsPtr, 16);
      int int32_4 = BitConverter.ToInt32(numprsPtr, 20);
      char ch=char.MinValue;
      if (int32_2 < str.Length)
        ch = str[int32_2];
      int vt;
      int num;
      switch (ch)
      {
        case '!':
          vt = vtInput != VariantType.Double ? 4 : 5;
          num = int.MaxValue;
          break;
        case '#':
          vt = 5;
          num = int.MaxValue;
          break;
        case '%':
          vt = 2;
          num = 0;
          break;
        case '&':
          vt = 3;
          num = 0;
          break;
        case '@':
          vt = 14;
          num = 4;
          break;
        default:
          if (vtInput == VariantType.Empty)
          {
            int dwVtBits = 16428;
            if ((int32_1 & 2048) != 0)
              dwVtBits = 32;
            return UnsafeNativeMethods.VarNumFromParseNum(numprsPtr, digitArray, dwVtBits);
          }
          if (int32_3 == 0)
            return UnsafeNativeMethods.VarNumFromParseNum(numprsPtr, digitArray, Conversion.ShiftVTBits((int) vtInput));
          Value = UnsafeNativeMethods.VarNumFromParseNum(numprsPtr, digitArray, 8);
          if ((Conversions.ToInteger(Value) & -65536) == 0)
            ;
          UnsafeNativeMethods.VariantChangeType(out Value, ref Value, (short) 0, checked ((short) vtInput));
          return Value;
      }
      if (checked (-int32_4) > num)
        throw ExceptionUtils.VbMakeException(13);
      Value = UnsafeNativeMethods.VarNumFromParseNum(numprsPtr, digitArray, Conversion.ShiftVTBits(vt));
      if (vtInput == VariantType.Empty)
        return Value;
      UnsafeNativeMethods.VariantChangeType(out Value, ref Value, (short) 0, checked ((short) vtInput));
      return Value;
    }
*/
    private static int ShiftVTBits(int vt)
    {
      switch (vt)
      {
        case 2:
          return 4;
        case 3:
          return 8;
        case 4:
          return 16;
        case 5:
          return 32;
        case 6:
        case 14:
          return 16384;
        case 7:
          return 128;
        case 8:
          return 256;
        case 9:
          return 512;
        case 10:
          return 1024;
        case 11:
          return 2048;
        case 12:
          return 4096;
        case 13:
          return 8192;
        case 17:
          return 131072;
        case 18:
          return 262144;
        case 20:
          return 1048576;
        default:
          return 0;
      }
    }
  }
}
