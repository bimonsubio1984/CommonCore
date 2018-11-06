// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.Information
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace Ported.VisualBasic
{
  /// <summary>The <see langword="Information" /> module contains the procedures used to return, test for, or verify information. </summary>
  [StandardModule]
  public sealed class Information
  {
    private static readonly int[] QBColorTable = new int[16]
    {
      0,
      8388608,
      32768,
      8421376,
      128,
      8388736,
      32896,
      12632256,
      8421504,
      16711680,
      65280,
      16776960,
      (int) byte.MaxValue,
      16711935,
      (int) ushort.MaxValue,
      16777215
    };
    internal const string COMObjectName = "__ComObject";

    /// <summary>Contains information about run-time errors.</summary>
    /// <returns>Contains information about run-time errors.</returns>
    public static ErrObject Err()
    {
      ProjectData projectData = ProjectData.GetProjectData();
      if (projectData.m_Err == null)
        projectData.m_Err = new ErrObject();
      return projectData.m_Err;
    }

    /// <summary>Returns an integer indicating the line number of the last executed statement. Read-only.</summary>
    /// <returns>Returns an integer indicating the line number of the last executed statement. Read-only.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static int Erl()
    {
      return ProjectData.GetProjectData().m_Err.Erl;
    }

    /// <summary>Returns a <see langword="Boolean" /> value indicating whether a variable points to an array.</summary>
    /// <param name="VarName">Required. <see langword="Object" /> variable.</param>
    /// <returns>Returns a <see langword="Boolean" /> value indicating whether a variable points to an array.</returns>
    public static bool IsArray(object VarName)
    {
      if (VarName == null)
        return false;
      return VarName is Array;
    }

    /// <summary>Returns a <see langword="Boolean" /> value indicating whether an expression represents a valid <see langword="Date" /> value.</summary>
    /// <param name="Expression">Required. <see langword="Object" /> expression.</param>
    /// <returns>Returns a <see langword="Boolean" /> value indicating whether an expression represents a valid <see langword="Date" /> value.</returns>
    public static bool IsDate(object Expression)
    {
      if (Expression == null)
        return false;
      if (Expression is DateTime)
        return true;
      string str = Expression as string;
      if (str != null)
      {
        DateTime Result= DateTime.MinValue ;
        return Conversions.TryParseDate(str, ref Result);
      }
      return false;
    }

    /// <summary>Returns a <see langword="Boolean" /> value indicating whether an expression evaluates to the <see cref="T:System.DBNull" /> class.</summary>
    /// <param name="Expression">Required. <see langword="Object" /> expression.</param>
    /// <returns>Returns a <see langword="Boolean" /> value indicating whether an expression evaluates to the <see cref="T:System.DBNull" /> class.</returns>
    public static bool IsDBNull(object Expression)
    {
      return Expression != null && Expression is DBNull;
    }

    /// <summary>Returns a <see langword="Boolean" /> value indicating whether an expression has no object assigned to it.</summary>
    /// <param name="Expression">Required. <see langword="Object" /> expression.</param>
    /// <returns>Returns a <see langword="Boolean" /> value indicating whether an expression has no object assigned to it.</returns>
    public static bool IsNothing(object Expression)
    {
      return Expression == null;
    }

    /// <summary>Returns a <see langword="Boolean" /> value indicating whether an expression is an exception type.</summary>
    /// <param name="Expression">Required. <see langword="Object" /> expression.</param>
    /// <returns>Returns a <see langword="Boolean" /> value indicating whether an expression is an exception type.</returns>
    public static bool IsError(object Expression)
    {
      if (Expression == null)
        return false;
      return Expression is Exception;
    }

    /// <summary>Returns a <see langword="Boolean" /> value indicating whether an expression evaluates to a reference type.</summary>
    /// <param name="Expression">Required. <see langword="Object" /> expression.</param>
    /// <returns>Returns a <see langword="Boolean" /> value indicating whether an expression evaluates to a reference type.</returns>
    public static bool IsReference(object Expression)
    {
      return !(Expression is ValueType);
    }

    /// <summary>Returns the lowest available subscript for the indicated dimension of an array.</summary>
    /// <param name="Array">Required. Array of any data type. The array in which you want to find the lowest possible subscript of a dimension.</param>
    /// <param name="Rank">Optional. <see langword="Integer" />. The dimension for which the lowest possible subscript is to be returned. Use 1 for the first dimension, 2 for the second, and so on. If <paramref name="Rank" /> is omitted, 1 is assumed.</param>
    /// <returns>
    /// <see langword="Integer" />. The lowest value the subscript for the specified dimension can contain. <see langword="LBound" /> always returns 0 as long as <paramref name="Array" /> has been initialized, even if it has no elements, for example if it is a zero-length string. If <paramref name="Array" /> is <see langword="Nothing" />, <see langword="LBound" /> throws an <see cref="T:System.ArgumentNullException" />.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Array" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="Rank" /> less than 1, or <paramref name="Rank" /> is greater than the rank of <paramref name="Array" />.</exception>
    public static int LBound(Array Array, int Rank = 1)
    {
      if (Array == null)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (Array)
        })), 9);
      if (Rank < 1 || Rank > Array.Rank)
        throw new RankException(Utils.GetResourceString("Argument_InvalidRank1", new string[1]
        {
          nameof (Rank)
        }));
      return Array.GetLowerBound(checked (Rank - 1));
    }

    /// <summary>Returns the highest available subscript for the indicated dimension of an array.</summary>
    /// <param name="Array">Required. Array of any data type. The array in which you want to find the highest possible subscript of a dimension.</param>
    /// <param name="Rank">Optional. <see langword="Integer" />. The dimension for which the highest possible subscript is to be returned. Use 1 for the first dimension, 2 for the second, and so on. If <paramref name="Rank" /> is omitted, 1 is assumed.</param>
    /// <returns>
    /// <see langword="Integer" />. The highest value the subscript for the specified dimension can contain. If <paramref name="Array" /> has only one element, <see langword="UBound" /> returns 0. If <paramref name="Array" /> has no elements, for example if it is a zero-length string, <see langword="UBound" /> returns -1. </returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="Array" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.RankException">
    /// <paramref name="Rank" /> is less than 1, or <paramref name="Rank" /> is greater than the rank of <paramref name="Array" />.</exception>
    public static int UBound(Array Array, int Rank = 1)
    {
      if (Array == null)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentNullException(Utils.GetResourceString("Argument_InvalidNullValue1", new string[1]
        {
          nameof (Array)
        })), 9);
      if (Rank < 1 || Rank > Array.Rank)
        throw new RankException(Utils.GetResourceString("Argument_InvalidRank1", new string[1]
        {
          nameof (Rank)
        }));
      return Array.GetUpperBound(checked (Rank - 1));
    }
