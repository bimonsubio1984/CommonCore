// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.VB6RandomFile
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class VB6RandomFile : VB6File
  {
    public VB6RandomFile(string FileName, OpenAccess access, OpenShare share, int lRecordLen)
      : base(FileName, access, share, lRecordLen)
    {
    }

    private void OpenFileHelper(FileMode fm, OpenAccess fa)
    {
      try
      {
        this.m_file = new FileStream(this.m_sFullPath, fm, (FileAccess) fa, (FileShare) this.m_share);
      }
      catch (FileNotFoundException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 53);
      }
      catch (DirectoryNotFoundException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 76);
      }
      catch (SecurityException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 53);
      }
      catch (IOException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 75);
      }
      catch (UnauthorizedAccessException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 75);
      }
      catch (ArgumentException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 75);
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
        throw ExceptionUtils.VbMakeException(51);
      }
    }

    internal override void OpenFile()
    {
      FileMode fm = !File.Exists(this.m_sFullPath) ? (this.m_access != OpenAccess.Read ? FileMode.Create : FileMode.OpenOrCreate) : FileMode.Open;
      if (this.m_access == OpenAccess.Default)
      {
        this.m_access = OpenAccess.ReadWrite;
        try
        {
          this.OpenFileHelper(fm, this.m_access);
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
        catch (Exception ex1)
        {
          this.m_access = OpenAccess.Write;
          try
          {
            this.OpenFileHelper(fm, this.m_access);
          }
          catch (StackOverflowException ex2)
          {
            throw ex2;
          }
          catch (OutOfMemoryException ex2)
          {
            throw ex2;
          }
          catch (ThreadAbortException ex2)
          {
            throw ex2;
          }
          catch (Exception ex2)
          {
            this.m_access = OpenAccess.Read;
            this.OpenFileHelper(fm, this.m_access);
          }
        }
      }
      else
        this.OpenFileHelper(fm, this.m_access);
      this.m_Encoding = Utils.GetFileIOEncoding();
      Stream file = (Stream) this.m_file;
      if (this.m_access == OpenAccess.Write || this.m_access == OpenAccess.ReadWrite)
      {
        this.m_sw = new StreamWriter(file, this.m_Encoding);
        this.m_sw.AutoFlush = true;
        this.m_bw = new BinaryWriter(file, this.m_Encoding);
      }
      if (this.m_access != OpenAccess.Read && this.m_access != OpenAccess.ReadWrite)
        return;
      this.m_br = new BinaryReader(file, this.m_Encoding);
      if (this.GetMode() != OpenMode.Binary)
        return;
      this.m_sr = new StreamReader(file, this.m_Encoding, false, 128);
    }

    internal override void CloseFile()
    {
      if (this.m_sw != null)
        this.m_sw.Flush();
      this.CloseTheFile();
    }

    internal override void Lock(long lStart, long lEnd)
    {
      if (lStart > lEnd)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Start"
        }));
      this.m_file.Lock(checked (lStart - 1L * (long) this.m_lRecordLen), checked (lEnd - lStart + 1L * (long) this.m_lRecordLen));
    }

    internal override void Unlock(long lStart, long lEnd)
    {
      if (lStart > lEnd)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Start"
        }));
      this.m_file.Unlock(checked (lStart - 1L * (long) this.m_lRecordLen), checked (lEnd - lStart + 1L * (long) this.m_lRecordLen));
    }

    public override OpenMode GetMode()
    {
      return OpenMode.Random;
    }

    internal override StreamReader GetStreamReader()
    {
      return new StreamReader((Stream) this.m_file, this.m_Encoding);
    }

    internal override bool EOF()
    {
      this.m_eof = this.m_position >= this.m_file.Length;
      return this.m_eof;
    }

    internal override long LOC()
    {
      if (this.m_lRecordLen == 0)
        throw ExceptionUtils.VbMakeException(51);
      return checked (this.m_position + (long) this.m_lRecordLen - 1L) / (long) this.m_lRecordLen;
    }

    internal override void Seek(long Position)
    {
      this.SetRecord(Position);
    }

    internal override long Seek()
    {
      return checked (this.LOC() + 1L);
    }

    internal override void GetObject(ref object Value, long RecordNumber = 0, bool ContainedInVariant = true)
    {
      Type type = (Type) null;
      this.ValidateReadable();
      this.SetRecord(RecordNumber);
      VT vt;
      if (ContainedInVariant)
      {
        vt = (VT) this.m_br.ReadInt16();
        checked { this.m_position += 2L; }
      }
      else
      {
        type = Value.GetType();
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.Object:
            vt = !type.IsValueType ? VT.Variant : VT.Structure;
            break;
          case TypeCode.Boolean:
            vt = VT.Boolean;
            break;
          case TypeCode.Char:
            vt = VT.Char;
            break;
          case TypeCode.Byte:
            vt = VT.Byte;
            break;
          case TypeCode.Int16:
            vt = VT.Short;
            break;
          case TypeCode.Int32:
            vt = VT.Integer;
            break;
          case TypeCode.Int64:
            vt = VT.Long;
            break;
          case TypeCode.Single:
            vt = VT.Single;
            break;
          case TypeCode.Double:
            vt = VT.Double;
            break;
          case TypeCode.Decimal:
            vt = VT.Decimal;
            break;
          case TypeCode.DateTime:
            vt = VT.Date;
            break;
          case TypeCode.String:
            vt = VT.String;
            break;
          default:
            vt = VT.Variant;
            break;
        }
      }
      if ((vt & VT.Array) != VT.Empty)
      {
        Array arr = (Array) null;
        VT vtype = vt ^ VT.Array;
        this.GetDynamicArray(ref arr, this.ComTypeFromVT(vtype), -1);
        Value = (object) arr;
      }
      else
      {
        switch (vt)
        {
          case VT.Short:
            Value = (object) this.GetShort(0L);
            break;
          case VT.Integer:
            Value = (object) this.GetInteger(0L);
            break;
          case VT.Single:
            Value = (object) this.GetSingle(0L);
            break;
          case VT.Double:
            Value = (object) this.GetDouble(0L);
            break;
          case VT.Currency:
            Value = (object) this.GetCurrency(0L);
            break;
          case VT.Date:
            Value = (object) this.GetDate(0L);
            break;
          case VT.String:
            Value = (object) this.GetLengthPrefixedString(0L);
            break;
          case VT.Boolean:
            Value = (object) this.GetBoolean(0L);
            break;
          case VT.Decimal:
            Value = (object) this.GetDecimal(0L);
            break;
          case VT.Byte:
            Value = (object) this.GetByte(0L);
            break;
          case VT.Char:
            Value = (object) this.GetChar(0L);
            break;
          case VT.Long:
            Value = (object) this.GetLong(0L);
            break;
          case VT.Structure:
            ValueType o = (ValueType) Value;
            this.GetRecord(0L, ref o, false);
            Value = (object) o;
            break;
          default:
            if (vt == VT.DBNull && ContainedInVariant)
            {
              Value = (object) DBNull.Value;
              break;
            }
            switch (vt)
            {
              case VT.Empty:
                Value = (object) null;
                return;
              case VT.DBNull:
                throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedIOType1", new string[1]
                {
                  "DBNull"
                })), 5);
              case VT.Currency:
                throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedIOType1", new string[1]
                {
                  "Currency"
                })), 5);
              default:
                throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedIOType1", new string[1]
                {
                  type.FullName
                })), 5);
            }
        }
      }
    }

    internal override void Get(ref ValueType Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      this.GetRecord(RecordNumber, ref Value, false);
    }

    internal override void Get(ref Array Value, long RecordNumber = 0, bool ArrayIsDynamic = false, bool StringIsFixedLength = false)
    {
      this.ValidateReadable();
      if (Value == null)
        throw new ArgumentException(Utils.GetResourceString("Argument_ArrayNotInitialized"));
      Type elementType = Value.GetType().GetElementType();
      int FixedStringLength = -1;
      int rank = Value.Rank;
      int SecondBound = -1;
      this.SetRecord(RecordNumber);
      if (this.m_file.Position >= this.m_file.Length)
        return;
      if (StringIsFixedLength && elementType == typeof (string))
      {
        object obj;
        if (rank == 1)
        {
          obj = Value.GetValue(0);
        }
        else
        {
          if (rank != 2)
            throw new ArgumentException(Utils.GetResourceString("Argument_UnsupportedArrayDimensions"));
          obj = Value.GetValue(0, 0);
        }
        FixedStringLength = obj != null ? ((string) obj).Length : 0;
        if (FixedStringLength == 0)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidFixedLengthString"));
      }
      if (ArrayIsDynamic)
      {
        Value = this.GetArrayDesc(elementType);
        rank = Value.Rank;
      }
      int upperBound = Value.GetUpperBound(0);
      if (rank != 1)
      {
        if (rank != 2)
          throw new ArgumentException(Utils.GetResourceString("Argument_UnsupportedArrayDimensions"));
        SecondBound = Value.GetUpperBound(1);
      }
      if (ArrayIsDynamic)
        this.GetArrayData(Value, elementType, upperBound, SecondBound, FixedStringLength);
      else
        this.GetFixedArray(RecordNumber, ref Value, elementType, upperBound, SecondBound, FixedStringLength);
    }

    internal override void Get(ref bool Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetBoolean(RecordNumber);
    }

    internal override void Get(ref byte Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetByte(RecordNumber);
    }

    internal override void Get(ref short Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetShort(RecordNumber);
    }

    internal override void Get(ref int Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetInteger(RecordNumber);
    }

    internal override void Get(ref long Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetLong(RecordNumber);
    }

    internal override void Get(ref char Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetChar(RecordNumber);
    }

    internal override void Get(ref float Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetSingle(RecordNumber);
    }

    internal override void Get(ref double Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetDouble(RecordNumber);
    }

    internal override void Get(ref Decimal Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetCurrency(RecordNumber);
    }

    internal override void Get(ref string Value, long RecordNumber = 0, bool StringIsFixedLength = false)
    {
      this.ValidateReadable();
      if (StringIsFixedLength)
      {
        int ByteLength = Value != null ? this.m_Encoding.GetByteCount(Value) : 0;
        Value = this.GetFixedLengthString(RecordNumber, ByteLength);
      }
      else
        Value = this.GetLengthPrefixedString(RecordNumber);
    }

    internal override void Get(ref DateTime Value, long RecordNumber = 0)
    {
      this.ValidateReadable();
      Value = this.GetDate(RecordNumber);
    }

    internal override void PutObject(object Value, long RecordNumber = 0, bool ContainedInVariant = true)
    {
      this.ValidateWriteable();
      if (Value == null)
      {
        this.PutEmpty(RecordNumber);
      }
      else
      {
        Type type = Value.GetType();
        if (type == null)
          throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedIOType1", new string[1]
          {
            "Empty"
          })), 5);
        if (type.IsArray)
        {
          this.PutDynamicArray(RecordNumber, (Array) Value, true, -1);
        }
        else
        {
          if (type.IsEnum)
            type = Enum.GetUnderlyingType(type);
          switch (Type.GetTypeCode(type))
          {
            case TypeCode.DBNull:
              this.PutShort(RecordNumber, (short) 1, false);
              break;
            case TypeCode.Boolean:
              this.PutBoolean(RecordNumber, BooleanType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.Char:
              this.PutChar(RecordNumber, CharType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.Byte:
              this.PutByte(RecordNumber, ByteType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.Int16:
              this.PutShort(RecordNumber, ShortType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.Int32:
              this.PutInteger(RecordNumber, IntegerType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.Int64:
              this.PutLong(RecordNumber, LongType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.Single:
              this.PutSingle(RecordNumber, SingleType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.Double:
              this.PutDouble(RecordNumber, DoubleType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.Decimal:
              this.PutDecimal(RecordNumber, DecimalType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.DateTime:
              this.PutDate(RecordNumber, DateType.FromObject(Value), ContainedInVariant);
              break;
            case TypeCode.String:
              this.PutVariantString(RecordNumber, Value.ToString());
              break;
            default:
              if (type == typeof (Missing))
                throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedIOType1", new string[1]
                {
                  "Missing"
                })), 5);
              if (type.IsValueType && !ContainedInVariant)
              {
                this.PutRecord(RecordNumber, (ValueType) Value);
                break;
              }
              if (ContainedInVariant && type.IsValueType)
                throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_PutObjectOfValueType1", new string[1]
                {
                  Utils.VBFriendlyName(type, Value)
                })), 5);
              throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("Argument_UnsupportedIOType1", new string[1]
              {
                Utils.VBFriendlyName(type, Value)
              })), 5);
          }
        }
      }
    }

    internal override void Put(ValueType Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutRecord(RecordNumber, Value);
    }

    internal override void Put(Array Value, long RecordNumber = 0, bool ArrayIsDynamic = false, bool StringIsFixedLength = false)
    {
      this.ValidateWriteable();
      if (Value == null)
      {
        this.PutEmpty(RecordNumber);
      }
      else
      {
        int upperBound = Value.GetUpperBound(0);
        int SecondBound = -1;
        int FixedStringLength = -1;
        if (Value.Rank == 2)
          SecondBound = Value.GetUpperBound(1);
        if (StringIsFixedLength)
          FixedStringLength = 0;
        Type elementType = Value.GetType().GetElementType();
        if (ArrayIsDynamic)
          this.PutDynamicArray(RecordNumber, Value, false, FixedStringLength);
        else
          this.PutFixedArray(RecordNumber, Value, elementType, FixedStringLength, upperBound, SecondBound);
      }
    }

    internal override void Put(bool Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutBoolean(RecordNumber, Value, false);
    }

    internal override void Put(byte Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutByte(RecordNumber, Value, false);
    }

    internal override void Put(short Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutShort(RecordNumber, Value, false);
    }

    internal override void Put(int Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutInteger(RecordNumber, Value, false);
    }

    internal override void Put(long Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutLong(RecordNumber, Value, false);
    }

    internal override void Put(char Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutChar(RecordNumber, Value, false);
    }

    internal override void Put(float Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutSingle(RecordNumber, Value, false);
    }

    internal override void Put(double Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutDouble(RecordNumber, Value, false);
    }

    internal override void Put(Decimal Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutCurrency(RecordNumber, Value, false);
    }

    internal override void Put(string Value, long RecordNumber = 0, bool StringIsFixedLength = false)
    {
      this.ValidateWriteable();
      if (StringIsFixedLength)
        this.PutString(RecordNumber, Value);
      else
        this.PutStringWithLength(RecordNumber, Value);
    }

    internal override void Put(DateTime Value, long RecordNumber = 0)
    {
      this.ValidateWriteable();
      this.PutDate(RecordNumber, Value, false);
    }

    protected void ValidateWriteable()
    {
      if (this.m_access != OpenAccess.ReadWrite && this.m_access != OpenAccess.Write)
        throw ExceptionUtils.VbMakeExceptionEx(75, Utils.GetResourceString("FileOpenedNoWrite"));
    }

    protected void ValidateReadable()
    {
      if (this.m_access != OpenAccess.ReadWrite && this.m_access != OpenAccess.Read)
        throw ExceptionUtils.VbMakeExceptionEx(75, Utils.GetResourceString("FileOpenedNoRead"));
    }
  }
}
