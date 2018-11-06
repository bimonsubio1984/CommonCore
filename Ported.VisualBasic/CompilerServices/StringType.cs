// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.StringType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated as of Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class StringType
  {
    private const string GENERAL_FORMAT = "G";

    private StringType()
    {
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to the specified <see langword="Boolean" />. </summary>
    /// <param name="Value">Required. <see langword="Boolean" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromBoolean(bool Value)
    {
      if (Value)
        return bool.TrueString;
      return bool.FalseString;
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to the specified <see langword="Byte" />. </summary>
    /// <param name="Value">Required. <see langword="Byte" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromByte(byte Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to the specified <see langword="Char" />. </summary>
    /// <param name="Value">Required. <see langword="Char" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromChar(char Value)
    {
      return Value.ToString();
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to a specified <see langword="Int16" /> (16-bit integer).</summary>
    /// <param name="Value">Required. <see langword="Int616" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromShort(short Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to a specified <see langword="Integer" />. </summary>
    /// <param name="Value">Required. <see langword="Integer" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromInteger(int Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to a specified <see langword="Int64" /> (64-bit integer).</summary>
    /// <param name="Value">Required. <see langword="Int64" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromLong(long Value)
    {
      return Value.ToString((string) null, (IFormatProvider) null);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to a specified <see langword="Single" />. </summary>
    /// <param name="Value">Required. <see langword="Single" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromSingle(float Value)
    {
      return StringType.FromSingle(Value, (NumberFormatInfo) null);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to a specified <see langword="Double" />. </summary>
    /// <param name="Value">Required. <see langword="Double" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromDouble(double Value)
    {
      return StringType.FromDouble(Value, (NumberFormatInfo) null);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to a specified <see langword="Single" /> and number format information. </summary>
    /// <param name="Value">Required. <see langword="Sinble" /> to convert to a <see langword="String" /> value.</param>
    /// <param name="NumberFormat">A <see cref="T:System.Globalization.NumberFormatInfo" /> object that defines how numeric values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromSingle(float Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString((string) null, (IFormatProvider) NumberFormat);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to a specified <see langword="Double" /> and number format information. </summary>
    /// <param name="Value">Required. <see langword="Double" /> to convert to a <see langword="String" /> value.</param>
    /// <param name="NumberFormat">A <see cref="T:System.Globalization.NumberFormatInfo" /> object that defines how numeric values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromDouble(double Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString("G", (IFormatProvider) NumberFormat);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to the specified date. </summary>
    /// <param name="Value">Required. Date to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromDate(DateTime Value)
    {
      long ticks = Value.TimeOfDay.Ticks;
      if (ticks == Value.Ticks || Value.Year == 1899 && Value.Month == 12 && Value.Day == 30)
        return Value.ToString("T", (IFormatProvider) null);
      if (ticks == 0L)
        return Value.ToString("d", (IFormatProvider) null);
      return Value.ToString("G", (IFormatProvider) null);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to the specified <see langword="Decimal" />. </summary>
    /// <param name="Value">Required. <see langword="Decimal" /> to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromDecimal(Decimal Value)
    {
      return StringType.FromDecimal(Value, (NumberFormatInfo) null);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to a specified <see langword="Decimal" /> and number format information. </summary>
    /// <param name="Value">Required. <see langword="Decimal" /> to convert to a <see langword="String" /> value.</param>
    /// <param name="NumberFormat">A <see cref="T:System.Globalization.NumberFormatInfo" /> object that defines how numeric values are formatted and displayed, depending on the culture.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromDecimal(Decimal Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString("G", (IFormatProvider) NumberFormat);
    }

    /// <summary>Returns a <see langword="String" /> value that corresponds to the specified object. </summary>
    /// <param name="Value">Required. Object to convert to a <see langword="String" /> value.</param>
    /// <returns>The <see langword="String" /> value corresponding to <paramref name="Value" />.</returns>
    public static string FromObject(object Value)
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
            return StringType.FromBoolean(convertible.ToBoolean((IFormatProvider) null));
          case TypeCode.Char:
            return StringType.FromChar(convertible.ToChar((IFormatProvider) null));
          case TypeCode.Byte:
            return StringType.FromByte(convertible.ToByte((IFormatProvider) null));
          case TypeCode.Int16:
            return StringType.FromShort(convertible.ToInt16((IFormatProvider) null));
          case TypeCode.Int32:
            return StringType.FromInteger(convertible.ToInt32((IFormatProvider) null));
          case TypeCode.Int64:
            return StringType.FromLong(convertible.ToInt64((IFormatProvider) null));
          case TypeCode.Single:
            return StringType.FromSingle(convertible.ToSingle((IFormatProvider) null));
          case TypeCode.Double:
            return StringType.FromDouble(convertible.ToDouble((IFormatProvider) null));
          case TypeCode.Decimal:
            return StringType.FromDecimal(convertible.ToDecimal((IFormatProvider) null));
          case TypeCode.DateTime:
            return StringType.FromDate(convertible.ToDateTime((IFormatProvider) null));
          case TypeCode.String:
            return convertible.ToString((IFormatProvider) null);
        }
      }
      else
      {
        char[] chArray = Value as char[];
        if (chArray != null && chArray.Rank == 1)
          return new string(CharArrayType.FromObject(Value));
      }
      throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(Value), "String"));
    }

    /// <summary>Compares two strings.</summary>
    /// <param name="sLeft">Required. String to compare with <paramref name="sRight" />.</param>
    /// <param name="sRight">Required. String to compare with <paramref name="sLeft" />.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive comparison; otherwise <see langword="False" />.</param>
    /// <returns>Value Condition zero The two strings are equal. less than zero
    /// <paramref name="sLeft" /> is less than <paramref name="sRight" />. greater than zero
    /// <paramref name="sLeft" /> is greater than <paramref name="sRight" />. </returns>
    public static int StrCmp(string sLeft, string sRight, bool TextCompare)
    {
      if ((object) sLeft == (object) sRight)
        return 0;
      if (sLeft == null)
        return sRight.Length == 0 ? 0 : -1;
      if (sRight == null)
        return sLeft.Length == 0 ? 0 : 1;
      if (TextCompare)
        return Utils.GetCultureInfo().CompareInfo.Compare(sLeft, sRight, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
      return string.CompareOrdinal(sLeft, sRight);
    }

    /// <summary>Compares the parameters <paramref name="Source" /> and <paramref name="Pattern" /> and returns the same results as the <see langword="Like" /> operator.</summary>
    /// <param name="Source">Required. Any <see langword="String" /> expression.</param>
    /// <param name="Pattern">Any <see langword="String" /> expression conforming to the pattern-matching conventions described in Like Operator.</param>
    /// <param name="CompareOption">Specifies how to compare strings to patterns, according to the <see cref="T:Ported.VisualBasic.CompareMethod" />. Can be <see langword="vbBinaryCompare" /> for binary comparison or <see langword="vbTextCompare" /> for comparison based on a case-insensitive text sort order determined by your system's <see langword="LocaleID" /> value.</param>
    /// <returns>A <see langword="Boolean" /> value indicating whether or not the string satisfies the pattern. If the value in string satisfies the pattern contained in pattern, result is <see langword="True" />. If the string does not satisfy the pattern, result is <see langword="False" />. If both string and pattern are empty strings, the result is <see langword="True" />.</returns>
    public static bool StrLike(string Source, string Pattern, CompareMethod CompareOption)
    {
      if (CompareOption == CompareMethod.Binary)
        return StringType.StrLikeBinary(Source, Pattern);
      return StringType.StrLikeText(Source, Pattern);
    }

    /// <summary>Compares the parameters <paramref name="Source" /> and <paramref name="Pattern" /> and returns the same results as the <see langword="Like" /> operator, using binary comparison.</summary>
    /// <param name="Source">Required. Any <see langword="String" /> expression.</param>
    /// <param name="Pattern">Required. Any <see langword="String" /> expression conforming to the pattern-matching conventions described in Like Operator.</param>
    /// <returns>A <see langword="Boolean" /> value indicating whether or not the string satisfies the pattern. If the value in string satisfies the pattern contained in pattern, result is <see langword="True" />. If the string does not satisfy the pattern, result is <see langword="False" />. If both string and pattern are empty strings, the result is <see langword="True" />.</returns>
    public static bool StrLikeBinary(string Source, string Pattern)
    {
      bool flag1 = false;
      int num1 = Pattern != null ? Pattern.Length : 0;
      int num2 = Source != null ? Source.Length : 0;
      int index1 = 0;
      char ch1=char.MinValue;
      if (index1 < num2)
        ch1 = Source[index1];
      int index2 = 0;
      bool flag2=false;
      int startIndex = 0;
      while (index2 < num1)
      {
        char p = Pattern[index2];
        bool Match=false;
        char ch2=char.MinValue;
        if (p == '*' && !flag2)
        {
          int num3 = StringType.AsteriskSkip(Pattern.Substring(checked (index2 + 1)), Source.Substring(startIndex), checked (num2 - startIndex), CompareMethod.Binary, Strings.m_InvariantCompareInfo);
          if (num3 < 0)
            return false;
          if (num3 > 0)
          {
            checked { startIndex += num3; }
            if (startIndex < num2)
              ch1 = Source[startIndex];
          }
        }
        else if (p == '?' && !flag2)
        {
          checked { ++startIndex; }
          if (startIndex < num2)
            ch1 = Source[startIndex];
        }
        else if (p == '#' && !flag2)
        {
          if (char.IsDigit(ch1))
          {
            checked { ++startIndex; }
            if (startIndex < num2)
              ch1 = Source[startIndex];
          }
          else
            break;
        }
        else
        {
          bool flag3=false;
          bool flag4=false;
          if (p == '-' && flag2 && (flag3 && !flag1) && (!flag4 && (checked (index2 + 1) >= num1 || Pattern[checked (index2 + 1)] != ']')))
          {
            flag4 = true;
          }
          else
          {
            bool SeenNot=false;
            if (p == '!' && flag2 && !SeenNot)
            {
              SeenNot = true;
              Match = true;
            }
            else if (p == '[' && !flag2)
            {
              flag2 = true;
              ch2 = char.MinValue;
              flag3 = false;
            }
            else if (p == ']' && flag2)
            {
              flag2 = false;
              if (flag3)
              {
                if (Match)
                {
                  checked { ++startIndex; }
                  if (startIndex < num2)
                    ch1 = Source[startIndex];
                }
                else
                  break;
              }
              else if (flag4)
              {
                if (!Match)
                  break;
              }
              else if (SeenNot)
              {
                if ('!' == ch1)
                {
                  checked { ++startIndex; }
                  if (startIndex < num2)
                    ch1 = Source[startIndex];
                }
                else
                  break;
              }
              Match = false;
              flag3 = false;
              SeenNot = false;
              flag4 = false;
            }
            else
            {
              flag3 = true;
              flag1 = false;
              if (flag2)
              {
                if (flag4)
                {
                  flag4 = false;
                  flag1 = true;
                  char ch3 = p;
                  if ((int) ch2 > (int) ch3)
                    throw ExceptionUtils.VbMakeException(93);
                  if (SeenNot && Match || !SeenNot && !Match)
                  {
                    Match = (int) ch1 > (int) ch2 && (int) ch1 <= (int) ch3;
                    if (SeenNot)
                      Match = !Match;
                  }
                }
                else
                {
                  ch2 = p;
                  Match = StringType.StrLikeCompareBinary(SeenNot, Match, p, ch1);
                }
              }
              else if ((int) p == (int) ch1 || SeenNot)
              {
                SeenNot = false;
                checked { ++startIndex; }
                if (startIndex < num2)
                  ch1 = Source[startIndex];
                else if (startIndex > num2)
                  return false;
              }
              else
                break;
            }
          }
        }
        checked { ++index2; }
      }
      if (flag2)
      {
        if (num2 == 0)
          return false;
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Pattern)
        }));
      }
      return index2 == num1 && startIndex == num2;
    }

    /// <summary>Compares the parameters <paramref name="Source" /> and <paramref name="Pattern" /> and returns the same results as the <see langword="Like" /> operator, using text comparison.</summary>
    /// <param name="Source">Required. Any <see langword="String" /> expression.</param>
    /// <param name="Pattern">Required. Any <see langword="String" /> expression conforming to the pattern-matching conventions described in Like Operator.</param>
    /// <returns>A <see langword="Boolean" /> value indicating whether or not the string satisfies the pattern. If the value in string satisfies the pattern contained in pattern, result is <see langword="True" />. If the string does not satisfy the pattern, result is <see langword="False" />. If both string and pattern are empty strings, the result is <see langword="True" />.</returns>
    public static bool StrLikeText(string Source, string Pattern)
    {
      bool flag1 = false;
      int num1 = Pattern != null ? Pattern.Length : 0;
      int num2 = Source != null ? Source.Length : 0;
      int index1 = 0;
      char ch1=char.MinValue;
      if (index1 < num2)
        ch1 = Source[index1];
      CompareInfo compareInfo = Utils.GetCultureInfo().CompareInfo;
      CompareOptions compareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
      int index2 = 0;
      bool flag2=false;
      int startIndex = 0;
      while (index2 < num1)
      {
        char p = Pattern[index2];
        bool Match=false;
        char ch2=char.MinValue;
        if (p == '*' && !flag2)
        {
          int num3 = StringType.AsteriskSkip(Pattern.Substring(checked (index2 + 1)), Source.Substring(startIndex), checked (num2 - startIndex), CompareMethod.Text, compareInfo);
          if (num3 < 0)
            return false;
          if (num3 > 0)
          {
            checked { startIndex += num3; }
            if (startIndex < num2)
              ch1 = Source[startIndex];
          }
        }
        else if (p == '?' && !flag2)
        {
          checked { ++startIndex; }
          if (startIndex < num2)
            ch1 = Source[startIndex];
        }
        else if (p == '#' && !flag2)
        {
          if (char.IsDigit(ch1))
          {
            checked { ++startIndex; }
            if (startIndex < num2)
              ch1 = Source[startIndex];
          }
          else
            break;
        }
        else
        {
          bool flag3 = false;
          bool flag4 = false;
          if (p == '-' && flag2 && (flag3 && !flag1) && (!flag4 && (checked (index2 + 1) >= num1 || Pattern[checked (index2 + 1)] != ']')))
          {
            flag4 = true;
          }
          else
          {
            bool SeenNot=false;
            if (p == '!' && flag2 && !SeenNot)
            {
              SeenNot = true;
              Match = true;
            }
            else if (p == '[' && !flag2)
            {
              flag2 = true;
              ch2 = char.MinValue;
              flag3 = false;
            }
            else if (p == ']' && flag2)
            {
              flag2 = false;
              if (flag3)
              {
                if (Match)
                {
                  checked { ++startIndex; }
                  if (startIndex < num2)
                    ch1 = Source[startIndex];
                }
                else
                  break;
              }
              else if (flag4)
              {
                if (!Match)
                  break;
              }
              else if (SeenNot)
              {
                if (compareInfo.Compare("!", Conversions.ToString(ch1)) == 0)
                {
                  checked { ++startIndex; }
                  if (startIndex < num2)
                    ch1 = Source[startIndex];
                }
                else
                  break;
              }
              Match = false;
              flag3 = false;
              SeenNot = false;
              flag4 = false;
            }
            else
            {
              flag3 = true;
              flag1 = false;
              if (flag2)
              {
                if (flag4)
                {
                  flag4 = false;
                  flag1 = true;
                  char ch3 = p;
                  if ((int) ch2 > (int) ch3)
                    throw ExceptionUtils.VbMakeException(93);
                  if (SeenNot && Match || !SeenNot && !Match)
                  {
                    Match = compareOptions != CompareOptions.Ordinal ? compareInfo.Compare(Conversions.ToString(ch2), Conversions.ToString(ch1), compareOptions) < 0 && compareInfo.Compare(Conversions.ToString(ch3), Conversions.ToString(ch1), compareOptions) >= 0 : (int) ch1 > (int) ch2 && (int) ch1 <= (int) ch3;
                    if (SeenNot)
                      Match = !Match;
                  }
                }
                else
                {
                  ch2 = p;
                  Match = StringType.StrLikeCompare(compareInfo, SeenNot, Match, p, ch1, compareOptions);
                }
              }
              else
              {
                if (compareOptions == CompareOptions.Ordinal)
                {
                  if ((int) p != (int) ch1 && !SeenNot)
                    break;
                }
                else
                {
                  string string1 = Conversions.ToString(p);
                  string string2 = Conversions.ToString(ch1);
                  while (checked (index2 + 1) < num1 && (UnicodeCategory.ModifierSymbol == char.GetUnicodeCategory(Pattern[checked (index2 + 1)]) || UnicodeCategory.NonSpacingMark == char.GetUnicodeCategory(Pattern[checked (index2 + 1)])))
                  {
                    string1 += Conversions.ToString(Pattern[checked (index2 + 1)]);
                    checked { ++index2; }
                  }
                  while (checked (startIndex + 1) < num2 && (UnicodeCategory.ModifierSymbol == char.GetUnicodeCategory(Source[checked (startIndex + 1)]) || UnicodeCategory.NonSpacingMark == char.GetUnicodeCategory(Source[checked (startIndex + 1)])))
                  {
                    string2 += Conversions.ToString(Source[checked (startIndex + 1)]);
                    checked { ++startIndex; }
                  }
                  if (compareInfo.Compare(string1, string2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) != 0 && !SeenNot)
                    break;
                }
                SeenNot = false;
                checked { ++startIndex; }
                if (startIndex < num2)
                  ch1 = Source[startIndex];
                else if (startIndex > num2)
                  return false;
              }
            }
          }
        }
        checked { ++index2; }
      }
      if (flag2)
      {
        if (num2 == 0)
          return false;
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Pattern)
        }));
      }
      return index2 == num1 && startIndex == num2;
    }

    private static bool StrLikeCompareBinary(bool SeenNot, bool Match, char p, char s)
    {
      if (SeenNot && Match)
        return (int) p != (int) s;
      if (!SeenNot && !Match)
        return (int) p == (int) s;
      return Match;
    }

    private static bool StrLikeCompare(CompareInfo ci, bool SeenNot, bool Match, char p, char s, CompareOptions Options)
    {
      if (SeenNot && Match)
      {
        if (Options == CompareOptions.Ordinal)
          return (int) p != (int) s;
        return ci.Compare(Conversions.ToString(p), Conversions.ToString(s), Options) != 0;
      }
      if (SeenNot || Match)
        return Match;
      if (Options == CompareOptions.Ordinal)
        return (int) p == (int) s;
      return ci.Compare(Conversions.ToString(p), Conversions.ToString(s), Options) == 0;
    }

    private static int AsteriskSkip(string Pattern, string Source, int SourceEndIndex, CompareMethod CompareOption, CompareInfo ci)
    {
      int num1 = Strings.Len(Pattern);
      int length=0;
      int Count = 0;
      while (length < num1)
      {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        switch (Pattern[length])
        {
          case '!':
            if (Pattern[checked (length + 1)] == ']')
            {
              flag2 = true;
              break;
            }
            flag1 = true;
            break;
          case '#':
          case '?':
            if (flag3)
            {
              flag2 = true;
              break;
            }
            checked { ++Count; }
            flag1 = true;
            break;
          case '*':
            if (Count > 0)
            {
              if (flag1)
              {
                int num2 = StringType.MultipleAsteriskSkip(Pattern, Source, Count, CompareOption);
                return checked (SourceEndIndex - num2);
              }
              string str = Pattern.Substring(0, length);
              CompareOptions options = CompareOption != CompareMethod.Binary ? CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth : CompareOptions.Ordinal;
              return ci.LastIndexOf(Source, str, options);
            }
            break;
          case '-':
            if (Pattern[checked (length + 1)] == ']')
            {
              flag2 = true;
              break;
            }
            break;
          case '[':
            if (flag3)
            {
              flag2 = true;
              break;
            }
            flag3 = true;
            break;
          case ']':
            if (flag2 || !flag3)
            {
              checked { ++Count; }
              flag1 = true;
            }
            flag2 = false;
            flag3 = false;
            break;
          default:
            if (flag3)
            {
              flag2 = true;
              break;
            }
            checked { ++Count; }
            break;
        }
        checked { ++length; }
      }
      return checked (SourceEndIndex - Count);
    }

    private static int MultipleAsteriskSkip(string Pattern, string Source, int Count, CompareMethod CompareOption)
    {
      int num = Strings.Len(Source);
      while (Count < num)
      {
        string Source1 = Source.Substring(checked (num - Count));
        bool flag;
        try
        {
          flag = StringType.StrLike(Source1, Pattern, CompareOption);
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
          flag = false;
        }
        if (!flag)
          checked { ++Count; }
        else
          break;
      }
      return Count;
    }

    /// <summary>Overwrites the <paramref name="sDest" /> parameter with the contents of the <paramref name="sInsert" /> parameter.</summary>
    /// <param name="sDest">Required. String variable to modify.</param>
    /// <param name="StartPosition">Required. Location in the <paramref name="sDest" /> parameter to begin overwriting from. The index is 1-based.</param>
    /// <param name="MaxInsertLength">Required. Maximum number of characters from the <paramref name="sInsert" /> parameter to use, starting from the first character.</param>
    /// <param name="sInsert">Required. String value to overwrite the <paramref name="sDest" /> parameter with.</param>
    public static void MidStmtStr(ref string sDest, int StartPosition, int MaxInsertLength, string sInsert)
    {
      int length=0;
      if (sDest != null)
        length = sDest.Length;
      int count1=0;
      if (sInsert != null)
        count1 = sInsert.Length;
      checked { --StartPosition; }
      if (StartPosition < 0 || StartPosition >= length)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Start"
        }));
      if (MaxInsertLength < 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Length"
        }));
      if (count1 > MaxInsertLength)
        count1 = MaxInsertLength;
      if (count1 > checked (length - StartPosition))
        count1 = checked (length - StartPosition);
      if (count1 == 0)
        return;
      StringBuilder stringBuilder = new StringBuilder(length);
      if (StartPosition > 0)
        stringBuilder.Append(sDest, 0, StartPosition);
      stringBuilder.Append(sInsert, 0, count1);
      int count2 = checked (length - StartPosition + count1);
      if (count2 > 0)
        stringBuilder.Append(sDest, checked (StartPosition + count1), count2);
      sDest = stringBuilder.ToString();
    }
  }
}
