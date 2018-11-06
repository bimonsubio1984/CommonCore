// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.GetHandler
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Reflection;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal sealed class GetHandler : IRecordEnum
  {
    private VB6File m_oFile;

    public GetHandler(VB6File oFile)
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
        object[] customAttributes1 = field_info.GetCustomAttributes(typeof (VBFixedArrayAttribute), false);
        Array arr = (Array) null;
        int FixedStringLength = -1;
        object[] customAttributes2 = field_info.GetCustomAttributes(typeof (VBFixedStringAttribute), false);
        if (customAttributes2 != null && customAttributes2.Length > 0)
        {
          VBFixedStringAttribute fixedStringAttribute = (VBFixedStringAttribute) customAttributes2[0];
          if (fixedStringAttribute.Length > 0)
            FixedStringLength = fixedStringAttribute.Length;
        }
        if (customAttributes1 == null || customAttributes1.Length == 0)
        {
          this.m_oFile.GetDynamicArray(ref arr, fieldType.GetElementType(), FixedStringLength);
        }
        else
        {
          VBFixedArrayAttribute fixedArrayAttribute = (VBFixedArrayAttribute) customAttributes1[0];
          int firstBound = fixedArrayAttribute.FirstBound;
          int secondBound = fixedArrayAttribute.SecondBound;
          arr = (Array) vValue;
          this.m_oFile.GetFixedArray(0L, ref arr, fieldType.GetElementType(), firstBound, secondBound, FixedStringLength);
        }
        vValue = (object) arr;
      }
      else
      {
        switch (Type.GetTypeCode(fieldType))
        {
          case TypeCode.DBNull:
            throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedFieldType2", field_info.Name, "DBNull")), 5);
          case TypeCode.Boolean:
            vValue = (object) this.m_oFile.GetBoolean(0L);
            break;
          case TypeCode.Char:
            vValue = (object) this.m_oFile.GetChar(0L);
            break;
          case TypeCode.Byte:
            vValue = (object) this.m_oFile.GetByte(0L);
            break;
          case TypeCode.Int16:
            vValue = (object) this.m_oFile.GetShort(0L);
            break;
          case TypeCode.Int32:
            vValue = (object) this.m_oFile.GetInteger(0L);
            break;
          case TypeCode.Int64:
            vValue = (object) this.m_oFile.GetLong(0L);
            break;
          case TypeCode.Single:
            vValue = (object) this.m_oFile.GetSingle(0L);
            break;
          case TypeCode.Double:
            vValue = (object) this.m_oFile.GetDouble(0L);
            break;
          case TypeCode.Decimal:
            vValue = (object) this.m_oFile.GetDecimal(0L);
            break;
          case TypeCode.DateTime:
            vValue = (object) this.m_oFile.GetDate(0L);
            break;
          case TypeCode.String:
            object[] customAttributes = field_info.GetCustomAttributes(typeof (VBFixedStringAttribute), false);
            if (customAttributes == null || customAttributes.Length == 0)
            {
              vValue = (object) this.m_oFile.GetLengthPrefixedString(0L);
              break;
            }
            int ByteLength = ((VBFixedStringAttribute) customAttributes[0]).Length;
            if (ByteLength == 0)
              ByteLength = -1;
            vValue = (object) this.m_oFile.GetFixedLengthString(0L, ByteLength);
            break;
          default:
            if (fieldType == typeof (object))
            {
              this.m_oFile.GetObject(ref vValue, 0L, true);
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