/*
    internal static string TypeNameOfCOMObject(object VarName, bool bThrowException)
    {
      string str = "__ComObject";
      try
      {
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
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
        if (bThrowException)
          throw ex;
        goto label_17;
      }
      Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.ITypeInfo pTypeInfo = (Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.ITypeInfo) null;
      string pBstrName = (string) null;
      string pBstrDocString = (string) null;
      string pBstrHelpFile = (string) null;
      Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.IProvideClassInfo provideClassInfo = VarName as Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.IProvideClassInfo;
      int pdwHelpContext;
      if (provideClassInfo != null)
      {
        try
        {
          pTypeInfo = provideClassInfo.GetClassInfo();
          if (pTypeInfo.GetDocumentation(-1, out pBstrName, out pBstrDocString, out pdwHelpContext, out pBstrHelpFile) >= 0)
          {
            str = pBstrName;
            goto label_17;
          }
          else
            pTypeInfo = (Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.ITypeInfo) null;
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
      Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.IDispatch dispatch = VarName as Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.IDispatch;
      if (dispatch != null && dispatch.GetTypeInfo(0, 1033, out pTypeInfo) >= 0 && pTypeInfo.GetDocumentation(-1, out pBstrName, out pBstrDocString, out pdwHelpContext, out pBstrHelpFile) >= 0)
        str = pBstrName;
label_17:
      if (str[0] == '_')
        str = str.Substring(1);
      return str;
    }
*/

    /// <summary>Returns an <see langword="Integer" /> value representing the RGB color code corresponding to the specified color number.</summary>
    /// <param name="Color">Required. A whole number in the range 0–15.</param>
    /// <returns>Returns an <see langword="Integer" /> value representing the RGB color code corresponding to the specified color number.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Color" /> is outside of range 0 to 15, inclusive.</exception>
    public static int QBColor(int Color)
    {
      if ((Color & 65520) != 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Color)
        }));
      return Information.QBColorTable[Color];
    }

    /// <summary>Returns an <see langword="Integer" /> value representing an RGB color value from a set of red, green and blue color components.</summary>
    /// <param name="Red">Required. <see langword="Integer" /> in the range 0–255, inclusive, that represents the intensity of the red component of the color.</param>
    /// <param name="Green">Required. <see langword="Integer" /> in the range 0–255, inclusive, that represents the intensity of the green component of the color.</param>
    /// <param name="Blue">Required. <see langword="Integer" /> in the range 0–255, inclusive, that represents the intensity of the blue component of the color.</param>
    /// <returns>Returns an <see langword="Integer" /> value representing an RGB color value from a set of red, green and blue color components.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="Green" />, <paramref name="Blue" />, or <paramref name="Red" /> is outside of range 0 to 255, inclusive.</exception>
    public static int RGB(int Red, int Green, int Blue)
    {
      if ((Red & int.MinValue) != 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Red)
        }));
      if ((Green & int.MinValue) != 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Green)
        }));
      if ((Blue & int.MinValue) != 0)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (Blue)
        }));
      if (Red > (int) byte.MaxValue)
        Red = (int) byte.MaxValue;
      if (Green > (int) byte.MaxValue)
        Green = (int) byte.MaxValue;
      if (Blue > (int) byte.MaxValue)
        Blue = (int) byte.MaxValue;
      return checked (Blue * 65536 + Green * 256 + Red);
    }

    /// <summary>Returns an <see langword="Integer" /> value containing the data type classification of a variable.</summary>
    /// <param name="VarName">Required. <see langword="Object" /> variable. If <see langword="Option Strict" /> is <see langword="Off" />, you can pass a variable of any data type except a structure.</param>
    /// <returns>Returns an <see langword="Integer" /> value containing the data type classification of a variable.</returns>
    public static VariantType VarType(object VarName)
    {
      if (VarName == null)
        return VariantType.Object;
      return Information.VarTypeFromComType(VarName.GetType());
    }

    internal static VariantType VarTypeFromComType(Type typ)
    {
      if (typ == null)
        return VariantType.Object;
      if (typ.IsArray)
      {
        typ = typ.GetElementType();
        if (typ.IsArray)
          return VariantType.Object | VariantType.Array;
        VariantType variantType = Information.VarTypeFromComType(typ);
        if ((variantType & VariantType.Array) != VariantType.Empty)
          return VariantType.Object | VariantType.Array;
        return variantType | VariantType.Array;
      }
      if (typ.IsEnum)
        typ = Enum.GetUnderlyingType(typ);
      if (typ == null)
        return VariantType.Empty;
      switch (Type.GetTypeCode(typ))
      {
        case TypeCode.DBNull:
          return VariantType.Null;
        case TypeCode.Boolean:
          return VariantType.Boolean;
        case TypeCode.Char:
          return VariantType.Char;
        case TypeCode.Byte:
          return VariantType.Byte;
        case TypeCode.Int16:
          return VariantType.Short;
        case TypeCode.Int32:
          return VariantType.Integer;
        case TypeCode.Int64:
          return VariantType.Long;
        case TypeCode.Single:
          return VariantType.Single;
        case TypeCode.Double:
          return VariantType.Double;
        case TypeCode.Decimal:
          return VariantType.Decimal;
        case TypeCode.DateTime:
          return VariantType.Date;
        case TypeCode.String:
          return VariantType.String;
        default:
          if (typ == typeof (Missing) || typ == typeof (Exception) || typ.IsSubclassOf(typeof (Exception)))
            return VariantType.Error;
          return typ.IsValueType ? VariantType.UserDefinedType : VariantType.Object;
      }
    }

    internal static bool IsOldNumericTypeCode(TypeCode TypCode)
    {
      switch (TypCode)
      {
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return true;
        default:
          return false;
      }
    }

    /// <summary>Returns a <see langword="Boolean" /> value indicating whether an expression can be evaluated as a number.</summary>
    /// <param name="Expression">Required. <see langword="Object" /> expression.</param>
    /// <returns>Returns a <see langword="Boolean" /> value indicating whether an expression can be evaluated as a number.</returns>
    public static bool IsNumeric(object Expression)
    {
      IConvertible convertible = Expression as IConvertible;
      if (convertible == null)
      {
        char[] chArray = Expression as char[];
        if (chArray == null)
          return false;
        Expression = (object) new string(chArray);
      }
      TypeCode typeCode = convertible.GetTypeCode();
      switch (typeCode)
      {
        case TypeCode.Char:
        case TypeCode.String:
          string str = convertible.ToString((IFormatProvider) null);
          try
          {
            long i64Value=0;
            if (Utils.IsHexOrOctValue(str, ref i64Value))
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
          double Result=0;
          return DoubleType.TryParse(str, ref Result);
        default:
          return Information.IsOldNumericTypeCode(typeCode);
      }
    }

    internal static string OldVBFriendlyNameOfTypeName(string typename)
    {
      string sRank = (string) null;
      int index = checked (typename.Length - 1);
      if (typename[index] == ']')
      {
        int num = typename.IndexOf('[');
        sRank = checked (num + 1) != index ? typename.Substring(num, checked (index - num + 1)).Replace('[', '(').Replace(']', ')') : "()";
        typename = typename.Substring(0, num);
      }
      string str = Information.OldVbTypeName(typename) ?? typename;
      if (sRank == null)
        return str;
      return str + Utils.AdjustArraySuffix(sRank);
    }

    /// <summary>Returns a <see langword="String" /> value containing data-type information about a variable.</summary>
    /// <param name="VarName">Required. <see langword="Object" /> variable. If <see langword="Option Strict" /> is <see langword="Off" />, you can pass a variable of any data type except a structure.</param>
    /// <returns>Returns a <see langword="String" /> value containing data-type information about a variable.</returns>
    public static string TypeName(object VarName)
    {
      if (VarName == null)
        return "Nothing";
      Type type = VarName.GetType();
      bool flag=false;
      if (type.IsArray)
      {
        flag = true;
        type = type.GetElementType();
      }
      string strA;
      if (type.IsEnum)
      {
        strA = type.Name;
      }
      else
      {
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.DBNull:
            strA = "DBNull";
            goto label_23;
          case TypeCode.Boolean:
            strA = "Boolean";
            goto label_23;
          case TypeCode.Char:
            strA = "Char";
            goto label_23;
          case TypeCode.Byte:
            strA = "Byte";
            goto label_23;
          case TypeCode.Int16:
            strA = "Short";
            goto label_23;
          case TypeCode.Int32:
            strA = "Integer";
            goto label_23;
          case TypeCode.Int64:
            strA = "Long";
            goto label_23;
          case TypeCode.Single:
            strA = "Single";
            goto label_23;
          case TypeCode.Double:
            strA = "Double";
            goto label_23;
          case TypeCode.Decimal:
            strA = "Decimal";
            goto label_23;
          case TypeCode.DateTime:
            strA = "Date";
            goto label_23;
          case TypeCode.String:
            strA = "String";
            goto label_23;
          default:
            strA = type.Name;
            if (type.IsCOMObject && string.CompareOrdinal(strA, "__ComObject") == 0)
            {
              strA = Information.LegacyTypeNameOfCOMObject(VarName, true);
              break;
            }
            break;
        }
      }
      int num = strA.IndexOf('+');
      if (num >= 0)
        strA = strA.Substring(checked (num + 1));
label_23:
      if (flag)
      {
        Array array = (Array) VarName;
        strA = Information.OldVBFriendlyNameOfTypeName(array.Rank != 1 ? strA + "[" + new string(',', checked (array.Rank - 1)) + "]" : strA + "[]");
      }
      return strA;
    }

    /// <summary>Returns a <see langword="String" /> value containing the system data type name of a variable.</summary>
    /// <param name="VbName">Required. A <see langword="String" /> variable containing a Visual Basic type name.</param>
    /// <returns>Returns a <see langword="String" /> value containing the system data type name of a variable.</returns>
    public static string SystemTypeName(string VbName)
    {
      string upperInvariant = Strings.Trim(VbName).ToUpperInvariant();
      if (Operators.CompareString(upperInvariant, "OBJECT", false) == 0)
        return "System.Object";
      if (Operators.CompareString(upperInvariant, "SHORT", false) == 0)
        return "System.Int16";
      if (Operators.CompareString(upperInvariant, "INTEGER", false) == 0)
        return "System.Int32";
      if (Operators.CompareString(upperInvariant, "SINGLE", false) == 0)
        return "System.Single";
      if (Operators.CompareString(upperInvariant, "DOUBLE", false) == 0)
        return "System.Double";
      if (Operators.CompareString(upperInvariant, "DATE", false) == 0)
        return "System.DateTime";
      if (Operators.CompareString(upperInvariant, "STRING", false) == 0)
        return "System.String";
      if (Operators.CompareString(upperInvariant, "BOOLEAN", false) == 0)
        return "System.Boolean";
      if (Operators.CompareString(upperInvariant, "DECIMAL", false) == 0)
        return "System.Decimal";
      if (Operators.CompareString(upperInvariant, "BYTE", false) == 0)
        return "System.Byte";
      if (Operators.CompareString(upperInvariant, "CHAR", false) == 0)
        return "System.Char";
      if (Operators.CompareString(upperInvariant, "LONG", false) == 0)
        return "System.Int64";
      return (string) null;
    }

    /// <summary>Returns a <see langword="String" /> value containing the Visual Basic data type name of a variable.</summary>
    /// <param name="UrtName">Required. <see langword="String" /> variable containing a type name used by the common language runtime.</param>
    /// <returns>Returns a <see langword="String" /> value containing the Visual Basic data type name of a variable.</returns>
    public static string VbTypeName(string UrtName)
    {
      return Information.OldVbTypeName(UrtName);
    }

    internal static string OldVbTypeName(string UrtName)
    {
      UrtName = Strings.Trim(UrtName).ToUpperInvariant();
      if (Operators.CompareString(Strings.Left(UrtName, 7), "SYSTEM.", false) == 0)
        UrtName = Strings.Mid(UrtName, 8);
      string Left = UrtName;
      if (Operators.CompareString(Left, "OBJECT", false) == 0)
        return "Object";
      if (Operators.CompareString(Left, "INT16", false) == 0)
        return "Short";
      if (Operators.CompareString(Left, "INT32", false) == 0)
        return "Integer";
      if (Operators.CompareString(Left, "SINGLE", false) == 0)
        return "Single";
      if (Operators.CompareString(Left, "DOUBLE", false) == 0)
        return "Double";
      if (Operators.CompareString(Left, "DATETIME", false) == 0)
        return "Date";
      if (Operators.CompareString(Left, "STRING", false) == 0)
        return "String";
      if (Operators.CompareString(Left, "BOOLEAN", false) == 0)
        return "Boolean";
      if (Operators.CompareString(Left, "DECIMAL", false) == 0)
        return "Decimal";
      if (Operators.CompareString(Left, "BYTE", false) == 0)
        return "Byte";
      if (Operators.CompareString(Left, "CHAR", false) == 0)
        return "Char";
      if (Operators.CompareString(Left, "INT64", false) == 0)
        return "Long";
      return (string) null;
    }

    internal static string LegacyTypeNameOfCOMObject(object VarName, bool bThrowException)
    {
      string str = "__ComObject";
      try
      {
        //new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
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
        if (bThrowException)
          throw ex;
        goto label_9;
      }
      Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.ITypeInfo pTypeInfo = (Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.ITypeInfo) null;
      string pBstrName = (string) null;
      string pBstrDocString = (string) null;
      string pBstrHelpFile = (string) null;
      Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.IDispatch dispatch = VarName as Ported.VisualBasic.CompilerServices.UnsafeNativeMethods.IDispatch;
      int pdwHelpContext;
      if (dispatch != null && dispatch.GetTypeInfo(0, 1033, out pTypeInfo) >= 0 && pTypeInfo.GetDocumentation(-1, out pBstrName, out pBstrDocString, out pdwHelpContext, out pBstrHelpFile) >= 0)
        str = pBstrName;
label_9:
      if (str[0] == '_')
        str = str.Substring(1);
      return str;
    }
  }
}
