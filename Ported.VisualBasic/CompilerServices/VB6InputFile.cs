// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.VB6InputFile
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Threading;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class VB6InputFile : VB6File
  {
    public VB6InputFile(string FileName, OpenShare share)
      : base(FileName, OpenAccess.Read, share, -1)
    {
    }

    internal override void OpenFile()
    {
      try
      {
        this.m_file = new FileStream(this.m_sFullPath, FileMode.Open, (FileAccess) this.m_access, (FileShare) this.m_share);
      }
      catch (FileNotFoundException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 53);
      }
      catch (SecurityException ex)
      {
        throw ExceptionUtils.VbMakeException(53);
      }
      catch (DirectoryNotFoundException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 76);
      }
      catch (IOException ex)
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
        throw ExceptionUtils.VbMakeException(ex, 76);
      }
      this.m_Encoding = Utils.GetFileIOEncoding();
      this.m_sr = new StreamReader((Stream) this.m_file, this.m_Encoding, false, 128);
      this.m_eof = this.m_file.Length == 0L;
    }

    public string ReadLine()
    {
      checked { this.m_position += (long) (this.m_Encoding.GetByteCount(this.m_sr.ReadLine()) + 2); }
      return (string) null;
    }

    internal override bool CanInput()
    {
      return true;
    }

    internal override bool EOF()
    {
      return this.m_eof;
    }

    public override OpenMode GetMode()
    {
      return OpenMode.Input;
    }

    internal object ParseInputString(ref string sInput)
    {
      object obj = (object) sInput;
      if (sInput[0] == '#' && sInput.Length != 1)
      {
        sInput = sInput.Substring(1, checked (sInput.Length - 2));
        if (Operators.CompareString(sInput, "NULL", false) == 0)
          obj = (object) DBNull.Value;
        else if (Operators.CompareString(sInput, "TRUE", false) == 0)
          obj = (object) true;
        else if (Operators.CompareString(sInput, "FALSE", false) == 0)
          obj = (object) false;
        else if (Operators.CompareString(Strings.Left(sInput, 6), "ERROR ", false) == 0)
        {
          int num=0;
          if (sInput.Length > 6)
            num = IntegerType.FromString(Strings.Mid(sInput, 7));
          obj = (object) num;
        }
        else
        {
          try
          {
            obj = (object) DateTime.Parse(Utils.ToHalfwidthNumbers(sInput, Utils.GetCultureInfo()));
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
      }
      return obj;
    }
/*
    internal override void Input(ref object obj)
    {
      switch (this.SkipWhiteSpaceEOF())
      {
        case 34:
          this.m_sr.Read();
          checked { ++this.m_position; }
          obj = (object) this.ReadInField((short) 1);
          this.SkipTrailingWhiteSpace();
          break;
        case 35:
          ref object local = ref obj;
          string sInput = this.InputStr();
          object inputString = this.ParseInputString(ref sInput);
          local = inputString;
          break;
        default:
          string str = this.ReadInField((short) 3);
          obj = Conversion.ParseInputField((object) str, VariantType.Empty);
          this.SkipTrailingWhiteSpace();
          break;
      }
    }
*/
    internal override void Input(ref bool Value)
    {
      ref bool local = ref Value;
      string sInput = this.InputStr();
      int num = BooleanType.FromObject(this.ParseInputString(ref sInput)) ? 1 : 0;
      local = num != 0;
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

    internal override void Input(ref char Value)
    {
      string str = this.InputStr();
      if (str.Length > 0)
        Value = str[0];
      else
        Value = char.MinValue;
    }

    internal override void Input(ref float Value)
    {
      Value = SingleType.FromObject(this.InputNum(VariantType.Single), Utils.GetInvariantCultureInfo().NumberFormat);
    }

    internal override void Input(ref double Value)
    {
      Value = DoubleType.FromObject(this.InputNum(VariantType.Double), Utils.GetInvariantCultureInfo().NumberFormat);
    }

    internal override void Input(ref Decimal Value)
    {
      Value = DecimalType.FromObject(this.InputNum(VariantType.Decimal), Utils.GetInvariantCultureInfo().NumberFormat);
    }

    internal override void Input(ref string Value)
    {
      Value = this.InputStr();
    }

    internal override void Input(ref DateTime Value)
    {
      ref DateTime local = ref Value;
      string sInput = this.InputStr();
      DateTime dateTime = DateType.FromObject(this.ParseInputString(ref sInput));
      local = dateTime;
    }

    internal override long LOC()
    {
      return checked (this.m_position + (long) sbyte.MaxValue) / 128L;
    }
  }
}
