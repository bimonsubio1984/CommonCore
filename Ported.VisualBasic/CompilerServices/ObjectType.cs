// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.ObjectType
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>This class has been deprecated as of Visual Basic 2005. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public sealed class ObjectType
  {
    private static readonly ObjectType.VType[,] WiderType = new ObjectType.VType[12, 12]
    {
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bool,
        ObjectType.VType.t_bool,
        ObjectType.VType.t_i2,
        ObjectType.VType.t_i4,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bool,
        ObjectType.VType.t_ui1,
        ObjectType.VType.t_i2,
        ObjectType.VType.t_i4,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_i2,
        ObjectType.VType.t_i2,
        ObjectType.VType.t_i2,
        ObjectType.VType.t_i4,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_i4,
        ObjectType.VType.t_i4,
        ObjectType.VType.t_i4,
        ObjectType.VType.t_i4,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_i8,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_dec,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r4,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_char,
        ObjectType.VType.t_str,
        ObjectType.VType.t_bad
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_r8,
        ObjectType.VType.t_str,
        ObjectType.VType.t_str,
        ObjectType.VType.t_date
      },
      {
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_bad,
        ObjectType.VType.t_date,
        ObjectType.VType.t_date
      }
    };
    private static readonly ObjectType.CC[,] ConversionClassTable = new ObjectType.CC[13, 13]
    {
      {
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Same,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Same,
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Same,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Wide,
        ObjectType.CC.Err,
        ObjectType.CC.Same,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Wide,
        ObjectType.CC.Err,
        ObjectType.CC.Wide,
        ObjectType.CC.Same,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Wide,
        ObjectType.CC.Err,
        ObjectType.CC.Wide,
        ObjectType.CC.Wide,
        ObjectType.CC.Same,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Wide,
        ObjectType.CC.Err,
        ObjectType.CC.Wide,
        ObjectType.CC.Wide,
        ObjectType.CC.Wide,
        ObjectType.CC.Same,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Wide,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Wide,
        ObjectType.CC.Err,
        ObjectType.CC.Wide,
        ObjectType.CC.Wide,
        ObjectType.CC.Wide,
        ObjectType.CC.Wide,
        ObjectType.CC.Same,
        ObjectType.CC.Err,
        ObjectType.CC.Wide,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Same,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Wide,
        ObjectType.CC.Err,
        ObjectType.CC.Wide,
        ObjectType.CC.Wide,
        ObjectType.CC.Wide,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Same,
        ObjectType.CC.Err,
        ObjectType.CC.Narr
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err,
        ObjectType.CC.Err
      },
      {
        ObjectType.CC.Err,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Wide,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Narr,
        ObjectType.CC.Err,
        ObjectType.CC.Same
      }
    };
    private const int TCMAX = 19;

    private static ObjectType.VType VTypeFromTypeCode(TypeCode typ)
    {
      switch (typ)
      {
        case TypeCode.Boolean:
          return ObjectType.VType.t_bool;
        case TypeCode.Char:
          return ObjectType.VType.t_char;
        case TypeCode.Byte:
          return ObjectType.VType.t_ui1;
        case TypeCode.Int16:
          return ObjectType.VType.t_i2;
        case TypeCode.Int32:
          return ObjectType.VType.t_i4;
        case TypeCode.Int64:
          return ObjectType.VType.t_i8;
        case TypeCode.Single:
          return ObjectType.VType.t_r4;
        case TypeCode.Double:
          return ObjectType.VType.t_r8;
        case TypeCode.Decimal:
          return ObjectType.VType.t_dec;
        case TypeCode.DateTime:
          return ObjectType.VType.t_date;
        case TypeCode.String:
          return ObjectType.VType.t_str;
        default:
          return ObjectType.VType.t_bad;
      }
    }

    private static ObjectType.VType2 VType2FromTypeCode(TypeCode typ)
    {
      switch (typ)
      {
        case TypeCode.Boolean:
          return ObjectType.VType2.t_bool;
        case TypeCode.Char:
          return ObjectType.VType2.t_char;
        case TypeCode.Byte:
          return ObjectType.VType2.t_ui1;
        case TypeCode.Int16:
          return ObjectType.VType2.t_i2;
        case TypeCode.Int32:
          return ObjectType.VType2.t_i4;
        case TypeCode.Int64:
          return ObjectType.VType2.t_i8;
        case TypeCode.Single:
          return ObjectType.VType2.t_r4;
        case TypeCode.Double:
          return ObjectType.VType2.t_r8;
        case TypeCode.Decimal:
          return ObjectType.VType2.t_dec;
        case TypeCode.DateTime:
          return ObjectType.VType2.t_date;
        case TypeCode.String:
          return ObjectType.VType2.t_str;
        default:
          return ObjectType.VType2.t_bad;
      }
    }

    private static TypeCode TypeCodeFromVType(ObjectType.VType vartyp)
    {
      switch (vartyp)
      {
        case ObjectType.VType.t_bool:
          return TypeCode.Boolean;
        case ObjectType.VType.t_ui1:
          return TypeCode.Byte;
        case ObjectType.VType.t_i2:
          return TypeCode.Int16;
        case ObjectType.VType.t_i4:
          return TypeCode.Int32;
        case ObjectType.VType.t_i8:
          return TypeCode.Int64;
        case ObjectType.VType.t_dec:
          return TypeCode.Decimal;
        case ObjectType.VType.t_r4:
          return TypeCode.Single;
        case ObjectType.VType.t_r8:
          return TypeCode.Double;
        case ObjectType.VType.t_char:
          return TypeCode.Char;
        case ObjectType.VType.t_str:
          return TypeCode.String;
        case ObjectType.VType.t_date:
          return TypeCode.DateTime;
        default:
          return TypeCode.Object;
      }
    }

    internal static Type TypeFromTypeCode(TypeCode vartyp)
    {
      switch (vartyp)
      {
        case TypeCode.Object:
          return typeof (object);
        case TypeCode.DBNull:
          return typeof (DBNull);
        case TypeCode.Boolean:
          return typeof (bool);
        case TypeCode.Char:
          return typeof (char);
        case TypeCode.SByte:
          return typeof (sbyte);
        case TypeCode.Byte:
          return typeof (byte);
        case TypeCode.Int16:
          return typeof (short);
        case TypeCode.UInt16:
          return typeof (ushort);
        case TypeCode.Int32:
          return typeof (int);
        case TypeCode.UInt32:
          return typeof (uint);
        case TypeCode.Int64:
          return typeof (long);
        case TypeCode.UInt64:
          return typeof (ulong);
        case TypeCode.Single:
          return typeof (float);
        case TypeCode.Double:
          return typeof (double);
        case TypeCode.Decimal:
          return typeof (Decimal);
        case TypeCode.DateTime:
          return typeof (DateTime);
        case TypeCode.String:
          return typeof (string);
        default:
          return (Type) null;
      }
    }

    internal static bool IsWiderNumeric(Type Type1, Type Type2)
    {
      TypeCode typeCode1 = Type.GetTypeCode(Type1);
      TypeCode typeCode2 = Type.GetTypeCode(Type2);
      if (Information.IsOldNumericTypeCode(typeCode1) && Information.IsOldNumericTypeCode(typeCode2) && (typeCode1 != TypeCode.Boolean && typeCode2 != TypeCode.Boolean) && !Type1.IsEnum)
        return ObjectType.WiderType[(int) ObjectType.VTypeFromTypeCode(typeCode1), (int) ObjectType.VTypeFromTypeCode(typeCode2)] == ObjectType.VTypeFromTypeCode(typeCode1);
      return false;
    }

    internal static bool IsWideningConversion(Type FromType, Type ToType)
    {
      TypeCode typeCode1 = Type.GetTypeCode(FromType);
      TypeCode typeCode2 = Type.GetTypeCode(ToType);
      if (typeCode1 == TypeCode.Object)
      {
        if (FromType == typeof (char[]) && (typeCode2 == TypeCode.String || ToType == typeof (char[])))
          return true;
        if (typeCode2 != TypeCode.Object)
          return false;
        if (!FromType.IsArray || !ToType.IsArray)
          return ToType.IsAssignableFrom(FromType);
        if (FromType.GetArrayRank() == ToType.GetArrayRank())
          return ToType.GetElementType().IsAssignableFrom(FromType.GetElementType());
        return false;
      }
      if (typeCode2 == TypeCode.Object)
      {
        if (ToType == typeof (char[]) && typeCode1 == TypeCode.String)
          return false;
        return ToType.IsAssignableFrom(FromType);
      }
      if (ToType.IsEnum)
        return false;
      switch (ObjectType.ConversionClassTable[(int) ObjectType.VType2FromTypeCode(typeCode2), (int) ObjectType.VType2FromTypeCode(typeCode1)])
      {
        case ObjectType.CC.Same:
        case ObjectType.CC.Wide:
          return true;
        default:
          return false;
      }
    }

    internal static TypeCode GetWidestType(object obj1, object obj2, bool IsAdd = false)
    {
      IConvertible convertible1 = obj1 as IConvertible;
      IConvertible convertible2 = obj2 as IConvertible;
      TypeCode typ1 = convertible1 == null ? (obj1 != null ? (!(obj1 is char[]) || ((Array) obj1).Rank != 1 ? TypeCode.Object : TypeCode.String) : TypeCode.Empty) : convertible1.GetTypeCode();
      TypeCode typ2 = convertible2 == null ? (obj2 != null ? (!(obj2 is char[]) || ((Array) obj2).Rank != 1 ? TypeCode.Object : TypeCode.String) : TypeCode.Empty) : convertible2.GetTypeCode();
      if (obj1 == null)
        return typ2;
      if (obj2 == null)
        return typ1;
      if (IsAdd && (typ1 == TypeCode.DBNull && typ2 == TypeCode.String || typ1 == TypeCode.String && typ2 == TypeCode.DBNull))
        return TypeCode.DBNull;
      return ObjectType.TypeCodeFromVType(ObjectType.WiderType[(int) ObjectType.VTypeFromTypeCode(typ1), (int) ObjectType.VTypeFromTypeCode(typ2)]);
    }

    internal static TypeCode GetWidestType(object obj1, TypeCode type2)
    {
      IConvertible convertible = obj1 as IConvertible;
      TypeCode typ = convertible == null ? (obj1 != null ? (!(obj1 is char[]) || ((Array) obj1).Rank != 1 ? TypeCode.Object : TypeCode.String) : TypeCode.Empty) : convertible.GetTypeCode();
      if (obj1 == null)
        return type2;
      return ObjectType.TypeCodeFromVType(ObjectType.WiderType[(int) ObjectType.VTypeFromTypeCode(typ), (int) ObjectType.VTypeFromTypeCode(type2)]);
    }

    /// <summary>Performs binary or text string comparison when given two objects.</summary>
    /// <param name="o1">Required. Any expression.</param>
    /// <param name="o2">Required. Any expression.</param>
    /// <param name="TextCompare">Required. <see langword="True" /> to perform a case-insensitive string comparison; otherwise, <see langword="False" />.</param>
    /// <returns>Value Condition -1
    /// <paramref name="o1" /> is less than <paramref name="o2" />. 0
    /// <paramref name="o1" /> is equal to <paramref name="o2" />. 1
    /// <paramref name="o1" /> is greater than <paramref name="o2" />. </returns>
    public static int ObjTst(object o1, object o2, bool TextCompare)
    {
      IConvertible convertible1 = o1 as IConvertible;
      TypeCode tc1 = convertible1 != null ? convertible1.GetTypeCode() : (o1 != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = o2 as IConvertible;
      TypeCode tc2 = convertible2 != null ? convertible2.GetTypeCode() : (o2 != null ? TypeCode.Object : TypeCode.Empty);
      if (tc1 == TypeCode.Object && o1 is char[] && (tc2 == TypeCode.String || tc2 == TypeCode.Empty || tc2 == TypeCode.Object && o2 is char[]))
      {
        o1 = (object) new string(CharArrayType.FromObject(o1));
        convertible1 = (IConvertible) o1;
        tc1 = TypeCode.String;
      }
      if (tc2 == TypeCode.Object && o2 is char[] && (tc1 == TypeCode.String || tc1 == TypeCode.Empty))
      {
        o2 = (object) new string(CharArrayType.FromObject(o2));
        convertible2 = (IConvertible) o2;
        tc2 = TypeCode.String;
      }
      switch (checked (unchecked ((int) tc1) * 19) + tc2)
      {
        case TypeCode.Empty:
          return 0;
        case TypeCode.Boolean:
          return ObjectType.ObjTstInt32(0, ObjectType.ToVBBool(convertible2));
        case TypeCode.Char:
          return ObjectType.ObjTstChar(char.MinValue, convertible2.ToChar((IFormatProvider) null));
        case TypeCode.Byte:
          return ObjectType.ObjTstByte((byte) 0, convertible2.ToByte((IFormatProvider) null));
        case TypeCode.Int16:
          return ObjectType.ObjTstInt16((short) 0, convertible2.ToInt16((IFormatProvider) null));
        case TypeCode.Int32:
          return ObjectType.ObjTstInt32(0, convertible2.ToInt32((IFormatProvider) null));
        case TypeCode.Int64:
          return ObjectType.ObjTstInt64(0L, convertible2.ToInt64((IFormatProvider) null));
        case TypeCode.Single:
          return ObjectType.ObjTstSingle(0.0f, convertible2.ToSingle((IFormatProvider) null));
        case TypeCode.Double:
          return ObjectType.ObjTstDouble(0.0, convertible2.ToDouble((IFormatProvider) null));
        case TypeCode.Decimal:
          return ObjectType.ObjTstDecimal((IConvertible) 0, convertible2);
        case TypeCode.DateTime:
          return ObjectType.ObjTstDateTime(DateType.FromObject((object) null), convertible2.ToDateTime((IFormatProvider) null));
        case TypeCode.String:
          return ObjectType.ObjTstStringString((string) null, o2.ToString(), TextCompare);
        case (TypeCode) 57:
          return ObjectType.ObjTstInt32(ObjectType.ToVBBool(convertible1), 0);
        case (TypeCode) 60:
          return ObjectType.ObjTstInt16(checked ((short) ObjectType.ToVBBool(convertible1)), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return ObjectType.ObjTstInt16(checked ((short) ObjectType.ToVBBool(convertible1)), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 66:
          return ObjectType.ObjTstInt32(ObjectType.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 68:
          return ObjectType.ObjTstInt64((long) ObjectType.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 70:
          return ObjectType.ObjTstSingle((float) ObjectType.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return ObjectType.ObjTstDouble((double) ObjectType.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 72:
          return ObjectType.ObjTstDecimal((IConvertible) ObjectType.ToVBBool(convertible1), convertible2);
        case (TypeCode) 75:
          return ObjectType.ObjTstBoolean(convertible1.ToBoolean((IFormatProvider) null), BooleanType.FromString(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 76:
          return ObjectType.ObjTstChar(convertible1.ToChar((IFormatProvider) null), char.MinValue);
        case (TypeCode) 80:
          return ObjectType.ObjTstChar(convertible1.ToChar((IFormatProvider) null), convertible2.ToChar((IFormatProvider) null));
        case (TypeCode) 94:
        case (TypeCode) 346:
          return ObjectType.ObjTstStringString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null), TextCompare);
        case (TypeCode) 114:
          return ObjectType.ObjTstByte(convertible1.ToByte((IFormatProvider) null), (byte) 0);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return ObjectType.ObjTstInt16(convertible1.ToInt16((IFormatProvider) null), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 120:
          return ObjectType.ObjTstByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 121:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return ObjectType.ObjTstInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 123:
        case (TypeCode) 142:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 180:
          return ObjectType.ObjTstInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 125:
        case (TypeCode) 144:
        case (TypeCode) 182:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 218:
        case (TypeCode) 220:
          return ObjectType.ObjTstInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 184:
        case (TypeCode) 222:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 256:
        case (TypeCode) 258:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return ObjectType.ObjTstSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 185:
        case (TypeCode) 223:
        case (TypeCode) 261:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 275:
        case (TypeCode) 277:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return ObjectType.ObjTstDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 129:
        case (TypeCode) 148:
        case (TypeCode) 186:
        case (TypeCode) 224:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 294:
        case (TypeCode) 296:
        case (TypeCode) 300:
          return ObjectType.ObjTstDecimal(convertible1, convertible2);
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 189:
        case (TypeCode) 227:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return ObjectType.ObjTstString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 133:
          return ObjectType.ObjTstInt16(convertible1.ToInt16((IFormatProvider) null), (short) 0);
        case (TypeCode) 171:
          return ObjectType.ObjTstInt32(convertible1.ToInt32((IFormatProvider) null), 0);
        case (TypeCode) 174:
          return ObjectType.ObjTstInt32(convertible1.ToInt32((IFormatProvider) null), ObjectType.ToVBBool(convertible2));
        case (TypeCode) 209:
          return ObjectType.ObjTstInt64(convertible1.ToInt64((IFormatProvider) null), 0L);
        case (TypeCode) 212:
          return ObjectType.ObjTstInt64(convertible1.ToInt64((IFormatProvider) null), (long) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 247:
          return ObjectType.ObjTstSingle(convertible1.ToSingle((IFormatProvider) null), 0.0f);
        case (TypeCode) 250:
          return ObjectType.ObjTstSingle(convertible1.ToSingle((IFormatProvider) null), (float) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 266:
          return ObjectType.ObjTstDouble(convertible1.ToDouble((IFormatProvider) null), 0.0);
        case (TypeCode) 269:
          return ObjectType.ObjTstDouble(convertible1.ToDouble((IFormatProvider) null), (double) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 285:
          return ObjectType.ObjTstDecimal(convertible1, (IConvertible) 0);
        case (TypeCode) 288:
          return ObjectType.ObjTstDecimal(convertible1, (IConvertible) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 304:
          return ObjectType.ObjTstDateTime(convertible1.ToDateTime((IFormatProvider) null), DateType.FromObject((object) null));
        case (TypeCode) 320:
          return ObjectType.ObjTstDateTime(convertible1.ToDateTime((IFormatProvider) null), convertible2.ToDateTime((IFormatProvider) null));
        case (TypeCode) 322:
          return ObjectType.ObjTstDateTime(convertible1.ToDateTime((IFormatProvider) null), DateType.FromString(convertible2.ToString((IFormatProvider) null), Utils.GetCultureInfo()));
        case (TypeCode) 342:
          return ObjectType.ObjTstStringString(o1.ToString(), (string) null, TextCompare);
        case (TypeCode) 345:
          return ObjectType.ObjTstBoolean(BooleanType.FromString(convertible1.ToString((IFormatProvider) null)), convertible2.ToBoolean((IFormatProvider) null));
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 351:
        case (TypeCode) 353:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return ObjectType.ObjTstString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 358:
          return ObjectType.ObjTstDateTime(DateType.FromString(convertible1.ToString((IFormatProvider) null), Utils.GetCultureInfo()), convertible2.ToDateTime((IFormatProvider) null));
        case (TypeCode) 360:
          return ObjectType.ObjTstStringString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null), TextCompare);
        default:
          throw ObjectType.GetNoValidOperatorException(o1, o2);
      }
    }

    private static int ObjTstDateTime(DateTime var1, DateTime var2)
    {
      long ticks1 = var1.Ticks;
      long ticks2 = var2.Ticks;
      if (ticks1 < ticks2)
        return -1;
      return ticks1 > ticks2 ? 1 : 0;
    }

    private static int ObjTstBoolean(bool b1, bool b2)
    {
      if (b1 == b2)
        return 0;
      return (b1 ? 1 : 0) < (b2 ? 1 : 0) ? 1 : -1;
    }

    private static int ObjTstDouble(double d1, double d2)
    {
      if (d1 < d2)
        return -1;
      return d1 > d2 ? 1 : 0;
    }

    private static int ObjTstChar(char ch1, char ch2)
    {
      if ((int) ch1 < (int) ch2)
        return -1;
      return (int) ch1 > (int) ch2 ? 1 : 0;
    }

    private static int ObjTstByte(byte by1, byte by2)
    {
      if ((uint) by1 < (uint) by2)
        return -1;
      return (uint) by1 > (uint) by2 ? 1 : 0;
    }

    private static int ObjTstSingle(float d1, float d2)
    {
      if ((double) d1 < (double) d2)
        return -1;
      return (double) d1 > (double) d2 ? 1 : 0;
    }

    private static int ObjTstInt16(short d1, short d2)
    {
      if ((int) d1 < (int) d2)
        return -1;
      return (int) d1 > (int) d2 ? 1 : 0;
    }

    private static int ObjTstInt32(int d1, int d2)
    {
      if (d1 < d2)
        return -1;
      return d1 > d2 ? 1 : 0;
    }

    private static int ObjTstInt64(long d1, long d2)
    {
      if (d1 < d2)
        return -1;
      return d1 > d2 ? 1 : 0;
    }

    private static int ObjTstDecimal(IConvertible i1, IConvertible i2)
    {
      Decimal d1 = i1.ToDecimal((IFormatProvider) null);
      Decimal d2 = i2.ToDecimal((IFormatProvider) null);
      if (Decimal.Compare(d1, d2) < 0)
        return -1;
      return Decimal.Compare(d1, d2) > 0 ? 1 : 0;
    }

    private static int ObjTstString(IConvertible conv1, TypeCode tc1, IConvertible conv2, TypeCode tc2)
    {
      double d1;
      switch (tc1)
      {
        case TypeCode.Boolean:
          d1 = (double) ObjectType.ToVBBool(conv1);
          break;
        case TypeCode.String:
          d1 = DoubleType.FromString(conv1.ToString((IFormatProvider) null));
          break;
        default:
          d1 = conv1.ToDouble((IFormatProvider) null);
          break;
      }
      double d2;
      switch (tc2)
      {
        case TypeCode.Boolean:
          d2 = (double) ObjectType.ToVBBool(conv2);
          break;
        case TypeCode.String:
          d2 = DoubleType.FromString(conv2.ToString((IFormatProvider) null));
          break;
        default:
          d2 = conv2.ToDouble((IFormatProvider) null);
          break;
      }
      return ObjectType.ObjTstDouble(d1, d2);
    }

    private static int ObjTstStringString(string s1, string s2, bool TextCompare)
    {
      if (s1 == null)
        return s2.Length > 0 ? -1 : 0;
      if (s2 == null)
        return s1.Length > 0 ? 1 : 0;
      if (TextCompare)
        return Utils.GetCultureInfo().CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
      return string.CompareOrdinal(s1, s2);
    }

    /// <summary>Performs a unary plus (+) operation.</summary>
    /// <param name="obj">Required. Any numeric expression.</param>
    /// <returns>The value of <paramref name="obj" />. (The sign of the <paramref name="obj" /> is unchanged.)</returns>
    public static object PlusObj(object obj)
    {
      if (obj == null)
        return (object) 0;
      IConvertible convertible = obj as IConvertible;
      switch (convertible != null ? (int) convertible.GetTypeCode() : (obj != null ? 1 : 0))
      {
        case 0:
          return (object) 0;
        case 3:
          if (obj is bool)
            return (object) (short) -((bool) obj ? 1 : 0);
          return (object) (short) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0);
        case 6:
        case 7:
        case 9:
        case 11:
        case 13:
        case 14:
        case 15:
          return obj;
        case 18:
          return (object) DoubleType.FromObject(obj);
        default:
          throw ObjectType.GetNoValidOperatorException(obj);
      }
    }

    /// <summary>Performs a unary minus (–) operation.</summary>
    /// <param name="obj">Required. Any numeric expression.</param>
    /// <returns>The negative value of <paramref name="obj" />.</returns>
    public static object NegObj(object obj)
    {
      IConvertible conv = obj as IConvertible;
      TypeCode tc = conv != null ? conv.GetTypeCode() : (obj != null ? TypeCode.Object : TypeCode.Empty);
      return ObjectType.InternalNegObj(obj, conv, tc);
    }

    private static object InternalNegObj(object obj, IConvertible conv, TypeCode tc)
    {
      short num1;
      double num2;
      switch (tc)
      {
        case TypeCode.Empty:
          return (object) 0;
        case TypeCode.Boolean:
                    num1 = !(obj is bool) ? (short)(conv.ToBoolean((IFormatProvider)null) ? 1 : 0) : (short)((bool)obj ? 1 : 0);
                    break;
        case TypeCode.Byte:
          num1 = !(obj is byte) ? (short) -conv.ToByte((IFormatProvider) null) : (short) -(byte) obj;
          break;
        case TypeCode.Int16:
          int num3 = !(obj is short) ? checked (-conv.ToInt16((IFormatProvider) null)) : checked (-(short) obj);
          if (num3 < (int) short.MinValue || num3 > (int) short.MaxValue)
            return (object) num3;
          return (object) checked ((short) num3);
        case TypeCode.Int32:
          long num4 = !(obj is int) ? checked (-(long) conv.ToInt32((IFormatProvider) null)) : checked (-(long) (int) obj);
          if (num4 < (long) int.MinValue || num4 > (long) int.MaxValue)
            return (object) num4;
          return (object) checked ((int) num4);
        case TypeCode.Int64:
          long num5;
          Decimal num6;
          try
          {
            num5 = !(obj is long) ? checked (-conv.ToInt64((IFormatProvider) null)) : checked (-(long) obj);
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
            num6 = Decimal.Negate(conv.ToDecimal((IFormatProvider) null));
            goto label_29;
          }
          return (object) num5;
label_29:
          return (object) num6;
        case TypeCode.Single:
          if (obj is float)
            return (object) (float) -(double) (float) obj;
          return (object) (float) -(double) conv.ToSingle((IFormatProvider) null);
        case TypeCode.Double:
          num2 = !(obj is double) ? -conv.ToDouble((IFormatProvider) null) : -(double) obj;
          goto label_28;
        case TypeCode.Decimal:
          try
          {
            return (object) (!(obj is Decimal) ? Decimal.Negate(conv.ToDecimal((IFormatProvider) null)) : Decimal.Negate((Decimal) obj));
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
            num2 = -conv.ToDouble((IFormatProvider) null);
            goto label_28;
          }
        case TypeCode.String:
          string str = obj as string;
          num2 = str == null ? -DoubleType.FromString(conv.ToString((IFormatProvider) null)) : -DoubleType.FromString(str);
          goto label_28;
        default:
          throw ObjectType.GetNoValidOperatorException(obj);
      }
      return (object) num1;
label_28:
      return (object) num2;
    }

    /// <summary>Performs a <see langword="Not" /> operation.</summary>
    /// <param name="obj">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>For <see langword="Boolean" /> operations, <see langword="False" /> if <paramref name="obj" /> is <see langword="True" />; otherwise, <see langword="True" />. For bitwise operations, 1 if <paramref name="obj" /> is 0; otherwise, 0.</returns>
    public static object NotObj(object obj)
    {
      if (obj == null)
        return (object) -1;
      IConvertible convertible = obj as IConvertible;
      switch (convertible == null ? TypeCode.Object : convertible.GetTypeCode())
      {
        case TypeCode.Boolean:
          return (object) !convertible.ToBoolean((IFormatProvider) null);
        case TypeCode.Byte:
          Type type1 = obj.GetType();
          byte num1 = convertible.ToByte((IFormatProvider) null);
          if (type1.IsEnum)
            return Enum.ToObject(type1, num1);
          return (object) num1;
        case TypeCode.Int16:
          Type type2 = obj.GetType();
          short num2 = convertible.ToInt16((IFormatProvider) null);
          if (type2.IsEnum)
            return Enum.ToObject(type2, num2);
          return (object) num2;
        case TypeCode.Int32:
          Type type3 = obj.GetType();
          int num3 = ~convertible.ToInt32((IFormatProvider) null);
          if (type3.IsEnum)
            return Enum.ToObject(type3, num3);
          return (object) num3;
        case TypeCode.Int64:
          Type type4 = obj.GetType();
          long num4 = ~convertible.ToInt64((IFormatProvider) null);
          if (type4.IsEnum)
            return Enum.ToObject(type4, num4);
          return (object) num4;
        case TypeCode.Single:
          return (object) ~Convert.ToInt64(convertible.ToDecimal((IFormatProvider) null));
        case TypeCode.Double:
          return (object) ~Convert.ToInt64(convertible.ToDecimal((IFormatProvider) null));
        case TypeCode.Decimal:
          return (object) ~Convert.ToInt64(convertible.ToDecimal((IFormatProvider) null));
        case TypeCode.String:
          return (object) ~LongType.FromString(convertible.ToString((IFormatProvider) null));
        default:
          throw ObjectType.GetNoValidOperatorException(obj);
      }
    }

    /// <summary>Performs a bitwise <see langword="And" /> operation.</summary>
    /// <param name="obj1">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <param name="obj2">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>1 if both <paramref name="obj1" /> and <paramref name="obj2" /> evaluate to 1; otherwise, 0.</returns>
    public static object BitAndObj(object obj1, object obj2)
    {
      if (obj1 == null && obj2 == null)
        return (object) 0;
      Type enumType1 = (Type) null;
      Type enumType2 = (Type) null;
      bool isEnum1=false;
      if (obj1 != null)
      {
        enumType1 = obj1.GetType();
        isEnum1 = enumType1.IsEnum;
      }
      bool isEnum2 = false;
      if (obj2 != null)
      {
        enumType2 = obj2.GetType();
        isEnum2 = enumType2.IsEnum;
      }
      switch (ObjectType.GetWidestType(obj1, obj2, false))
      {
        case TypeCode.Boolean:
          if (enumType1 == enumType2)
            return (object) (BooleanType.FromObject(obj1) & BooleanType.FromObject(obj2));
          return (object) (short) ((int) ShortType.FromObject(obj1) & (int) ShortType.FromObject(obj2));
        case TypeCode.Byte:
          byte num1 = (byte) ((int) ByteType.FromObject(obj1) & (int) ByteType.FromObject(obj2));
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num1;
          if (isEnum1)
            return Enum.ToObject(enumType1, num1);
          if (isEnum2)
            return Enum.ToObject(enumType2, num1);
          break;
        case TypeCode.Int16:
          short num2 = (short) ((int) ShortType.FromObject(obj1) & (int) ShortType.FromObject(obj2));
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num2;
          if (isEnum1)
            return Enum.ToObject(enumType1, num2);
          if (isEnum2)
            return Enum.ToObject(enumType2, num2);
          break;
        case TypeCode.Int32:
          int num3 = IntegerType.FromObject(obj1) & IntegerType.FromObject(obj2);
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num3;
          if (isEnum1)
            return Enum.ToObject(enumType1, num3);
          if (isEnum2)
            return Enum.ToObject(enumType2, num3);
          break;
        case TypeCode.Int64:
          long num4 = LongType.FromObject(obj1) & LongType.FromObject(obj2);
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num4;
          if (isEnum1)
            return Enum.ToObject(enumType1, num4);
          if (isEnum2)
            return Enum.ToObject(enumType2, num4);
          break;
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.String:
          return (object) (LongType.FromObject(obj1) & LongType.FromObject(obj2));
      }
      throw ObjectType.GetNoValidOperatorException(obj1, obj2);
    }

    /// <summary>Performs a bitwise <see langword="Or" /> operation.</summary>
    /// <param name="obj1">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <param name="obj2">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>0 if both <paramref name="obj1" /> and <paramref name="obj2" /> evaluate to 0; otherwise, 1.</returns>
    public static object BitOrObj(object obj1, object obj2)
    {
      if (obj1 == null && obj2 == null)
        return (object) 0;
      Type enumType1 = (Type) null;
      Type enumType2 = (Type) null;
      bool isEnum1 = false;
      if (obj1 != null)
      {
        enumType1 = obj1.GetType();
        isEnum1 = enumType1.IsEnum;
      }
      bool isEnum2=false;
      if (obj2 != null)
      {
        enumType2 = obj2.GetType();
        isEnum2 = enumType2.IsEnum;
      }
      switch (ObjectType.GetWidestType(obj1, obj2, false))
      {
        case TypeCode.Boolean:
          if (enumType1 == enumType2)
            return (object) (BooleanType.FromObject(obj1) | BooleanType.FromObject(obj2));
          return (object) (short) ((int) ShortType.FromObject(obj1) | (int) ShortType.FromObject(obj2));
        case TypeCode.Byte:
          byte num1 = (byte) ((int) ByteType.FromObject(obj1) | (int) ByteType.FromObject(obj2));
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num1;
          if (isEnum1)
            return Enum.ToObject(enumType1, num1);
          if (isEnum2)
            return Enum.ToObject(enumType2, num1);
          break;
        case TypeCode.Int16:
          short num2 = (short) ((int) ShortType.FromObject(obj1) | (int) ShortType.FromObject(obj2));
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num2;
          if (isEnum1)
            return Enum.ToObject(enumType1, num2);
          if (isEnum2)
            return Enum.ToObject(enumType2, num2);
          break;
        case TypeCode.Int32:
          int num3 = IntegerType.FromObject(obj1) | IntegerType.FromObject(obj2);
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num3;
          if (isEnum1)
            return Enum.ToObject(enumType1, num3);
          if (isEnum2)
            return Enum.ToObject(enumType2, num3);
          break;
        case TypeCode.Int64:
          long num4 = LongType.FromObject(obj1) | LongType.FromObject(obj2);
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num4;
          if (isEnum1)
            return Enum.ToObject(enumType1, num4);
          if (isEnum2)
            return Enum.ToObject(enumType2, num4);
          break;
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.String:
          return (object) (LongType.FromObject(obj1) | LongType.FromObject(obj2));
      }
      throw ObjectType.GetNoValidOperatorException(obj1, obj2);
    }

    /// <summary>Performs an <see langword="Xor" /> operation.</summary>
    /// <param name="obj1">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <param name="obj2">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>A numeric value that represents the bitwise exclusion (exclusive bitwise disjunction) of two numeric bit patterns. For more information, see Xor Operator (Visual Basic).</returns>
    public static object BitXorObj(object obj1, object obj2)
    {
      if (obj1 == null && obj2 == null)
        return (object) 0;
      Type enumType1 = (Type) null;
      Type enumType2 = (Type) null;
      bool isEnum1 = false;
      if (obj1 != null)
      {
        enumType1 = obj1.GetType();
        isEnum1 = enumType1.IsEnum;
      }
      bool isEnum2 = false;
      if (obj2 != null)
      {
        enumType2 = obj2.GetType();
        isEnum2 = enumType2.IsEnum;
      }
      switch (ObjectType.GetWidestType(obj1, obj2, false))
      {
        case TypeCode.Boolean:
          if (enumType1 == enumType2)
            return (object) (BooleanType.FromObject(obj1) ^ BooleanType.FromObject(obj2));
          return (object) (short) ((int) ShortType.FromObject(obj1) ^ (int) ShortType.FromObject(obj2));
        case TypeCode.Byte:
          byte num1 = (byte) ((int) ByteType.FromObject(obj1) ^ (int) ByteType.FromObject(obj2));
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num1;
          if (isEnum1)
            return Enum.ToObject(enumType1, num1);
          if (isEnum2)
            return Enum.ToObject(enumType2, num1);
          break;
        case TypeCode.Int16:
          short num2 = (short) ((int) ShortType.FromObject(obj1) ^ (int) ShortType.FromObject(obj2));
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num2;
          if (isEnum1)
            return Enum.ToObject(enumType1, num2);
          if (isEnum2)
            return Enum.ToObject(enumType2, num2);
          break;
        case TypeCode.Int32:
          int num3 = IntegerType.FromObject(obj1) ^ IntegerType.FromObject(obj2);
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num3;
          if (isEnum1)
            return Enum.ToObject(enumType1, num3);
          if (isEnum2)
            return Enum.ToObject(enumType2, num3);
          break;
        case TypeCode.Int64:
          long num4 = LongType.FromObject(obj1) ^ LongType.FromObject(obj2);
          if (isEnum1 && isEnum2 && enumType1 != enumType2 || (!isEnum1 || !isEnum2))
            return (object) num4;
          if (isEnum1)
            return Enum.ToObject(enumType1, num4);
          if (isEnum2)
            return Enum.ToObject(enumType2, num4);
          break;
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.String:
          return (object) (LongType.FromObject(obj1) ^ LongType.FromObject(obj2));
      }
      throw ObjectType.GetNoValidOperatorException(obj1, obj2);
    }

    /// <summary>Performs an addition (+) operation.</summary>
    /// <param name="o1">Required. Any numeric expression.</param>
    /// <param name="o2">Required. Any numeric expression.</param>
    /// <returns>The sum of <paramref name="o1" /> and <paramref name="o2" />.</returns>
    public static object AddObj(object o1, object o2)
    {
      IConvertible convertible1 = o1 as IConvertible;
      TypeCode tc1 = convertible1 != null ? convertible1.GetTypeCode() : (o1 != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = o2 as IConvertible;
      TypeCode tc2 = convertible2 != null ? convertible2.GetTypeCode() : (o2 != null ? TypeCode.Object : TypeCode.Empty);
      if (tc1 == TypeCode.Object && o1 is char[] && (tc2 == TypeCode.String || tc2 == TypeCode.Empty || tc2 == TypeCode.Object && o2 is char[]))
      {
        o1 = (object) new string(CharArrayType.FromObject(o1));
        convertible1 = (IConvertible) o1;
        tc1 = TypeCode.String;
      }
      if (tc2 == TypeCode.Object && o2 is char[] && (tc1 == TypeCode.String || tc1 == TypeCode.Empty))
      {
        o2 = (object) new string(CharArrayType.FromObject(o2));
        convertible2 = (IConvertible) o2;
        tc2 = TypeCode.String;
      }
      switch (checked (unchecked ((int) tc1) * 19) + tc2)
      {
        case TypeCode.Empty:
          return (object) 0;
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return o2;
        case TypeCode.String:
        case (TypeCode) 56:
          return o2;
        case (TypeCode) 57:
        case (TypeCode) 114:
        case (TypeCode) 133:
        case (TypeCode) 171:
        case (TypeCode) 209:
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return o1;
        case (TypeCode) 60:
          return ObjectType.AddInt16(checked ((short) ObjectType.ToVBBool(convertible1)), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return ObjectType.AddInt16(checked ((short) ObjectType.ToVBBool(convertible1)), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 66:
          return ObjectType.AddInt32(ObjectType.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 68:
          return ObjectType.AddInt64((long) ObjectType.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 70:
          return ObjectType.AddSingle((float) ObjectType.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return ObjectType.AddDouble((double) ObjectType.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 72:
          return ObjectType.AddDecimal(ObjectType.ToVBBoolConv(convertible1), convertible2);
        case (TypeCode) 75:
        case (TypeCode) 345:
          return ObjectType.AddString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 80:
        case (TypeCode) 94:
        case (TypeCode) 320:
        case (TypeCode) 322:
        case (TypeCode) 346:
        case (TypeCode) 358:
        case (TypeCode) 360:
          return (object) (StringType.FromObject(o1) + StringType.FromObject(o2));
        case (TypeCode) 117:
        case (TypeCode) 136:
          return ObjectType.AddInt16(convertible1.ToInt16((IFormatProvider) null), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 120:
          return ObjectType.AddByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 121:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return ObjectType.AddInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 123:
        case (TypeCode) 142:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 180:
          return ObjectType.AddInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 125:
        case (TypeCode) 144:
        case (TypeCode) 182:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 218:
        case (TypeCode) 220:
          return ObjectType.AddInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 184:
        case (TypeCode) 222:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 256:
        case (TypeCode) 258:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return ObjectType.AddSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 185:
        case (TypeCode) 223:
        case (TypeCode) 261:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 275:
        case (TypeCode) 277:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return ObjectType.AddDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 129:
        case (TypeCode) 148:
        case (TypeCode) 186:
        case (TypeCode) 224:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 294:
        case (TypeCode) 296:
        case (TypeCode) 300:
          return ObjectType.AddDecimal(convertible1, convertible2);
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 189:
        case (TypeCode) 227:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return ObjectType.AddString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 174:
          return ObjectType.AddInt32(convertible1.ToInt32((IFormatProvider) null), ObjectType.ToVBBool(convertible2));
        case (TypeCode) 212:
          return ObjectType.AddInt64(convertible1.ToInt64((IFormatProvider) null), (long) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 250:
          return ObjectType.AddSingle(convertible1.ToSingle((IFormatProvider) null), (float) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 269:
          return ObjectType.AddDouble(convertible1.ToDouble((IFormatProvider) null), (double) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 288:
          return ObjectType.AddDecimal(convertible1, ObjectType.ToVBBoolConv(convertible2));
        case (TypeCode) 342:
        case (TypeCode) 344:
          return o1;
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 351:
        case (TypeCode) 353:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return ObjectType.AddString(convertible1, tc1, convertible2, tc2);
        default:
          throw ObjectType.GetNoValidOperatorException(o1, o2);
      }
    }

    private static object AddString(IConvertible conv1, TypeCode tc1, IConvertible conv2, TypeCode tc2)
    {
      double num1;
      switch (tc1)
      {
        case TypeCode.Boolean:
          num1 = (double) ObjectType.ToVBBool(conv1);
          break;
        case TypeCode.String:
          num1 = DoubleType.FromString(conv1.ToString((IFormatProvider) null));
          break;
        default:
          num1 = conv1.ToDouble((IFormatProvider) null);
          break;
      }
      double num2;
      switch (tc2)
      {
        case TypeCode.Boolean:
          num2 = (double) ObjectType.ToVBBool(conv2);
          break;
        case TypeCode.String:
          num2 = DoubleType.FromString(conv2.ToString((IFormatProvider) null));
          break;
        default:
          num2 = conv2.ToDouble((IFormatProvider) null);
          break;
      }
      return (object) (num1 + num2);
    }

    private static object AddByte(byte i1, byte i2)
    {
      short num = checked ((short) unchecked ((int) i1 + (int) i2));
      if (num >= (short) 0 && num <= (short) byte.MaxValue)
        return (object) checked ((byte) num);
      return (object) num;
    }

    private static object AddInt16(short i1, short i2)
    {
      int num = checked ((int) i1 + (int) i2);
      if (num >= (int) short.MinValue && num <= (int) short.MaxValue)
        return (object) checked ((short) num);
      return (object) num;
    }

    private static object AddInt32(int i1, int i2)
    {
      long num = checked ((long) i1 + (long) i2);
      if (num >= (long) int.MinValue && num <= (long) int.MaxValue)
        return (object) checked ((int) num);
      return (object) num;
    }

    private static object AddInt64(long i1, long i2)
    {
      try
      {
        return (object) checked (i1 + i2);
      }
      catch (OverflowException ex)
      {
        return (object) Decimal.Add(new Decimal(i1), new Decimal(i2));
      }
    }

    private static object AddSingle(float f1, float f2)
    {
      double d = (double) f1 + (double) f2;
      if (d <= 3.40282346638529E+38 && d >= -3.40282346638529E+38)
        return (object) (float) d;
      if (double.IsInfinity(d) && (float.IsInfinity(f1) || float.IsInfinity(f2)))
        return (object) (float) d;
      return (object) d;
    }

    private static object AddDouble(double d1, double d2)
    {
      return (object) (d1 + d2);
    }

    private static object AddDecimal(IConvertible conv1, IConvertible conv2)
    {
      Decimal d1=0;
      if (conv1 != null)
        d1 = conv1.ToDecimal((IFormatProvider) null);
      Decimal d2 = conv2.ToDecimal((IFormatProvider) null);
      try
      {
        return (object) Decimal.Add(d1, d2);
      }
      catch (OverflowException ex)
      {
        return (object) (Convert.ToDouble(d1) + Convert.ToDouble(d2));
      }
    }

    private static int ToVBBool(IConvertible conv)
    {
      return conv.ToBoolean((IFormatProvider) null) ? -1 : 0;
    }

    private static IConvertible ToVBBoolConv(IConvertible conv)
    {
      if (conv.ToBoolean((IFormatProvider) null))
        return (IConvertible) (-1);
      return (IConvertible) 0;
    }

    /// <summary>Performs a subtraction (–) operation.</summary>
    /// <param name="o1">Required. Any numeric expression. </param>
    /// <param name="o2">Required. Any numeric expression.</param>
    /// <returns>The difference between <paramref name="o1" /> and <paramref name="o2" />.</returns>
    public static object SubObj(object o1, object o2)
    {
      IConvertible convertible1 = o1 as IConvertible;
      TypeCode tc1 = convertible1 != null ? convertible1.GetTypeCode() : (o1 != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = o2 as IConvertible;
      TypeCode typeCode = convertible2 != null ? convertible2.GetTypeCode() : (o2 != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) tc1) * 19) + typeCode)
      {
        case TypeCode.Empty:
          return (object) 0;
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return ObjectType.InternalNegObj(o2, convertible2, typeCode);
        case TypeCode.String:
          return ObjectType.SubStringString((string) null, convertible2.ToString((IFormatProvider) null));
        case (TypeCode) 57:
        case (TypeCode) 114:
        case (TypeCode) 133:
        case (TypeCode) 171:
        case (TypeCode) 209:
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return o1;
        case (TypeCode) 60:
          return ObjectType.SubInt16(checked ((short) ObjectType.ToVBBool(convertible1)), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return ObjectType.SubInt16(checked ((short) ObjectType.ToVBBool(convertible1)), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 66:
          return ObjectType.SubInt32(ObjectType.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 68:
          return ObjectType.SubInt64((long) ObjectType.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 70:
          return ObjectType.SubSingle((float) ObjectType.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return ObjectType.SubDouble((double) ObjectType.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 72:
          return ObjectType.SubDecimal(ObjectType.ToVBBoolConv(convertible1), convertible2);
        case (TypeCode) 75:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 189:
        case (TypeCode) 227:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
        case (TypeCode) 345:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 351:
        case (TypeCode) 353:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return ObjectType.SubString(convertible1, tc1, convertible2, typeCode);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return ObjectType.SubInt16(convertible1.ToInt16((IFormatProvider) null), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 120:
          return ObjectType.SubByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 121:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return ObjectType.SubInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 123:
        case (TypeCode) 142:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 180:
          return ObjectType.SubInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 125:
        case (TypeCode) 144:
        case (TypeCode) 182:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 218:
        case (TypeCode) 220:
          return ObjectType.SubInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 184:
        case (TypeCode) 222:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 256:
        case (TypeCode) 258:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return ObjectType.SubSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 185:
        case (TypeCode) 223:
        case (TypeCode) 261:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 275:
        case (TypeCode) 277:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return ObjectType.SubDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 129:
        case (TypeCode) 148:
        case (TypeCode) 186:
        case (TypeCode) 224:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 294:
        case (TypeCode) 296:
        case (TypeCode) 300:
          return ObjectType.SubDecimal(convertible1, convertible2);
        case (TypeCode) 174:
          return ObjectType.SubInt32(convertible1.ToInt32((IFormatProvider) null), ObjectType.ToVBBool(convertible2));
        case (TypeCode) 212:
          return ObjectType.SubInt64(convertible1.ToInt64((IFormatProvider) null), (long) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 250:
          return ObjectType.SubSingle(convertible1.ToSingle((IFormatProvider) null), (float) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 269:
          return ObjectType.SubDouble(convertible1.ToDouble((IFormatProvider) null), (double) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 288:
          return ObjectType.SubDecimal(convertible1, ObjectType.ToVBBoolConv(convertible2));
        case (TypeCode) 342:
          return ObjectType.SubStringString(convertible1.ToString((IFormatProvider) null), (string) null);
        case (TypeCode) 360:
          return ObjectType.SubStringString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null));
        default:
          throw ObjectType.GetNoValidOperatorException(o1, o2);
      }
    }

    private static object SubString(IConvertible conv1, TypeCode tc1, IConvertible conv2, TypeCode tc2)
    {
      double num1;
      switch (tc1)
      {
        case TypeCode.Boolean:
          num1 = (double) ObjectType.ToVBBool(conv1);
          break;
        case TypeCode.String:
          num1 = DoubleType.FromString(conv1.ToString((IFormatProvider) null));
          break;
        default:
          num1 = conv1.ToDouble((IFormatProvider) null);
          break;
      }
      double num2;
      switch (tc2)
      {
        case TypeCode.Boolean:
          num2 = (double) ObjectType.ToVBBool(conv2);
          break;
        case TypeCode.String:
          num2 = DoubleType.FromString(conv2.ToString((IFormatProvider) null));
          break;
        default:
          num2 = conv2.ToDouble((IFormatProvider) null);
          break;
      }
      return (object) (num1 - num2);
    }

    private static object SubStringString(string s1, string s2)
    {
      double num1=0;
      if (s1 != null)
        num1 = DoubleType.FromString(s1);
      double num2 = 0;
      if (s2 != null)
        num2 = DoubleType.FromString(s2);
      return (object) (num1 - num2);
    }

    private static object SubByte(byte i1, byte i2)
    {
      short num = checked ((short) unchecked ((int) i1 - (int) i2));
      if (num >= (short) 0 && num <= (short) byte.MaxValue)
        return (object) checked ((byte) num);
      return (object) num;
    }

    private static object SubInt16(short i1, short i2)
    {
      int num = checked ((int) i1 - (int) i2);
      if (num >= (int) short.MinValue && num <= (int) short.MaxValue)
        return (object) checked ((short) num);
      return (object) num;
    }

    private static object SubInt32(int i1, int i2)
    {
      long num = checked ((long) i1 - (long) i2);
      if (num >= (long) int.MinValue && num <= (long) int.MaxValue)
        return (object) checked ((int) num);
      return (object) num;
    }

    private static object SubInt64(long i1, long i2)
    {
      try
      {
        return (object) checked (i1 - i2);
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
        return (object) Decimal.Subtract(new Decimal(i1), new Decimal(i2));
      }
    }

    private static object SubSingle(float f1, float f2)
    {
      double d = (double) f1 - (double) f2;
      if (d <= 3.40282346638529E+38 && d >= -3.40282346638529E+38)
        return (object) (float) d;
      if (double.IsInfinity(d) && (float.IsInfinity(f1) || float.IsInfinity(f2)))
        return (object) (float) d;
      return (object) d;
    }

    private static object SubDouble(double d1, double d2)
    {
      return (object) (d1 - d2);
    }

    private static object SubDecimal(IConvertible conv1, IConvertible conv2)
    {
      Decimal d1 = conv1.ToDecimal((IFormatProvider) null);
      Decimal d2 = conv2.ToDecimal((IFormatProvider) null);
      try
      {
        return (object) Decimal.Subtract(d1, d2);
      }
      catch (OverflowException ex)
      {
        return (object) (Convert.ToDouble(d1) - Convert.ToDouble(d2));
      }
    }

    /// <summary>Performs a multiplication (*) operation.</summary>
    /// <param name="o1">Required. Any numeric expression.</param>
    /// <param name="o2">Required. Any numeric expression.</param>
    /// <returns>The product of <paramref name="o1" /> and <paramref name="o2" />.</returns>
    public static object MulObj(object o1, object o2)
    {
      IConvertible convertible1 = o1 as IConvertible;
      TypeCode tc1 = convertible1 != null ? convertible1.GetTypeCode() : (o1 != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = o2 as IConvertible;
      TypeCode tc2 = convertible2 != null ? convertible2.GetTypeCode() : (o2 != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) tc1) * 19) + tc2)
      {
        case TypeCode.Empty:
        case TypeCode.Int32:
        case (TypeCode) 171:
          return (object) 0;
        case TypeCode.Boolean:
        case TypeCode.Int16:
        case (TypeCode) 57:
        case (TypeCode) 133:
          return (object) (short) 0;
        case TypeCode.Byte:
        case (TypeCode) 114:
          return (object) (byte) 0;
        case TypeCode.Int64:
        case (TypeCode) 209:
          return (object) 0L;
        case TypeCode.Single:
        case (TypeCode) 247:
          return (object) 0.0f;
        case TypeCode.Double:
        case (TypeCode) 266:
          return (object) 0.0;
        case TypeCode.Decimal:
        case (TypeCode) 285:
          return (object) Decimal.Zero;
        case TypeCode.String:
        case (TypeCode) 342:
          return (object) 0.0;
        case (TypeCode) 60:
          return ObjectType.MulInt16(checked ((short) ObjectType.ToVBBool(convertible1)), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return ObjectType.MulInt16(checked ((short) ObjectType.ToVBBool(convertible1)), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 66:
          return ObjectType.MulInt32(ObjectType.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 68:
          return ObjectType.MulInt64((long) ObjectType.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 70:
          return ObjectType.MulSingle((float) ObjectType.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return ObjectType.MulDouble((double) ObjectType.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 72:
          return ObjectType.MulDecimal(ObjectType.ToVBBoolConv(convertible1), convertible2);
        case (TypeCode) 75:
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 189:
        case (TypeCode) 227:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
        case (TypeCode) 345:
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 351:
        case (TypeCode) 353:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return ObjectType.MulString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return ObjectType.MulInt16(convertible1.ToInt16((IFormatProvider) null), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 120:
          return ObjectType.MulByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 121:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return ObjectType.MulInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 123:
        case (TypeCode) 142:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 180:
          return ObjectType.MulInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 125:
        case (TypeCode) 144:
        case (TypeCode) 182:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 218:
        case (TypeCode) 220:
          return ObjectType.MulInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 184:
        case (TypeCode) 222:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 256:
        case (TypeCode) 258:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return ObjectType.MulSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 185:
        case (TypeCode) 223:
        case (TypeCode) 261:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 275:
        case (TypeCode) 277:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return ObjectType.MulDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 129:
        case (TypeCode) 148:
        case (TypeCode) 186:
        case (TypeCode) 224:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 294:
        case (TypeCode) 296:
        case (TypeCode) 300:
          return ObjectType.MulDecimal(convertible1, convertible2);
        case (TypeCode) 174:
          return ObjectType.MulInt32(convertible1.ToInt32((IFormatProvider) null), ObjectType.ToVBBool(convertible2));
        case (TypeCode) 212:
          return ObjectType.MulInt64(convertible1.ToInt64((IFormatProvider) null), (long) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 250:
          return ObjectType.MulSingle(convertible1.ToSingle((IFormatProvider) null), (float) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 269:
          return ObjectType.MulDouble(convertible1.ToDouble((IFormatProvider) null), (double) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 288:
          return ObjectType.MulDecimal(convertible1, ObjectType.ToVBBoolConv(convertible2));
        case (TypeCode) 360:
          return ObjectType.MulStringString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null));
        default:
          throw ObjectType.GetNoValidOperatorException(o1, o2);
      }
    }

    private static object MulString(IConvertible conv1, TypeCode tc1, IConvertible conv2, TypeCode tc2)
    {
      double num1;
      switch (tc1)
      {
        case TypeCode.Boolean:
          num1 = (double) ObjectType.ToVBBool(conv1);
          break;
        case TypeCode.String:
          num1 = DoubleType.FromString(conv1.ToString((IFormatProvider) null));
          break;
        default:
          num1 = conv1.ToDouble((IFormatProvider) null);
          break;
      }
      double num2;
      switch (tc2)
      {
        case TypeCode.Boolean:
          num2 = (double) ObjectType.ToVBBool(conv2);
          break;
        case TypeCode.String:
          num2 = DoubleType.FromString(conv2.ToString((IFormatProvider) null));
          break;
        default:
          num2 = conv2.ToDouble((IFormatProvider) null);
          break;
      }
      return (object) (num1 * num2);
    }

    private static object MulStringString(string s1, string s2)
    {
      double num1 = 0;
      if (s1 != null)
        num1 = DoubleType.FromString(s1);
      double num2 = 0;
      if (s2 != null)
        num2 = DoubleType.FromString(s2);
      return (object) (num1 * num2);
    }

    private static object MulByte(byte i1, byte i2)
    {
      int num = checked ((int) i1 * (int) i2);
      if (num >= 0 && num <= (int) byte.MaxValue)
        return (object) checked ((byte) num);
      if (num >= (int) short.MinValue && num <= (int) short.MaxValue)
        return (object) checked ((short) num);
      return (object) num;
    }

    private static object MulInt16(short i1, short i2)
    {
      int num = checked ((int) i1 * (int) i2);
      if (num >= (int) short.MinValue && num <= (int) short.MaxValue)
        return (object) checked ((short) num);
      return (object) num;
    }

    private static object MulInt32(int i1, int i2)
    {
      long num = checked ((long) i1 * (long) i2);
      if (num >= (long) int.MinValue && num <= (long) int.MaxValue)
        return (object) checked ((int) num);
      return (object) num;
    }

    private static object MulInt64(long i1, long i2)
    {
      try
      {
        return (object) checked (i1 * i2);
      }
      catch (OverflowException ex1)
      {
        try
        {
          return (object) Decimal.Multiply(new Decimal(i1), new Decimal(i2));
        }
        catch (OverflowException ex2)
        {
          return (object) ((double) i1 * (double) i2);
        }
      }
    }

    private static object MulSingle(float f1, float f2)
    {
      double d = (double) f1 * (double) f2;
      if (d <= 3.40282346638529E+38 && d >= -3.40282346638529E+38)
        return (object) (float) d;
      if (double.IsInfinity(d) && (float.IsInfinity(f1) || float.IsInfinity(f2)))
        return (object) (float) d;
      return (object) d;
    }

    private static object MulDouble(double d1, double d2)
    {
      return (object) (d1 * d2);
    }

    private static object MulDecimal(IConvertible conv1, IConvertible conv2)
    {
      Decimal d1 = conv1.ToDecimal((IFormatProvider) null);
      Decimal d2 = conv2.ToDecimal((IFormatProvider) null);
      try
      {
        return (object) Decimal.Multiply(d1, d2);
      }
      catch (OverflowException ex)
      {
        return (object) (Convert.ToDouble(d1) * Convert.ToDouble(d2));
      }
    }

    /// <summary>Performs a division (/) operation.</summary>
    /// <param name="o1">Required. Any numeric expression.</param>
    /// <param name="o2">Required. Any numeric expression.</param>
    /// <returns>The full quotient of <paramref name="o1" /> divided by <paramref name="o2" />, including any remainder.</returns>
    public static object DivObj(object o1, object o2)
    {
      IConvertible convertible1 = o1 as IConvertible;
      TypeCode tc1 = convertible1 != null ? convertible1.GetTypeCode() : (o1 != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = o2 as IConvertible;
      TypeCode tc2 = convertible2 != null ? convertible2.GetTypeCode() : (o2 != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) tc1) * 19) + tc2)
      {
        case TypeCode.Empty:
          return ObjectType.DivDouble(0.0, 0.0);
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return ObjectType.DivDouble(0.0, convertible2.ToDouble((IFormatProvider) null));
        case TypeCode.String:
          return ObjectType.DivString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 57:
          return ObjectType.DivDouble((double) ObjectType.ToVBBool(convertible1), 0.0);
        case (TypeCode) 60:
          return ObjectType.DivDouble((double) ObjectType.ToVBBool(convertible1), (double) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 63:
        case (TypeCode) 64:
        case (TypeCode) 66:
        case (TypeCode) 68:
        case (TypeCode) 71:
          return ObjectType.DivDouble((double) ObjectType.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 70:
          return ObjectType.DivSingle((float) ObjectType.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 72:
          return ObjectType.DivDecimal(ObjectType.ToVBBoolConv(convertible1), (IConvertible) convertible2.ToDecimal((IFormatProvider) null));
        case (TypeCode) 75:
          return ObjectType.DivString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 114:
        case (TypeCode) 133:
        case (TypeCode) 171:
        case (TypeCode) 209:
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return ObjectType.DivDouble(convertible1.ToDouble((IFormatProvider) null), 0.0);
        case (TypeCode) 117:
        case (TypeCode) 136:
        case (TypeCode) 174:
        case (TypeCode) 212:
        case (TypeCode) 269:
          return ObjectType.DivDouble(convertible1.ToDouble((IFormatProvider) null), (double) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 120:
        case (TypeCode) 121:
        case (TypeCode) 123:
        case (TypeCode) 125:
        case (TypeCode) 128:
        case (TypeCode) 139:
        case (TypeCode) 140:
        case (TypeCode) 142:
        case (TypeCode) 144:
        case (TypeCode) 147:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 180:
        case (TypeCode) 182:
        case (TypeCode) 185:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 218:
        case (TypeCode) 220:
        case (TypeCode) 223:
        case (TypeCode) 261:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 275:
        case (TypeCode) 277:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return ObjectType.DivDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 184:
        case (TypeCode) 222:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 256:
        case (TypeCode) 258:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return ObjectType.DivSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 129:
        case (TypeCode) 148:
        case (TypeCode) 186:
        case (TypeCode) 224:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 294:
        case (TypeCode) 296:
        case (TypeCode) 300:
          return ObjectType.DivDecimal(convertible1, convertible2);
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 189:
        case (TypeCode) 227:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return ObjectType.DivString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 250:
          return ObjectType.DivSingle(convertible1.ToSingle((IFormatProvider) null), (float) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 288:
          return ObjectType.DivDecimal(convertible1, ObjectType.ToVBBoolConv(convertible2));
        case (TypeCode) 342:
          return ObjectType.DivString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 345:
          return ObjectType.DivString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 351:
        case (TypeCode) 353:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return ObjectType.DivString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 360:
          return ObjectType.DivStringString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null));
        default:
          throw ObjectType.GetNoValidOperatorException(o1, o2);
      }
    }

    private static object DivString(IConvertible conv1, TypeCode tc1, IConvertible conv2, TypeCode tc2)
    {
      double num1;
      switch (tc1)
      {
        case TypeCode.Boolean:
          num1 = (double) ObjectType.ToVBBool(conv1);
          break;
        case TypeCode.String:
          num1 = DoubleType.FromString(conv1.ToString((IFormatProvider) null));
          break;
        default:
          num1 = conv1.ToDouble((IFormatProvider) null);
          break;
      }
      double num2;
      switch (tc2)
      {
        case TypeCode.Boolean:
          num2 = (double) ObjectType.ToVBBool(conv2);
          break;
        case TypeCode.String:
          num2 = DoubleType.FromString(conv2.ToString((IFormatProvider) null));
          break;
        default:
          num2 = conv2.ToDouble((IFormatProvider) null);
          break;
      }
      return (object) (num1 / num2);
    }

    private static object DivStringString(string s1, string s2)
    {
      double num1 = 0;
      if (s1 != null)
        num1 = DoubleType.FromString(s1);
      double num2 = 0;
      if (s2 != null)
        num2 = DoubleType.FromString(s2);
      return (object) (num1 / num2);
    }

    private static object DivDouble(double d1, double d2)
    {
      return (object) (d1 / d2);
    }

    private static object DivSingle(float sng1, float sng2)
    {
      float f = sng1 / sng2;
      if (!float.IsInfinity(f))
        return (object) f;
      if (float.IsInfinity(sng1) || float.IsInfinity(sng2))
        return (object) f;
      return (object) ((double) sng1 / (double) sng2);
    }

    private static object DivDecimal(IConvertible conv1, IConvertible conv2)
    {
      Decimal d1 = 0;
      if (conv1 != null)
        d1 = conv1.ToDecimal((IFormatProvider) null);
      Decimal d2 = 0;
      if (conv2 != null)
        d2 = conv2.ToDecimal((IFormatProvider) null);
      try
      {
        return (object) Decimal.Divide(d1, d2);
      }
      catch (OverflowException ex)
      {
        return (object) (float) ((double) Convert.ToSingle(d1) / (double) Convert.ToSingle(d2));
      }
    }

    /// <summary>Performs an exponent (^) operation.</summary>
    /// <param name="obj1">Required. Any numeric expression.</param>
    /// <param name="obj2">Required. Any numeric expression.</param>
    /// <returns>The result of <paramref name="obj1" /> raised to the power of <paramref name="obj2" />.</returns>
    public static object PowObj(object obj1, object obj2)
    {
      if (obj1 == null && obj2 == null)
        return (object) 1.0;
      switch (ObjectType.GetWidestType(obj1, obj2, false))
      {
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.String:
          return (object) Math.Pow(DoubleType.FromObject(obj1), DoubleType.FromObject(obj2));
        default:
          throw ObjectType.GetNoValidOperatorException(obj1, obj2);
      }
    }

    /// <summary>Performs a modulus (<see langword="Mod" />) operation.</summary>
    /// <param name="o1">Required. Any numeric expression.</param>
    /// <param name="o2">Required. Any numeric expression.</param>
    /// <returns>The remainder after <paramref name="o1" /> is divided by <paramref name="o2" />. </returns>
    public static object ModObj(object o1, object o2)
    {
      IConvertible convertible1 = o1 as IConvertible;
      IConvertible convertible2 = o2 as IConvertible;
      TypeCode tc1 = convertible1 == null ? (o1 != null ? TypeCode.Object : TypeCode.Empty) : convertible1.GetTypeCode();
      TypeCode tc2;
      if (convertible2 != null)
      {
        tc2 = convertible2.GetTypeCode();
      }
      else
      {
        convertible2 = (IConvertible) null;
        tc2 = o2 != null ? TypeCode.Object : TypeCode.Empty;
      }
      switch (checked (unchecked ((int) tc1) * 19) + tc2)
      {
        case TypeCode.Empty:
          return ObjectType.ModInt32(0, 0);
        case TypeCode.Boolean:
          return ObjectType.ModInt16((short) 0, checked ((short) ObjectType.ToVBBool(convertible2)));
        case TypeCode.Byte:
          return ObjectType.ModByte((byte) 0, convertible2.ToByte((IFormatProvider) null));
        case TypeCode.Int16:
          return ObjectType.ModInt16((short) 0, checked ((short) ObjectType.ToVBBool(convertible2)));
        case TypeCode.Int32:
          return ObjectType.ModInt32(0, convertible2.ToInt32((IFormatProvider) null));
        case TypeCode.Int64:
          return ObjectType.ModInt64(0L, convertible2.ToInt64((IFormatProvider) null));
        case TypeCode.Single:
          return ObjectType.ModSingle(0.0f, convertible2.ToSingle((IFormatProvider) null));
        case TypeCode.Double:
          return ObjectType.ModDouble(0.0, convertible2.ToDouble((IFormatProvider) null));
        case TypeCode.Decimal:
          return ObjectType.ModDecimal((IConvertible) null, convertible2);
        case TypeCode.String:
          return ObjectType.ModString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 57:
          return ObjectType.ModInt16(checked ((short) ObjectType.ToVBBool(convertible1)), (short) 0);
        case (TypeCode) 60:
          return ObjectType.ModInt16(checked ((short) ObjectType.ToVBBool(convertible1)), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return ObjectType.ModInt16(checked ((short) ObjectType.ToVBBool(convertible1)), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 66:
          return ObjectType.ModInt32(ObjectType.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 68:
          return ObjectType.ModInt64((long) ObjectType.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 70:
          return ObjectType.ModSingle((float) ObjectType.ToVBBool(convertible1), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 71:
          return ObjectType.ModDouble((double) ObjectType.ToVBBool(convertible1), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 72:
          return ObjectType.ModDecimal(ObjectType.ToVBBoolConv(convertible1), convertible2);
        case (TypeCode) 75:
          return ObjectType.ModString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 114:
          return ObjectType.ModByte(convertible1.ToByte((IFormatProvider) null), (byte) 0);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return ObjectType.ModInt16(convertible1.ToInt16((IFormatProvider) null), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 120:
          return ObjectType.ModByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 121:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return ObjectType.ModInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 123:
        case (TypeCode) 142:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 180:
          return ObjectType.ModInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 125:
        case (TypeCode) 144:
        case (TypeCode) 182:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 218:
        case (TypeCode) 220:
          return ObjectType.ModInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 127:
        case (TypeCode) 146:
        case (TypeCode) 184:
        case (TypeCode) 222:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 256:
        case (TypeCode) 258:
        case (TypeCode) 260:
        case (TypeCode) 262:
        case (TypeCode) 298:
          return ObjectType.ModSingle(convertible1.ToSingle((IFormatProvider) null), convertible2.ToSingle((IFormatProvider) null));
        case (TypeCode) 128:
        case (TypeCode) 147:
        case (TypeCode) 185:
        case (TypeCode) 223:
        case (TypeCode) 261:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 275:
        case (TypeCode) 277:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 299:
          return ObjectType.ModDouble(convertible1.ToDouble((IFormatProvider) null), convertible2.ToDouble((IFormatProvider) null));
        case (TypeCode) 129:
        case (TypeCode) 148:
        case (TypeCode) 186:
        case (TypeCode) 224:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 294:
        case (TypeCode) 296:
        case (TypeCode) 300:
          return ObjectType.ModDecimal(convertible1, convertible2);
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 189:
        case (TypeCode) 227:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return ObjectType.ModString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 133:
          return ObjectType.ModInt16(convertible1.ToInt16((IFormatProvider) null), (short) 0);
        case (TypeCode) 171:
          return ObjectType.ModInt32(convertible1.ToInt32((IFormatProvider) null), 0);
        case (TypeCode) 174:
          return ObjectType.ModInt32(convertible1.ToInt32((IFormatProvider) null), ObjectType.ToVBBool(convertible2));
        case (TypeCode) 209:
          return ObjectType.ModInt64(convertible1.ToInt64((IFormatProvider) null), 0L);
        case (TypeCode) 212:
          return ObjectType.ModInt64(convertible1.ToInt64((IFormatProvider) null), (long) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 247:
          return ObjectType.ModSingle(convertible1.ToSingle((IFormatProvider) null), 0.0f);
        case (TypeCode) 250:
          return ObjectType.ModSingle(convertible1.ToSingle((IFormatProvider) null), (float) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 266:
          return ObjectType.ModDouble(convertible1.ToDouble((IFormatProvider) null), 0.0);
        case (TypeCode) 269:
          return ObjectType.ModDouble(convertible1.ToDouble((IFormatProvider) null), (double) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 285:
          return ObjectType.ModDecimal(convertible1, (IConvertible) null);
        case (TypeCode) 288:
          return ObjectType.ModDecimal(convertible1, ObjectType.ToVBBoolConv(convertible2));
        case (TypeCode) 342:
          return ObjectType.ModString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 345:
          return ObjectType.ModString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 351:
        case (TypeCode) 353:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return ObjectType.ModString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 360:
          return ObjectType.ModStringString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null));
        default:
          throw ObjectType.GetNoValidOperatorException(o1, o2);
      }
    }

    private static object ModString(IConvertible conv1, TypeCode tc1, IConvertible conv2, TypeCode tc2)
    {
      double num1;
      switch (tc1)
      {
        case TypeCode.Boolean:
          num1 = (double) ObjectType.ToVBBool(conv1);
          break;
        case TypeCode.String:
          num1 = DoubleType.FromString(conv1.ToString((IFormatProvider) null));
          break;
        default:
          num1 = conv1.ToDouble((IFormatProvider) null);
          break;
      }
      double num2;
      switch (tc2)
      {
        case TypeCode.Boolean:
          num2 = (double) ObjectType.ToVBBool(conv2);
          break;
        case TypeCode.String:
          num2 = DoubleType.FromString(conv2.ToString((IFormatProvider) null));
          break;
        default:
          num2 = conv2.ToDouble((IFormatProvider) null);
          break;
      }
      return (object) (num1 % num2);
    }

    private static object ModStringString(string s1, string s2)
    {
      double num1 = 0;
      if (s1 != null)
        num1 = DoubleType.FromString(s1);
      double num2 = 0;
      if (s2 != null)
        num2 = DoubleType.FromString(s2);
      return (object) (num1 % num2);
    }

    private static object ModByte(byte i1, byte i2)
    {
      return (object) checked ((byte) unchecked ((uint) i1 % (uint) i2));
    }

    private static object ModInt16(short i1, short i2)
    {
      int num = (int) i1 % (int) i2;
      if (num < (int) short.MinValue || num > (int) short.MaxValue)
        return (object) num;
      return (object) checked ((short) num);
    }

    private static object ModInt32(int i1, int i2)
    {
      long num = (long) i1 % (long) i2;
      if (num < (long) int.MinValue || num > (long) int.MaxValue)
        return (object) num;
      return (object) checked ((int) num);
    }

    private static object ModInt64(long i1, long i2)
    {
      try
      {
        return (object) (i1 % i2);
      }
      catch (OverflowException ex)
      {
        Decimal d1 = Decimal.Remainder(new Decimal(i1), new Decimal(i2));
        if (Decimal.Compare(d1, new Decimal(0, int.MinValue, 0, true, (byte) 0)) < 0 || Decimal.Compare(d1, new Decimal(-1, int.MaxValue, 0, false, (byte) 0)) > 0)
          return (object) d1;
        return (object) Convert.ToInt64(d1);
      }
    }

    private static object ModSingle(float sng1, float sng2)
    {
      return (object) (float) ((double) sng1 % (double) sng2);
    }

    private static object ModDouble(double d1, double d2)
    {
      return (object) (d1 % d2);
    }

    private static object ModDecimal(IConvertible conv1, IConvertible conv2)
    {
      Decimal d1 = 0;
      if (conv1 != null)
        d1 = conv1.ToDecimal((IFormatProvider) null);
      Decimal d2 = 0;
      if (conv2 != null)
        d2 = conv2.ToDecimal((IFormatProvider) null);
      return (object) Decimal.Remainder(d1, d2);
    }

    /// <summary>Performs an integer division (\) operation.</summary>
    /// <param name="o1">Required. Any numeric expression.</param>
    /// <param name="o2">Required. Any numeric expression.</param>
    /// <returns>The integer quotient of <paramref name="o1" /> divided by <paramref name="o2" />, which discards any remainder and retains only the integer portion.</returns>
    public static object IDivObj(object o1, object o2)
    {
      IConvertible convertible1 = o1 as IConvertible;
      TypeCode tc1 = convertible1 != null ? convertible1.GetTypeCode() : (o1 != null ? TypeCode.Object : TypeCode.Empty);
      IConvertible convertible2 = o2 as IConvertible;
      TypeCode tc2 = convertible2 != null ? convertible2.GetTypeCode() : (o2 != null ? TypeCode.Object : TypeCode.Empty);
      switch (checked (unchecked ((int) tc1) * 19) + tc2)
      {
        case TypeCode.Empty:
          return ObjectType.IDivideInt32(0, 0);
        case TypeCode.Boolean:
          return ObjectType.IDivideInt64(0L, (long) ObjectType.ToVBBool(convertible2));
        case TypeCode.Byte:
          return ObjectType.IDivideByte((byte) 0, convertible2.ToByte((IFormatProvider) null));
        case TypeCode.Int16:
          return ObjectType.IDivideInt16((short) 0, convertible2.ToInt16((IFormatProvider) null));
        case TypeCode.Int32:
          return ObjectType.IDivideInt32(0, convertible2.ToInt32((IFormatProvider) null));
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return ObjectType.IDivideInt64(0L, convertible2.ToInt64((IFormatProvider) null));
        case TypeCode.String:
          return ObjectType.IDivideInt64(0L, LongType.FromString(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 57:
          return ObjectType.IDivideInt16(checked ((short) ObjectType.ToVBBool(convertible1)), (short) 0);
        case (TypeCode) 60:
          return ObjectType.IDivideInt16(checked ((short) ObjectType.ToVBBool(convertible1)), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 63:
        case (TypeCode) 64:
          return ObjectType.IDivideInt16(checked ((short) ObjectType.ToVBBool(convertible1)), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 66:
          return ObjectType.IDivideInt32(ObjectType.ToVBBool(convertible1), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 68:
        case (TypeCode) 70:
        case (TypeCode) 71:
        case (TypeCode) 72:
          return ObjectType.IDivideInt64((long) ObjectType.ToVBBool(convertible1), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 75:
          return ObjectType.IDivideInt64((long) ObjectType.ToVBBool(convertible1), LongType.FromString(convertible2.ToString((IFormatProvider) null)));
        case (TypeCode) 114:
          return ObjectType.IDivideByte(convertible1.ToByte((IFormatProvider) null), (byte) 0);
        case (TypeCode) 117:
        case (TypeCode) 136:
          return ObjectType.IDivideInt16(convertible1.ToInt16((IFormatProvider) null), checked ((short) ObjectType.ToVBBool(convertible2)));
        case (TypeCode) 120:
          return ObjectType.IDivideByte(convertible1.ToByte((IFormatProvider) null), convertible2.ToByte((IFormatProvider) null));
        case (TypeCode) 121:
        case (TypeCode) 139:
        case (TypeCode) 140:
          return ObjectType.IDivideInt16(convertible1.ToInt16((IFormatProvider) null), convertible2.ToInt16((IFormatProvider) null));
        case (TypeCode) 123:
        case (TypeCode) 142:
        case (TypeCode) 177:
        case (TypeCode) 178:
        case (TypeCode) 180:
          return ObjectType.IDivideInt32(convertible1.ToInt32((IFormatProvider) null), convertible2.ToInt32((IFormatProvider) null));
        case (TypeCode) 125:
        case (TypeCode) 127:
        case (TypeCode) 128:
        case (TypeCode) 129:
        case (TypeCode) 144:
        case (TypeCode) 146:
        case (TypeCode) 147:
        case (TypeCode) 148:
        case (TypeCode) 182:
        case (TypeCode) 184:
        case (TypeCode) 185:
        case (TypeCode) 186:
        case (TypeCode) 215:
        case (TypeCode) 216:
        case (TypeCode) 218:
        case (TypeCode) 220:
        case (TypeCode) 222:
        case (TypeCode) 223:
        case (TypeCode) 224:
        case (TypeCode) 253:
        case (TypeCode) 254:
        case (TypeCode) 256:
        case (TypeCode) 258:
        case (TypeCode) 260:
        case (TypeCode) 261:
        case (TypeCode) 262:
        case (TypeCode) 272:
        case (TypeCode) 273:
        case (TypeCode) 275:
        case (TypeCode) 277:
        case (TypeCode) 279:
        case (TypeCode) 280:
        case (TypeCode) 281:
        case (TypeCode) 291:
        case (TypeCode) 292:
        case (TypeCode) 294:
        case (TypeCode) 296:
        case (TypeCode) 298:
        case (TypeCode) 299:
        case (TypeCode) 300:
          return ObjectType.IDivideInt64(convertible1.ToInt64((IFormatProvider) null), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 132:
        case (TypeCode) 151:
        case (TypeCode) 189:
        case (TypeCode) 227:
        case (TypeCode) 265:
        case (TypeCode) 284:
        case (TypeCode) 303:
          return ObjectType.IDivideString(convertible1, tc1, convertible2, tc2);
        case (TypeCode) 133:
          return ObjectType.IDivideInt16(convertible1.ToInt16((IFormatProvider) null), (short) 0);
        case (TypeCode) 171:
          return ObjectType.IDivideInt32(convertible1.ToInt32((IFormatProvider) null), 0);
        case (TypeCode) 174:
          return ObjectType.IDivideInt32(convertible1.ToInt32((IFormatProvider) null), ObjectType.ToVBBool(convertible2));
        case (TypeCode) 209:
        case (TypeCode) 247:
        case (TypeCode) 266:
        case (TypeCode) 285:
          return ObjectType.IDivideInt64(convertible1.ToInt64((IFormatProvider) null), 0L);
        case (TypeCode) 212:
        case (TypeCode) 250:
        case (TypeCode) 269:
        case (TypeCode) 288:
          return ObjectType.IDivideInt64(convertible1.ToInt64((IFormatProvider) null), (long) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 342:
          return ObjectType.IDivideInt64(LongType.FromString(convertible1.ToString((IFormatProvider) null)), 0L);
        case (TypeCode) 345:
          return ObjectType.IDivideInt64(LongType.FromString(convertible1.ToString((IFormatProvider) null)), (long) ObjectType.ToVBBool(convertible2));
        case (TypeCode) 348:
        case (TypeCode) 349:
        case (TypeCode) 351:
        case (TypeCode) 353:
        case (TypeCode) 355:
        case (TypeCode) 356:
        case (TypeCode) 357:
          return ObjectType.IDivideInt64(LongType.FromString(convertible1.ToString((IFormatProvider) null)), convertible2.ToInt64((IFormatProvider) null));
        case (TypeCode) 360:
          return ObjectType.IDivideStringString(convertible1.ToString((IFormatProvider) null), convertible2.ToString((IFormatProvider) null));
        default:
          throw ObjectType.GetNoValidOperatorException(o1, o2);
      }
    }

    private static object IDivideString(IConvertible conv1, TypeCode tc1, IConvertible conv2, TypeCode tc2)
    {
      long num1;
      switch (tc1)
      {
        case TypeCode.Boolean:
          num1 = (long) ObjectType.ToVBBool(conv1);
          break;
        case TypeCode.String:
          try
          {
            num1 = LongType.FromString(conv1.ToString((IFormatProvider) null));
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
            throw ObjectType.GetNoValidOperatorException((object) conv1, (object) conv2);
          }
        default:
          num1 = conv1.ToInt64((IFormatProvider) null);
          break;
      }
      long num2;
      switch (tc2)
      {
        case TypeCode.Boolean:
          num2 = (long) ObjectType.ToVBBool(conv2);
          break;
        case TypeCode.String:
          try
          {
            num2 = LongType.FromString(conv2.ToString((IFormatProvider) null));
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
            throw ObjectType.GetNoValidOperatorException((object) conv1, (object) conv2);
          }
        default:
          num2 = conv2.ToInt64((IFormatProvider) null);
          break;
      }
      return (object) (num1 / num2);
    }

    private static object IDivideStringString(string s1, string s2)
    {
      long num1 = 0;
      if (s1 != null)
        num1 = LongType.FromString(s1);
      long num2 = 0;
      if (s2 != null)
        num2 = LongType.FromString(s2);
      return (object) (num1 / num2);
    }

    private static object IDivideByte(byte d1, byte d2)
    {
      return (object) checked ((byte) unchecked ((uint) d1 / (uint) d2));
    }

    private static object IDivideInt16(short d1, short d2)
    {
      return (object) checked ((short) unchecked ((int) d1 / (int) d2));
    }

    private static object IDivideInt32(int d1, int d2)
    {
      return (object) (d1 / d2);
    }

    private static object IDivideInt64(long d1, long d2)
    {
      return (object) (d1 / d2);
    }

    /// <summary>Performs an arithmetic left shift (&lt;&lt;) operation.</summary>
    /// <param name="o1">Required. Integral numeric expression. The bit pattern to be shifted. The data type must be an integral type (<see langword="SByte" />, <see langword="Byte" />, <see langword="Short" />, <see langword="UShort" />, <see langword="Integer" />, <see langword="UInteger" />, <see langword="Long" />, or <see langword="ULong" />).</param>
    /// <param name="amount">Required. Numeric expression. The number of bits to shift the bit pattern. The data type must be <see langword="Integer" /> or widen to <see langword="Integer" />.</param>
    /// <returns>An integral numeric value. The result of shifting the bit pattern. The data type is the same as that of <paramref name="o1" />.</returns>
    public static object ShiftLeftObj(object o1, int amount)
    {
      IConvertible convertible = o1 as IConvertible;
      switch (convertible != null ? (int) convertible.GetTypeCode() : (o1 != null ? 1 : 0))
      {
        case 0:
          return (object) (0 << amount);
        case 3:
          return (object) (short) ((int) (short) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0) << (amount & 15));
        case 6:
          return (object) (byte) ((uint) convertible.ToByte((IFormatProvider) null) << (amount & 7));
        case 7:
          return (object) (short) ((int) convertible.ToInt16((IFormatProvider) null) << (amount & 15));
        case 9:
          return (object) (convertible.ToInt32((IFormatProvider) null) << amount);
        case 11:
        case 13:
        case 14:
        case 15:
          return (object) (convertible.ToInt64((IFormatProvider) null) << amount);
        case 18:
          return (object) (LongType.FromString(convertible.ToString((IFormatProvider) null)) << amount);
        default:
          throw ObjectType.GetNoValidOperatorException(o1);
      }
    }

    /// <summary>Performs an arithmetic right shift (&gt;&gt;) operation.</summary>
    /// <param name="o1">Required. Integral numeric expression. The bit pattern to be shifted. The data type must be an integral type (<see langword="SByte" />, <see langword="Byte" />, <see langword="Short" />, <see langword="UShort" />, <see langword="Integer" />, <see langword="UInteger" />, <see langword="Long" />, or <see langword="ULong" />).</param>
    /// <param name="amount">Required. Numeric expression. The number of bits to shift the bit pattern. The data type must be <see langword="Integer" /> or widen to <see langword="Integer" />.</param>
    /// <returns>An integral numeric value. The result of shifting the bit pattern. The data type is the same as that of <paramref name="o1" />.</returns>
    public static object ShiftRightObj(object o1, int amount)
    {
      IConvertible convertible = o1 as IConvertible;
      switch (convertible != null ? (int) convertible.GetTypeCode() : (o1 != null ? 1 : 0))
      {
        case 0:
          return (object) (0 >> amount);
        case 3:
          return (object) (short) ((int) (short) -(convertible.ToBoolean((IFormatProvider) null) ? 1 : 0) >> (amount & 15));
        case 6:
          return (object) (byte) ((uint) convertible.ToByte((IFormatProvider) null) >> (amount & 7));
        case 7:
          return (object) (short) ((int) convertible.ToInt16((IFormatProvider) null) >> (amount & 15));
        case 9:
          return (object) (convertible.ToInt32((IFormatProvider) null) >> amount);
        case 11:
        case 13:
        case 14:
        case 15:
          return (object) (convertible.ToInt64((IFormatProvider) null) >> amount);
        case 18:
          return (object) (LongType.FromString(convertible.ToString((IFormatProvider) null)) >> amount);
        default:
          throw ObjectType.GetNoValidOperatorException(o1);
      }
    }

    /// <summary>Performs an <see langword="Xor" /> comparison.</summary>
    /// <param name="obj1">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <param name="obj2">Required. Any <see langword="Boolean" /> or numeric expression.</param>
    /// <returns>A <see langword="Boolean" /> or numeric value. For a <see langword="Boolean" /> comparison, the return value is the logical exclusion (exclusive logical disjunction) of two <see langword="Boolean" /> values. For bitwise (numeric) operations, the return value is a numeric value that represents the bitwise exclusion (exclusive bitwise disjunction) of two numeric bit patterns. For more information, see Xor Operator (Visual Basic).</returns>
    public static object XorObj(object obj1, object obj2)
    {
      if (obj1 == null && obj2 == null)
        return (object) false;
      switch (ObjectType.GetWidestType(obj1, obj2, false))
      {
        case TypeCode.Boolean:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.Int32:
        case TypeCode.Int64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.String:
          return (object) (BooleanType.FromObject(obj1) ^ BooleanType.FromObject(obj2));
        default:
          throw ObjectType.GetNoValidOperatorException(obj1, obj2);
      }
    }

    /// <summary>Performs a <see langword="Like" /> comparison.</summary>
    /// <param name="vLeft">Required. Any expression.</param>
    /// <param name="vRight">Required. Any string expression conforming to the pattern-matching conventions described in Like Operator.</param>
    /// <param name="CompareOption">Required. A <see cref="T:Ported.VisualBasic.CompareMethod" /> value that specifies that the operation use either text or binary comparison.</param>
    /// <returns>
    /// <see langword="True" /> if the string representation of the value in <paramref name="vLeft" /> satisfies the pattern that is contained in <paramref name="vRight" />; otherwise, <see langword="False" />. <see langword="True" /> if both <paramref name="vLeft" /> and <paramref name="vRight" /> are <see langword="Nothing" />.</returns>
    public static bool LikeObj(object vLeft, object vRight, CompareMethod CompareOption)
    {
      return StringType.StrLike(StringType.FromObject(vLeft), StringType.FromObject(vRight), CompareOption);
    }

    /// <summary>Performs a string concatenation (&amp;) operation.</summary>
    /// <param name="vLeft">Required. Any expression.</param>
    /// <param name="vRight">Required. Any expression.</param>
    /// <returns>A string representing the concatenation of <paramref name="vLeft" /> and <paramref name="vRight" />.</returns>
    public static object StrCatObj(object vLeft, object vRight)
    {
      bool flag1 = vLeft is DBNull;
      bool flag2 = vRight is DBNull;
      if (flag1 & flag2)
        return vLeft;
      if (flag1 & !flag2)
        vLeft = (object) "";
      else if (flag2 & !flag1)
        vRight = (object) "";
      return (object) (StringType.FromObject(vLeft) + StringType.FromObject(vRight));
    }

    internal static object CTypeHelper(object obj, TypeCode toType)
    {
      if (obj == null)
        return (object) null;
      switch (toType)
      {
        case TypeCode.Boolean:
          return (object) BooleanType.FromObject(obj);
        case TypeCode.Char:
          return (object) CharType.FromObject(obj);
        case TypeCode.Byte:
          return (object) ByteType.FromObject(obj);
        case TypeCode.Int16:
          return (object) ShortType.FromObject(obj);
        case TypeCode.Int32:
          return (object) IntegerType.FromObject(obj);
        case TypeCode.Int64:
          return (object) LongType.FromObject(obj);
        case TypeCode.Single:
          return (object) SingleType.FromObject(obj);
        case TypeCode.Double:
          return (object) DoubleType.FromObject(obj);
        case TypeCode.Decimal:
          return (object) DecimalType.FromObject(obj);
        case TypeCode.DateTime:
          return (object) DateType.FromObject(obj);
        case TypeCode.String:
          return (object) StringType.FromObject(obj);
        default:
          throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(obj), Utils.VBFriendlyName(ObjectType.TypeFromTypeCode(toType))));
      }
    }

    internal static object CTypeHelper(object obj, Type toType)
    {
      if (obj == null)
        return (object) null;
      if (toType == typeof (object))
        return obj;
      Type typ = obj.GetType();
      bool flag=false;
      if (toType.IsByRef)
      {
        toType = toType.GetElementType();
        flag = true;
      }
      if (typ.IsByRef)
        typ = typ.GetElementType();
      object obj1;
      if (typ == toType || toType == typeof (object))
      {
        if (!flag)
          return obj;
        obj1 = ObjectType.GetObjectValuePrimitive(obj);
      }
      else
      {
        TypeCode typeCode = Type.GetTypeCode(toType);
        if (typeCode == TypeCode.Object)
        {
          if (toType == typeof (object) || toType.IsInstanceOfType(obj))
            return obj;
          string str = obj as string;
          if (str != null && toType == typeof (char[]))
            return (object) CharArrayType.FromString(str);
          throw new InvalidCastException(Utils.GetResourceString("InvalidCast_FromTo", Utils.VBFriendlyName(typ), Utils.VBFriendlyName(toType)));
        }
        obj1 = ObjectType.CTypeHelper(obj, typeCode);
      }
      if (toType.IsEnum)
        return Enum.ToObject(toType, obj1);
      return obj1;
    }

    private static Exception GetNoValidOperatorException(object Operand)
    {
      return (Exception) new InvalidCastException(Utils.GetResourceString("NoValidOperator_OneOperand", new string[1]
      {
        Utils.VBFriendlyName(Operand)
      }));
    }

    private static Exception GetNoValidOperatorException(object Left, object Right)
    {
      string str1;
      if (Left == null)
      {
        str1 = "'Nothing'";
      }
      else
      {
        string str2 = Left as string;
        if (str2 != null)
          str1 = Utils.GetResourceString("NoValidOperator_StringType1", new string[1]
          {
            Strings.Left(str2, 32)
          });
        else
          str1 = Utils.GetResourceString("NoValidOperator_NonStringType1", new string[1]
          {
            Utils.VBFriendlyName(Left)
          });
      }
      string str3;
      if (Right == null)
      {
        str3 = "'Nothing'";
      }
      else
      {
        string str2 = Right as string;
        if (str2 != null)
          str3 = Utils.GetResourceString("NoValidOperator_StringType1", new string[1]
          {
            Strings.Left(str2, 32)
          });
        else
          str3 = Utils.GetResourceString("NoValidOperator_NonStringType1", new string[1]
          {
            Utils.VBFriendlyName(Right)
          });
      }
      return (Exception) new InvalidCastException(Utils.GetResourceString("NoValidOperator_TwoOperands", str1, str3));
    }

    /// <summary>Returns a boxed primitive value. This method is used to prevent copying structures multiple times.</summary>
    /// <param name="o">Required. Any expression.</param>
    /// <returns>The primitive value of <paramref name="o" /> typed as object.</returns>
    public static object GetObjectValuePrimitive(object o)
    {
      if (o == null)
        return (object) null;
      IConvertible convertible = o as IConvertible;
      if (convertible == null)
        return o;
      switch (convertible.GetTypeCode())
      {
        case TypeCode.Boolean:
          return (object) convertible.ToBoolean((IFormatProvider) null);
        case TypeCode.Char:
          return (object) convertible.ToChar((IFormatProvider) null);
        case TypeCode.SByte:
          return (object) convertible.ToSByte((IFormatProvider) null);
        case TypeCode.Byte:
          return (object) convertible.ToByte((IFormatProvider) null);
        case TypeCode.Int16:
          return (object) convertible.ToInt16((IFormatProvider) null);
        case TypeCode.UInt16:
          return (object) convertible.ToUInt16((IFormatProvider) null);
        case TypeCode.Int32:
          return (object) convertible.ToInt32((IFormatProvider) null);
        case TypeCode.UInt32:
          return (object) convertible.ToUInt32((IFormatProvider) null);
        case TypeCode.Int64:
          return (object) convertible.ToInt64((IFormatProvider) null);
        case TypeCode.UInt64:
          return (object) convertible.ToUInt64((IFormatProvider) null);
        case TypeCode.Single:
          return (object) convertible.ToSingle((IFormatProvider) null);
        case TypeCode.Double:
          return (object) convertible.ToDouble((IFormatProvider) null);
        case TypeCode.Decimal:
          return (object) convertible.ToDecimal((IFormatProvider) null);
        case TypeCode.DateTime:
          return (object) convertible.ToDateTime((IFormatProvider) null);
        case TypeCode.String:
          return o;
        default:
          return o;
      }
    }

    private enum VType
    {
      t_bad,
      t_bool,
      t_ui1,
      t_i2,
      t_i4,
      t_i8,
      t_dec,
      t_r4,
      t_r8,
      t_char,
      t_str,
      t_date,
    }

    private enum VType2
    {
      t_bad,
      t_bool,
      t_ui1,
      t_char,
      t_i2,
      t_i4,
      t_i8,
      t_r4,
      t_r8,
      t_date,
      t_dec,
      t_ref,
      t_str,
    }

    private enum CC : byte
    {
      Err,
      Same,
      Narr,
      Wide,
    }
  }
}
