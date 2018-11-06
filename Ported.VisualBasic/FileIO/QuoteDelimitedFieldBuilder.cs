// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.QuoteDelimitedFieldBuilder
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System.Text;
using System.Text.RegularExpressions;

namespace Ported.VisualBasic.FileIO
{
  internal class QuoteDelimitedFieldBuilder
  {
    private StringBuilder m_Field;
    private bool m_FieldFinished;
    private int m_Index;
    private int m_DelimiterLength;
    private Regex m_DelimiterRegex;
    private string m_SpaceChars;
    private bool m_MalformedLine;

    public QuoteDelimitedFieldBuilder(Regex DelimiterRegex, string SpaceChars)
    {
      this.m_Field = new StringBuilder();
      this.m_DelimiterRegex = DelimiterRegex;
      this.m_SpaceChars = SpaceChars;
    }

    public bool FieldFinished
    {
      get
      {
        return this.m_FieldFinished;
      }
    }

    public string Field
    {
      get
      {
        return this.m_Field.ToString();
      }
    }

    public int Index
    {
      get
      {
        return this.m_Index;
      }
    }

    public int DelimiterLength
    {
      get
      {
        return this.m_DelimiterLength;
      }
    }

    public bool MalformedLine
    {
      get
      {
        return this.m_MalformedLine;
      }
    }

    public void BuildField(string Line, int StartAt)
    {
      this.m_Index = StartAt;
      int length = Line.Length;
      while (this.m_Index < length)
      {
        if (Line[this.m_Index] == '"')
        {
          if (checked (this.m_Index + 1) == length)
          {
            this.m_FieldFinished = true;
            this.m_DelimiterLength = 1;
            checked { ++this.m_Index; }
            break;
          }
          if (checked (this.m_Index + 1) < Line.Length & Line[checked (this.m_Index + 1)] == '"')
          {
            this.m_Field.Append('"');
            checked { this.m_Index += 2; }
          }
          else
          {
            Match match = this.m_DelimiterRegex.Match(Line, checked (this.m_Index + 1));
            int num1 = match.Success ? checked (match.Index - 1) : checked (length - 1);
            int num2 = checked (this.m_Index + 1);
            int num3 = num1;
            int index = num2;
            while (index <= num3)
            {
              if (this.m_SpaceChars.IndexOf(Line[index]) < 0)
              {
                this.m_MalformedLine = true;
                return;
              }
              checked { ++index; }
            }
            this.m_DelimiterLength = checked (1 + num1 - this.m_Index);
            if (match.Success)
              checked { this.m_DelimiterLength += match.Length; }
            this.m_FieldFinished = true;
            break;
          }
        }
        else
        {
          this.m_Field.Append(Line[this.m_Index]);
          checked { ++this.m_Index; }
        }
      }
    }
  }
}
