// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.Utils
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>Contains utilities that the Visual Basic compiler uses.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class Utils
  {
    internal static char[] m_achIntlSpace = new char[2]
    {
      ' ',
      '　'
    };
    private static readonly Type VoidType = Type.GetType("System.Void");
    private static readonly object ResourceManagerSyncObj = new object();
    internal const int SEVERITY_ERROR = -2147483648;
    internal const int FACILITY_CONTROL = 655360;
    internal const int FACILITY_RPC = 65536;
    internal const int FACILITY_ITF = 262144;
    internal const int SCODE_FACILITY = 536805376;
    private const int ERROR_INVALID_PARAMETER = 87;
    internal const char chPeriod = '.';
    internal const char chSpace = ' ';
    internal const char chIntlSpace = '　';
    internal const char chZero = '0';
    internal const char chHyphen = '-';
    internal const char chPlus = '+';
    internal const char chLetterA = 'A';
    internal const char chLetterZ = 'Z';
    internal const char chColon = ':';
    internal const char chSlash = '/';
    internal const char chBackslash = '\\';
    internal const char chTab = '\t';
    internal const char chCharH0A = '\n';
    internal const char chCharH0B = '\v';
    internal const char chCharH0C = '\f';
    internal const char chCharH0D = '\r';
    internal const char chLineFeed = '\n';
    internal const char chDblQuote = '"';
    internal const char chGenericManglingChar = '`';
    internal const CompareOptions OptionCompareTextFlags = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
    private static ResourceManager m_VBAResourceManager;
    private static bool m_TriedLoadingResourceManager;
    private const string ResourceMsgDefault = "Message text unavailable.  Resource file 'Ported.VisualBasic resources' not found.";
    private const string VBDefaultErrorID = "ID95";
    private static Assembly m_VBRuntimeAssembly;

    private Utils()
    {
    }

    internal static ResourceManager VBAResourceManager
    {
      get
      {
        if (Utils.m_VBAResourceManager != null)
          return Utils.m_VBAResourceManager;
        object resourceManagerSyncObj = Utils.ResourceManagerSyncObj;
        ObjectFlowControl.CheckForSyncLockOnValueType(resourceManagerSyncObj);
        Monitor.Enter(resourceManagerSyncObj);
        try
        {
          if (!Utils.m_TriedLoadingResourceManager)
          {
            try
            {
              Utils.m_VBAResourceManager = new ResourceManager("Ported.VisualBasic", Assembly.GetExecutingAssembly());
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
            Utils.m_TriedLoadingResourceManager = true;
          }
        }
        finally
        {
          Monitor.Exit(resourceManagerSyncObj);
        }
        return Utils.m_VBAResourceManager;
      }
    }

    internal static string GetResourceString(vbErrors ResourceId)
    {
      return Utils.GetResourceString("ID" + Conversions.ToString((int) ResourceId));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static string GetResourceString(string ResourceKey)
    {
      if (Utils.VBAResourceManager == null)
        return "Message text unavailable.  Resource file 'Ported.VisualBasic resources' not found.";
      string str;
      try
      {
        str = Utils.VBAResourceManager.GetString(ResourceKey, Utils.GetCultureInfo()) ?? Utils.VBAResourceManager.GetString("ID95");
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
        str = "Message text unavailable.  Resource file 'Ported.VisualBasic resources' not found.";
      }
      return str;
    }

    internal static string GetResourceString(string ResourceKey, bool NotUsed)
    {
      if (Utils.VBAResourceManager == null)
        return "Message text unavailable.  Resource file 'Ported.VisualBasic resources' not found.";
      string str;
      try
      {
        str = Utils.VBAResourceManager.GetString(ResourceKey, Utils.GetCultureInfo()) ?? Utils.VBAResourceManager.GetString(ResourceKey);
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
        str = (string) null;
      }
      return str;
    }

    /// <summary>Retrieves and formats a localized resource string or error message.</summary>
    /// <param name="ResourceKey">The identifier of the string or error message to retrieve.</param>
    /// <param name="Args">An array of parameters to replace placeholders in the string or error message.</param>
    /// <returns>A formatted resource string or error message.</returns>
    public static string GetResourceString(string ResourceKey, params string[] Args)
    {
      string format = (string) null;
      string Left = (string) null;
      try
      {
        format = Utils.GetResourceString(ResourceKey);
        Left = string.Format((IFormatProvider) Thread.CurrentThread.CurrentUICulture, format, (object[]) Args);
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
      finally
      {
      }
      if (Operators.CompareString(Left, "", false) != 0)
        return Left;
      return format;
    }

    internal static string StdFormat(string s)
    {
      NumberFormatInfo numberFormat = Thread.CurrentThread.CurrentCulture.NumberFormat;
      int index = s.IndexOf(numberFormat.NumberDecimalSeparator);
      if (index == -1)
        return s;
      char ch1=char.MinValue;
      char ch2 = char.MinValue;
      char ch3 = char.MinValue;
      try
      {
        ch1 = s[0];
        ch2 = s[1];
        ch3 = s[2];
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
      if (s[index] == '.')
      {
        if (ch1 == '0' && ch2 == '.')
          return s.Substring(1);
        if (ch1 != '-' && ch1 != '+' && ch1 != ' ' || (ch2 != '0' || ch3 != '.'))
          return s;
      }
      StringBuilder stringBuilder = new StringBuilder(s);
      stringBuilder[index] = '.';
      string str;
      if (ch1 == '0' && ch2 == '.')
        str = stringBuilder.ToString(1, checked (stringBuilder.Length - 1));
      else if ((ch1 == '-' || ch1 == '+' || ch1 == ' ') && (ch2 == '0' && ch3 == '.'))
      {
        stringBuilder.Remove(1, 1);
        str = stringBuilder.ToString();
      }
      else
        str = stringBuilder.ToString();
      return str;
    }

    internal static string OctFromLong(long Val)
    {
      string Expression = "";
      int int32 = Convert.ToInt32('0');
      bool flag=false;
      if (Val < 0L)
      {
        Val = checked (long.MaxValue + Val + 1L);
        flag = true;
      }
      do
      {
        int num = checked ((int) unchecked (Val % 8L));
        Val >>= 3;
        Expression += Conversions.ToString(Strings.ChrW(checked (num + int32)));
      }
      while (Val > 0L);
      string str = Strings.StrReverse(Expression);
      if (flag)
        str = "1" + str;
      return str;
    }

    internal static string OctFromULong(ulong Val)
    {
      string Expression = "";
      int int32 = Convert.ToInt32('0');
      do
      {
        int num = checked ((int) unchecked (Val % 8UL));
        Val >>= 3;
        Expression += Conversions.ToString(Strings.ChrW(checked (num + int32)));
      }
      while (Val != 0UL);
      return Strings.StrReverse(Expression);
    }

    [DebuggerHidden]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    internal static void SetTime(DateTime dtTime)
    {
      NativeTypes.SystemTime systime = new NativeTypes.SystemTime();
      SafeNativeMethods.GetLocalTime(systime);
      systime.wHour = checked ((short) dtTime.Hour);
      systime.wMinute = checked ((short) dtTime.Minute);
      systime.wSecond = checked ((short) dtTime.Second);
      systime.wMilliseconds = checked ((short) dtTime.Millisecond);
      if (UnsafeNativeMethods.SetLocalTime(systime) != 0)
        return;
      if (Marshal.GetLastWin32Error() == 87)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
      throw new SecurityException(Utils.GetResourceString("SetLocalTimeFailure"));
    }

    [DebuggerHidden]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    internal static void SetDate(DateTime vDate)
    {
      NativeTypes.SystemTime systime = new NativeTypes.SystemTime();
      SafeNativeMethods.GetLocalTime(systime);
      systime.wYear = checked ((short) vDate.Year);
      systime.wMonth = checked ((short) vDate.Month);
      systime.wDay = checked ((short) vDate.Day);
      if (UnsafeNativeMethods.SetLocalTime(systime) != 0)
        return;
      if (Marshal.GetLastWin32Error() == 87)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue"));
      throw new SecurityException(Utils.GetResourceString("SetLocalDateFailure"));
    }

    internal static DateTimeFormatInfo GetDateTimeFormatInfo()
    {
      return Thread.CurrentThread.CurrentCulture.DateTimeFormat;
    }

    /// <summary>Throws a localized Visual Basic exception.</summary>
    /// <param name="hr">The Visual Basic error identifier.</param>
    public static void ThrowException(int hr)
    {
      throw ExceptionUtils.VbMakeException(hr);
    }

    internal static int MapHRESULT(int lNumber)
    {
      if (lNumber > 0)
        return lNumber;
      if ((lNumber & 536805376) == 655360)
        return lNumber & (int) ushort.MaxValue;
      int num;
      switch (lNumber)
      {
        case -2147467263:
          num = 32768;
          break;
        case -2147467262:
          num = 430;
          break;
        case -2147467260:
          num = 287;
          break;
        case -2147352575:
          num = 438;
          break;
        case -2147352573:
          num = 438;
          break;
        case -2147352572:
          num = 448;
          break;
        case -2147352571:
          num = 13;
          break;
        case -2147352570:
          num = 438;
          break;
        case -2147352569:
          num = 446;
          break;
        case -2147352568:
          num = 458;
          break;
        case -2147352566:
          num = 6;
          break;
        case -2147352565:
          num = 9;
          break;
        case -2147352564:
          num = 447;
          break;
        case -2147352563:
          num = 10;
          break;
        case -2147352562:
          num = 450;
          break;
        case -2147352561:
          num = 449;
          break;
        case -2147352559:
          num = 451;
          break;
        case -2147352558:
          num = 11;
          break;
        case -2147319786:
          num = 32790;
          break;
        case -2147319785:
          num = 461;
          break;
        case -2147319784:
          num = 32792;
          break;
        case -2147319783:
          num = 32793;
          break;
        case -2147319780:
          num = 32796;
          break;
        case -2147319779:
          num = 32797;
          break;
        case -2147319769:
          num = 32807;
          break;
        case -2147319768:
          num = 32808;
          break;
        case -2147319767:
          num = 32809;
          break;
        case -2147319766:
          num = 32810;
          break;
        case -2147319765:
          num = 32811;
          break;
        case -2147319764:
          num = 32812;
          break;
        case -2147319763:
          num = 32813;
          break;
        case -2147319762:
          num = 32814;
          break;
        case -2147319761:
          num = 453;
          break;
        case -2147317571:
          num = 35005;
          break;
        case -2147317563:
          num = 35013;
          break;
        case -2147316576:
          num = 13;
          break;
        case -2147316575:
          num = 9;
          break;
        case -2147316574:
          num = 57;
          break;
        case -2147316573:
          num = 322;
          break;
        case -2147312566:
          num = 48;
          break;
        case -2147312509:
          num = 40067;
          break;
        case -2147312508:
          num = 40068;
          break;
        case -2147287039:
          num = 32774;
          break;
        case -2147287038:
          num = 53;
          break;
        case -2147287037:
          num = 76;
          break;
        case -2147287036:
          num = 67;
          break;
        case -2147287035:
          num = 70;
          break;
        case -2147287034:
          num = 32772;
          break;
        case -2147287032:
          num = 7;
          break;
        case -2147287022:
          num = 67;
          break;
        case -2147287021:
          num = 70;
          break;
        case -2147287015:
          num = 32771;
          break;
        case -2147287011:
          num = 32773;
          break;
        case -2147287010:
          num = 32772;
          break;
        case -2147287008:
          num = 75;
          break;
        case -2147287007:
          num = 70;
          break;
        case -2147286960:
          num = 58;
          break;
        case -2147286928:
          num = 61;
          break;
        case -2147286789:
          num = 32792;
          break;
        case -2147286788:
          num = 53;
          break;
        case -2147286787:
          num = 32792;
          break;
        case -2147286786:
          num = 32768;
          break;
        case -2147286784:
          num = 70;
          break;
        case -2147286783:
          num = 70;
          break;
        case -2147286782:
          num = 32773;
          break;
        case -2147286781:
          num = 57;
          break;
        case -2147286780:
          num = 32793;
          break;
        case -2147286779:
          num = 32793;
          break;
        case -2147286778:
          num = 32789;
          break;
        case -2147286777:
          num = 32793;
          break;
        case -2147286776:
          num = 32793;
          break;
        case -2147221230:
          num = 429;
          break;
        case -2147221164:
          num = 429;
          break;
        case -2147221021:
          num = 429;
          break;
        case -2147221018:
          num = 432;
          break;
        case -2147221014:
          num = 432;
          break;
        case -2147221005:
          num = 429;
          break;
        case -2147221003:
          num = 429;
          break;
        case -2147220994:
          num = 429;
          break;
        case -2147024891:
          num = 70;
          break;
        case -2147024882:
          num = 7;
          break;
        case -2147024809:
          num = 5;
          break;
        case -2147023174:
          num = 462;
          break;
        case -2146959355:
          num = 429;
          break;
        default:
          num = lNumber;
          break;
      }
      return num;
    }

    internal static CultureInfo GetCultureInfo()
    {
      return Thread.CurrentThread.CurrentCulture;
    }

    /// <summary>Sets the culture of the current thread.</summary>
    /// <param name="Culture">A <see cref="T:System.Globalization.CultureInfo" /> object to set as the culture of the current thread.</param>
    /// <returns>The previous value of the <see cref="P:System.Threading.Thread.CurrentCulture" /> property for the current thread.</returns>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.SelfAffectingThreading)]
    public static object SetCultureInfo(CultureInfo Culture)
    {
      CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
      Thread.CurrentThread.CurrentCulture = Culture;
      return (object) currentCulture;
    }

    internal static CultureInfo GetInvariantCultureInfo()
    {
      return CultureInfo.InvariantCulture;
    }

    internal static Encoding GetFileIOEncoding()
    {
      return Encoding.Default;
    }

    internal static int GetLocaleCodePage()
    {
      return Thread.CurrentThread.CurrentCulture.TextInfo.ANSICodePage;
    }

    internal static Assembly VBRuntimeAssembly
    {
      get
      {
        if (Utils.m_VBRuntimeAssembly != null)
          return Utils.m_VBRuntimeAssembly;
        Utils.m_VBRuntimeAssembly = Assembly.GetExecutingAssembly();
        return Utils.m_VBRuntimeAssembly;
      }
    }

    /// <summary>Used by the Visual Basic compiler as a helper for <see langword="Redim" />.</summary>
    /// <param name="arySrc">The array to be copied.</param>
    /// <param name="aryDest">The destination array.</param>
    /// <returns>The copied array.</returns>
    public static Array CopyArray(Array arySrc, Array aryDest)
    {
      if (arySrc == null)
        return aryDest;
      int length1 = arySrc.Length;
      if (length1 == 0)
        return aryDest;
      if (aryDest.Rank != arySrc.Rank)
        throw ExceptionUtils.VbMakeException((Exception) new InvalidCastException(Utils.GetResourceString("Array_RankMismatch")), 9);
      int num1 = 0;
      int num2 = checked (aryDest.Rank - 2);
      int dimension = num1;
      while (dimension <= num2)
      {
        if (aryDest.GetUpperBound(dimension) != arySrc.GetUpperBound(dimension))
          throw ExceptionUtils.VbMakeException((Exception) new ArrayTypeMismatchException(Utils.GetResourceString("Array_TypeMismatch")), 9);
        checked { ++dimension; }
      }
      if (length1 > aryDest.Length)
        length1 = aryDest.Length;
      if (arySrc.Rank > 1)
      {
        int rank = arySrc.Rank;
        int length2 = arySrc.GetLength(checked (rank - 1));
        int length3 = aryDest.GetLength(checked (rank - 1));
        if (length3 == 0)
          return aryDest;
        int length4 = Math.Min(length2, length3);
        int num3 = 0;
        int num4 = checked (unchecked (arySrc.Length / length2) - 1);
        int num5 = num3;
        while (num5 <= num4)
        {
          Array.Copy(arySrc, checked (num5 * length2), aryDest, checked (num5 * length3), length4);
          checked { ++num5; }
        }
      }
      else
        Array.Copy(arySrc, aryDest, length1);
      return aryDest;
    }

    internal static string ToHalfwidthNumbers(string s, CultureInfo culture)
    {
      switch (culture.LCID & 1023)
      {
        case 4:
        case 17:
        case 18:
          return Strings.vbLCMapString(culture, 4194304, s);
        default:
          return s;
      }
    }

    internal static bool IsHexOrOctValue(string Value, ref long i64Value)
    {
      int length = Value.Length;
      int index=0;
      while (index < length)
      {
        char ch = Value[index];
        if (ch != '&' || checked (index + 2) >= length)
        {
          if (ch != ' ' && ch != '　')
            return false;
          checked { ++index; }
        }
        else
        {
          char lower = char.ToLower(Value[checked (index + 1)], CultureInfo.InvariantCulture);
          string halfwidthNumbers = Utils.ToHalfwidthNumbers(Value.Substring(checked (index + 2)), Utils.GetCultureInfo());
          if (lower == 'h')
          {
            i64Value = Convert.ToInt64(halfwidthNumbers, 16);
          }
          else
          {
            if (lower != 'o')
              throw new FormatException();
            i64Value = Convert.ToInt64(halfwidthNumbers, 8);
          }
          return true;
        }
      }
      return false;
    }

    internal static bool IsHexOrOctValue(string Value, ref ulong ui64Value)
    {
      int length = Value.Length;
      int index=0;
      while (index < length)
      {
        char ch = Value[index];
        if (ch != '&' || checked (index + 2) >= length)
        {
          if (ch != ' ' && ch != '　')
            return false;
          checked { ++index; }
        }
        else
        {
          char lower = char.ToLower(Value[checked (index + 1)], CultureInfo.InvariantCulture);
          string halfwidthNumbers = Utils.ToHalfwidthNumbers(Value.Substring(checked (index + 2)), Utils.GetCultureInfo());
          if (lower == 'h')
          {
            ui64Value = Convert.ToUInt64(halfwidthNumbers, 16);
          }
          else
          {
            if (lower != 'o')
              throw new FormatException();
            ui64Value = Convert.ToUInt64(halfwidthNumbers, 8);
          }
          return true;
        }
      }
      return false;
    }

    internal static string VBFriendlyName(object Obj)
    {
      if (Obj == null)
        return "Nothing";
      return Utils.VBFriendlyName(Obj.GetType(), Obj);
    }

    internal static string VBFriendlyName(Type typ)
    {
      return Utils.VBFriendlyNameOfType(typ, false);
    }

    internal static string VBFriendlyName(Type typ, object o)
    {
      //if (typ.IsCOMObject && Operators.CompareString(typ.FullName, "System.__ComObject", false) == 0)
      //return Information.TypeNameOfCOMObject(o, false);
      return Utils.VBFriendlyNameOfType(typ, false);
    }

    internal static string VBFriendlyNameOfType(Type typ, bool FullName = false)
    {
      string suffixAndElementType = Utils.GetArraySuffixAndElementType(ref typ);
      string str1;
      switch (!typ.IsEnum ? Type.GetTypeCode(typ) : TypeCode.Object)
      {
        case TypeCode.DBNull:
          str1 = "DBNull";
          break;
        case TypeCode.Boolean:
          str1 = "Boolean";
          break;
        case TypeCode.Char:
          str1 = "Char";
          break;
        case TypeCode.SByte:
          str1 = "SByte";
          break;
        case TypeCode.Byte:
          str1 = "Byte";
          break;
        case TypeCode.Int16:
          str1 = "Short";
          break;
        case TypeCode.UInt16:
          str1 = "UShort";
          break;
        case TypeCode.Int32:
          str1 = "Integer";
          break;
        case TypeCode.UInt32:
          str1 = "UInteger";
          break;
        case TypeCode.Int64:
          str1 = "Long";
          break;
        case TypeCode.UInt64:
          str1 = "ULong";
          break;
        case TypeCode.Single:
          str1 = "Single";
          break;
        case TypeCode.Double:
          str1 = "Double";
          break;
        case TypeCode.Decimal:
          str1 = "Decimal";
          break;
        case TypeCode.DateTime:
          str1 = "Date";
          break;
        case TypeCode.String:
          str1 = "String";
          break;
        default:
          if (Symbols.IsGenericParameter(typ))
          {
            str1 = typ.Name;
            break;
          }
          string str2 = (string) null;
          string genericArgsSuffix = Utils.GetGenericArgsSuffix(typ);
          string str3;
          if (FullName)
          {
            if (typ.IsNested)
            {
              str2 = Utils.VBFriendlyNameOfType(typ.DeclaringType, true);
              str3 = typ.Name;
            }
            else
              str3 = typ.FullName;
          }
          else
            str3 = typ.Name;
          if (genericArgsSuffix != null)
          {
            int length = str3.LastIndexOf('`');
            if (length != -1)
              str3 = str3.Substring(0, length);
            str1 = str3 + genericArgsSuffix;
          }
          else
            str1 = str3;
          if (str2 != null)
          {
            str1 = str2 + "." + str1;
            break;
          }
          break;
      }
      if (suffixAndElementType != null)
        str1 += suffixAndElementType;
      return str1;
    }

    private static string GetArraySuffixAndElementType(ref Type typ)
    {
      if (!typ.IsArray)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      do
      {
        stringBuilder.Append("(");
        stringBuilder.Append(',', checked (typ.GetArrayRank() - 1));
        stringBuilder.Append(")");
        typ = typ.GetElementType();
      }
      while (typ.IsArray);
      return stringBuilder.ToString();
    }

    private static string GetGenericArgsSuffix(Type typ)
    {
      if (!typ.IsGenericType)
        return (string) null;
      Type[] genericArguments = typ.GetGenericArguments();
      int length = genericArguments.Length;
      int num1 = length;
      if (typ.IsNested && typ.DeclaringType.IsGenericType)
        checked { num1 -= typ.DeclaringType.GetGenericArguments().Length; }
      if (num1 == 0)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("(Of ");
      int num2 = checked (length - num1);
      int num3 = checked (length - 1);
      int index = num2;
      while (index <= num3)
      {
        stringBuilder.Append(Utils.VBFriendlyNameOfType(genericArguments[index], false));
        if (index != checked (length - 1))
          stringBuilder.Append(',');
        checked { ++index; }
      }
      stringBuilder.Append(")");
      return stringBuilder.ToString();
    }

    internal static string ParameterToString(ParameterInfo Parameter)
    {
      string str1 = "";
      Type typ = Parameter.ParameterType;
      if (Parameter.IsOptional)
        str1 += "[";
      if (typ.IsByRef)
      {
        str1 += "ByRef ";
        typ = typ.GetElementType();
      }
      else if (Symbols.IsParamArray(Parameter))
        str1 += "ParamArray ";
      string str2 = str1 + Parameter.Name + " As " + Utils.VBFriendlyNameOfType(typ, true);
      if (Parameter.IsOptional)
      {
        object defaultValue = Parameter.DefaultValue;
        if (defaultValue == null)
        {
          str2 += " = Nothing";
        }
        else
        {
          Type type = defaultValue.GetType();
          if (type != Utils.VoidType)
            str2 = !Symbols.IsEnum(type) ? str2 + " = " + Conversions.ToString(defaultValue) : str2 + " = " + Enum.GetName(type, defaultValue);
        }
        str2 += "]";
      }
      return str2;
    }

    /// <summary>Returns a Visual Basic method signature.</summary>
    /// <param name="Method">A <see cref="T:System.Reflection.MethodBase" /> object to return a Visual Basic method signature for.</param>
    /// <returns>The Visual Basic method signature for the supplied <see cref="T:System.Reflection.MethodBase" /> object.</returns>
    public static string MethodToString(MethodBase Method)
    {
      Type typ1 = (Type) null;
      string str1 = "";
      if (Method.MemberType == MemberTypes.Method)
        typ1 = ((MethodInfo) Method).ReturnType;
      if (Method.IsPublic)
        str1 += "Public ";
      else if (Method.IsPrivate)
        str1 += "Private ";
      else if (Method.IsAssembly)
        str1 += "Friend ";
      if ((Method.Attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
      {
        if (!Method.DeclaringType.IsInterface)
          str1 += "Overrides ";
      }
      else if (Symbols.IsShared((MemberInfo) Method))
        str1 += "Shared ";
      Symbols.UserDefinedOperator userDefinedOperator = Symbols.UserDefinedOperator.UNDEF;
      if (Symbols.IsUserDefinedOperator(Method))
        userDefinedOperator = Symbols.MapToUserDefinedOperator(Method);
      string str2;
      switch (userDefinedOperator)
      {
        case Symbols.UserDefinedOperator.UNDEF:
          str2 = typ1 == null || typ1 == Utils.VoidType ? str1 + "Sub " : str1 + "Function ";
          break;
        case Symbols.UserDefinedOperator.Narrow:
          str1 += "Narrowing ";
          goto default;
        case Symbols.UserDefinedOperator.Widen:
          str1 += "Widening ";
          goto default;
        default:
          str2 = str1 + "Operator ";
          break;
      }
      string str3 = userDefinedOperator == Symbols.UserDefinedOperator.UNDEF ? (Method.MemberType != MemberTypes.Constructor ? str2 + Method.Name : str2 + "New") : str2 + Symbols.OperatorNames[(int) userDefinedOperator];
      if (Symbols.IsGeneric(Method))
      {
        string str4 = str3 + "(Of ";
        bool flag = true;
        Type[] typeParameters = Symbols.GetTypeParameters((MemberInfo) Method);
        int index = 0;
        while (index < typeParameters.Length)
        {
          Type typ2 = typeParameters[index];
          if (!flag)
            str4 += ", ";
          else
            flag = false;
          str4 += Utils.VBFriendlyNameOfType(typ2, false);
          checked { ++index; }
        }
        str3 = str4 + ")";
      }
      string str5 = str3 + "(";
      bool flag1 = true;
      ParameterInfo[] parameters = Method.GetParameters();
      int index1 = 0;
      while (index1 < parameters.Length)
      {
        ParameterInfo Parameter = parameters[index1];
        if (!flag1)
          str5 += ", ";
        else
          flag1 = false;
        str5 += Utils.ParameterToString(Parameter);
        checked { ++index1; }
      }
      string str6 = str5 + ")";
      if (typ1 != null && typ1 != Utils.VoidType)
        str6 = str6 + " As " + Utils.VBFriendlyNameOfType(typ1, true);
      return str6;
    }

    internal static string PropertyToString(PropertyInfo Prop)
    {
      string str1 = "";
      MethodInfo methodInfo = Prop.GetGetMethod();
      Utils.PropertyKind propertyKind;
      ParameterInfo[] parameterInfoArray1;
      Type typ;
      if (methodInfo != null)
      {
        propertyKind = Prop.GetSetMethod() == null ? Utils.PropertyKind.ReadOnly : Utils.PropertyKind.ReadWrite;
        parameterInfoArray1 = methodInfo.GetParameters();
        typ = methodInfo.ReturnType;
      }
      else
      {
        propertyKind = Utils.PropertyKind.WriteOnly;
        methodInfo = Prop.GetSetMethod();
        ParameterInfo[] parameters = methodInfo.GetParameters();
        parameterInfoArray1 = new ParameterInfo[checked (parameters.Length - 2 + 1)];
        Array.Copy((Array) parameters, (Array) parameterInfoArray1, parameterInfoArray1.Length);
        typ = parameters[checked (parameters.Length - 1)].ParameterType;
      }
      string str2 = str1 + "Public ";
      if ((methodInfo.Attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
      {
        if (!Prop.DeclaringType.IsInterface)
          str2 += "Overrides ";
      }
      else if (Symbols.IsShared((MemberInfo) methodInfo))
        str2 += "Shared ";
      if (propertyKind == Utils.PropertyKind.ReadOnly)
        str2 += "ReadOnly ";
      if (propertyKind == Utils.PropertyKind.WriteOnly)
        str2 += "WriteOnly ";
      string str3 = str2 + "Property " + Prop.Name + "(";
      bool flag = true;
      ParameterInfo[] parameterInfoArray2 = parameterInfoArray1;
      int index = 0;
      while (index < parameterInfoArray2.Length)
      {
        ParameterInfo Parameter = parameterInfoArray2[index];
        if (!flag)
          str3 += ", ";
        else
          flag = false;
        str3 += Utils.ParameterToString(Parameter);
        checked { ++index; }
      }
      return str3 + ") As " + Utils.VBFriendlyNameOfType(typ, true);
    }

    internal static string AdjustArraySuffix(string sRank)
    {
      string str = (string) null;
      int length = sRank.Length;
      while (length > 0)
      {
        char ch = sRank[checked (length - 1)];
        switch (ch)
        {
          case '(':
            str += ")";
            break;
          case ')':
            str += "(";
            break;
          case ',':
            str += Conversions.ToString(ch);
            break;
          default:
            str = Conversions.ToString(ch) + str;
            break;
        }
        checked { --length; }
      }
      return str;
    }

    internal static string MemberToString(MemberInfo Member)
    {
      switch (Member.MemberType)
      {
        case MemberTypes.Constructor:
        case MemberTypes.Method:
          return Utils.MethodToString((MethodBase) Member);
        case MemberTypes.Field:
          return Utils.FieldToString((FieldInfo) Member);
        case MemberTypes.Property:
          return Utils.PropertyToString((PropertyInfo) Member);
        default:
          return Member.Name;
      }
    }

    internal static string FieldToString(FieldInfo Field)
    {
      string str = "";
      Type fieldType = Field.FieldType;
      if (Field.IsPublic)
        str += "Public ";
      else if (Field.IsPrivate)
        str += "Private ";
      else if (Field.IsAssembly)
        str += "Friend ";
      else if (Field.IsFamily)
        str += "Protected ";
      else if (Field.IsFamilyOrAssembly)
        str += "Protected Friend ";
      return str + Field.Name + " As " + Utils.VBFriendlyNameOfType(fieldType, true);
    }

    private enum PropertyKind
    {
      ReadWrite,
      ReadOnly,
      WriteOnly,
    }
  }
}
