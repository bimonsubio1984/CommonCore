// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.VB6OutputFile
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.IO;
using System.Security;

namespace Ported.VisualBasic.CompilerServices
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class VB6OutputFile : VB6File
  {
    public VB6OutputFile()
    {
    }

    public VB6OutputFile(string FileName, OpenShare share, bool fAppend)
      : base(FileName, OpenAccess.Write, share, -1)
    {
      this.m_fAppend = fAppend;
    }

    internal override void OpenFile()
    {
      try
      {
        if (this.m_fAppend)
        {
          if (File.Exists(this.m_sFullPath))
            this.m_file = new FileStream(this.m_sFullPath, FileMode.Open, (FileAccess) this.m_access, (FileShare) this.m_share);
          else
            this.m_file = new FileStream(this.m_sFullPath, FileMode.Create, (FileAccess) this.m_access, (FileShare) this.m_share);
        }
        else
          this.m_file = new FileStream(this.m_sFullPath, FileMode.Create, (FileAccess) this.m_access, (FileShare) this.m_share);
      }
      catch (FileNotFoundException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 53);
      }
      catch (SecurityException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 53);
      }
      catch (DirectoryNotFoundException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 76);
      }
      catch (IOException ex)
      {
        throw ExceptionUtils.VbMakeException((Exception) ex, 75);
      }
      this.m_Encoding = Utils.GetFileIOEncoding();
      this.m_sw = new StreamWriter((Stream) this.m_file, this.m_Encoding);
      this.m_sw.AutoFlush = true;
      if (!this.m_fAppend)
        return;
      long length = this.m_file.Length;
      this.m_file.Position = length;
      this.m_position = length;
    }

    internal override void WriteLine(string s)
    {
      if (s == null)
      {
        this.m_sw.WriteLine();
        checked { this.m_position += 2L; }
      }
      else
      {
        if (this.m_bPrint && this.m_lWidth != 0 && this.m_lCurrentColumn >= this.m_lWidth)
        {
          this.m_sw.WriteLine();
          checked { this.m_position += 2L; }
        }
        this.m_sw.WriteLine(s);
        checked { this.m_position += (long) (this.m_Encoding.GetByteCount(s) + 2); }
      }
      this.m_lCurrentColumn = 0;
    }

    internal override void WriteString(string s)
    {
      if (s == null || s.Length == 0)
        return;
      if (this.m_bPrint && this.m_lWidth != 0 && (this.m_lCurrentColumn >= this.m_lWidth || this.m_lCurrentColumn != 0 && checked (this.m_lCurrentColumn + s.Length) > this.m_lWidth))
      {
        this.m_sw.WriteLine();
        checked { this.m_position += 2L; }
        this.m_lCurrentColumn = 0;
      }
      this.m_sw.Write(s);
      checked { this.m_position += (long) this.m_Encoding.GetByteCount(s); }
      checked { this.m_lCurrentColumn += s.Length; }
    }

    internal override bool CanWrite()
    {
      return true;
    }

    public override OpenMode GetMode()
    {
      return !this.m_fAppend ? OpenMode.Output : OpenMode.Append;
    }

    internal override bool EOF()
    {
      return true;
    }

    internal override long LOC()
    {
      return checked (this.m_position + (long) sbyte.MaxValue) / 128L;
    }
  }
}
