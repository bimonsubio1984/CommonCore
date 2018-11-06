// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.VB6File
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal abstract class VB6File
  {
    internal int m_lCurrentColumn;
    internal int m_lWidth;
    internal int m_lRecordLen;
    internal long m_lRecordStart;
    internal string m_sFullPath;
    internal OpenShare m_share;
    internal OpenAccess m_access;
    internal bool m_eof;
    internal long m_position;
    internal FileStream m_file;
    internal bool m_fAppend;
    internal bool m_bPrint;
    protected StreamWriter m_sw;
    protected StreamReader m_sr;
    protected BinaryWriter m_bw;
    protected BinaryReader m_br;
    protected Encoding m_Encoding;
    protected const int lchTab = 9;
    protected const int lchCR = 13;
    protected const int lchLF = 10;
    protected const int lchSpace = 32;
    protected const int lchIntlSpace = 12288;
    protected const int lchDoubleQuote = 34;
    protected const int lchPound = 35;
    protected const int lchComma = 44;
    protected const int EOF_INDICATOR = -1;
    protected const int EOF_CHAR = 26;
    protected const short FIN_NUMTERMCHAR = 6;
    protected const short FIN_LINEINP = 0;
    protected const short FIN_QSTRING = 1;
    protected const short FIN_STRING = 2;
    protected const short FIN_NUMBER = 3;

    protected VB6File()
    {
    }

    protected VB6File(string sPath, OpenAccess access, OpenShare share, int lRecordLen)
    {
      if (access != OpenAccess.Read && access != OpenAccess.ReadWrite && access != OpenAccess.Write)
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Access"
        }));
      this.m_access = access;
      if (share != OpenShare.Shared && share != OpenShare.LockRead && (share != OpenShare.LockReadWrite && share != OpenShare.LockWrite))
        throw new ArgumentException(Utils.GetResourceString("Argument_InvalidValue1", new string[1]
        {
          "Share"
        }));
      this.m_share = share;
      this.m_lRecordLen = lRecordLen;
      this.m_sFullPath = new FileInfo(sPath).FullName;
    }

    internal string GetAbsolutePath()
    {
      return this.m_sFullPath;
    }

    internal virtual void OpenFile()
    {
      try
      {
        if (File.Exists(this.m_sFullPath))
          this.m_file = new FileStream(this.m_sFullPath, FileMode.Open, (FileAccess) this.m_access, (FileShare) this.m_share);
        else
          this.m_file = new FileStream(this.m_sFullPath, FileMode.Create, (FileAccess) this.m_access, (FileShare) this.m_share);
      }
      catch (SecurityException ex)
      {
        throw ExceptionUtils.VbMakeException(53);
      }
    }

    internal virtual void CloseFile()
    {
      this.CloseTheFile();
    }

    protected void CloseTheFile()
    {
      if (this.m_sw != null)
      {
        this.m_sw.Close();
        this.m_sw = (StreamWriter) null;
      }
      if (this.m_sr != null)
      {
        this.m_sr.Close();
        this.m_sr = (StreamReader) null;
      }
      if (this.m_file == null)
        return;
      this.m_file.Close();
      this.m_file = (FileStream) null;
    }

    internal int GetColumn()
    {
      return this.m_lCurrentColumn;
    }

    internal void SetColumn(int lColumn)
    {
      if (this.m_lWidth != 0 && this.m_lCurrentColumn != 0 && checked (lColumn + 14) > this.m_lWidth)
        this.WriteLine((string) null);
      else
        this.SPC(checked (lColumn - this.m_lCurrentColumn));
    }

    internal int GetWidth()
    {
      return this.m_lWidth;
    }

    internal void SetWidth(int RecordWidth)
    {
      if (RecordWidth < 0 || RecordWidth > (int) byte.MaxValue)
        throw ExceptionUtils.VbMakeException(5);
      this.m_lWidth = RecordWidth;
    }

    internal virtual void WriteLine(string s)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void WriteString(string s)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual bool EOF()
    {
      return this.m_eof;
    }

    internal long LOF()
    {
      return this.m_file.Length;
    }

    internal virtual long LOC()
    {
      if (this.m_lRecordLen == -1 || this.GetMode() != OpenMode.Random)
        return checked (this.m_position + 1L);
      if (this.m_lRecordLen == 0)
        throw ExceptionUtils.VbMakeException(51);
      if (this.m_position == 0L)
        return 0;
      return checked (unchecked (this.m_position / (long) this.m_lRecordLen) + 1L);
    }

    internal virtual StreamReader GetStreamReader()
    {
      return this.m_sr;
    }

    internal void SetRecord(long RecordNumber)
    {
      if (this.m_lRecordLen == 0 || RecordNumber == 0L)
        return;
      long offset=0;
      if (this.m_lRecordLen == -1)
      {
        if (RecordNumber == -1L)
          return;
        offset = checked (RecordNumber - 1L);
      }
      else
      {
        switch (RecordNumber)
        {
          case -1:
            long pos = this.GetPos();
            if (pos == 0L)
            {
              this.m_lRecordStart = 0L;
              return;
            }
            if (pos % (long) this.m_lRecordLen == 0L)
            {
              this.m_lRecordStart = pos;
              return;
            }
            offset = checked ((long) this.m_lRecordLen * unchecked (pos / (long) this.m_lRecordLen) + 1L);
            break;
          case 0:
            break;
          default:
            offset = this.m_lRecordLen != -1 ? checked (RecordNumber - 1L * (long) this.m_lRecordLen) : RecordNumber;
            break;
        }
      }
      this.SeekOffset(offset);
      this.m_lRecordStart = offset;
    }

    internal virtual void Seek(long BaseOnePosition)
    {
      if (BaseOnePosition <= 0L)
        throw ExceptionUtils.VbMakeException(63);
      long num = checked (BaseOnePosition - 1L);
      if (num > this.m_file.Length)
        this.m_file.SetLength(num);
      this.m_file.Position = num;
      this.m_position = num;
      this.m_eof = this.m_position >= this.m_file.Length;
      if (this.m_sr == null)
        return;
      this.m_sr.DiscardBufferedData();
    }

    internal virtual long Seek()
    {
      return checked (this.m_position + 1L);
    }

    internal void SeekOffset(long offset)
    {
      this.m_position = offset;
      this.m_file.Position = offset;
      if (this.m_sr == null)
        return;
      this.m_sr.DiscardBufferedData();
    }

    internal long GetPos()
    {
      return this.m_position;
    }

    internal virtual void Lock()
    {
      this.m_file.Lock(0L, (long) int.MaxValue);
    }

    internal virtual void Unlock()
    {
      this.m_file.Unlock(0L, (long) int.MaxValue);
    }

    internal virtual void Lock(long Record)
    {
      if (this.m_lRecordLen == -1)
        this.m_file.Lock(checked (Record - 1L), 1L);
      else
        this.m_file.Lock(checked (Record - 1L * (long) this.m_lRecordLen), (long) this.m_lRecordLen);
    }

    internal virtual void Unlock(long Record)
    {
      if (this.m_lRecordLen == -1)
        this.m_file.Unlock(checked (Record - 1L), 1L);
      else
        this.m_file.Unlock(checked (Record - 1L * (long) this.m_lRecordLen), (long) this.m_lRecordLen);
    }

    internal virtual void Lock(long RecordStart, long RecordEnd)
    {
      if (this.m_lRecordLen == -1)
        this.m_file.Lock(checked (RecordStart - 1L), checked (RecordEnd - RecordStart + 1L));
      else
        this.m_file.Lock(checked (RecordStart - 1L * (long) this.m_lRecordLen), checked (RecordEnd - RecordStart + 1L * (long) this.m_lRecordLen));
    }

    internal virtual void Unlock(long RecordStart, long RecordEnd)
    {
      if (this.m_lRecordLen == -1)
        this.m_file.Unlock(checked (RecordStart - 1L), checked (RecordEnd - RecordStart + 1L));
      else
        this.m_file.Unlock(checked (RecordStart - 1L * (long) this.m_lRecordLen), checked (RecordEnd - RecordStart + 1L * (long) this.m_lRecordLen));
    }

    internal string LineInput()
    {
      this.ValidateReadable();
      string s = this.m_sr.ReadLine() ?? "";
      checked { this.m_position += (long) (this.m_Encoding.GetByteCount(s) + 2); }
      this.m_eof = this.CheckEOF(this.m_sr.Peek());
      return s;
    }

    internal virtual bool CanInput()
    {
      return false;
    }

    internal virtual bool CanWrite()
    {
      return false;
    }

    protected virtual void InputObject(ref object Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    protected virtual string InputStr()
    {
      this.ValidateReadable();
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

    protected virtual object InputNum(VariantType vt)
    {
      this.ValidateReadable();
      this.SkipWhiteSpaceEOF();
      object obj = (object) this.ReadInField((short) 3);
      this.SkipTrailingWhiteSpace();
      return obj;
    }

    public abstract OpenMode GetMode();

    internal string InputString(int lLen)
    {
      this.ValidateReadable();
      StringBuilder stringBuilder = new StringBuilder(lLen);
      OpenMode mode = this.GetMode();
      int num1 = 1;
      int num2 = lLen;
      int num3 = num1;
      while (num3 <= num2)
      {
        int CharCode;
        if (mode == OpenMode.Binary)
        {
          CharCode = this.m_br.Read();
          checked { ++this.m_position; }
          if (CharCode == -1)
            break;
        }
        else
        {
          if (mode != OpenMode.Input)
            throw ExceptionUtils.VbMakeException(54);
          CharCode = this.m_sr.Read();
          checked { ++this.m_position; }
          if (CharCode == -1 | CharCode == 26)
          {
            this.m_eof = true;
            throw ExceptionUtils.VbMakeException(62);
          }
        }
        if (CharCode != 0)
          stringBuilder.Append(Strings.ChrW(CharCode));
        checked { ++num3; }
      }
      this.m_eof = mode != OpenMode.Binary ? this.CheckEOF(this.m_sr.Peek()) : this.m_br.PeekChar() == -1;
      return stringBuilder.ToString();
    }

    internal void SPC(int iCount)
    {
      if (iCount <= 0)
        return;
      int num = this.GetColumn();
      int width = this.GetWidth();
      if (width != 0)
      {
        if (iCount >= width)
          iCount %= width;
        if (checked (iCount + num) > width)
        {
          checked { iCount -= width - num; }
          goto label_8;
        }
      }
      checked { iCount += num; }
      if (iCount >= num)
        goto label_9;
label_8:
      this.WriteLine((string) null);
      num = 0;
label_9:
      if (iCount <= num)
        return;
      this.WriteString(new string(' ', checked (iCount - num)));
    }

    internal void Tab(int Column)
    {
      if (Column < 1)
        Column = 1;
      checked { --Column; }
      int num = this.GetColumn();
      int width = this.GetWidth();
      if (width != 0 && Column >= width)
        Column %= width;
      if (Column < num)
      {
        this.WriteLine((string) null);
        num = 0;
      }
      if (Column <= num)
        return;
      this.WriteString(new string(' ', checked (Column - num)));
    }

    internal void SetPrintMode()
    {
      switch (this.GetMode())
      {
        case OpenMode.Input:
        case OpenMode.Random:
        case OpenMode.Binary:
          throw ExceptionUtils.VbMakeException(54);
        default:
          this.m_bPrint = true;
          break;
      }
    }

    internal static VT VTType(object VarName)
    {
      if (VarName == null)
        return VT.Variant;
      return VB6File.VTFromComType(VarName.GetType());
    }

    internal static VT VTFromComType(Type typ)
    {
      if (typ == null)
        return VT.Variant;
      if (typ.IsArray)
      {
        typ = typ.GetElementType();
        if (typ.IsArray)
          return VT.Variant | VT.Array;
        VT vt = VB6File.VTFromComType(typ);
        if ((vt & VT.Array) != VT.Empty)
          return VT.Variant | VT.Array;
        return vt | VT.Array;
      }
      if (typ.IsEnum)
        typ = Enum.GetUnderlyingType(typ);
      if (typ == null)
        return VT.Empty;
      switch (Type.GetTypeCode(typ))
      {
        case TypeCode.DBNull:
          return VT.DBNull;
        case TypeCode.Boolean:
          return VT.Boolean;
        case TypeCode.Char:
          return VT.Char;
        case TypeCode.Byte:
          return VT.Byte;
        case TypeCode.Int16:
          return VT.Short;
        case TypeCode.Int32:
          return VT.Integer;
        case TypeCode.Int64:
          return VT.Long;
        case TypeCode.Single:
          return VT.Single;
        case TypeCode.Double:
          return VT.Double;
        case TypeCode.Decimal:
          return VT.Decimal;
        case TypeCode.DateTime:
          return VT.Date;
        case TypeCode.String:
          return VT.String;
        default:
          if (typ == typeof (Missing) || typ == typeof (Exception) || typ.IsSubclassOf(typeof (Exception)))
            return VT.Error;
          return typ.IsValueType ? VT.Structure : VT.Variant;
      }
    }

    internal void PutFixedArray(long RecordNumber, Array arr, Type ElementType, int FixedStringLength = -1, int FirstBound = -1, int SecondBound = -1)
    {
      this.SetRecord(RecordNumber);
      if (ElementType == null)
        ElementType = arr.GetType().GetElementType();
      this.PutArrayData(arr, ElementType, FixedStringLength, FirstBound, SecondBound);
    }

    internal void PutDynamicArray(long RecordNumber, Array arr, bool ContainedInVariant = true, int FixedStringLength = -1)
    {
      int num = 0;
      int upperBound = 0;
      if (arr == null)
      {
        num = 0;
      }
      else
      {
        num = arr.Rank;
        upperBound = arr.GetUpperBound(0);
      }
      int SecondBound = 0;
      switch (num)
      {
        case 0:
          this.SetRecord(RecordNumber);
          if (ContainedInVariant)
          {
            VT vt = VB6File.VTType((object) arr);
            this.m_bw.Write((short) vt);
            checked { this.m_position += 2L; }
            if ((vt & VT.Array) == VT.Empty)
              throw ExceptionUtils.VbMakeException(458);
          }
          this.PutArrayDesc(arr);
          if (num == 0)
            break;
          this.PutArrayData(arr, arr.GetType().GetElementType(), FixedStringLength, upperBound, SecondBound);
          break;
        case 1:
          SecondBound = -1;
          goto case 0;
        case 2:
          SecondBound = arr.GetUpperBound(1);
          goto case 0;
        default:
          throw new ArgumentException(Utils.GetResourceString("Argument_UnsupportedArrayDimensions"));
      }
    }

    internal void LengthCheck(int Length)
    {
      if (this.m_lRecordLen == -1)
        return;
      if (Length > this.m_lRecordLen)
        throw ExceptionUtils.VbMakeException(59);
      if (checked (this.GetPos() + (long) Length) > checked (this.m_lRecordStart + (long) this.m_lRecordLen))
        throw ExceptionUtils.VbMakeException(59);
    }

    internal void PutFixedLengthString(long RecordNumber, string s, int lengthToWrite)
    {
      char Character = ' ';
      if (s == null)
        s = "";
      if (Operators.CompareString(s, "", false) == 0)
        Character = char.MinValue;
      int byteCount = this.m_Encoding.GetByteCount(s);
      if (byteCount > lengthToWrite)
      {
        if (byteCount == s.Length)
        {
          s = Strings.Left(s, lengthToWrite);
        }
        else
        {
          byte[] bytes = this.m_Encoding.GetBytes(s);
          s = this.m_Encoding.GetString(bytes, 0, lengthToWrite);
          byteCount = this.m_Encoding.GetByteCount(s);
          if (byteCount > lengthToWrite)
          {
            int index = checked (lengthToWrite - 1);
            while (index >= 0)
            {
              bytes[index] = (byte) 0;
              s = this.m_Encoding.GetString(bytes, 0, lengthToWrite);
              byteCount = this.m_Encoding.GetByteCount(s);
              if (byteCount > lengthToWrite)
                checked { index += -1; }
              else
                break;
            }
          }
        }
      }
      if (byteCount < lengthToWrite)
        s += Strings.StrDup(checked (lengthToWrite - byteCount), Character);
      this.SetRecord(RecordNumber);
      this.LengthCheck(lengthToWrite);
      this.m_sw.Write(s);
      checked { this.m_position += (long) lengthToWrite; }
    }

    internal void PutVariantString(long RecordNumber, string s)
    {
      if (s == null)
        s = "";
      int byteCount = this.m_Encoding.GetByteCount(s);
      this.SetRecord(RecordNumber);
      this.LengthCheck(checked (byteCount + 2 + 2));
      this.m_bw.Write((short) 8);
      this.m_bw.Write(checked ((short) byteCount));
      if (byteCount != 0)
        this.m_sw.Write(s);
      checked { this.m_position += (long) (byteCount + 2 + 2); }
    }

    internal void PutString(long RecordNumber, string s)
    {
      if (s == null)
        s = "";
      int byteCount = this.m_Encoding.GetByteCount(s);
      this.SetRecord(RecordNumber);
      this.LengthCheck(byteCount);
      if (byteCount != 0)
        this.m_sw.Write(s);
      checked { this.m_position += (long) byteCount; }
    }

    internal void PutStringWithLength(long RecordNumber, string s)
    {
      if (s == null)
        s = "";
      int byteCount = this.m_Encoding.GetByteCount(s);
      this.SetRecord(RecordNumber);
      this.LengthCheck(checked (byteCount + 2));
      this.m_bw.Write(checked ((short) byteCount));
      if (byteCount != 0)
        this.m_sw.Write(s);
      checked { this.m_position += (long) (byteCount + 2); }
    }

    internal void PutDate(long RecordNumber, DateTime dt, bool ContainedInVariant = false)
    {
      int Length = 8;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 7);
      this.m_bw.Write(dt.ToOADate());
      checked { this.m_position += (long) Length; }
    }

    internal void PutShort(long RecordNumber, short i, bool ContainedInVariant = false)
    {
      int Length = 2;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 2);
      this.m_bw.Write(i);
      checked { this.m_position += (long) Length; }
    }

    internal void PutInteger(long RecordNumber, int l, bool ContainedInVariant = false)
    {
      int Length = 4;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 3);
      this.m_bw.Write(l);
      checked { this.m_position += (long) Length; }
    }

    internal void PutLong(long RecordNumber, long l, bool ContainedInVariant = false)
    {
      int Length = 8;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 20);
      this.m_bw.Write(l);
      checked { this.m_position += (long) Length; }
    }

    internal void PutByte(long RecordNumber, byte byt, bool ContainedInVariant = false)
    {
      int Length = 1;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 17);
      this.m_bw.Write(byt);
      checked { this.m_position += (long) Length; }
    }

    internal void PutChar(long RecordNumber, char ch, bool ContainedInVariant = false)
    {
      int Length = 2;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 18);
      this.m_bw.Write(ch);
      checked { this.m_position += (long) Length; }
    }

    internal void PutSingle(long RecordNumber, float sng, bool ContainedInVariant = false)
    {
      int Length = 4;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 4);
      this.m_bw.Write(sng);
      checked { this.m_position += (long) Length; }
    }

    internal void PutDouble(long RecordNumber, double dbl, bool ContainedInVariant = false)
    {
      int Length = 8;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 5);
      this.m_bw.Write(dbl);
      checked { this.m_position += (long) Length; }
    }

    internal void PutEmpty(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      this.LengthCheck(2);
      this.m_bw.Write((short) 0);
      checked { this.m_position += 2L; }
    }

    internal void PutBoolean(long RecordNumber, bool b, bool ContainedInVariant = false)
    {
      int Length = 2;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 11);
      if (b)
        this.m_bw.Write((short) -1);
      else
        this.m_bw.Write((short) 0);
      checked { this.m_position += (long) Length; }
    }

    internal void PutDecimal(long RecordNumber, Decimal dec, bool ContainedInVariant = false)
    {
      int Length = 16;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 14);
      int[] bits = Decimal.GetBits(dec);
      byte num1 = checked ((byte) unchecked (bits[3] & int.MaxValue / 65536));
      int num2 = bits[0];
      int num3 = bits[1];
      int num4 = bits[2];
      byte num5 = 0;
      if ((bits[3] & int.MinValue) != 0)
        num5 = (byte) 128;
      this.m_bw.Write((short) 14);
      this.m_bw.Write(num1);
      this.m_bw.Write(num5);
      this.m_bw.Write(num4);
      this.m_bw.Write(num2);
      this.m_bw.Write(num3);
      checked { this.m_position += (long) Length; }
    }

    internal void PutCurrency(long RecordNumber, Decimal dec, bool ContainedInVariant = false)
    {
      int Length = 16;
      if (ContainedInVariant)
        checked { Length += 2; }
      this.SetRecord(RecordNumber);
      this.LengthCheck(Length);
      if (ContainedInVariant)
        this.m_bw.Write((short) 6);
      this.m_bw.Write(Decimal.ToOACurrency(dec));
      checked { this.m_position += (long) Length; }
    }

    internal void PutRecord(long RecordNumber, ValueType o)
    {
      if (o == null)
        throw new NullReferenceException();
      this.SetRecord(RecordNumber);
      IRecordEnum intfRecEnum = (IRecordEnum) new PutHandler(this);
      if (intfRecEnum == null)
        throw ExceptionUtils.VbMakeException(5);
      StructUtils.EnumerateUDT(o, intfRecEnum, false);
    }

    internal Type ComTypeFromVT(VT vtype)
    {
      switch (vtype)
      {
        case VT.Empty:
          return (Type) null;
        case VT.DBNull:
          return typeof (DBNull);
        case VT.Short:
          return typeof (short);
        case VT.Integer:
          return typeof (int);
        case VT.Single:
          return typeof (float);
        case VT.Double:
          return typeof (double);
        case VT.Date:
          return typeof (DateTime);
        case VT.String:
          return typeof (string);
        case VT.Error:
          return typeof (Exception);
        case VT.Boolean:
          return typeof (bool);
        case VT.Variant:
          return typeof (object);
        case VT.Decimal:
          return typeof (Decimal);
        case VT.Byte:
          return typeof (byte);
        case VT.Char:
          return typeof (char);
        case VT.Long:
          return typeof (long);
        default:
          throw ExceptionUtils.VbMakeException(458);
      }
    }

    internal void GetFixedArray(long RecordNumber, ref Array arr, Type FieldType, int FirstBound = -1, int SecondBound = -1, int FixedStringLength = -1)
    {
      arr = SecondBound != -1 ? Array.CreateInstance(FieldType, checked (FirstBound + 1), checked (SecondBound + 1)) : Array.CreateInstance(FieldType, checked (FirstBound + 1));
      this.SetRecord(RecordNumber);
      this.GetArrayData(arr, FieldType, FirstBound, SecondBound, FixedStringLength);
    }

    internal void GetDynamicArray(ref Array arr, Type t, int FixedStringLength = -1)
    {
      arr = this.GetArrayDesc(t);
      int rank = arr.Rank;
      int upperBound = arr.GetUpperBound(0);
      int SecondBound = rank != 1 ? arr.GetUpperBound(1) : -1;
      this.GetArrayData(arr, t, upperBound, SecondBound, FixedStringLength);
    }

    private void PutArrayDesc(Array arr)
    {
      short num1 = arr != null ? checked ((short) arr.Rank) : (short) 0;
      this.m_bw.Write(num1);
      checked { this.m_position += 2L; }
      if (num1 == (short) 0)
        return;
      int num2 = 0;
      int num3 = checked ((int) num1 - 1);
      int dimension = num2;
      while (dimension <= num3)
      {
        this.m_bw.Write(arr.GetLength(dimension));
        this.m_bw.Write(arr.GetLowerBound(dimension));
        checked { this.m_position += 8L; }
        checked { ++dimension; }
      }
    }

    internal Array GetArrayDesc(Type typ)
    {
      int num1 = (int) this.m_br.ReadInt16();
      checked { this.m_position += 2L; }
      if (num1 == 0)
        return Array.CreateInstance(typ, 0);
      int[] lengths = new int[checked (num1 - 1 + 1)];
      int[] lowerBounds = new int[checked (num1 - 1 + 1)];
      int num2 = 0;
      int num3 = checked (num1 - 1);
      int index = num2;
      while (index <= num3)
      {
        lengths[index] = this.m_br.ReadInt32();
        lowerBounds[index] = this.m_br.ReadInt32();
        checked { this.m_position += 8L; }
        checked { ++index; }
      }
      return Array.CreateInstance(typ, lengths, lowerBounds);
    }

    internal virtual string GetLengthPrefixedString(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      if (this.EOF())
        return "";
      return this.ReadString();
    }

    internal virtual string GetFixedLengthString(long RecordNumber, int ByteLength)
    {
      this.SetRecord(RecordNumber);
      return this.ReadString(ByteLength);
    }

    protected string ReadString(int ByteLength)
    {
      if (ByteLength == 0)
        return (string) null;
      byte[] bytes = this.m_br.ReadBytes(ByteLength);
      checked { this.m_position += (long) ByteLength; }
      return this.m_Encoding.GetString(bytes);
    }

    protected string ReadString()
    {
      int num = (int) this.m_br.ReadInt16();
      checked { this.m_position += 2L; }
      if (num == 0)
        return (string) null;
      this.LengthCheck(num);
      return this.ReadString(num);
    }

    internal DateTime GetDate(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      double d = this.m_br.ReadDouble();
      checked { this.m_position += 8L; }
      return DateTime.FromOADate(d);
    }

    internal short GetShort(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      short num = this.m_br.ReadInt16();
      checked { this.m_position += 2L; }
      return num;
    }

    internal int GetInteger(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      int num = this.m_br.ReadInt32();
      checked { this.m_position += 4L; }
      return num;
    }

    internal long GetLong(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      long num = this.m_br.ReadInt64();
      checked { this.m_position += 8L; }
      return num;
    }

    internal byte GetByte(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      byte num = this.m_br.ReadByte();
      checked { ++this.m_position; }
      return num;
    }

    internal char GetChar(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      char ch = this.m_br.ReadChar();
      checked { ++this.m_position; }
      return ch;
    }

    internal float GetSingle(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      float num = this.m_br.ReadSingle();
      checked { this.m_position += 4L; }
      return num;
    }

    internal double GetDouble(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      double num = this.m_br.ReadDouble();
      checked { this.m_position += 8L; }
      return num;
    }

    internal Decimal GetDecimal(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      int num1 = (int) this.m_br.ReadInt16();
      byte scale = this.m_br.ReadByte();
      byte num2 = this.m_br.ReadByte();
      int hi = this.m_br.ReadInt32();
      int lo = this.m_br.ReadInt32();
      int mid = this.m_br.ReadInt32();
      checked { this.m_position += 16L; }
      bool isNegative=false;
      if (num2 != (byte) 0)
        isNegative = true;
      return new Decimal(lo, mid, hi, isNegative, scale);
    }

    internal Decimal GetCurrency(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      long cy = this.m_br.ReadInt64();
      checked { this.m_position += 8L; }
      return Decimal.FromOACurrency(cy);
    }

    internal bool GetBoolean(long RecordNumber)
    {
      this.SetRecord(RecordNumber);
      short num = this.m_br.ReadInt16();
      checked { this.m_position += 2L; }
      return num != (short) 0;
    }

    internal void GetRecord(long RecordNumber, ref ValueType o, bool ContainedInVariant = false)
    {
      if (o == null)
        throw new NullReferenceException();
      this.SetRecord(RecordNumber);
      IRecordEnum intfRecEnum = (IRecordEnum) new GetHandler(this);
      if (intfRecEnum == null)
        throw ExceptionUtils.VbMakeException(5);
      StructUtils.EnumerateUDT(o, intfRecEnum, true);
    }

    internal void PutArrayData(Array arr, Type typ, int FixedStringLength, int FirstBound, int SecondBound)
    {
      string str1 = (string) null;
      char[] buffer1 = (char[]) null;
      int num1=0;
      int num2 = 0;
      if (arr == null)
      {
        num1 = -1;
        num2 = -1;
      }
      else if (arr.GetUpperBound(0) > FirstBound)
        throw new ArgumentException(Utils.GetResourceString("Argument_ArrayDimensionsDontMatch"));
      if (typ == null)
        typ = arr.GetType().GetElementType();
      VT vtype = VB6File.VTFromComType(typ);
      int num3;
      int num4;
      if (SecondBound == -1)
      {
        num3 = 0;
        num4 = FirstBound;
        if (arr != null)
          num1 = arr.GetUpperBound(0);
      }
      else
      {
        num3 = SecondBound;
        num4 = FirstBound;
        if (arr != null)
        {
          if (arr.Rank != 2 || arr.GetUpperBound(1) != SecondBound)
            throw new ArgumentException(Utils.GetResourceString("Argument_ArrayDimensionsDontMatch"));
          num1 = arr.GetUpperBound(0);
          num2 = arr.GetUpperBound(1);
        }
      }
      if (vtype == VT.String)
      {
        if (FixedStringLength == 0)
        {
          object obj = SecondBound != -1 ? arr.GetValue(0, 0) : arr.GetValue(0);
          if (obj != null)
            FixedStringLength = obj.ToString().Length;
        }
        if (FixedStringLength == 0)
          throw new ArgumentException(Utils.GetResourceString("Argument_InvalidFixedLengthString"));
        if (FixedStringLength > 0)
        {
          str1 = Strings.StrDup(FixedStringLength, ' ');
          buffer1 = str1.ToCharArray();
        }
      }
      int byteLength = this.GetByteLength(vtype);
      if (SecondBound == -1 && byteLength > 0 && num4 == num1)
      {
        int count = checked (byteLength * num4 + 1);
        if (checked (this.GetPos() + (long) count) <= checked (this.m_lRecordStart + (long) this.m_lRecordLen))
        {
          byte[] buffer2 = new byte[checked (count - 1 + 1)];
          Buffer.BlockCopy(arr, 0, (Array) buffer2, 0, count);
          this.m_bw.Write(buffer2);
          checked { this.m_position += (long) count; }
          return;
        }
      }
      int num5 = 0;
      int num6 = num3;
      int index2 = num5;
      while (index2 <= num6)
      {
        int num7 = 0;
        int num8 = num4;
        int num9 = num7;
        while (num9 <= num8)
        {
          object obj;
          try
          {
            obj = SecondBound != -1 ? (num9 > num1 || index2 > num2 ? (object) null : arr.GetValue(num9, index2)) : (num9 <= num1 ? arr.GetValue(num9) : (object) null);
          }
          catch (IndexOutOfRangeException ex)
          {
            obj = (object) 0;
          }
          switch (vtype)
          {
            case VT.Empty:
            case VT.DBNull:
              checked { ++num9; }
              continue;
            case VT.Short:
              this.LengthCheck(2);
              this.m_bw.Write(ShortType.FromObject(obj));
              checked { this.m_position += 2L; }
              goto case VT.Empty;
            case VT.Integer:
              this.LengthCheck(4);
              this.m_bw.Write(IntegerType.FromObject(obj));
              checked { this.m_position += 4L; }
              goto case VT.Empty;
            case VT.Single:
              this.LengthCheck(4);
              this.m_bw.Write(SingleType.FromObject(obj));
              checked { this.m_position += 4L; }
              goto case VT.Empty;
            case VT.Double:
              this.LengthCheck(8);
              this.m_bw.Write(DoubleType.FromObject(obj));
              checked { this.m_position += 8L; }
              goto case VT.Empty;
            case VT.Date:
              this.LengthCheck(8);
              this.m_bw.Write(DateType.FromObject(obj).ToOADate());
              checked { this.m_position += 8L; }
              goto case VT.Empty;
            case VT.String:
              string str2;
              int num10;
              if (obj == null)
              {
                if (FixedStringLength > 0)
                {
                  str2 = str1;
                  num10 = FixedStringLength;
                }
                else
                {
                  str2 = "";
                  num10 = 0;
                }
              }
              else
              {
                str2 = obj.ToString();
                num10 = this.m_Encoding.GetByteCount(str2);
                if (FixedStringLength > 0 && num10 > FixedStringLength)
                {
                  if (num10 == str2.Length)
                  {
                    str2 = Strings.Left(str2, FixedStringLength);
                    num10 = FixedStringLength;
                  }
                  else
                  {
                    str2 = this.m_Encoding.GetString(this.m_Encoding.GetBytes(str2), 0, FixedStringLength);
                    num10 = this.m_Encoding.GetByteCount(str2);
                  }
                }
              }
              if (num10 > (int) short.MaxValue)
                throw ExceptionUtils.VbMakeException((Exception) new ArgumentException(Utils.GetResourceString("FileIO_StringLengthExceeded")), 5);
              if (FixedStringLength > 0)
              {
                this.LengthCheck(FixedStringLength);
                this.m_sw.Write(str2);
                if (num10 < FixedStringLength)
                  this.m_sw.Write(buffer1, 0, checked (FixedStringLength - num10));
                checked { this.m_position += (long) FixedStringLength; }
                goto case VT.Empty;
              }
              else
              {
                this.LengthCheck(checked (num10 + 2));
                this.m_bw.Write(checked ((short) num10));
                this.m_sw.Write(str2);
                checked { this.m_position += (long) (2 + num10); }
                goto case VT.Empty;
              }
            case VT.Error:
              throw ExceptionUtils.VbMakeException(13);
            case VT.Boolean:
              this.LengthCheck(2);
              if (BooleanType.FromObject(obj))
                this.m_bw.Write((short) -1);
              else
                this.m_bw.Write((short) 0);
              checked { this.m_position += 2L; }
              goto case VT.Empty;
            case VT.Variant:
              this.PutObject(obj, 0L, true);
              goto case VT.Empty;
            case VT.Decimal:
              this.LengthCheck(8);
              this.m_bw.Write(Decimal.ToOACurrency(DecimalType.FromObject(obj)));
              checked { this.m_position += 8L; }
              goto case VT.Empty;
            case VT.Byte:
              this.LengthCheck(1);
              this.m_bw.Write(ByteType.FromObject(obj));
              checked { ++this.m_position; }
              goto case VT.Empty;
            case VT.Char:
              this.LengthCheck(2);
              this.m_bw.Write(CharType.FromObject(obj));
              checked { this.m_position += 2L; }
              goto case VT.Empty;
            case VT.Long:
              this.LengthCheck(8);
              this.m_bw.Write(LongType.FromObject(obj));
              checked { this.m_position += 8L; }
              goto case VT.Empty;
            case VT.Structure:
              this.PutObject(obj, 0L, false);
              goto case VT.Empty;
            default:
              if ((vtype & VT.Array) != VT.Empty)
                throw ExceptionUtils.VbMakeException(13);
              throw ExceptionUtils.VbMakeException(458);
          }
        }
        checked { ++index2; }
      }
    }

    internal void GetArrayData(Array arr, Type typ, int FirstBound = -1, int SecondBound = -1, int FixedStringLength = -1)
    {
      object obj = (object) null;
      if (arr == null)
        throw new ArgumentException(Utils.GetResourceString("Argument_ArrayNotInitialized"));
      if (typ == null)
        typ = arr.GetType().GetElementType();
      VT vtype = VB6File.VTFromComType(typ);
      int num1;
      int num2;
      if (SecondBound == -1)
      {
        num1 = 0;
        num2 = FirstBound;
      }
      else
      {
        num1 = SecondBound;
        num2 = FirstBound;
      }
      int byteLength = this.GetByteLength(vtype);
      if (SecondBound == -1 && byteLength > 0 && num2 == arr.GetUpperBound(0))
      {
        int count = checked (byteLength * num2 + 1);
        if (count <= checked (arr.Length * byteLength))
        {
          Buffer.BlockCopy((Array) this.m_br.ReadBytes(count), 0, arr, 0, count);
          checked { this.m_position += (long) count; }
          return;
        }
      }
      int num3 = 0;
      int num4 = num1;
      int index2 = num3;
      while (index2 <= num4)
      {
        int num5 = 0;
        int num6 = num2;
        int num7 = num5;
        while (num7 <= num6)
        {
          switch (vtype)
          {
            case VT.Empty:
            case VT.DBNull:
            case VT.Error:
              try
              {
                if (SecondBound == -1)
                  arr.SetValue(obj, num7);
                else
                  arr.SetValue(obj, num7, index2);
              }
              catch (IndexOutOfRangeException ex)
              {
                throw new ArgumentException(Utils.GetResourceString("Argument_ArrayDimensionsDontMatch"));
              }
              checked { ++num7; }
              continue;
            case VT.Short:
              obj = (object) this.m_br.ReadInt16();
              checked { this.m_position += 2L; }
              goto case VT.Empty;
            case VT.Integer:
              obj = (object) this.m_br.ReadInt32();
              checked { this.m_position += 4L; }
              goto case VT.Empty;
            case VT.Single:
              obj = (object) this.m_br.ReadSingle();
              checked { this.m_position += 4L; }
              goto case VT.Empty;
            case VT.Double:
              obj = (object) this.m_br.ReadDouble();
              checked { this.m_position += 8L; }
              goto case VT.Empty;
            case VT.Date:
              obj = (object) DateTime.FromOADate(this.m_br.ReadDouble());
              checked { this.m_position += 8L; }
              goto case VT.Empty;
            case VT.String:
              obj = FixedStringLength < 0 ? (object) this.ReadString() : (object) this.ReadString(FixedStringLength);
              goto case VT.Empty;
            case VT.Boolean:
              obj = (object) ((uint) this.m_br.ReadInt16() > 0U);
              checked { this.m_position += 2L; }
              goto case VT.Empty;
            case VT.Variant:
              obj = SecondBound != -1 ? arr.GetValue(num7, index2) : arr.GetValue(num7);
              this.GetObject(ref obj, 0L, true);
              goto case VT.Empty;
            case VT.Decimal:
              long cy = this.m_br.ReadInt64();
              checked { this.m_position += 8L; }
              obj = (object) Decimal.FromOACurrency(cy);
              goto case VT.Empty;
            case VT.Byte:
              obj = (object) this.m_br.ReadByte();
              checked { ++this.m_position; }
              goto case VT.Empty;
            case VT.Char:
              obj = (object) this.m_br.ReadChar();
              checked { ++this.m_position; }
              goto case VT.Empty;
            case VT.Long:
              obj = (object) this.m_br.ReadInt64();
              checked { this.m_position += 8L; }
              goto case VT.Empty;
            case VT.Structure:
              obj = SecondBound != -1 ? arr.GetValue(num7, index2) : arr.GetValue(num7);
              this.GetObject(ref obj, 0L, false);
              goto case VT.Empty;
            default:
              if ((vtype & VT.Array) == VT.Empty)
                throw ExceptionUtils.VbMakeException(458);
              vtype ^= VT.Array;
              if (vtype == VT.Variant)
                throw ExceptionUtils.VbMakeException(13);
              if (vtype > VT.Variant)
              {
                if (vtype != VT.Byte)
                {
                  if (vtype != VT.Decimal)
                  {
                    if (vtype != VT.Char)
                    {
                      if (vtype != VT.Long)
                        throw ExceptionUtils.VbMakeException(458);
                      goto case VT.Empty;
                    }
                    else
                      goto case VT.Empty;
                  }
                  else
                    goto case VT.Empty;
                }
                else
                  goto case VT.Empty;
              }
              else
                goto case VT.Empty;
          }
        }
        checked { ++index2; }
      }
    }

    private int GetByteLength(VT vtype)
    {
      switch (vtype)
      {
        case VT.Short:
          return 2;
        case VT.Integer:
          return 4;
        case VT.Single:
          return 4;
        case VT.Double:
          return 8;
        case VT.Byte:
          return 1;
        case VT.Long:
          return 8;
        default:
          return -1;
      }
    }

    private void PrintTab(TabInfo ti)
    {
      if (ti.Column == (short) -1)
      {
        int column = this.GetColumn();
        this.SetColumn(checked (column + 14 - unchecked (column % 14)));
      }
      else
        this.Tab((int) ti.Column);
    }

    private string AddSpaces(string s)
    {
      string negativeSign = Thread.CurrentThread.CurrentCulture.NumberFormat.NegativeSign;
      if (negativeSign.Length == 1)
      {
        if ((int) s[0] == (int) negativeSign[0])
          return s + " ";
      }
      else if (Operators.CompareString(Strings.Left(s, negativeSign.Length), negativeSign, false) == 0)
        return s + " ";
      return " " + s + " ";
    }

    internal void PrintLine(params object[] Output)
    {
      this.Print(Output);
      this.WriteLine((string) null);
    }

    internal void Print(params object[] Output)
    {
      this.SetPrintMode();
      if (Output == null || Output.Length == 0)
        return;
      int upperBound = Output.GetUpperBound(0);
      int num1 = -1;
      int num2 = 0;
      int num3 = upperBound;
      int index = num2;
      while (index <= num3)
      {
        string str = (string) null;
        object obj1 = Output[index];
        Type type;
        if (obj1 == null)
        {
          type = (Type) null;
        }
        else
        {
          type = obj1.GetType();
          if (type.IsEnum)
            type = Enum.GetUnderlyingType(type);
        }
        if (obj1 == null)
          str = "";
        string s;
        if (type == null)
        {
          s = "";
        }
        else
        {
          switch (Type.GetTypeCode(type))
          {
            case TypeCode.DBNull:
              s = "Null";
              break;
            case TypeCode.Boolean:
              s = StringType.FromBoolean(BooleanType.FromObject(obj1));
              break;
            case TypeCode.Char:
              s = StringType.FromChar(CharType.FromObject(obj1));
              break;
            case TypeCode.Byte:
              s = this.AddSpaces(StringType.FromByte(ByteType.FromObject(obj1)));
              break;
            case TypeCode.Int16:
              s = this.AddSpaces(StringType.FromShort(ShortType.FromObject(obj1)));
              break;
            case TypeCode.Int32:
              s = this.AddSpaces(StringType.FromInteger(IntegerType.FromObject(obj1)));
              break;
            case TypeCode.Int64:
              s = this.AddSpaces(StringType.FromLong(LongType.FromObject(obj1)));
              break;
            case TypeCode.Single:
              s = this.AddSpaces(StringType.FromSingle(SingleType.FromObject(obj1)));
              break;
            case TypeCode.Double:
              s = this.AddSpaces(StringType.FromDouble(DoubleType.FromObject(obj1)));
              break;
            case TypeCode.Decimal:
              s = this.AddSpaces(StringType.FromDecimal(DecimalType.FromObject(obj1)));
              break;
            case TypeCode.DateTime:
              s = StringType.FromDate(DateType.FromObject(obj1)) + " ";
              break;
            case TypeCode.String:
              s = obj1.ToString();
              break;
            default:
              if (type == typeof (TabInfo))
              {
                object obj2 = obj1;
                TabInfo tabInfo;
                tabInfo.Column = 0;
                this.PrintTab(obj2 != null ? (TabInfo) obj2 : tabInfo);
                num1 = index;
                goto label_34;
              }
              else if (type == typeof (SpcInfo))
              {
                object obj2 = obj1;
                SpcInfo spcInfo;
                spcInfo.Count = 0;
                this.SPC((int) (obj2 != null ? (SpcInfo) obj2 : spcInfo).Count);
                num1 = index;
                goto label_34;
              }
              else
              {
                if (type == typeof (Missing))
                {
                  s = "Error 448";
                  break;
                }
                throw new ArgumentException(Utils.GetResourceString("Argument_UnsupportedIOType1", new string[1]
                {
                  Utils.VBFriendlyName(type)
                }));
              }
          }
        }
        if (num1 != checked (index - 1))
        {
          int column = this.GetColumn();
          this.SetColumn(checked (column + 14 - unchecked (column % 14)));
        }
        this.WriteString(s);
label_34:
        checked { ++index; }
      }
    }

    internal void WriteLineHelper(params object[] Output)
    {
      this.InternalWriteHelper(Output);
      this.WriteLine((string) null);
    }

    internal void WriteHelper(params object[] Output)
    {
      this.InternalWriteHelper(Output);
      this.WriteString(",");
    }

    private void InternalWriteHelper(params object[] Output)
    {
      Type type1 = typeof (SpcInfo);
      Type type2 = type1;
      NumberFormatInfo numberFormat = Utils.GetInvariantCultureInfo().NumberFormat;
      int num = 0;
      int upperBound = Output.GetUpperBound(0);
      int index = num;
      while (index <= upperBound)
      {
        object obj1 = Output[index];
        if (obj1 == null)
        {
          this.WriteString("#ERROR 448#");
        }
        else
        {
          if (type2 != type1)
            this.WriteString(",");
          type2 = obj1.GetType();
          if (type2 == type1)
          {
            object obj2 = obj1;
            SpcInfo spcInfo;
            spcInfo.Count = 0;

            this.SPC((int) (obj2 != null ? (SpcInfo) obj2 : spcInfo).Count);
          }
          else if (type2 == typeof (TabInfo))
          {
            object obj2 = obj1;
            TabInfo tabInfo;
            tabInfo.Column = 0;
            TabInfo ti = obj2 != null ? (TabInfo) obj2 : tabInfo;
            if (ti.Column >= (short) 0)
              this.PrintTab(ti);
          }
          else if (type2 == typeof (Missing))
          {
            this.WriteString("#ERROR 448#");
          }
          else
          {
            switch (Type.GetTypeCode(type2))
            {
              case TypeCode.DBNull:
                this.WriteString("#NULL#");
                break;
              case TypeCode.Boolean:
                if (BooleanType.FromObject(obj1))
                {
                  this.WriteString("#TRUE#");
                  break;
                }
                this.WriteString("#FALSE#");
                break;
              case TypeCode.Char:
                this.WriteString(StringType.FromChar(CharType.FromObject(obj1)));
                break;
              case TypeCode.Byte:
                this.WriteString(StringType.FromByte(ByteType.FromObject(obj1)));
                break;
              case TypeCode.Int16:
                this.WriteString(StringType.FromShort(ShortType.FromObject(obj1)));
                break;
              case TypeCode.Int32:
                this.WriteString(StringType.FromInteger(IntegerType.FromObject(obj1)));
                break;
              case TypeCode.Int64:
                this.WriteString(StringType.FromLong(LongType.FromObject(obj1)));
                break;
              case TypeCode.Single:
                this.WriteString(this.IOStrFromSingle(SingleType.FromObject(obj1), numberFormat));
                break;
              case TypeCode.Double:
                this.WriteString(this.IOStrFromDouble(DoubleType.FromObject(obj1), numberFormat));
                break;
              case TypeCode.Decimal:
                this.WriteString(this.IOStrFromDecimal(DecimalType.FromObject(obj1), numberFormat));
                break;
              case TypeCode.DateTime:
                //this.WriteString(this.FormatUniversalDate(DateType.FromObject(obj1)));
                this.WriteString("Not implemented");
                break;
              case TypeCode.String:
                this.WriteString(this.GetQuotedString(obj1.ToString()));
                break;
              default:
                if (!(obj1 is char[]) || ((Array) obj1).Rank != 1)
                  throw ExceptionUtils.VbMakeException(5);
                this.WriteString(new string(CharArrayType.FromObject(obj1)));
                break;
            }
          }
        }
        checked { ++index; }
      }
    }

    private string IOStrFromSingle(float Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString((string) null, (IFormatProvider) NumberFormat);
    }

    private string IOStrFromDouble(double Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString((string) null, (IFormatProvider) NumberFormat);
    }

    private string IOStrFromDecimal(Decimal Value, NumberFormatInfo NumberFormat)
    {
      return Value.ToString("G29", (IFormatProvider) NumberFormat);
    }

    /*
    internal string FormatUniversalDate(DateTime dt)
    {
      string format = "T";
      bool flag=false;
      if (dt.Year != 0 || dt.Month != 1 || dt.Day != 1)
      {
        flag = true;
        format = "d";
      }
      if (checked (dt.Hour + dt.Minute + dt.Second) != 0 && flag)
        format = "F";
      return dt.ToString(format, (IFormatProvider) FileSystem.m_WriteDateFormatInfo);
    }
*/

    protected string GetQuotedString(string Value)
    {
      return "\"" + Value.Replace("\"", "\"\"") + "\"";
    }

    protected void ValidateRec(long RecordNumber)
    {
      if (RecordNumber < 1L)
        throw ExceptionUtils.VbMakeException(63);
    }

    internal virtual void GetObject(ref object Value, long RecordNumber = 0, bool ContainedInVariant = true)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref ValueType Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref Array Value, long RecordNumber = 0, bool ArrayIsDynamic = false, bool StringIsFixedLength = false)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref bool Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref byte Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref short Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref int Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref long Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref char Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref float Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref double Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref Decimal Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref string Value, long RecordNumber = 0, bool StringIsFixedLength = false)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Get(ref DateTime Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void PutObject(object Value, long RecordNumber = 0, bool ContainedInVariant = true)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(object Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(ValueType Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(Array Value, long RecordNumber = 0, bool ArrayIsDynamic = false, bool StringIsFixedLength = false)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(bool Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(byte Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(short Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(int Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(long Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(char Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(float Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(double Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(Decimal Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(string Value, long RecordNumber = 0, bool StringIsFixedLength = false)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Put(DateTime Value, long RecordNumber = 0)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref object obj)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref bool Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref byte Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref short Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref int Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref long Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref char Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref float Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref double Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref Decimal Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref string Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    internal virtual void Input(ref DateTime Value)
    {
      throw ExceptionUtils.VbMakeException(54);
    }

    protected int SkipWhiteSpace()
    {
      int num = this.m_sr.Peek();
      if (this.CheckEOF(num))
      {
        this.m_eof = true;
      }
      else
      {
        while (this.IntlIsSpace(num) || num == 9)
        {
          this.m_sr.Read();
          checked { ++this.m_position; }
          num = this.m_sr.Peek();
          if (this.CheckEOF(num))
          {
            this.m_eof = true;
            break;
          }
        }
      }
      return num;
    }

    private string GetFileInTerm(short iTermType)
    {
      switch (iTermType)
      {
        case 0:
          return "\r";
        case 1:
          return "\"";
        case 2:
          return ",\r";
        case 3:
          return " ,\t\r";
        case 6:
          return " ,\t\r";
        default:
          throw ExceptionUtils.VbMakeException(5);
      }
    }

    protected bool IntlIsSpace(int lch)
    {
      return lch == 32 | lch == 12288;
    }

    protected bool IntlIsDoubleQuote(int lch)
    {
      return lch == 34;
    }

    protected bool IntlIsComma(int lch)
    {
      return lch == 44;
    }

    protected int SkipWhiteSpaceEOF()
    {
      int lChar = this.SkipWhiteSpace();
      if (this.CheckEOF(lChar))
        throw ExceptionUtils.VbMakeException(62);
      return lChar;
    }

    protected void SkipTrailingWhiteSpace()
    {
      int num1 = this.m_sr.Peek();
      if (this.CheckEOF(num1))
      {
        this.m_eof = true;
      }
      else
      {
        int num2;
        if (this.IntlIsSpace(num1) || this.IntlIsDoubleQuote(num1) || num1 == 9)
        {
          num2 = this.m_sr.Read();
          checked { ++this.m_position; }
          num1 = this.m_sr.Peek();
          if (this.CheckEOF(num1))
          {
            this.m_eof = true;
            return;
          }
          while (this.IntlIsSpace(num1) || num1 == 9)
          {
            this.m_sr.Read();
            checked { ++this.m_position; }
            num1 = this.m_sr.Peek();
            if (this.CheckEOF(num1))
            {
              this.m_eof = true;
              return;
            }
          }
        }
        if (num1 == 13)
        {
          int lChar = this.m_sr.Read();
          checked { ++this.m_position; }
          if (this.CheckEOF(lChar))
          {
            this.m_eof = true;
            return;
          }
          if (this.m_sr.Peek() == 10)
          {
            num2 = this.m_sr.Read();
            checked { ++this.m_position; }
          }
        }
        else if (this.IntlIsComma(num1))
        {
          num2 = this.m_sr.Read();
          checked { ++this.m_position; }
        }
        if (!this.CheckEOF(this.m_sr.Peek()))
          return;
        this.m_eof = true;
      }
    }

    protected string ReadInField(short iTermType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string fileInTerm = this.GetFileInTerm(iTermType);
      int num = this.m_sr.Peek();
      if (this.CheckEOF(num))
      {
        this.m_eof = true;
      }
      else
      {
        while (fileInTerm.IndexOf(Strings.ChrW(num)) == -1)
        {
          int CharCode = this.m_sr.Read();
          checked { ++this.m_position; }
          if (CharCode != 0)
            stringBuilder.Append(Strings.ChrW(CharCode));
          num = this.m_sr.Peek();
          if (this.CheckEOF(num))
          {
            this.m_eof = true;
            break;
          }
        }
      }
      return iTermType == (short) 2 || iTermType == (short) 3 ? Strings.RTrim(stringBuilder.ToString()) : stringBuilder.ToString();
    }

    protected bool CheckEOF(int lChar)
    {
      return lChar == -1 || lChar == 26;
    }

    private void ValidateReadable()
    {
      if (this.m_access != OpenAccess.ReadWrite && this.m_access != OpenAccess.Read)
        throw new NullReferenceException(new NullReferenceException().Message, (Exception) new IOException(Utils.GetResourceString("FileOpenedNoRead")));
    }
  }
}
