// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.PutHandler
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Reflection;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal sealed class PutHandler : IRecordEnum
  {
    public VB6File m_oFile;

    public PutHandler(VB6File oFile)
    {
      this.m_oFile = oFile;
    }

    public bool Callback(FieldInfo field_info, ref object vValue)
    {
      Type fieldType = field_info.FieldType;
      if (fieldType == null)
        throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "Empty")), 5);
      if (fieldType.IsArray)
      {
        int FixedStringLength = -1;
        object[] customAttributes1 = field_info.GetCustomAttributes(typeof (VBFixedArrayAttribute), false);
        VBFixedArrayAttribute fixedArrayAttribute = customAttributes1 == null || customAttributes1.Length == 0 ? (VBFixedArrayAttribute) null : (VBFixedArrayAttribute) customAttributes1[0];
        Type elementType = fieldType.GetElementType();
        if (elementType == typeof (string))
        {
          object[] customAttributes2 = field_info.GetCustomAttributes(typeof (VBFixedStringAttribute), false);
          FixedStringLength = customAttributes2 == null || customAttributes2.Length == 0 ? -1 : ((VBFixedStringAttribute) customAttributes2[0]).Length;
        }
        if (fixedArrayAttribute == null)
          this.m_oFile.PutDynamicArray(0L, (Array) vValue, false, FixedStringLength);
        else
          this.m_oFile.PutFixedArray(0L, (Array) vValue, elementType, FixedStringLength, fixedArrayAttribute.FirstBound, fixedArrayAttribute.SecondBound);
      }
      else
      {
        switch (Type.GetTypeCode(fieldType))
        {
          case TypeCode.DBNull:
            throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "DBNull")), 5);
          case TypeCode.Boolean:
            this.m_oFile.PutBoolean(0L, BooleanType.FromObject(vValue), false);
            break;
          case TypeCode.Char:
            this.m_oFile.PutChar(0L, CharType.FromObject(vValue), false);
            break;
          case TypeCode.Byte:
            this.m_oFile.PutByte(0L, ByteType.FromObject(vValue), false);
            break;
          case TypeCode.Int16:
            this.m_oFile.PutShort(0L, ShortType.FromObject(vValue), false);
            break;
          case TypeCode.Int32:
            this.m_oFile.PutInteger(0L, IntegerType.FromObject(vValue), false);
            break;
          case TypeCode.Int64:
            this.m_oFile.PutLong(0L, LongType.FromObject(vValue), false);
            break;
          case TypeCode.Single:
            this.m_oFile.PutSingle(0L, SingleType.FromObject(vValue), false);
            break;
          case TypeCode.Double:
            this.m_oFile.PutDouble(0L, DoubleType.FromObject(vValue), false);
            break;
          case TypeCode.Decimal:
            this.m_oFile.PutDecimal(0L, DecimalType.FromObject(vValue), false);
            break;
          case TypeCode.DateTime:
            this.m_oFile.PutDate(0L, DateType.FromObject(vValue), false);
            break;
          case TypeCode.String:
            string s = vValue == null ? (string) null : vValue.ToString();
            object[] customAttributes = field_info.GetCustomAttributes(typeof (VBFixedStringAttribute), false);
            if (customAttributes == null || customAttributes.Length == 0)
            {
              this.m_oFile.PutStringWithLength(0L, s);
              break;
            }
            int lengthToWrite = ((VBFixedStringAttribute) customAttributes[0]).Length;
            if (lengthToWrite == 0)
              lengthToWrite = -1;
            this.m_oFile.PutFixedLengthString(0L, s, lengthToWrite);
            break;
          default:
            if (fieldType == typeof (object))
            {
              this.m_oFile.PutObject(vValue, 0L, true);
              break;
            }
            if (fieldType == typeof (Exception))
              throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "Exception")), 5);
            if (fieldType == typeof (Missing))
              throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "Missing")), 5);
            throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, fieldType.Name)), 5);
        }
      }
      bool flag=false;
      return flag;
    }
  }
}
