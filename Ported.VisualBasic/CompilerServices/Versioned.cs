// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.Versioned
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>The <see cref="T:Ported.VisualBasic.CompilerServices.Versioned" /> module contains procedures used to interact with objects, applications, and systems.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class Versioned
  {
    private Versioned()
    {
    }

    /// <summary>Executes a method on an object, or sets or returns a property on an object.</summary>
    /// <param name="Instance">Required. <see langword="Object" />. A pointer to the object exposing the property or method.</param>
    /// <param name="MethodName">Required. <see langword="String" />. A string expression containing the name of the property or method on the object.</param>
    /// <param name="UseCallType">Required. An enumeration member of type <see cref="T:Ported.VisualBasic.CallType" /> representing the type of procedure being called. The value of <see langword="CallType" /> can be <see langword="Method" />, <see langword="Get" />, or <see langword="Set" />.</param>
    /// <param name="Arguments">Optional. <see langword="ParamArray" />. A parameter array containing the arguments to be passed to the property or method being called.</param>
    /// <returns>Executes a method on an object, or sets or returns a property on an object.</returns>
    public static object CallByName(object Instance, string MethodName, CallType UseCallType, params object[] Arguments)
    {
      switch (UseCallType)
      {
        case CallType.Method:
          return NewLateBinding.LateCall(Instance, (Type) null, MethodName, Arguments, (string[]) null, (Type[]) null, (bool[]) null, false);
        case CallType.Get:
          return NewLateBinding.LateGet(Instance, (Type) null, MethodName, Arguments, (string[]) null, (Type[]) null, (bool[]) null);
        case CallType.Let:
        case CallType.Set:
          NewLateBinding.LateSet(Instance, (Type) null, MethodName, Arguments, (string[]) null, (Type[]) null, false, false, UseCallType);
          return (object) null;
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
          {
            "CallType"
          }));
      }
    }

    /// <summary>Returns a Boolean value indicating whether an expression can be evaluated as a number.</summary>
    /// <param name="Expression">Required. <see langword="Object" /> expression.</param>
    /// <returns>Returns a Boolean value indicating whether an expression can be evaluated as a number.</returns>
    public static bool IsNumeric(object Expression)
    {
      IConvertible convertible = Expression as IConvertible;
      if (convertible == null)
        return false;
      switch (convertible.GetTypeCode())
      {
        case TypeCode.Boolean:
          return true;
        case TypeCode.Char:
        case TypeCode.String:
          string str = convertible.ToString((IFormatProvider) null);
          try
          {
            long i64Value=0;
            if (Utils.IsHexOrOctValue(str, ref i64Value))
              return true;
          }
          catch (FormatException ex)
          {
            return false;
          }
          double Result=0;
          return Conversions.TryParseDouble(str, ref Result);
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
          return true;
        default:
          return false;
      }
    }

    /// <summary>Returns a string value containing data type information about a variable.</summary>
    /// <param name="Expression">Required. <see langword="Object" /> variable. If <see langword="Option Strict" /> is <see langword="Off" />, you can pass a variable of any data type except a structure.</param>
    /// <returns>Returns a string value containing data type information about a variable.</returns>
    public static string TypeName(object Expression)
    {
      if (Expression == null)
        return "Nothing";
      Type type = Expression.GetType();
      return !type.IsCOMObject || string.CompareOrdinal(type.Name, "__ComObject") != 0 ? Utils.VBFriendlyNameOfType(type, false) : "Not implemented" /*Information.TypeNameOfCOMObject(Expression, true)*/;
    }

    /// <summary>Returns a string value containing the system data type name of a variable.</summary>
    /// <param name="VbName">Required. A string variable containing a Visual Basic type name.</param>
    /// <returns>Returns a string value containing the system data type name of a variable.</returns>
    public static string SystemTypeName(string VbName)
    {
      string upperInvariant = Strings.Trim(VbName).ToUpperInvariant();
      if (Operators.CompareString(upperInvariant, "BOOLEAN", false) == 0)
        return "System.Boolean";
      if (Operators.CompareString(upperInvariant, "SBYTE", false) == 0)
        return "System.SByte";
      if (Operators.CompareString(upperInvariant, "BYTE", false) == 0)
        return "System.Byte";
      if (Operators.CompareString(upperInvariant, "SHORT", false) == 0)
        return "System.Int16";
      if (Operators.CompareString(upperInvariant, "USHORT", false) == 0)
        return "System.UInt16";
      if (Operators.CompareString(upperInvariant, "INTEGER", false) == 0)
        return "System.Int32";
      if (Operators.CompareString(upperInvariant, "UINTEGER", false) == 0)
        return "System.UInt32";
      if (Operators.CompareString(upperInvariant, "LONG", false) == 0)
        return "System.Int64";
      if (Operators.CompareString(upperInvariant, "ULONG", false) == 0)
        return "System.UInt64";
      if (Operators.CompareString(upperInvariant, "DECIMAL", false) == 0)
        return "System.Decimal";
      if (Operators.CompareString(upperInvariant, "SINGLE", false) == 0)
        return "System.Single";
      if (Operators.CompareString(upperInvariant, "DOUBLE", false) == 0)
        return "System.Double";
      if (Operators.CompareString(upperInvariant, "DATE", false) == 0)
        return "System.DateTime";
      if (Operators.CompareString(upperInvariant, "CHAR", false) == 0)
        return "System.Char";
      if (Operators.CompareString(upperInvariant, "STRING", false) == 0)
        return "System.String";
      if (Operators.CompareString(upperInvariant, "OBJECT", false) == 0)
        return "System.Object";
      return (string) null;
    }

    /// <summary>Returns a string value containing the Visual Basic data type name of a variable.</summary>
    /// <param name="SystemName">Required. String variable containing a type name used by the common language runtime.</param>
    /// <returns>Returns a string value containing the Visual Basic data type name of a variable.</returns>
    public static string VbTypeName(string SystemName)
    {
      SystemName = Strings.Trim(SystemName).ToUpperInvariant();
      if (Operators.CompareString(Strings.Left(SystemName, 7), "SYSTEM.", false) == 0)
        SystemName = Strings.Mid(SystemName, 8);
      string Left = SystemName;
      if (Operators.CompareString(Left, "BOOLEAN", false) == 0)
        return "Boolean";
      if (Operators.CompareString(Left, "SBYTE", false) == 0)
        return "SByte";
      if (Operators.CompareString(Left, "BYTE", false) == 0)
        return "Byte";
      if (Operators.CompareString(Left, "INT16", false) == 0)
        return "Short";
      if (Operators.CompareString(Left, "UINT16", false) == 0)
        return "UShort";
      if (Operators.CompareString(Left, "INT32", false) == 0)
        return "Integer";
      if (Operators.CompareString(Left, "UINT32", false) == 0)
        return "UInteger";
      if (Operators.CompareString(Left, "INT64", false) == 0)
        return "Long";
      if (Operators.CompareString(Left, "UINT64", false) == 0)
        return "ULong";
      if (Operators.CompareString(Left, "DECIMAL", false) == 0)
        return "Decimal";
      if (Operators.CompareString(Left, "SINGLE", false) == 0)
        return "Single";
      if (Operators.CompareString(Left, "DOUBLE", false) == 0)
        return "Double";
      if (Operators.CompareString(Left, "DATETIME", false) == 0)
        return "Date";
      if (Operators.CompareString(Left, "CHAR", false) == 0)
        return "Char";
      if (Operators.CompareString(Left, "STRING", false) == 0)
        return "String";
      if (Operators.CompareString(Left, "OBJECT", false) == 0)
        return "Object";
      return (string) null;
    }
  }
}
