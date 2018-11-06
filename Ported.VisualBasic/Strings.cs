// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Strings
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="Strings" /> module contains procedures used to perform string operations. </summary>
  [StandardModule]
  public sealed class Strings
  {
    private static readonly string[] CurrencyPositiveFormatStrings = new string[4]
    {
      "'$'n",
      "n'$'",
      "'$' n",
      "n '$'"
    };
    private static readonly string[] CurrencyNegativeFormatStrings = new string[16]
    {
      "('$'n)",
      "-'$'n",
      "'$'-n",
      "'$'n-",
      "(n'$')",
      "-n'$'",
      "n-'$'",
      "n'$'-",
      "-n '$'",
      "-'$' n",
      "n '$'-",
      "'$' n-",
      "'$'- n",
      "n- '$'",
      "('$' n)",
      "(n '$')"
    };
    private static readonly string[] NumberNegativeFormatStrings = new string[5]
    {
      "(n)",
      "-n",
      "- n",
      "n-",
      "n -"
    };
    internal static readonly CompareInfo m_InvariantCompareInfo = CultureInfo.InvariantCulture.CompareInfo;
    private static object m_SyncObject = new object();
    private const int CODEPAGE_SIMPLIFIED_CHINESE = 936;
    private const int CODEPAGE_TRADITIONAL_CHINESE = 950;
    private const CompareOptions STANDARD_COMPARE_FLAGS = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
    private const int InvariantCultureID = 127;
    private const string NAMEDFORMAT_FIXED = "fixed";
    private const string NAMEDFORMAT_YES_NO = "yes/no";
    private const string NAMEDFORMAT_ON_OFF = "on/off";
    private const string NAMEDFORMAT_PERCENT = "percent";
    private const string NAMEDFORMAT_STANDARD = "standard";
    private const string NAMEDFORMAT_CURRENCY = "currency";
    private const string NAMEDFORMAT_LONG_TIME = "long time";
    private const string NAMEDFORMAT_LONG_DATE = "long date";
    private const string NAMEDFORMAT_SCIENTIFIC = "scientific";
    private const string NAMEDFORMAT_TRUE_FALSE = "true/false";
    private const string NAMEDFORMAT_SHORT_TIME = "short time";
    private const string NAMEDFORMAT_SHORT_DATE = "short date";
    private const string NAMEDFORMAT_MEDIUM_DATE = "medium date";
    private const string NAMEDFORMAT_MEDIUM_TIME = "medium time";
    private const string NAMEDFORMAT_GENERAL_DATE = "general date";
    private const string NAMEDFORMAT_GENERAL_NUMBER = "general number";
    private static CultureInfo m_LastUsedYesNoCulture;
    private static string m_CachedYesNoFormatStyle;
    private static CultureInfo m_LastUsedOnOffCulture;
    private static string m_CachedOnOffFormatStyle;
    private static CultureInfo m_LastUsedTrueFalseCulture;
    private static string m_CachedTrueFalseFormatStyle;

    private static string CachedYesNoFormatStyle
    {
      get
      {
        CultureInfo cultureInfo = Utils.GetCultureInfo();
        object syncObject = Strings.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          if (Strings.m_LastUsedYesNoCulture != cultureInfo)
          {
            Strings.m_LastUsedYesNoCulture = cultureInfo;
            Strings.m_CachedYesNoFormatStyle = Utils.GetResourceString("YesNoFormatStyle");
          }
          return Strings.m_CachedYesNoFormatStyle;
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }
    }

    private static string CachedOnOffFormatStyle
    {
      get
      {
        CultureInfo cultureInfo = Utils.GetCultureInfo();
        object syncObject = Strings.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          if (Strings.m_LastUsedOnOffCulture != cultureInfo)
          {
            Strings.m_LastUsedOnOffCulture = cultureInfo;
            Strings.m_CachedOnOffFormatStyle = Utils.GetResourceString("OnOffFormatStyle");
          }
          return Strings.m_CachedOnOffFormatStyle;
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }
    }

    private static string CachedTrueFalseFormatStyle
    {
      get
      {
        CultureInfo cultureInfo = Utils.GetCultureInfo();
        object syncObject = Strings.m_SyncObject;
        ObjectFlowControl.CheckForSyncLockOnValueType(syncObject);
        Monitor.Enter(syncObject);
        try
        {
          if (Strings.m_LastUsedTrueFalseCulture != cultureInfo)
          {
            Strings.m_LastUsedTrueFalseCulture = cultureInfo;
            Strings.m_CachedTrueFalseFormatStyle = Utils.GetResourceString("TrueFalseFormatStyle");
          }
          return Strings.m_CachedTrueFalseFormatStyle;
        }
        finally
        {
          Monitor.Exit(syncObject);
        }
      }
    }

    private static int PRIMARYLANGID(int lcid)
    {
      return lcid & 1023;
    }

    /// <summary>Returns an <see langword="Integer" /> value representing the character code corresponding to a character.</summary>
    /// <param name="String">Required. Any valid <see langword="Char" /> or <see langword="String" /> expression. If <paramref name="String" /> is a <see langword="String" /> expression, only the first character of the string is used for input. If <paramref name="String" /> is <see langword="Nothing" /> or contains no characters, an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>Returns an <see langword="Integer" /> value representing the character code corresponding to a character.</returns>
    public static int Asc(char String)
    {
      int int32 = Convert.ToInt32(String);
      if (int32 < 128)
        return int32;
      try
      {
        Encoding fileIoEncoding = Utils.GetFileIOEncoding();
        char[] chars = new char[1]{ String };
        if (fileIoEncoding.IsSingleByte)
        {
          byte[] bytes = new byte[1];
          fileIoEncoding.GetBytes(chars, 0, 1, bytes, 0);
          return (int) bytes[0];
        }
        byte[] bytes1 = new byte[2];
        if (fileIoEncoding.GetBytes(chars, 0, 1, bytes1, 0) == 1)
          return (int) bytes1[0];
        if (BitConverter.IsLittleEndian)
        {
          byte num = bytes1[0];
          bytes1[0] = bytes1[1];
          bytes1[1] = num;
        }
        return (int) BitConverter.ToInt16(bytes1, 0);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns an <see langword="Integer" /> value representing the character code corresponding to a character.</summary>
    /// <param name="String">Required. Any valid <see langword="Char" /> or <see langword="String" /> expression. If <paramref name="String" /> is a <see langword="String" /> expression, only the first character of the string is used for input. If <paramref name="String" /> is <see langword="Nothing" /> or contains no characters, an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>Returns an <see langword="Integer" /> value representing the character code corresponding to a character.</returns>
    public static int Asc(string String)
    {
      if (String == null || String.Length == 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_LengthGTZero1", new string[1]
        {
          nameof (String)
        }));
      return Strings.Asc(String[0]);
    }

    /// <summary>Returns an <see langword="Integer" /> value representing the character code corresponding to a character.</summary>
    /// <param name="String">Required. Any valid <see langword="Char" /> or <see langword="String" /> expression. If <paramref name="String" /> is a <see langword="String" /> expression, only the first character of the string is used for input. If <paramref name="String" /> is <see langword="Nothing" /> or contains no characters, an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>Returns an <see langword="Integer" /> value representing the character code corresponding to a character.</returns>
    public static int AscW(string String)
    {
      if (String == null || String.Length == 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_LengthGTZero1", new string[1]
        {
          nameof (String)
        }));
      return (int) String[0];
    }

    /// <summary>Returns an <see langword="Integer" /> value representing the character code corresponding to a character.</summary>
    /// <param name="String">Required. Any valid <see langword="Char" /> or <see langword="String" /> expression. If <paramref name="String" /> is a <see langword="String" /> expression, only the first character of the string is used for input. If <paramref name="String" /> is <see langword="Nothing" /> or contains no characters, an <see cref="T:System.ArgumentException" /> error occurs.</param>
    /// <returns>Returns an <see langword="Integer" /> value representing the character code corresponding to a character.</returns>
    public static int AscW(char String)
    {
      return (int) String;
    }

    /// <summary>Returns the character associated with the specified character code.</summary>
    /// <param name="CharCode">Required. An <see langword="Integer" /> expression representing the <paramref name="code point" />, or character code, for the character.</param>
    /// <returns>Returns the character associated with the specified character code.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="CharCode" /> &lt; 0 or &gt; 255 for <see langword="Chr" />.</exception>
    public static char Chr(int CharCode)
    {
      if (CharCode < (int) short.MinValue || CharCode > (int) ushort.MaxValue)
        throw new ArgumentException(Utils.GetResourceString("Argument_RangeTwoBytes1", new string[1]
        {
          nameof (CharCode)
        }));
      if (CharCode >= 0)
      {
        if (CharCode <= (int) sbyte.MaxValue)
          return Convert.ToChar(CharCode);
      }
      try
      {
        Encoding encoding = Encoding.GetEncoding(Utils.GetLocaleCodePage());
        if (encoding.IsSingleByte && (CharCode < 0 || CharCode > (int) byte.MaxValue))
          throw ExceptionUtils.VbMakeException(5);
        char[] chars1 = new char[2];
        byte[] bytes = new byte[2];
        Decoder decoder = encoding.GetDecoder();
        int chars2;
        if (CharCode >= 0 && CharCode <= (int) byte.MaxValue)
        {
          bytes[0] = checked ((byte) (CharCode & (int) byte.MaxValue));
          chars2 = decoder.GetChars(bytes, 0, 1, chars1, 0);
        }
        else
        {
          bytes[0] = checked ((byte) ((CharCode & 65280) >> 8));
          bytes[1] = checked ((byte) (CharCode & (int) byte.MaxValue));
          chars2 = decoder.GetChars(bytes, 0, 2, chars1, 0);
        }
        return chars1[0];
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns the character associated with the specified character code.</summary>
    /// <param name="CharCode">Required. An <see langword="Integer" /> expression representing the <paramref name="code point" />, or character code, for the character. </param>
    /// <returns>Returns the character associated with the specified character code.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="CharCode" /> &lt; -32768 or &gt; 65535 for <see langword="ChrW" />.</exception>
    public static char ChrW(int CharCode)
    {
      if (CharCode < (int) short.MinValue || CharCode > (int) ushort.MaxValue)
        throw new ArgumentException(Utils.GetResourceString("Argument_RangeTwoBytes1", new string[1]
        {
          nameof (CharCode)
        }));
      return Convert.ToChar(CharCode & (int) ushort.MaxValue);
    }

    /// <summary>Returns a zero-based array containing a subset of a <see langword="String" /> array based on specified filter criteria.</summary>
    /// <param name="Source">Required. One-dimensional array of strings to be searched.</param>
    /// <param name="Match">Required. String to search for.</param>
    /// <param name="Include">Optional. <see langword="Boolean" /> value indicating whether to return substrings that include or exclude <paramref name="Match" />. If <paramref name="Include" /> is <see langword="True" />, the <see langword="Filter" /> function returns the subset of the array that contains <paramref name="Match" /> as a substring. If <paramref name="Include" /> is <see langword="False" />, the <see langword="Filter" /> function returns the subset of the array that does not contain <paramref name="Match" /> as a substring.</param>
    /// <param name="Compare">Optional. Numeric value indicating the kind of string comparison to use. See "Settings" for values.</param>
    /// <returns>Returns a zero-based array containing a subset of a <see langword="String" /> array based on specified filter criteria.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Source" /> is <see langword="Nothing" /> or is not a one-dimensional array.</exception>
    public static string[] Filter(object[] Source, string Match, bool Include = true, [OptionCompare] CompareMethod Compare = CompareMethod.Binary)
    {
      int num1 = Information.UBound((Array) Source, 1);
      string[] Source1 = new string[checked (num1 + 1)];
      try
      {
        int num2 = 0;
        int num3 = num1;
        int index = num2;
        while (index <= num3)
        {
          Source1[index] = Conversions.ToString(Source[index]);
          checked { ++index; }
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
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValueType2", nameof (Source), "String"));
      }
      return Strings.Filter(Source1, Match, Include, Compare);
    }

    /// <summary>Returns a zero-based array containing a subset of a <see langword="String" /> array based on specified filter criteria.</summary>
    /// <param name="Source">Required. One-dimensional array of strings to be searched.</param>
    /// <param name="Match">Required. String to search for.</param>
    /// <param name="Include">Optional. <see langword="Boolean" /> value indicating whether to return substrings that include or exclude <paramref name="Match" />. If <paramref name="Include" /> is <see langword="True" />, the <see langword="Filter" /> function returns the subset of the array that contains <paramref name="Match" /> as a substring. If <paramref name="Include" /> is <see langword="False" />, the <see langword="Filter" /> function returns the subset of the array that does not contain <paramref name="Match" /> as a substring.</param>
    /// <param name="Compare">Optional. Numeric value indicating the kind of string comparison to use. See "Settings" for values.</param>
    /// <returns>Returns a zero-based array containing a subset of a <see langword="String" /> array based on specified filter criteria.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Source" /> is <see langword="Nothing" /> or is not a one-dimensional array.</exception>
    public static string[] Filter(string[] Source, string Match, bool Include = true, [OptionCompare] CompareMethod Compare = CompareMethod.Binary)
    {
      try
      {
        if (Source.Rank != 1)
          throw new ArgumentException(Utils.GetResourceString("Argument_RankEQOne1"));
        if (Match == null || Match.Length == 0)
          return (string[]) null;
        int length = Source.Length;
        CompareInfo compareInfo = Utils.GetCultureInfo().CompareInfo;
        CompareOptions options= CompareOptions.None;
        if (Compare == CompareMethod.Text)
          options = CompareOptions.IgnoreCase;
        string[] strArray = new string[checked (length - 1 + 1)];
        int num1 = 0;
        int num2 = checked (length - 1);
        int index1 = num1;
        int index2=0;
        while (index1 <= num2)
        {
          string source = Source[index1];
          if (source != null && compareInfo.IndexOf(source, Match, options) >= 0 == Include)
          {
            strArray[index2] = source;
            checked { ++index2; }
          }
          checked { ++index1; }
        }
        if (index2 == 0)
          return new string[0];
        if (index2 == strArray.Length)
          return strArray;
        return (string[]) Utils.CopyArray((Array) strArray, (Array) new string[checked (index2 - 1 + 1)]);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns an integer specifying the start position of the first occurrence of one string within another.</summary>
    /// <param name="String1">Required. <see langword="String" /> expression being searched.</param>
    /// <param name="String2">Required. <see langword="String" /> expression sought.</param>
    /// <param name="Compare">Optional. Specifies the type of string comparison. If <paramref name="Compare" /> is omitted, the <see langword="Option Compare" /> setting determines the type of comparison. </param>
    /// <returns>IfInStr returns
    /// <paramref name="String1" /> is zero length or <see langword="Nothing" />0
    /// <paramref name="String2" /> is zero length or <see langword="Nothing" />The starting position for the search, which defaults to the first character position.
    /// <paramref name="String2" /> is not found0
    /// <paramref name="String2" /> is found within <paramref name="String1" />Position where match begins</returns>
    public static int InStr(string String1, string String2, [OptionCompare] CompareMethod Compare = CompareMethod.Binary)
    {
      if (Compare == CompareMethod.Binary)
        return checked (Strings.InternalInStrBinary(0, String1, String2) + 1);
      return checked (Strings.InternalInStrText(0, String1, String2) + 1);
    }

    /// <summary>Returns an integer specifying the start position of the first occurrence of one string within another.</summary>
    /// <param name="Start">Optional. Numeric expression that sets the starting position for each search. If omitted, search begins at the first character position. The start index is 1-based.</param>
    /// <param name="String1">Required. <see langword="String" /> expression being searched.</param>
    /// <param name="String2">Required. <see langword="String" /> expression sought.</param>
    /// <param name="Compare">Optional. Specifies the type of string comparison. If <paramref name="Compare" /> is omitted, the <see langword="Option Compare" /> setting determines the type of comparison. </param>
    /// <returns>IfInStr returns
    /// <paramref name="String1" /> is zero length or <see langword="Nothing" />0
    /// <paramref name="String2" /> is zero length or <see langword="Nothing" />
    /// <paramref name="start" />
    /// 
    /// <paramref name="String2" /> is not found0
    /// <paramref name="String2" /> is found within <paramref name="String1" />Position where match begins
    /// <paramref name="Start" /> &gt; length of <paramref name="String1" />0</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Start" /> &lt; 1.</exception>
    public static int InStr(int Start, string String1, string String2, [OptionCompare] CompareMethod Compare = CompareMethod.Binary)
    {
      if (Start < 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_GTZero1", new string[1]
        {
          nameof (Start)
        }));
      if (Compare == CompareMethod.Binary)
        return checked (Strings.InternalInStrBinary(Start - 1, String1, String2) + 1);
      return checked (Strings.InternalInStrText(Start - 1, String1, String2) + 1);
    }

    private static int InternalInStrBinary(int StartPos, string sSrc, string sFind)
    {
      int num = sSrc == null ? 0 : sSrc.Length;
      if (StartPos > num || num == 0)
        return -1;
      if (sFind == null || sFind.Length == 0)
        return StartPos;
      return Strings.m_InvariantCompareInfo.IndexOf(sSrc, sFind, StartPos, CompareOptions.Ordinal);
    }

    private static int InternalInStrText(int lStartPos, string sSrc, string sFind)
    {
      int num = sSrc == null ? 0 : sSrc.Length;
      if (lStartPos > num || num == 0)
        return -1;
      if (sFind == null || sFind.Length == 0)
        return lStartPos;
      return Utils.GetCultureInfo().CompareInfo.IndexOf(sSrc, sFind, lStartPos, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
    }

    /// <summary>Returns the position of the first occurrence of one string within another, starting from the right side of the string.</summary>
    /// <param name="StringCheck">Required. String expression being searched.</param>
    /// <param name="StringMatch">Required. String expression being searched for.</param>
    /// <param name="Start">Optional. Numeric expression setting the one-based starting position for each search, starting from the left side of the string. If <paramref name="Start" /> is omitted then –1 is used, meaning the search begins at the last character position. Search then proceeds from right to left.</param>
    /// <param name="Compare">Optional. Numeric value indicating the kind of comparison to use when evaluating substrings. If omitted, a binary comparison is performed. See Settings for values.</param>
    /// <returns>IfInStrRev returns
    /// <paramref name="StringCheck" /> is zero-length0
    /// <paramref name="StringMatch" /> is zero-length
    /// <paramref name="Start" />
    /// 
    /// <paramref name="StringMatch" /> is not found0
    /// <paramref name="StringMatch" /> is found within <paramref name="StringCheck" />Position at which the first match is found, starting with the right side of the string.
    /// <paramref name="Start" /> is greater than length of <paramref name="StringMatch" />0</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Start" /> = 0 or <paramref name="Start" /> &lt; -1.</exception>
    public static int InStrRev(string StringCheck, string StringMatch, int Start = -1, [OptionCompare] CompareMethod Compare = CompareMethod.Binary)
    {
      try
      {
        if (Start == 0 || Start < -1)
          throw new ArgumentException(Utils.GetResourceString("Argument_MinusOneOrGTZero1", new string[1]
          {
            nameof (Start)
          }));
        int num = StringCheck != null ? StringCheck.Length : 0;
        if (Start == -1)
          Start = num;
        if (Start > num || num == 0)
          return 0;
        if (StringMatch == null || StringMatch.Length == 0)
          return Start;
        if (Compare == CompareMethod.Binary)
          return checked (Strings.m_InvariantCompareInfo.LastIndexOf(StringCheck, StringMatch, Start - 1, Start, CompareOptions.Ordinal) + 1);
        return checked (Utils.GetCultureInfo().CompareInfo.LastIndexOf(StringCheck, StringMatch, Start - 1, Start, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) + 1);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns a string created by joining a number of substrings contained in an array.</summary>
    /// <param name="SourceArray">Required. One-dimensional array containing substrings to be joined.</param>
    /// <param name="Delimiter">Optional. Any string, used to separate the substrings in the returned string. If omitted, the space character (" ") is used. If <paramref name="Delimiter" /> is a zero-length string ("") or <see langword="Nothing" />, all items in the list are concatenated with no delimiters.</param>
    /// <returns>Returns a string created by joining a number of substrings contained in an array.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="SourceArray" /> is not one dimensional.</exception>
    public static string Join(object[] SourceArray, string Delimiter = " ")
    {
      int num1 = Information.UBound((Array) SourceArray, 1);
      string[] SourceArray1 = new string[checked (num1 + 1)];
      try
      {
        int num2 = 0;
        int num3 = num1;
        int index = num2;
        while (index <= num3)
        {
          SourceArray1[index] = Conversions.ToString(SourceArray[index]);
          checked { ++index; }
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
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValueType2", nameof (SourceArray), "String"));
      }
      return Strings.Join(SourceArray1, Delimiter);
    }

    /// <summary>Returns a string created by joining a number of substrings contained in an array.</summary>
    /// <param name="SourceArray">Required. One-dimensional array containing substrings to be joined.</param>
    /// <param name="Delimiter">Optional. Any string, used to separate the substrings in the returned string. If omitted, the space character (" ") is used. If <paramref name="Delimiter" /> is a zero-length string ("") or <see langword="Nothing" />, all items in the list are concatenated with no delimiters.</param>
    /// <returns>Returns a string created by joining a number of substrings contained in an array.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="SourceArray" /> is not one dimensional.</exception>
    public static string Join(string[] SourceArray, string Delimiter = " ")
    {
      try
      {
        if (Strings.IsArrayEmpty((Array) SourceArray))
          return (string) null;
        if (SourceArray.Rank != 1)
          throw new ArgumentException(Utils.GetResourceString("Argument_RankEQOne1"));
        return string.Join(Delimiter, SourceArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns a string or character converted to lowercase.</summary>
    /// <param name="Value">Required. Any valid <see langword="String" /> or <see langword="Char" /> expression.</param>
    /// <returns>Returns a string or character converted to lowercase.</returns>
    public static string LCase(string Value)
    {
      try
      {
        if (Value == null)
          return (string) null;
        return Thread.CurrentThread.CurrentCulture.TextInfo.ToLower(Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns a string or character converted to lowercase.</summary>
    /// <param name="Value">Required. Any valid <see langword="String" /> or <see langword="Char" /> expression.</param>
    /// <returns>Returns a string or character converted to lowercase.</returns>
    public static char LCase(char Value)
    {
      try
      {
        return Thread.CurrentThread.CurrentCulture.TextInfo.ToLower(Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(bool Expression)
    {
      return 2;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    [CLSCompliant(false)]
    public static int Len(sbyte Expression)
    {
      return 1;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(byte Expression)
    {
      return 1;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(short Expression)
    {
      return 2;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    [CLSCompliant(false)]
    public static int Len(ushort Expression)
    {
      return 2;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(int Expression)
    {
      return 4;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    [CLSCompliant(false)]
    public static int Len(uint Expression)
    {
      return 4;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(long Expression)
    {
      return 8;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    [CLSCompliant(false)]
    public static int Len(ulong Expression)
    {
      return 8;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(Decimal Expression)
    {
      return 8;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(float Expression)
    {
      return 4;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(double Expression)
    {
      return 8;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(DateTime Expression)
    {
      return 8;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(char Expression)
    {
      return 2;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(string Expression)
    {
      if (Expression == null)
        return 0;
      return Expression.Length;
    }

    /// <summary>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</summary>
    /// <param name="Expression">Any valid <see langword="String" /> expression or variable name. If <paramref name="Expression" /> is of type <see langword="Object" />, the <see langword="Len" /> function returns the size as it will be written to the file by the <see langword="FilePut" /> function.</param>
    /// <returns>Returns an integer containing either the number of characters in a string or the nominal number of bytes required to store a variable.</returns>
    public static int Len(object Expression)
    {
      if (Expression == null)
        return 0;
      IConvertible convertible = Expression as IConvertible;
      if (convertible != null)
      {
        switch (convertible.GetTypeCode())
        {
          case TypeCode.Boolean:
            return 2;
          case TypeCode.Char:
            return 2;
          case TypeCode.SByte:
            return 1;
          case TypeCode.Byte:
            return 1;
          case TypeCode.Int16:
            return 2;
          case TypeCode.UInt16:
            return 2;
          case TypeCode.Int32:
            return 4;
          case TypeCode.UInt32:
            return 4;
          case TypeCode.Int64:
            return 8;
          case TypeCode.UInt64:
            return 8;
          case TypeCode.Single:
            return 4;
          case TypeCode.Double:
            return 8;
          case TypeCode.Decimal:
            return 16;
          case TypeCode.DateTime:
            return 8;
          case TypeCode.String:
            return Expression.ToString().Length;
        }
      }
      else
      {
        char[] chArray = Expression as char[];
        if (chArray != null)
          return chArray.Length;
      }
      if (!(Expression is ValueType))
        throw ExceptionUtils.VbMakeException(13);
      //new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      int recordLength = StructUtils.GetRecordLength(Expression, 1);
      //PermissionSet.RevertAssert();
      return recordLength;
    }

    /// <summary>Returns a string in which a specified substring has been replaced with another substring a specified number of times.</summary>
    /// <param name="Expression">Required. String expression containing substring to replace.</param>
    /// <param name="Find">Required. Substring being searched for.</param>
    /// <param name="Replacement">Required. Replacement substring.</param>
    /// <param name="Start">Optional. Position within <paramref name="Expression" /> that starts a substring used for replacement. The return value of <see langword="Replace" /> is a string that begins at <paramref name="Start" />, with appropriate substitutions. If omitted, 1 is assumed.</param>
    /// <param name="Count">Optional. Number of substring substitutions to perform. If omitted, the default value is –1, which means "make all possible substitutions."</param>
    /// <param name="Compare">Optional. Numeric value indicating the kind of comparison to use when evaluating substrings. See Settings for values.</param>
    /// <returns>
    /// <see langword="Replace" /> returns the following values.IfReplace returns
    ///         <paramref name="Find" /> is zero-length or <see langword="Nothing" />Copy of <paramref name="Expression" />
    ///         <paramref name="Replace" /> is zero-lengthCopy of <paramref name="Expression" /> with no occurrences of <paramref name="Find" />
    ///         <paramref name="Expression" /> is zero-length or <see langword="Nothing" />, or <paramref name="Start" /> is greater than length of <paramref name="Expression" />
    ///         <see langword="Nothing" />
    /// 
    ///         <paramref name="Count" /> is 0Copy of <paramref name="Expression" /></returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Count" /> &lt; -1 or <paramref name="Start" /> &lt;= 0.</exception>
    public static string Replace(string Expression, string Find, string Replacement, int Start = 1, int Count = -1, [OptionCompare] CompareMethod Compare = CompareMethod.Binary)
    {
      try
      {
        if (Count < -1)
          throw new ArgumentException(Utils.GetResourceString("Argument_GEMinusOne1", new string[1]
          {
            nameof (Count)
          }));
        if (Start <= 0)
          throw new ArgumentException(Utils.GetResourceString("Argument_GTZero1", new string[1]
          {
            nameof (Start)
          }));
        if (Expression == null || Start > Expression.Length)
          return (string) null;
        if (Start != 1)
          Expression = Expression.Substring(checked (Start - 1));
        if (Find != null && Find.Length != 0)
        {
          switch (Count)
          {
            case -1:
              Count = Expression.Length;
              break;
            case 0:
              goto label_10;
          }
          return Strings.ReplaceInternal(Expression, Find, Replacement, Count, Compare);
        }
label_10:
        return Expression;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static string ReplaceInternal(string Expression, string Find, string Replacement, int Count, CompareMethod Compare)
    {
      int length1 = Expression.Length;
      int length2 = Find.Length;
      StringBuilder stringBuilder = new StringBuilder(length1);
      CompareInfo compareInfo;
      CompareOptions options;
      if (Compare == CompareMethod.Text)
      {
        compareInfo = Utils.GetCultureInfo().CompareInfo;
        options = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
      }
      else
      {
        compareInfo = Strings.m_InvariantCompareInfo;
        options = CompareOptions.Ordinal;
      }
      int startIndex=0;
      int num1 = 0;
      for (; startIndex < length1; startIndex = checked (num1 + length2))
      {
        int num2=0;
        if (num2 == Count)
        {
          stringBuilder.Append(Expression.Substring(startIndex));
          break;
        }
        num1 = compareInfo.IndexOf(Expression, Find, startIndex, options);
        if (num1 < 0)
        {
          stringBuilder.Append(Expression.Substring(startIndex));
          break;
        }
        stringBuilder.Append(Expression.Substring(startIndex, checked (num1 - startIndex)));
        stringBuilder.Append(Replacement);
        checked { ++num2; }
      }
      return stringBuilder.ToString();
    }

    /// <summary>Returns a string consisting of the specified number of spaces.</summary>
    /// <param name="Number">Required. <see langword="Integer" /> expression. The number of spaces you want in the string.</param>
    /// <returns>Returns a string consisting of the specified number of spaces.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> &lt; 0.</exception>
    public static string Space(int Number)
    {
      if (Number >= 0)
        return new string(' ', Number);
      throw new ArgumentException(Utils.GetResourceString("Argument_GEZero1", new string[1]
      {
        nameof (Number)
      }));
    }

    /// <summary>Returns a zero-based, one-dimensional array containing a specified number of substrings.</summary>
    /// <param name="Expression">Required. <see langword="String" /> expression containing substrings and delimiters.</param>
    /// <param name="Delimiter">Optional. Any single character used to identify substring limits. If <paramref name="Delimiter" /> is omitted, the space character (" ") is assumed to be the delimiter.</param>
    /// <param name="Limit">Optional. Maximum number of substrings into which the input string should be split. The default, –1, indicates that the input string should be split at every occurrence of the <paramref name="Delimiter" /> string.</param>
    /// <param name="Compare">Optional. Numeric value indicating the comparison to use when evaluating substrings. See "Settings" for values.</param>
    /// <returns>
    /// <see langword="String" /> array. If <paramref name="Expression" /> is a zero-length string (""), <see langword="Split" /> returns a single-element array containing a zero-length string. If <paramref name="Delimiter" /> is a zero-length string, or if it does not appear anywhere in <paramref name="Expression" />, <see langword="Split" /> returns a single-element array containing the entire <paramref name="Expression" /> string.</returns>
    public static string[] Split(string Expression, string Delimiter = " ", int Limit = -1, [OptionCompare] CompareMethod Compare = CompareMethod.Binary)
    {
      try
      {
        if (Expression == null || Expression.Length == 0)
          return new string[1]{ "" };
        if (Limit == -1)
          Limit = checked (Expression.Length + 1);
        if ((Delimiter != null ? Delimiter.Length : 0) != 0)
          return Strings.SplitHelper(Expression, Delimiter, Limit, (int) Compare);
        return new string[1]{ Expression };
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static string[] SplitHelper(string sSrc, string sFind, int cMaxSubStrings, int Compare)
    {
      int num1 = sFind != null ? sFind.Length : 0;
      int num2 = sSrc != null ? sSrc.Length : 0;
      if (num1 == 0)
        return new string[1]{ sSrc };
      if (num2 == 0)
        return new string[1]{ sSrc };
      int num3 = 20;
      if (num3 > cMaxSubStrings)
        num3 = cMaxSubStrings;
      string[] strArray = new string[checked (num3 + 1)];
      CompareOptions options;
      CompareInfo compareInfo;
      if (Compare == 0)
      {
        options = CompareOptions.Ordinal;
        compareInfo = Strings.m_InvariantCompareInfo;
      }
      else
      {
        compareInfo = Utils.GetCultureInfo().CompareInfo;
        options = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
      }
      int startIndex=0;
      int index=0;
      while (startIndex < num2)
      {
        int num4 = compareInfo.IndexOf(sSrc, sFind, startIndex, checked (num2 - startIndex), options);
        if (num4 == -1 || checked (index + 1) == cMaxSubStrings)
        {
          string str = sSrc.Substring(startIndex) ?? "";
          strArray[index] = str;
          break;
        }
        string str1 = sSrc.Substring(startIndex, checked (num4 - startIndex)) ?? "";
        strArray[index] = str1;
        startIndex = checked (num4 + num1);
        checked { ++index; }
        if (index > num3)
        {
          checked { num3 += 20; }
          if (num3 > cMaxSubStrings)
            num3 = checked (cMaxSubStrings + 1);
          strArray = (string[]) Utils.CopyArray((Array) strArray, (Array) new string[checked (num3 + 1)]);
        }
        strArray[index] = "";
        if (index == cMaxSubStrings)
        {
          string str2 = sSrc.Substring(startIndex) ?? "";
          strArray[index] = str2;
          break;
        }
      }
      if (checked (index + 1) == strArray.Length)
        return strArray;
      return (string[]) Utils.CopyArray((Array) strArray, (Array) new string[checked (index + 1)]);
    }

    /// <summary>Returns a left-aligned string containing the specified string adjusted to the specified length.</summary>
    /// <param name="Source">Required. <see langword="String" /> expression. Name of string variable.</param>
    /// <param name="Length">Required. <see langword="Integer" /> expression. Length of returned string.</param>
    /// <returns>Returns a left-aligned string containing the specified string adjusted to the specified length.</returns>
    public static string LSet(string Source, int Length)
    {
      if (Length == 0)
        return "";
      if (Source == null)
        return new string(' ', Length);
      if (Length > Source.Length)
        return Source.PadRight(Length);
      return Source.Substring(0, Length);
    }

    /// <summary>Returns a right-aligned string containing the specified string adjusted to the specified length. </summary>
    /// <param name="Source">Required. <see langword="String" /> expression. Name of string variable.</param>
    /// <param name="Length">Required. <see langword="Integer" /> expression. Length of returned string.</param>
    /// <returns>Returns a right-aligned string containing the specified string adjusted to the specified length. </returns>
    public static string RSet(string Source, int Length)
    {
      if (Length == 0)
        return "";
      if (Source == null)
        return new string(' ', Length);
      if (Length > Source.Length)
        return Source.PadLeft(Length);
      return Source.Substring(0, Length);
    }

    /// <summary>Returns a string or object consisting of the specified character repeated the specified number of times.</summary>
    /// <param name="Number">Required. <see langword="Integer" /> expression. The length to the string to be returned.</param>
    /// <param name="Character">Required. Any valid <see langword="Char" />, <see langword="String" />, or <see langword="Object" /> expression. Only the first character of the expression will be used. If Character is of type <see langword="Object" />, it must contain either a <see langword="Char" /> or a <see langword="String " />value. </param>
    /// <returns>Returns a string or object consisting of the specified character repeated the specified number of times.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is less than 0 or <paramref name="Character" /> type is not valid.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Character" /> is <see langword="Nothing" />.</exception>
    public static object StrDup(int Number, object Character)
    {
      if (Number < 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Number)
        }));
      if (Character == null)
        throw new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (Character)
        }));
      string str = Character as string;
      char c;
      if (str != null)
      {
        if (str.Length == 0)
          throw new ArgumentException(Utils.GetResourceString("Argument_LengthGTZero1", new string[1]
          {
            nameof (Character)
          }));
        c = str[0];
      }
      else
      {
        try
        {
          c = Conversions.ToChar(Character);
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
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            nameof (Character)
          }));
        }
      }
      return (object) new string(c, Number);
    }

    /// <summary>Returns a string or object consisting of the specified character repeated the specified number of times.</summary>
    /// <param name="Number">Required. <see langword="Integer" /> expression. The length to the string to be returned.</param>
    /// <param name="Character">Required. Any valid <see langword="Char" />, <see langword="String" />, or <see langword="Object" /> expression. Only the first character of the expression will be used. If Character is of type <see langword="Object" />, it must contain either a <see langword="Char" /> or a <see langword="String " />value. </param>
    /// <returns>Returns a string or object consisting of the specified character repeated the specified number of times.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is less than 0 or <paramref name="Character" /> type is not valid.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Character" /> is <see langword="Nothing" />.</exception>
    public static string StrDup(int Number, char Character)
    {
      if (Number < 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_GEZero1", new string[1]
        {
          nameof (Number)
        }));
      return new string(Character, Number);
    }

    /// <summary>Returns a string or object consisting of the specified character repeated the specified number of times.</summary>
    /// <param name="Number">Required. <see langword="Integer" /> expression. The length to the string to be returned.</param>
    /// <param name="Character">Required. Any valid <see langword="Char" />, <see langword="String" />, or <see langword="Object" /> expression. Only the first character of the expression will be used. If Character is of type <see langword="Object" />, it must contain either a <see langword="Char" /> or a <see langword="String " />value. </param>
    /// <returns>Returns a string or object consisting of the specified character repeated the specified number of times.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Number" /> is less than 0 or <paramref name="Character" /> type is not valid.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Character" /> is <see langword="Nothing" />.</exception>
    public static string StrDup(int Number, string Character)
    {
      if (Number < 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_GEZero1", new string[1]
        {
          nameof (Number)
        }));
      if (Character == null || Character.Length == 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_LengthGTZero1", new string[1]
        {
          nameof (Character)
        }));
      return new string(Character[0], Number);
    }

    /// <summary>Returns a string in which the character order of a specified string is reversed.</summary>
    /// <param name="Expression">Required. String expression whose characters are to be reversed. If <paramref name="Expression" /> is a zero-length string (""), a zero-length string is returned.</param>
    /// <returns>Returns a string in which the character order of a specified string is reversed.</returns>
    public static string StrReverse(string Expression)
    {
      if (Expression == null)
        return "";
      int length = Expression.Length;
      if (length == 0)
        return "";
      int num1 = 0;
      int num2 = checked (length - 1);
      int SrcIndex = num1;
      while (SrcIndex <= num2)
      {
        switch (char.GetUnicodeCategory(Expression[SrcIndex]))
        {
          case UnicodeCategory.NonSpacingMark:
          case UnicodeCategory.SpacingCombiningMark:
          case UnicodeCategory.EnclosingMark:
          case UnicodeCategory.Surrogate:
            return Strings.InternalStrReverse(Expression, SrcIndex, length);
          default:
            checked { ++SrcIndex; }
            continue;
        }
      }
      char[] charArray = Expression.ToCharArray();
      Array.Reverse((Array) charArray);
      return new string(charArray);
    }

    private static string InternalStrReverse(string Expression, int SrcIndex, int Length)
    {
      StringBuilder stringBuilder = new StringBuilder(Length);
      stringBuilder.Length = Length;
      TextElementEnumerator elementEnumerator = StringInfo.GetTextElementEnumerator(Expression, SrcIndex);
      if (!elementEnumerator.MoveNext())
        return "";
      int index1 = 0;
      int index2 = checked (Length - 1);
      while (index1 < SrcIndex)
      {
        stringBuilder[index2] = Expression[index1];
        checked { --index2; }
        checked { ++index1; }
      }
      int num = elementEnumerator.ElementIndex;
      while (index2 >= 0)
      {
        SrcIndex = num;
        num = !elementEnumerator.MoveNext() ? Length : elementEnumerator.ElementIndex;
        int index3 = checked (num - 1);
        while (index3 >= SrcIndex)
        {
          stringBuilder[index2] = Expression[index3];
          checked { --index2; }
          checked { --index3; }
        }
      }
      return stringBuilder.ToString();
    }

    /// <summary>Returns a string or character containing the specified string converted to uppercase.</summary>
    /// <param name="Value">Required. Any valid <see langword="String" /> or <see langword="Char" /> expression.</param>
    /// <returns>Returns a string or character containing the specified string converted to uppercase.</returns>
    public static string UCase(string Value)
    {
      try
      {
        if (Value == null)
          return "";
        return Thread.CurrentThread.CurrentCulture.TextInfo.ToUpper(Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns a string or character containing the specified string converted to uppercase.</summary>
    /// <param name="Value">Required. Any valid <see langword="String" /> or <see langword="Char" /> expression.</param>
    /// <returns>Returns a string or character containing the specified string converted to uppercase.</returns>
    public static char UCase(char Value)
    {
      try
      {
        return Thread.CurrentThread.CurrentCulture.TextInfo.ToUpper(Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static bool FormatNamed(object Expression, string Style, ref string ReturnValue)
    {
      int length = Style.Length;
      ReturnValue = (string) null;
      switch (length)
      {
        case 5:
          switch (Style[0])
          {
            case 'F':
            case 'f':
              if (string.Compare(Style, "fixed", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDouble(Expression).ToString("0.00", (IFormatProvider) null);
                return true;
              }
              break;
          };
          break;
        case 6:
          switch (Style[0])
          {
            case 'O':
            case 'o':
              if (string.Compare(Style, "on/off", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = (-(Conversions.ToBoolean(Expression) ? 1 : 0)).ToString(Strings.CachedOnOffFormatStyle, (IFormatProvider) null);
                return true;
              }
              break;
            case 'Y':
            case 'y':
              if (string.Compare(Style, "yes/no", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = (-(Conversions.ToBoolean(Expression) ? 1 : 0)).ToString(Strings.CachedYesNoFormatStyle, (IFormatProvider) null);
                return true;
              }
              break;
          };
          break;
         case 7:
          switch (Style[0])
          {
            case 'P':
            case 'p':
              if (string.Compare(Style, "percent", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDouble(Expression).ToString("0.00%", (IFormatProvider) null);
                return true;
              }
              break;
          }

           break;
        case 8:
          switch (Style[0])
          {
            case 'C':
            case 'c':
              if (string.Compare(Style, "currency", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDouble(Expression).ToString("C", (IFormatProvider) null);
                return true;
              }
              break;
            case 'S':
            case 's':
              if (string.Compare(Style, "standard", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDouble(Expression).ToString("N2", (IFormatProvider) null);
                return true;
              }
              break;
          }
          break;
        case 9:
          switch (Style[5])
          {
            case 'D':
            case 'd':
              if (string.Compare(Style, "long date", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDate(Expression).ToString("D", (IFormatProvider) null);
                return true;
              }
              break;
            case 'T':
            case 't':
              if (string.Compare(Style, "long time", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDate(Expression).ToString("T", (IFormatProvider) null);
                return true;
              }
              break;
          }
          break;
        case 10:
          switch (Style[6])
          {
            case 'A':
            case 'a':
              if (string.Compare(Style, "true/false", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = (-(Conversions.ToBoolean(Expression) ? 1 : 0)).ToString(Strings.CachedTrueFalseFormatStyle, (IFormatProvider) null);
                return true;
              }
              break;
            case 'D':
            case 'd':
              if (string.Compare(Style, "short date", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDate(Expression).ToString("d", (IFormatProvider) null);
                return true;
              }
              break;
            case 'I':
            case 'i':
              if (string.Compare(Style, "scientific", StringComparison.OrdinalIgnoreCase) == 0)
              {
                double d = Conversions.ToDouble(Expression);
                ReturnValue = double.IsNaN(d) || double.IsInfinity(d) ? d.ToString("G", (IFormatProvider) null) : d.ToString("0.00E+00", (IFormatProvider) null);
                return true;
              }
              break;
            case 'T':
            case 't':
              if (string.Compare(Style, "short time", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDate(Expression).ToString("t", (IFormatProvider) null);
                return true;
              }
              break;
          }

          break;
        case 11:
          switch (Style[7])
          {
            case 'D':
            case 'd':
              if (string.Compare(Style, "medium date", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDate(Expression).ToString("D", (IFormatProvider) null);
                return true;
              }
              break;
            case 'T':
            case 't':
              if (string.Compare(Style, "medium time", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDate(Expression).ToString("T", (IFormatProvider) null);
                return true;
              }
              break;
          }

          break;
        case 12:
          switch (Style[0])
          {
            case 'G':
            case 'g':
              if (string.Compare(Style, "general date", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDate(Expression).ToString("G", (IFormatProvider) null);
                return true;
              }
              break;
          }
          break;
        case 14:
          switch (Style[0])
          {
            case 'G':
            case 'g':
              if (string.Compare(Style, "general number", StringComparison.OrdinalIgnoreCase) == 0)
              {
                ReturnValue = Conversions.ToDouble(Expression).ToString("G", (IFormatProvider) null);
                return true;
              }
              break;
          }

          break;
      }
      return false;
    }

    /// <summary>Returns a string formatted according to instructions contained in a format <see langword="String" /> expression.</summary>
    /// <param name="Expression">Required. Any valid expression.</param>
    /// <param name="Style">Optional. A valid named or user-defined format <see langword="String" /> expression.</param>
    /// <returns>Returns a string formatted according to instructions contained in a format <see langword="String" /> expression.</returns>
    public static string Format(object Expression, string Style = "")
    {
      try
      {
        IFormatProvider formatProvider = (IFormatProvider) null;
        if (Expression == null || Expression.GetType() == null)
          return "";
        if (Style == null || Style.Length == 0)
          return Conversions.ToString(Expression);
        IConvertible convertible = (IConvertible) Expression;
        TypeCode typeCode = convertible.GetTypeCode();
        if (Style.Length > 0)
        {
          try
          {
            string ReturnValue = (string) null;
            if (Strings.FormatNamed(Expression, Style, ref ReturnValue))
              return ReturnValue;
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
            return Conversions.ToString(Expression);
          }
        }
        IFormattable formattable = Expression as IFormattable;
        if (formattable == null)
        {
          typeCode = Convert.GetTypeCode(Expression);
          switch (typeCode)
          {
            case TypeCode.Boolean:
            case TypeCode.String:
              break;
            default:
              throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
              {
                nameof (Expression)
              }));
          }
        }
        switch (typeCode)
        {
          case TypeCode.Empty:
            return "";
          case TypeCode.Object:
          case TypeCode.Char:
          case TypeCode.SByte:
          case TypeCode.Byte:
          case TypeCode.Int16:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
          case TypeCode.Int64:
          case TypeCode.UInt64:
          case TypeCode.Decimal:
          case TypeCode.DateTime:
            return formattable.ToString(Style, formatProvider);
          case TypeCode.DBNull:
            return "";
          case TypeCode.Boolean:
            return string.Format(formatProvider, Style, new object[1]
            {
              (object) Conversions.ToString(convertible.ToBoolean((IFormatProvider) null))
            });
          case TypeCode.Single:
            float num1 = convertible.ToSingle((IFormatProvider) null);
            if (Style == null || Style.Length == 0)
              return Conversions.ToString(num1);
            if ((double) num1 == 0.0)
              num1 = 0.0f;
            return num1.ToString(Style, formatProvider);
          case TypeCode.Double:
            double num2 = convertible.ToDouble((IFormatProvider) null);
            if (Style == null || Style.Length == 0)
              return Conversions.ToString(num2);
            if (num2 == 0.0)
              num2 = 0.0;
            return num2.ToString(Style, formatProvider);
          case TypeCode.String:
            return string.Format(formatProvider, Style, new object[1]
            {
              Expression
            });
          default:
            return formattable.ToString(Style, formatProvider);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns an expression formatted as a currency value using the currency symbol defined in the system control panel.</summary>
    /// <param name="Expression">Required. Expression to be formatted.</param>
    /// <param name="NumDigitsAfterDecimal">Optional. Numeric value indicating how many places are displayed to the right of the decimal. Default value is –1, which indicates that the computer's regional settings are used.</param>
    /// <param name="IncludeLeadingDigit">Optional. <see cref="T:Ported.VisualBasic.TriState" /> enumeration that indicates whether or not a leading zero is displayed for fractional values. See "Remarks" for values.</param>
    /// <param name="UseParensForNegativeNumbers">Optional. <see cref="T:Ported.VisualBasic.TriState" /> enumeration that indicates whether or not to place negative values within parentheses. See "Remarks" for values.</param>
    /// <param name="GroupDigits">Optional. <see cref="T:Ported.VisualBasic.TriState" /> enumeration that indicates whether or not numbers are grouped using the group delimiter specified in the computer's regional settings. See "Remarks" for values.</param>
    /// <returns>Returns an expression formatted as a currency value using the currency symbol defined in the system control panel.</returns>
    /// <exception cref="T:System.ArgumentException">Number of digits after decimal point is greater than 99.</exception>
    /// <exception cref="T:System.InvalidCastException">Type is not numeric.</exception>
    public static string FormatCurrency(object Expression, int NumDigitsAfterDecimal = -1, TriState IncludeLeadingDigit = TriState.UseDefault, TriState UseParensForNegativeNumbers = TriState.UseDefault, TriState GroupDigits = TriState.UseDefault)
    {
      IFormatProvider formatProvider = (IFormatProvider) null;
      try
      {
        Strings.ValidateTriState(IncludeLeadingDigit);
        Strings.ValidateTriState(UseParensForNegativeNumbers);
        Strings.ValidateTriState(GroupDigits);
        if (NumDigitsAfterDecimal > 99)
          throw new ArgumentException(Utils.GetResourceString("Argument_Range0to99_1", new string[1]
          {
            nameof (NumDigitsAfterDecimal)
          }));
        if (Expression == null)
          return "";
        Type type = Expression.GetType();
        if (type == typeof (string))
          Expression = (object) Conversions.ToDouble(Expression);
        else if (!Symbols.IsNumericType(type))
          throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(type), "Currency"));
        IFormattable formattable = (IFormattable) Expression;
        if (IncludeLeadingDigit == TriState.False)
        {
          double num = Conversions.ToDouble(Expression);
          if (num >= 1.0 || num <= -1.0)
            IncludeLeadingDigit = TriState.True;
        }
        string currencyFormatString = Strings.GetCurrencyFormatString(IncludeLeadingDigit, NumDigitsAfterDecimal, UseParensForNegativeNumbers, GroupDigits, ref formatProvider);
        return formattable.ToString(currencyFormatString, formatProvider);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns a string expression representing a date/time value.</summary>
    /// <param name="Expression">Required. <see langword="Date" /> expression to be formatted.</param>
    /// <param name="NamedFormat">Optional. Numeric value that indicates the date/time format used. If omitted, <see langword="DateFormat.GeneralDate" /> is used.</param>
    /// <returns>Returns a string expression representing a date/time value.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="NamedFormat" /> setting is not valid.</exception>
    public static string FormatDateTime(DateTime Expression, DateFormat NamedFormat = DateFormat.GeneralDate)
    {
      try
      {
        string format;
        switch (NamedFormat)
        {
          case DateFormat.GeneralDate:
            format = Expression.TimeOfDay.Ticks != Expression.Ticks ? (Expression.TimeOfDay.Ticks != 0L ? "G" : "d") : "T";
            break;
          case DateFormat.LongDate:
            format = "D";
            break;
          case DateFormat.ShortDate:
            format = "d";
            break;
          case DateFormat.LongTime:
            format = "T";
            break;
          case DateFormat.ShortTime:
            format = "HH:mm";
            break;
          default:
            throw ExceptionUtils.VbMakeException(5);
        }
        return Expression.ToString(format, (IFormatProvider) null);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns an expression formatted as a number.</summary>
    /// <param name="Expression">Required. Expression to be formatted.</param>
    /// <param name="NumDigitsAfterDecimal">Optional. Numeric value indicating how many places are displayed to the right of the decimal. The default value is –1, which indicates that the computer's regional settings are used.</param>
    /// <param name="IncludeLeadingDigit">Optional. <see cref="T:Ported.VisualBasic.TriState" /> constant that indicates whether a leading 0 is displayed for fractional values. See "Settings" for values.</param>
    /// <param name="UseParensForNegativeNumbers">Optional. <see cref="T:Ported.VisualBasic.TriState" /> constant that indicates whether to place negative values within parentheses. See "Settings" for values.</param>
    /// <param name="GroupDigits">Optional. <see cref="T:Ported.VisualBasic.TriState" /> constant that indicates whether or not numbers are grouped using the group delimiter specified in the locale settings. See "Settings" for values.</param>
    /// <returns>Returns an expression formatted as a number.</returns>
    /// <exception cref="T:System.InvalidCastException">Type is not numeric.</exception>
    public static string FormatNumber(object Expression, int NumDigitsAfterDecimal = -1, TriState IncludeLeadingDigit = TriState.UseDefault, TriState UseParensForNegativeNumbers = TriState.UseDefault, TriState GroupDigits = TriState.UseDefault)
    {
      try
      {
        Strings.ValidateTriState(IncludeLeadingDigit);
        Strings.ValidateTriState(UseParensForNegativeNumbers);
        Strings.ValidateTriState(GroupDigits);
        if (Expression == null)
          return "";
        Type type = Expression.GetType();
        if (type == typeof (string))
          Expression = (object) Conversions.ToDouble(Expression);
        else if (type == typeof (bool))
          Expression = !Conversions.ToBoolean(Expression) ? (object) 0.0 : (object) -1.0;
        else if (!Symbols.IsNumericType(type))
          throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(type), "Currency"));
        return ((IFormattable) Expression).ToString(Strings.GetNumberFormatString(NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits), (IFormatProvider) null);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    internal static string GetFormatString(int NumDigitsAfterDecimal, TriState IncludeLeadingDigit, TriState UseParensForNegativeNumbers, TriState GroupDigits, Strings.FormatType FormatTypeValue)
    {
      StringBuilder stringBuilder = new StringBuilder(30);
      NumberFormatInfo format = (NumberFormatInfo) Utils.GetCultureInfo().GetFormat(typeof (NumberFormatInfo));
      if (NumDigitsAfterDecimal < -1)
        throw ExceptionUtils.VbMakeException(5);
      if (NumDigitsAfterDecimal == -1)
      {
        switch (FormatTypeValue)
        {
          case Strings.FormatType.Number:
            NumDigitsAfterDecimal = format.NumberDecimalDigits;
            break;
          case Strings.FormatType.Percent:
            NumDigitsAfterDecimal = format.NumberDecimalDigits;
            break;
          case Strings.FormatType.Currency:
            NumDigitsAfterDecimal = format.CurrencyDecimalDigits;
            break;
        }
      }
      if (GroupDigits == TriState.UseDefault)
      {
        GroupDigits = TriState.True;
        switch (FormatTypeValue)
        {
          case Strings.FormatType.Number:
            if (Strings.IsArrayEmpty((Array) format.NumberGroupSizes))
            {
              GroupDigits = TriState.False;
              break;
            }
            break;
          case Strings.FormatType.Percent:
            if (Strings.IsArrayEmpty((Array) format.PercentGroupSizes))
            {
              GroupDigits = TriState.False;
              break;
            }
            break;
          case Strings.FormatType.Currency:
            if (Strings.IsArrayEmpty((Array) format.CurrencyGroupSizes))
            {
              GroupDigits = TriState.False;
              break;
            }
            break;
        }
      }
      if (UseParensForNegativeNumbers == TriState.UseDefault)
      {
        UseParensForNegativeNumbers = TriState.False;
        switch (FormatTypeValue)
        {
          case Strings.FormatType.Number:
            if (format.NumberNegativePattern == 0)
            {
              UseParensForNegativeNumbers = TriState.True;
              break;
            }
            break;
          case Strings.FormatType.Currency:
            if (format.CurrencyNegativePattern == 0)
            {
              UseParensForNegativeNumbers = TriState.True;
              break;
            }
            break;
        }
      }
      string str1 = GroupDigits != TriState.True ? "" : "#,##";
      string str2 = IncludeLeadingDigit == TriState.False ? "#" : "0";
      string str3 = NumDigitsAfterDecimal <= 0 ? "" : "." + new string('0', NumDigitsAfterDecimal);
      if (FormatTypeValue == Strings.FormatType.Currency)
        stringBuilder.Append(format.CurrencySymbol);
      stringBuilder.Append(str1);
      stringBuilder.Append(str2);
      stringBuilder.Append(str3);
      if (FormatTypeValue == Strings.FormatType.Percent)
        stringBuilder.Append(format.PercentSymbol);
      if (UseParensForNegativeNumbers == TriState.True)
      {
        string str4 = stringBuilder.ToString();
        stringBuilder.Append(";(");
        stringBuilder.Append(str4);
        stringBuilder.Append(")");
      }
      return stringBuilder.ToString();
    }

    internal static string GetCurrencyFormatString(TriState IncludeLeadingDigit, int NumDigitsAfterDecimal, TriState UseParensForNegativeNumbers, TriState GroupDigits, ref IFormatProvider formatProvider)
    {
      string str1 = "C";
      NumberFormatInfo nfi = (NumberFormatInfo) ((NumberFormatInfo) Utils.GetCultureInfo().GetFormat(typeof (NumberFormatInfo))).Clone();
      if (GroupDigits == TriState.False)
        nfi.CurrencyGroupSizes = new int[1]{ 0 };
      int currencyPositivePattern = nfi.CurrencyPositivePattern;
      int index = nfi.CurrencyNegativePattern;
      switch (UseParensForNegativeNumbers)
      {
        case TriState.UseDefault:
          switch (index)
          {
            case 0:
            case 4:
            case 14:
            case 15:
              UseParensForNegativeNumbers = TriState.True;
              break;
            default:
              UseParensForNegativeNumbers = TriState.False;
              break;
          }
          break;
        case TriState.False:
          switch (index)
          {
            case 0:
              index = 1;
              break;
            case 4:
              index = 5;
              break;
            case 14:
              index = 9;
              break;
            case 15:
              index = 10;
              break;
          }
          break;          
        default:
          UseParensForNegativeNumbers = TriState.True;
          switch (index)
          {
            case 1:
            case 2:
            case 3:
              index = 0;
              break;
            case 5:
            case 6:
            case 7:
              index = 4;
              break;
            case 8:
            case 10:
            case 13:
              index = 15;
              break;
            case 9:
            case 11:
            case 12:
              index = 14;
              break;
          }
          break;
      }
      nfi.CurrencyNegativePattern = index;
      if (NumDigitsAfterDecimal == -1)
        NumDigitsAfterDecimal = nfi.CurrencyDecimalDigits;
      nfi.CurrencyDecimalDigits = NumDigitsAfterDecimal;
      formatProvider = (IFormatProvider) new FormatInfoHolder(nfi);
      if (IncludeLeadingDigit != TriState.False)
        return str1;
      nfi.NumberGroupSizes = nfi.CurrencyGroupSizes;
      string str2 = Strings.CurrencyPositiveFormatStrings[currencyPositivePattern] + ";" + Strings.CurrencyNegativeFormatStrings[index];
      string newValue = GroupDigits != TriState.False ? (IncludeLeadingDigit != TriState.False ? "#,##0" : "#,###") : (IncludeLeadingDigit != TriState.False ? "0" : "#");
      if (NumDigitsAfterDecimal > 0)
        newValue = newValue + "." + new string('0', NumDigitsAfterDecimal);
      if (string.CompareOrdinal("$", nfi.CurrencySymbol) != 0)
        str2 = str2.Replace("$", nfi.CurrencySymbol.Replace("'", "''"));
      return str2.Replace("n", newValue);
    }

    internal static string GetNumberFormatString(int NumDigitsAfterDecimal, TriState IncludeLeadingDigit, TriState UseParensForNegativeNumbers, TriState GroupDigits)
    {
      NumberFormatInfo format = (NumberFormatInfo) Utils.GetCultureInfo().GetFormat(typeof (NumberFormatInfo));
      if (NumDigitsAfterDecimal == -1)
        NumDigitsAfterDecimal = format.NumberDecimalDigits;
      else if (NumDigitsAfterDecimal > 99 || NumDigitsAfterDecimal < -1)
        throw new ArgumentException(Utils.GetResourceString("Argument_Range0to99_1", new string[1]
        {
          nameof (NumDigitsAfterDecimal)
        }));
      if (GroupDigits == TriState.UseDefault)
        GroupDigits = format.NumberGroupSizes == null || format.NumberGroupSizes.Length == 0 ? TriState.False : TriState.True;
      int index1 = format.NumberNegativePattern;
      switch (UseParensForNegativeNumbers)
      {
        case TriState.UseDefault:
          UseParensForNegativeNumbers = index1 != 0 ? TriState.False : TriState.True;
          break;
        case TriState.False:
          if (index1 == 0)
          {
            index1 = 1;
            break;
          }
          break;
        default:
          UseParensForNegativeNumbers = TriState.True;
          switch (index1)
          {
            case 1:
            case 2:
            case 3:
            case 4:
              index1 = 0;
              break;
          }
          break;
        }
      if (UseParensForNegativeNumbers == TriState.UseDefault)
        UseParensForNegativeNumbers = TriState.True;
      string Expression = "n;" + Strings.NumberNegativeFormatStrings[index1];
      if (string.CompareOrdinal("-", format.NegativeSign) != 0)
        Expression = Expression.Replace("-", "\"" + format.NegativeSign + "\"");
      string Replacement = IncludeLeadingDigit == TriState.False ? "#" : "0";
      if (GroupDigits != TriState.False && format.NumberGroupSizes.Length != 0)
      {
        if (format.NumberGroupSizes.Length == 1)
        {
          Replacement = "#," + new string('#', format.NumberGroupSizes[0]) + Replacement;
        }
        else
        {
          Replacement = new string('#', checked (format.NumberGroupSizes[0] - 1)) + Replacement;
          int num = 1;
          int upperBound = format.NumberGroupSizes.GetUpperBound(0);
          int index2 = num;
          while (index2 <= upperBound)
          {
            Replacement = "," + new string('#', format.NumberGroupSizes[index2]) + "," + Replacement;
            checked { ++index2; }
          }
        }
      }
      if (NumDigitsAfterDecimal > 0)
        Replacement = Replacement + "." + new string('0', NumDigitsAfterDecimal);
      return Strings.Replace(Expression, "n", Replacement, 1, -1, CompareMethod.Binary);
    }

    /// <summary>Returns an expression formatted as a percentage (that is, multiplied by 100) with a trailing % character.</summary>
    /// <param name="Expression">Required. Expression to be formatted.</param>
    /// <param name="NumDigitsAfterDecimal">Optional. Numeric value indicating how many places to the right of the decimal are displayed. Default value is –1, which indicates that the locale settings are used.</param>
    /// <param name="IncludeLeadingDigit">Optional. <see cref="T:Ported.VisualBasic.TriState" /> constant that indicates whether or not a leading zero displays for fractional values. See "Settings" for values.</param>
    /// <param name="UseParensForNegativeNumbers">Optional. <see cref="T:Ported.VisualBasic.TriState" /> constant that indicates whether or not to place negative values within parentheses. See "Settings" for values.</param>
    /// <param name="GroupDigits">Optional. <see cref="T:Ported.VisualBasic.TriState" /> constant that indicates whether or not numbers are grouped using the group delimiter specified in the locale settings. See "Settings" for values.</param>
    /// <returns>Returns an expression formatted as a percentage (that is, multiplied by 100) with a trailing % character.</returns>
    /// <exception cref="T:System.InvalidCastException">Type is not numeric.</exception>
    public static string FormatPercent(object Expression, int NumDigitsAfterDecimal = -1, TriState IncludeLeadingDigit = TriState.UseDefault, TriState UseParensForNegativeNumbers = TriState.UseDefault, TriState GroupDigits = TriState.UseDefault)
    {
      Strings.ValidateTriState(IncludeLeadingDigit);
      Strings.ValidateTriState(UseParensForNegativeNumbers);
      Strings.ValidateTriState(GroupDigits);
      if (Expression == null)
        return "";
      Type type = Expression.GetType();
      if (type == typeof (string))
        Expression = (object) Conversions.ToDouble(Expression);
      else if (!Symbols.IsNumericType(type))
        throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(type), "numeric"));
      return ((IFormattable) Expression).ToString(Strings.GetFormatString(NumDigitsAfterDecimal, IncludeLeadingDigit, UseParensForNegativeNumbers, GroupDigits, Strings.FormatType.Percent), (IFormatProvider) null);
    }

    /// <summary>Returns a <see langword="Char" /> value representing the character from the specified index in the supplied string.</summary>
    /// <param name="str">Required. Any valid <see langword="String" /> expression.</param>
    /// <param name="Index">Required. <see langword="Integer" /> expression. The (1-based) index of the character in <paramref name="str" /> to be returned.</param>
    /// <returns>
    /// <see langword="Char" /> value representing the character from the specified index in the supplied string.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="str" /> is <see langword="Nothing" />, <paramref name="Index" /> &lt; 1, or <paramref name="Index" /> is greater than index of last character of <paramref name="str" />.</exception>
    public static char GetChar(string str, int Index)
    {
      if (str == null)
        throw new ArgumentException(Utils.GetResourceString("Argument_LengthGTZero1", new string[1]
        {
          "String"
        }));
      if (Index < 1)
        throw new ArgumentException(Utils.GetResourceString("Argument_GEOne1", new string[1]
        {
          nameof (Index)
        }));
      if (Index > str.Length)
        throw new ArgumentException(Utils.GetResourceString("Argument_IndexLELength2", nameof (Index), "String"));
      return str[checked (Index - 1)];
    }

    /// <summary>Returns a string containing a specified number of characters from the left side of a string.</summary>
    /// <param name="str">Required. <see langword="String" /> expression from which the leftmost characters are returned.</param>
    /// <param name="Length">Required. <see langword="Integer" /> expression. Numeric expression indicating how many characters to return. If 0, a zero-length string ("") is returned. If greater than or equal to the number of characters in <paramref name="str" />, the entire string is returned.</param>
    /// <returns>Returns a string containing a specified number of characters from the left side of a string.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Length" /> &lt; 0.</exception>
    public static string Left(string str, int Length)
    {
      if (Length < 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_GEZero1", new string[1]
        {
          nameof (Length)
        }));
      if (Length == 0 || str == null)
        return "";
      if (Length >= str.Length)
        return str;
      return str.Substring(0, Length);
    }

    /// <summary>Returns a string containing a copy of a specified string with no leading spaces (<see langword="LTrim" />), no trailing spaces (<see langword="RTrim" />), or no leading or trailing spaces (<see langword="Trim" />).</summary>
    /// <param name="str">Required. Any valid <see langword="String" /> expression.</param>
    /// <returns>Returns a string containing a copy of a specified string with no leading spaces (<see langword="LTrim" />), no trailing spaces (<see langword="RTrim" />), or no leading or trailing spaces (<see langword="Trim" />).</returns>
    public static string LTrim(string str)
    {
      if (str == null || str.Length == 0)
        return "";
      switch (str[0])
      {
        case ' ':
        case '　':
          return str.TrimStart(Utils.m_achIntlSpace);
        default:
          return str;
      }
    }

    /// <summary>Returns a string that contains all the characters starting from a specified position in a string.</summary>
    /// <param name="str">Required. <see langword="String" /> expression from which characters are returned.</param>
    /// <param name="Start">Required. <see langword="Integer" /> expression. Starting position of the characters to return. If <paramref name="Start" /> is greater than the number of characters in <paramref name="str" />, the <see langword="Mid" /> function returns a zero-length string (""). <paramref name="Start" /> is one-based.</param>
    /// <returns>A string that consists of all the characters starting from the specified position in the string.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Start" /> &lt;= 0 or <paramref name="Length" /> &lt; 0.</exception>
    public static string Mid(string str, int Start)
    {
      try
      {
        if (str == null)
          return (string) null;
        return Strings.Mid(str, Start, str.Length);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns a string that contains a specified number of characters starting from a specified position in a string.</summary>
    /// <param name="str">Required. <see langword="String" /> expression from which characters are returned.</param>
    /// <param name="Start">Required. <see langword="Integer" /> expression. Starting position of the characters to return. If <paramref name="Start" /> is greater than the number of characters in <paramref name="str" />, the <see langword="Mid" /> function returns a zero-length string (""). <paramref name="Start" /> is one based.</param>
    /// <param name="Length">Optional. <see langword="Integer" /> expression. Number of characters to return. If omitted or if there are fewer than <paramref name="Length" /> characters in the text (including the character at position <paramref name="Start" />), all characters from the start position to the end of the string are returned.</param>
    /// <returns>A string that consists of the specified number of characters starting from the specified position in the string.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Start" /> &lt;= 0 or <paramref name="Length" /> &lt; 0.</exception>
    public static string Mid(string str, int Start, int Length)
    {
      if (Start <= 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_GTZero1", new string[1]
        {
          nameof (Start)
        }));
      if (Length < 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_GEZero1", new string[1]
        {
          nameof (Length)
        }));
      if (Length == 0 || str == null)
        return "";
      int length = str.Length;
      if (Start > length)
        return "";
      if (checked (Start + Length) > length)
        return str.Substring(checked (Start - 1));
      return str.Substring(checked (Start - 1), Length);
    }

    /// <summary>Returns a string containing a specified number of characters from the right side of a string.</summary>
    /// <param name="str">Required. <see langword="String" /> expression from which the rightmost characters are returned.</param>
    /// <param name="Length">Required. <see langword="Integer" />. Numeric expression indicating how many characters to return. If 0, a zero-length string ("") is returned. If greater than or equal to the number of characters in <paramref name="str" />, the entire string is returned.</param>
    /// <returns>Returns a string containing a specified number of characters from the right side of a string.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Length" /> &lt; 0.</exception>
    public static string Right(string str, int Length)
    {
      if (Length < 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_GEZero1", new string[1]
        {
          nameof (Length)
        }));
      if (Length == 0 || str == null)
        return "";
      int length = str.Length;
      if (Length >= length)
        return str;
      return str.Substring(checked (length - Length), Length);
    }

    /// <summary>Returns a string containing a copy of a specified string with no leading spaces (<see langword="LTrim" />), no trailing spaces (<see langword="RTrim" />), or no leading or trailing spaces (<see langword="Trim" />).</summary>
    /// <param name="str">Required. Any valid <see langword="String" /> expression.</param>
    /// <returns>Returns a string containing a copy of a specified string with no leading spaces (<see langword="LTrim" />), no trailing spaces (<see langword="RTrim" />), or no leading or trailing spaces (<see langword="Trim" />).</returns>
    public static string RTrim(string str)
    {
      try
      {
        if (str == null || str.Length == 0)
          return "";
        switch (str[checked (str.Length - 1)])
        {
          case ' ':
          case '　':
            return str.TrimEnd(Utils.m_achIntlSpace);
          default:
            return str;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns a string containing a copy of a specified string with no leading spaces (<see langword="LTrim" />), no trailing spaces (<see langword="RTrim" />), or no leading or trailing spaces (<see langword="Trim" />).</summary>
    /// <param name="str">Required. Any valid <see langword="String" /> expression.</param>
    /// <returns>Returns a string containing a copy of a specified string with no leading spaces (<see langword="LTrim" />), no trailing spaces (<see langword="RTrim" />), or no leading or trailing spaces (<see langword="Trim" />).</returns>
    public static string Trim(string str)
    {
      try
      {
        if (str == null || str.Length == 0)
          return "";
        switch (str[0])
        {
          case ' ':
          case '　':
            return str.Trim(Utils.m_achIntlSpace);
          default:
            switch (str[checked (str.Length - 1)])
            {
              case ' ':
              case '　':
                return str.Trim(Utils.m_achIntlSpace);
              default:
                return str;
            }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>Returns -1, 0, or 1, based on the result of a string comparison. </summary>
    /// <param name="String1">Required. Any valid <see langword="String" /> expression.</param>
    /// <param name="String2">Required. Any valid <see langword="String" /> expression.</param>
    /// <param name="Compare">Optional. Specifies the type of string comparison. If <paramref name="Compare" /> is omitted, the <see langword="Option Compare" /> setting determines the type of comparison.</param>
    /// <returns>The <see langword="StrComp" /> function has the following return values.IfStrComp returns
    /// <paramref name="String1" /> sorts ahead of <paramref name="String2" />-1
    /// <paramref name="String1" /> is equal to <paramref name="String2" /> 0
    /// <paramref name="String1" /> sorts after <paramref name="String2" /> 1</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Compare" /> value is not valid.</exception>
    public static int StrComp(string String1, string String2, [OptionCompare] CompareMethod Compare = CompareMethod.Binary)
    {
      try
      {
        if (Compare == CompareMethod.Binary)
          return Operators.CompareString(String1, String2, false);
        if (Compare == CompareMethod.Text)
          return Operators.CompareString(String1, String2, true);
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Compare)
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    internal static bool IsValidCodePage(int codepage)
    {
      bool flag = false;
      try
      {
        if (Encoding.GetEncoding(codepage) != null)
          flag = true;
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
      return flag;
    }

    /// <summary>Returns a string converted as specified.</summary>
    /// <param name="str">Required. <see langword="String" /> expression to be converted.</param>
    /// <param name="Conversion">Required. <see cref="T:Ported.VisualBasic.VbStrConv" /> member. The enumeration value specifying the type of conversion to perform.</param>
    /// <param name="LocaleID">Optional. The <see langword="LocaleID" /> value, if different from the system <see langword="LocaleID" /> value. (The system <see langword="LocaleID" /> value is the default.)</param>
    /// <returns>Returns a string converted as specified.</returns>
    /// <exception cref="T:System.ArgumentException">Unsupported <paramref name="LocaleID" />, <paramref name="Conversion" /> &lt; 0 or &gt; 2048, or unsupported conversion for specified locale.</exception>
    public static string StrConv(string str, VbStrConv Conversion, int LocaleID = 0)
    {
      try
      {
        CultureInfo loc;
        if (LocaleID != 0)
        {
          if (LocaleID != 1)
          {
            try
            {
              loc = new CultureInfo(LocaleID & (int) ushort.MaxValue);
              goto label_8;
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
              throw new ArgumentException(Utils.GetResourceString("Argument_LCIDNotSupported1", new string[1]
              {
                Conversions.ToString(LocaleID)
              }));
            }
          }
        }
        loc = Utils.GetCultureInfo();
        LocaleID = loc.LCID;
label_8:
        int num1 = Strings.PRIMARYLANGID(LocaleID);
        if ((Conversion & ~(VbStrConv.ProperCase | VbStrConv.Wide | VbStrConv.Narrow | VbStrConv.Katakana | VbStrConv.Hiragana | VbStrConv.SimplifiedChinese | VbStrConv.TraditionalChinese | VbStrConv.LinguisticCasing)) != VbStrConv.None)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidVbStrConv"));
        int num2=0;
        int dwMapFlags=0;
        switch (Conversion & (VbStrConv.SimplifiedChinese | VbStrConv.TraditionalChinese))
        {
          case VbStrConv.SimplifiedChinese:
            if (!Strings.IsValidCodePage(936) || !Strings.IsValidCodePage(950))
              throw new ArgumentException(Utils.GetResourceString("Argument_SCNotSupported"));
            dwMapFlags = num2 | 33554432;
            break;
          case VbStrConv.TraditionalChinese:
            if (!Strings.IsValidCodePage(936) || !Strings.IsValidCodePage(950))
              throw new ArgumentException(Utils.GetResourceString("Argument_TCNotSupported"));
            dwMapFlags = num2 | 67108864;
            break;
          case VbStrConv.SimplifiedChinese | VbStrConv.TraditionalChinese:
            throw new ArgumentException(Utils.GetResourceString("Argument_StrConvSCandTC"));
        }
        switch (Conversion & VbStrConv.ProperCase)
        {
          case VbStrConv.None:
            if ((Conversion & VbStrConv.LinguisticCasing) != VbStrConv.None)
              throw new ArgumentException(Utils.GetResourceString("LinguisticRequirements"));
            break;
          case VbStrConv.Uppercase:
            if (Conversion == VbStrConv.Uppercase)
              return loc.TextInfo.ToUpper(str);
            dwMapFlags |= 512;
            break;
          case VbStrConv.Lowercase:
            if (Conversion == VbStrConv.Lowercase)
              return loc.TextInfo.ToLower(str);
            dwMapFlags |= 256;
            break;
          case VbStrConv.ProperCase:
            dwMapFlags = 0;
            break;
        }
        if ((Conversion & (VbStrConv.Katakana | VbStrConv.Hiragana)) != VbStrConv.None && (num1 != 17 || !Strings.ValidLCID(LocaleID)))
          throw new ArgumentException(Utils.GetResourceString("Argument_JPNNotSupported"));
        if ((Conversion & (VbStrConv.Wide | VbStrConv.Narrow)) != VbStrConv.None)
        {
          if (num1 != 17 && num1 != 18 && num1 != 4)
            throw new ArgumentException(Utils.GetResourceString("Argument_WideNarrowNotApplicable"));
          if (!Strings.ValidLCID(LocaleID))
            throw new ArgumentException(Utils.GetResourceString("Argument_LocalNotSupported"));
        }
        switch (Conversion & (VbStrConv.Wide | VbStrConv.Narrow))
        {
          case VbStrConv.Wide:
            dwMapFlags |= 8388608;
            break;
          case VbStrConv.Narrow:
            dwMapFlags |= 4194304;
            break;
          case VbStrConv.Wide | VbStrConv.Narrow:
            throw new ArgumentException(Utils.GetResourceString("Argument_IllegalWideNarrow"));
        }
        switch (Conversion & (VbStrConv.Katakana | VbStrConv.Hiragana))
        {
          case VbStrConv.Katakana:
            dwMapFlags |= 2097152;
            break;
          case VbStrConv.Hiragana:
            dwMapFlags |= 1048576;
            break;
          case VbStrConv.Katakana | VbStrConv.Hiragana:
            throw new ArgumentException(Utils.GetResourceString("Argument_IllegalKataHira"));
        }
        if ((Conversion & VbStrConv.ProperCase) == VbStrConv.ProperCase)
          return Strings.ProperCaseString(loc, dwMapFlags, str);
        if (dwMapFlags != 0)
          return Strings.vbLCMapString(loc, dwMapFlags, str);
        return str;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    internal static bool ValidLCID(int LocaleID)
    {
      try
      {
        CultureInfo cultureInfo = new CultureInfo(LocaleID);
        return true;
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

    private static string ProperCaseString(CultureInfo loc, int dwMapFlags, string sSrc)
    {
      if ((sSrc != null ? sSrc.Length : 0) == 0)
        return "";
      StringBuilder stringBuilder = new StringBuilder(Strings.vbLCMapString(loc, dwMapFlags | 256, sSrc));
      return loc.TextInfo.ToTitleCase(stringBuilder.ToString());
    }

    internal static string vbLCMapString(CultureInfo loc, int dwMapFlags, string sSrc)
    {
      int num1 = sSrc != null ? sSrc.Length : 0;
      if (num1 == 0)
        return "";
      int lcid = loc.LCID;
      Encoding encoding = Encoding.GetEncoding(loc.TextInfo.ANSICodePage);
      int num2;
      if (!encoding.IsSingleByte)
      {
        string s = sSrc;
        byte[] bytes = encoding.GetBytes(s);
        int cchDest = Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.LCMapStringA(lcid, dwMapFlags, bytes, bytes.Length, (byte[]) null, 0);
        byte[] numArray = new byte[checked (cchDest - 1 + 1)];
        num2 = Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.LCMapStringA(lcid, dwMapFlags, bytes, bytes.Length, numArray, cchDest);
        return encoding.GetString(numArray);
      }
      string lpDestStr = new string(' ', num1);
      num2 = Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.LCMapString(lcid, dwMapFlags, ref sSrc, num1, ref lpDestStr, num1);
      return lpDestStr;
    }

    private static void ValidateTriState(TriState Param)
    {
      if (Param != TriState.True && Param != TriState.False && Param != TriState.UseDefault)
        throw ExceptionUtils.VbMakeException(5);
    }

    private static bool IsArrayEmpty(Array array)
    {
      if (array == null)
        return true;
      return array.Length == 0;
    }

    private enum NamedFormats
    {
      UNKNOWN,
      GENERAL_NUMBER,
      LONG_TIME,
      MEDIUM_TIME,
      SHORT_TIME,
      GENERAL_DATE,
      LONG_DATE,
      MEDIUM_DATE,
      SHORT_DATE,
      FIXED,
      STANDARD,
      PERCENT,
      SCIENTIFIC,
      CURRENCY,
      TRUE_FALSE,
      YES_NO,
      ON_OFF,
    }

    internal enum FormatType
    {
      Number,
      Percent,
      Currency,
    }
  }
}
