// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.VB6BinaryFile
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.IO;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class VB6BinaryFile : VB6RandomFile
  {
    public VB6BinaryFile(string FileName, OpenAccess access, OpenShare share)
      : base(FileName, access, share, -1)
    {
    }

    internal override void Lock(long lStart, long lEnd)
    {
      if (lStart > lEnd)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Start"
        }));
      long num = this.m_lRecordLen != -1 ? (long) this.m_lRecordLen : 1L;
      this.m_file.Lock(checked (lStart - 1L * num), checked (lEnd - lStart + 1L * num));
    }

    internal override void Unlock(long lStart, long lEnd)
    {
      if (lStart > lEnd)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Start"
        }));
      long num = this.m_lRecordLen != -1 ? (long) this.m_lRecordLen : 1L;
      this.m_file.Unlock(checked (lStart - 1L * num), checked (lEnd - lStart + 1L * num));
    }

    public override OpenMode GetMode()
    {
      return OpenMode.Binary;
    }

    internal override long Seek()
    {
      return checked (this.m_position + 1L);
    }

    internal override void Seek(long BaseOnePosition)
    {
      if (BaseOnePosition <= 0L)
        throw ExceptionUtils.VbMakeException(63);
      long num = checked (BaseOnePosition - 1L);
      this.m_file.Position = num;
      this.m_position = num;
      if (this.m_sr == null)
        return;
      this.m_sr.DiscardBufferedData();
    }

    internal override long LOC()
    {
      return this.m_position;
    }

    internal override bool CanInput()
    {
      return true;
    }

    internal override bool CanWrite()
    {
      return true;
    }

    internal override void Input(ref object Value)
    {
      Value = (object) this.InputStr();
    }

    internal override void Input(ref string Value)
    {
      Value = this.InputStr();
    }

    internal override void Input(ref char Value)
    {
      string str = this.InputStr();
      if (str.Length > 0)
        Value = str[0];
      else
        Value = char.MinValue;
    }

    internal override void Input(ref bool Value)
    {
      Value = BooleanType.FromString(this.InputStr());
    }

    internal override void Input(ref byte Value)
    {
      Value = ByteType.FromObject(this.InputNum(VariantType.Byte));
    }

    internal override void Input(ref short Value)
    {
      Value = ShortType.FromObject(this.InputNum(VariantType.Short));
    }

    internal override void Input(ref int Value)
    {
      Value = IntegerType.FromObject(this.InputNum(VariantType.Integer));
    }

    internal override void Input(ref long Value)
    {
      Value = LongType.FromObject(this.InputNum(VariantType.Long));
    }

    internal override void Input(ref float Value)
    {
      Value = SingleType.FromObject(this.InputNum(VariantType.Single));
    }

    internal override void Input(ref double Value)
    {
      Value = DoubleType.FromObject(this.InputNum(VariantType.Double));
    }

    internal override void Input(ref Decimal Value)
    {
      Value = DecimalType.FromObject(this.InputNum(VariantType.Decimal));
    }

    internal override void Input(ref DateTime Value)
    {
      Value = DateType.FromString(this.InputStr(), Utils.GetCultureInfo());
    }

    internal override void Put(string Value, long RecordNumber = 0, bool StringIsFixedLength = false)
    {
      this.ValidateWriteable();
      this.PutString(RecordNumber, Value);
    }

    internal override void Get(ref string Value, long RecordNumber = 0, bool StringIsFixedLength = false)
    {
      this.ValidateReadable();
      int ByteLength = Value != null ? this.m_Encoding.GetByteCount(Value) : 0;
      Value = this.GetFixedLengthString(RecordNumber, ByteLength);
    }

    protected override string InputStr()
    {
      if (this.m_access != OpenAccess.ReadWrite && this.m_access != OpenAccess.Read)
        throw new NullReferenceException(new NullReferenceException().Message, (Exception) new IOException(Utils.GetResourceString("FileOpenedNoRead")));
      string str;
      if (this.SkipWhiteSpaceEOF() == 34)
      {
        this.m_sr.Read();
        checked { ++this.m_position; }
        str = this.ReadInField((short) 1);
      }
      else
        str = this.ReadInField((short) 2);
      this.SkipTrailingWhiteSpace();
      return str;
    }
  }
}
