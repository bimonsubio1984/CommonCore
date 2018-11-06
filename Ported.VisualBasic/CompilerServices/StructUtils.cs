// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.StructUtils
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Reflection;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class StructUtils
  {
    private StructUtils()
    {
    }

    internal static object EnumerateUDT(ValueType oStruct, IRecordEnum intfRecEnum, bool fGet)
    {
      Type type = oStruct.GetType();
      if (Information.VarTypeFromComType(type) != VariantType.UserDefinedType || type.IsPrimitive)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          nameof (oStruct)
        }));
      FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
      int num1 = 0;
      int upperBound = fields.GetUpperBound(0);
      int num2 = num1;
      int num3 = upperBound;
      int index = num2;
      while (index <= num3)
      {
        FieldInfo FieldInfo = fields[index];
        Type fieldType = FieldInfo.FieldType;
        object obj = FieldInfo.GetValue((object) oStruct);
        if (Information.VarTypeFromComType(fieldType) == VariantType.UserDefinedType)
        {
          if (fieldType.IsPrimitive)
            throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", FieldInfo.Name, fieldType.Name)), 5);
          StructUtils.EnumerateUDT((ValueType) obj, intfRecEnum, fGet);
        }
        else
          intfRecEnum.Callback(FieldInfo, ref obj);
        if (fGet)
          FieldInfo.SetValue((object) oStruct, obj);
        checked { ++index; }
      }
      return (object) null;
    }

    internal static int GetRecordLength(object o, int PackSize = -1)
    {
      if (o == null)
        return 0;
      StructUtils.StructByteLengthHandler byteLengthHandler = new StructUtils.StructByteLengthHandler(PackSize);
      IRecordEnum intfRecEnum = (IRecordEnum) byteLengthHandler;
      if (intfRecEnum == null)
        throw ExceptionUtils.VbMakeException(5);
      StructUtils.EnumerateUDT((ValueType) o, intfRecEnum, false);
      return byteLengthHandler.Length;
    }

    private sealed class StructByteLengthHandler : IRecordEnum
    {
      private int m_StructLength;
      private int m_PackSize;

      internal StructByteLengthHandler(int PackSize)
      {
        this.m_PackSize = PackSize;
      }

      internal int Length
      {
        get
        {
          if (this.m_PackSize == 1)
            return this.m_StructLength;
          return checked (this.m_StructLength + unchecked (this.m_StructLength % this.m_PackSize));
        }
      }

      internal void SetAlignment(int size)
      {
        if (this.m_PackSize == 1)
          return;
        checked { this.m_StructLength += unchecked (this.m_StructLength % size); }
      }

      bool IRecordEnum.Callback(FieldInfo field_info, ref object vValue)
      {
        Type fieldType = field_info.FieldType;
        if (fieldType == null)
          throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "Empty")), 5);
        if (fieldType.IsArray)
        {
          object[] customAttributes = field_info.GetCustomAttributes(typeof (VBFixedArrayAttribute), false);
          VBFixedArrayAttribute fixedArrayAttribute = customAttributes == null || customAttributes.Length == 0 ? (VBFixedArrayAttribute) null : (VBFixedArrayAttribute) customAttributes[0];
          Type elementType = fieldType.GetElementType();
          int num = 0;
          int size = 0;
          int align = 0;
          if (fixedArrayAttribute == null)
          {
            num = 1;
            size = 4;
          }
          else
          {
            num = fixedArrayAttribute.Length;
            this.GetFieldSize(field_info, elementType, ref align, ref size);
          }
          this.SetAlignment(align);
          checked { this.m_StructLength += num * size; }
          return false;
        }
        int align1 = 0;
        int size1 = 0;
        this.GetFieldSize(field_info, fieldType, ref align1, ref size1);
        this.SetAlignment(align1);
        checked { this.m_StructLength += size1; }
        return false;
      }

      private void GetFieldSize(FieldInfo field_info, Type FieldType, ref int align, ref int size)
      {
        switch (Type.GetTypeCode(FieldType))
        {
          case TypeCode.DBNull:
            throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "DBNull")), 5);
          case TypeCode.Boolean:
            align = 2;
            size = 2;
            break;
          case TypeCode.Char:
            align = 2;
            size = 2;
            break;
          case TypeCode.Byte:
            align = 1;
            size = 1;
            break;
          case TypeCode.Int16:
            align = 2;
            size = 2;
            break;
          case TypeCode.Int32:
            align = 4;
            size = 4;
            break;
          case TypeCode.Int64:
            align = 8;
            size = 8;
            break;
          case TypeCode.Single:
            align = 4;
            size = 4;
            break;
          case TypeCode.Double:
            align = 8;
            size = 8;
            break;
          case TypeCode.Decimal:
            align = 16;
            size = 16;
            break;
          case TypeCode.DateTime:
            align = 8;
            size = 8;
            break;
          case TypeCode.String:
            object[] customAttributes = field_info.GetCustomAttributes(typeof (VBFixedStringAttribute), false);
            if (customAttributes == null || customAttributes.Length == 0)
            {
              align = 4;
              size = 4;
              break;
            }
            int num = ((VBFixedStringAttribute) customAttributes[0]).Length;
            if (num == 0)
              num = -1;
            size = num;
            break;
        }
        if (FieldType == typeof (Exception))
          throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "Exception")), 5);
        if (FieldType == typeof (Missing))
          throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "Missing")), 5);
        if (FieldType == typeof (object))
          throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "Object")), 5);
      }
    }
  }
}
