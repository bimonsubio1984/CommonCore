// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.TextFieldParser
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace Ported.VisualBasic.FileIO
{
  /// <summary>Provides methods and properties for parsing structured text files.</summary>
  public class TextFieldParser : IDisposable
  {
    private bool m_Disposed;
    private TextReader m_Reader;
    private string[] m_CommentTokens;
    private long m_LineNumber;
    private bool m_EndOfData;
    private string m_ErrorLine;
    private long m_ErrorLineNumber;
    private FieldType m_TextFieldType;
    private int[] m_FieldWidths;
    private string[] m_Delimiters;
    private int[] m_FieldWidthsCopy;
    private string[] m_DelimitersCopy;
    private Regex m_DelimiterRegex;
    private Regex m_DelimiterWithEndCharsRegex;
    private const RegexOptions REGEX_OPTIONS = RegexOptions.CultureInvariant;
    private int[] m_WhitespaceCodes;
    private Regex m_BeginQuotesRegex;
    private Regex m_WhiteSpaceRegEx;
    private bool m_TrimWhiteSpace;
    private int m_Position;
    private int m_PeekPosition;
    private int m_CharsRead;
    private bool m_NeedPropertyCheck;
    private const int DEFAULT_BUFFER_LENGTH = 4096;
    private const int DEFAULT_BUILDER_INCREASE = 10;
    private char[] m_Buffer;
    private int m_LineLength;
    private bool m_HasFieldsEnclosedInQuotes;
    private string m_SpaceChars;
    private int m_MaxLineSize;
    private int m_MaxBufferSize;
    private const string BEGINS_WITH_QUOTE = "\\G[{0}]*\"";
    private const string ENDING_QUOTE = "\"[{0}]*";
    private bool m_LeaveOpen;

    /// <summary>Initializes a new instance of the <see langword="TextFieldParser" /> class.</summary>
    /// <param name="path">
    /// <see langword="String" />. The complete path of the file to be parsed.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is an empty string.</exception>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(string path)
    {
      this.m_CommentTokens = new string[0];
      this.m_LineNumber = 1L;
      this.m_EndOfData = false;
      this.m_ErrorLine = "";
      this.m_ErrorLineNumber = -1L;
      this.m_TextFieldType = FieldType.Delimited;
      this.m_WhitespaceCodes = new int[23]
      {
        9,
        11,
        12,
        32,
        133,
        160,
        5760,
        8192,
        8193,
        8194,
        8195,
        8196,
        8197,
        8198,
        8199,
        8200,
        8201,
        8202,
        8203,
        8232,
        8233,
        12288,
        65279
      };
      this.m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
      this.m_TrimWhiteSpace = true;
      this.m_Position = 0;
      this.m_PeekPosition = 0;
      this.m_CharsRead = 0;
      this.m_NeedPropertyCheck = true;
      this.m_Buffer = new char[4096];
      this.m_HasFieldsEnclosedInQuotes = true;
      this.m_MaxLineSize = 10000000;
      this.m_MaxBufferSize = 10000000;
      this.m_LeaveOpen = false;
      this.InitializeFromPath(path, Encoding.UTF8, true);
    }

    /// <summary>Initializes a new instance of the <see langword="TextFieldParser" /> class.</summary>
    /// <param name="path">
    /// <see langword="String" />. The complete path of the file to be parsed.</param>
    /// <param name="defaultEncoding">
    /// <see cref="T:System.Text.Encoding" />. The character encoding to use if encoding is not determined from file. Default is <see cref="P:System.Text.Encoding.UTF8" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is an empty string or <paramref name="defaultEncoding" /> is <see langword="Nothing" />.</exception>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(string path, Encoding defaultEncoding)
    {
      this.m_CommentTokens = new string[0];
      this.m_LineNumber = 1L;
      this.m_EndOfData = false;
      this.m_ErrorLine = "";
      this.m_ErrorLineNumber = -1L;
      this.m_TextFieldType = FieldType.Delimited;
      this.m_WhitespaceCodes = new int[23]
      {
        9,
        11,
        12,
        32,
        133,
        160,
        5760,
        8192,
        8193,
        8194,
        8195,
        8196,
        8197,
        8198,
        8199,
        8200,
        8201,
        8202,
        8203,
        8232,
        8233,
        12288,
        65279
      };
      this.m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
      this.m_TrimWhiteSpace = true;
      this.m_Position = 0;
      this.m_PeekPosition = 0;
      this.m_CharsRead = 0;
      this.m_NeedPropertyCheck = true;
      this.m_Buffer = new char[4096];
      this.m_HasFieldsEnclosedInQuotes = true;
      this.m_MaxLineSize = 10000000;
      this.m_MaxBufferSize = 10000000;
      this.m_LeaveOpen = false;
      this.InitializeFromPath(path, defaultEncoding, true);
    }

    /// <summary>Initializes a new instance of the <see langword="TextFieldParser" /> class.</summary>
    /// <param name="path">
    /// <see langword="String" />. The complete path of the file to be parsed.</param>
    /// <param name="defaultEncoding">
    /// <see cref="T:System.Text.Encoding" />. The character encoding to use if encoding is not determined from file. Default is <see cref="P:System.Text.Encoding.UTF8" />.</param>
    /// <param name="detectEncoding">
    /// <see langword="Boolean" />. Indicates whether to look for byte order marks at the beginning of the file. Default is <see langword="True" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="path" /> is an empty string or <paramref name="defaultEncoding" /> is <see langword="Nothing" />.</exception>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(string path, Encoding defaultEncoding, bool detectEncoding)
    {
      this.m_CommentTokens = new string[0];
      this.m_LineNumber = 1L;
      this.m_EndOfData = false;
      this.m_ErrorLine = "";
      this.m_ErrorLineNumber = -1L;
      this.m_TextFieldType = FieldType.Delimited;
      this.m_WhitespaceCodes = new int[23]
      {
        9,
        11,
        12,
        32,
        133,
        160,
        5760,
        8192,
        8193,
        8194,
        8195,
        8196,
        8197,
        8198,
        8199,
        8200,
        8201,
        8202,
        8203,
        8232,
        8233,
        12288,
        65279
      };
      this.m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
      this.m_TrimWhiteSpace = true;
      this.m_Position = 0;
      this.m_PeekPosition = 0;
      this.m_CharsRead = 0;
      this.m_NeedPropertyCheck = true;
      this.m_Buffer = new char[4096];
      this.m_HasFieldsEnclosedInQuotes = true;
      this.m_MaxLineSize = 10000000;
      this.m_MaxBufferSize = 10000000;
      this.m_LeaveOpen = false;
      this.InitializeFromPath(path, defaultEncoding, detectEncoding);
    }

    /// <summary>Initializes a new instance of the <see langword="TextFieldParser" /> class.</summary>
    /// <param name="stream">
    /// <see cref="T:System.IO.Stream" />. The stream to be parsed.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> cannot be read from.</exception>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(Stream stream)
    {
      this.m_CommentTokens = new string[0];
      this.m_LineNumber = 1L;
      this.m_EndOfData = false;
      this.m_ErrorLine = "";
      this.m_ErrorLineNumber = -1L;
      this.m_TextFieldType = FieldType.Delimited;
      this.m_WhitespaceCodes = new int[23]
      {
        9,
        11,
        12,
        32,
        133,
        160,
        5760,
        8192,
        8193,
        8194,
        8195,
        8196,
        8197,
        8198,
        8199,
        8200,
        8201,
        8202,
        8203,
        8232,
        8233,
        12288,
        65279
      };
      this.m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
      this.m_TrimWhiteSpace = true;
      this.m_Position = 0;
      this.m_PeekPosition = 0;
      this.m_CharsRead = 0;
      this.m_NeedPropertyCheck = true;
      this.m_Buffer = new char[4096];
      this.m_HasFieldsEnclosedInQuotes = true;
      this.m_MaxLineSize = 10000000;
      this.m_MaxBufferSize = 10000000;
      this.m_LeaveOpen = false;
      this.InitializeFromStream(stream, Encoding.UTF8, true);
    }

    /// <summary>Initializes a new instance of the <see langword="TextFieldParser" /> class.</summary>
    /// <param name="stream">
    /// <see cref="T:System.IO.Stream" />. The stream to be parsed.</param>
    /// <param name="defaultEncoding">
    /// <see cref="T:System.Text.Encoding" />. The character encoding to use if encoding is not determined from file. Default is <see cref="P:System.Text.Encoding.UTF8" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> or <paramref name="defaultEncoding" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> cannot be read from.</exception>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(Stream stream, Encoding defaultEncoding)
    {
      this.m_CommentTokens = new string[0];
      this.m_LineNumber = 1L;
      this.m_EndOfData = false;
      this.m_ErrorLine = "";
      this.m_ErrorLineNumber = -1L;
      this.m_TextFieldType = FieldType.Delimited;
      this.m_WhitespaceCodes = new int[23]
      {
        9,
        11,
        12,
        32,
        133,
        160,
        5760,
        8192,
        8193,
        8194,
        8195,
        8196,
        8197,
        8198,
        8199,
        8200,
        8201,
        8202,
        8203,
        8232,
        8233,
        12288,
        65279
      };
      this.m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
      this.m_TrimWhiteSpace = true;
      this.m_Position = 0;
      this.m_PeekPosition = 0;
      this.m_CharsRead = 0;
      this.m_NeedPropertyCheck = true;
      this.m_Buffer = new char[4096];
      this.m_HasFieldsEnclosedInQuotes = true;
      this.m_MaxLineSize = 10000000;
      this.m_MaxBufferSize = 10000000;
      this.m_LeaveOpen = false;
      this.InitializeFromStream(stream, defaultEncoding, true);
    }

    /// <summary>Initializes a new instance of the <see langword="TextFieldParser" /> class.</summary>
    /// <param name="stream">
    /// <see cref="T:System.IO.Stream" />. The stream to be parsed.</param>
    /// <param name="defaultEncoding">
    /// <see cref="T:System.Text.Encoding" />. The character encoding to use if encoding is not determined from file. Default is <see cref="P:System.Text.Encoding.UTF8" />.</param>
    /// <param name="detectEncoding">
    /// <see langword="Boolean" />. Indicates whether to look for byte order marks at the beginning of the file. Default is <see langword="True" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> or <paramref name="defaultEncoding" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> cannot be read from.</exception>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(Stream stream, Encoding defaultEncoding, bool detectEncoding)
    {
      this.m_CommentTokens = new string[0];
      this.m_LineNumber = 1L;
      this.m_EndOfData = false;
      this.m_ErrorLine = "";
      this.m_ErrorLineNumber = -1L;
      this.m_TextFieldType = FieldType.Delimited;
      this.m_WhitespaceCodes = new int[23]
      {
        9,
        11,
        12,
        32,
        133,
        160,
        5760,
        8192,
        8193,
        8194,
        8195,
        8196,
        8197,
        8198,
        8199,
        8200,
        8201,
        8202,
        8203,
        8232,
        8233,
        12288,
        65279
      };
      this.m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
      this.m_TrimWhiteSpace = true;
      this.m_Position = 0;
      this.m_PeekPosition = 0;
      this.m_CharsRead = 0;
      this.m_NeedPropertyCheck = true;
      this.m_Buffer = new char[4096];
      this.m_HasFieldsEnclosedInQuotes = true;
      this.m_MaxLineSize = 10000000;
      this.m_MaxBufferSize = 10000000;
      this.m_LeaveOpen = false;
      this.InitializeFromStream(stream, defaultEncoding, detectEncoding);
    }

    /// <summary>Initializes a new instance of the <see langword="TextFieldParser" /> class.</summary>
    /// <param name="stream">
    /// <see cref="T:System.IO.Stream" />. The stream to be parsed.</param>
    /// <param name="defaultEncoding">
    /// <see cref="T:System.Text.Encoding" />. The character encoding to use if encoding is not determined from file. Default is <see cref="P:System.Text.Encoding.UTF8" />.</param>
    /// <param name="detectEncoding">
    /// <see langword="Boolean" />. Indicates whether to look for byte order marks at the beginning of the file. Default is <see langword="True" />.</param>
    /// <param name="leaveOpen">
    /// <see langword="Boolean" />. Indicates whether to leave <paramref name="stream" /> open when the <see langword="TextFieldParser" /> object is closed. Default is <see langword="False" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="stream" /> or <paramref name="defaultEncoding" /> is <see langword="Nothing" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="stream" /> cannot be read from.</exception>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(Stream stream, Encoding defaultEncoding, bool detectEncoding, bool leaveOpen)
    {
      this.m_CommentTokens = new string[0];
      this.m_LineNumber = 1L;
      this.m_EndOfData = false;
      this.m_ErrorLine = "";
      this.m_ErrorLineNumber = -1L;
      this.m_TextFieldType = FieldType.Delimited;
      this.m_WhitespaceCodes = new int[23]
      {
        9,
        11,
        12,
        32,
        133,
        160,
        5760,
        8192,
        8193,
        8194,
        8195,
        8196,
        8197,
        8198,
        8199,
        8200,
        8201,
        8202,
        8203,
        8232,
        8233,
        12288,
        65279
      };
      this.m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
      this.m_TrimWhiteSpace = true;
      this.m_Position = 0;
      this.m_PeekPosition = 0;
      this.m_CharsRead = 0;
      this.m_NeedPropertyCheck = true;
      this.m_Buffer = new char[4096];
      this.m_HasFieldsEnclosedInQuotes = true;
      this.m_MaxLineSize = 10000000;
      this.m_MaxBufferSize = 10000000;
      this.m_LeaveOpen = false;
      this.m_LeaveOpen = leaveOpen;
      this.InitializeFromStream(stream, defaultEncoding, detectEncoding);
    }

    /// <summary>Initializes a new instance of the <see langword="TextFieldParser" /> class.</summary>
    /// <param name="reader">
    /// <see cref="T:System.IO.TextReader" />. The <see cref="T:System.IO.TextReader" /> stream to be parsed. </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="reader" /> is <see langword="Nothing" />.</exception>
    //[HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public TextFieldParser(TextReader reader)
    {
      this.m_CommentTokens = new string[0];
      this.m_LineNumber = 1L;
      this.m_EndOfData = false;
      this.m_ErrorLine = "";
      this.m_ErrorLineNumber = -1L;
      this.m_TextFieldType = FieldType.Delimited;
      this.m_WhitespaceCodes = new int[23]
      {
        9,
        11,
        12,
        32,
        133,
        160,
        5760,
        8192,
        8193,
        8194,
        8195,
        8196,
        8197,
        8198,
        8199,
        8200,
        8201,
        8202,
        8203,
        8232,
        8233,
        12288,
        65279
      };
      this.m_WhiteSpaceRegEx = new Regex("\\s", RegexOptions.CultureInvariant);
      this.m_TrimWhiteSpace = true;
      this.m_Position = 0;
      this.m_PeekPosition = 0;
      this.m_CharsRead = 0;
      this.m_NeedPropertyCheck = true;
      this.m_Buffer = new char[4096];
      this.m_HasFieldsEnclosedInQuotes = true;
      this.m_MaxLineSize = 10000000;
      this.m_MaxBufferSize = 10000000;
      this.m_LeaveOpen = false;
      if (reader == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (reader));
      this.m_Reader = reader;
      this.ReadToBuffer();
    }

    /// <summary>Defines comment tokens. A comment token is a string that, when placed at the beginning of a line, indicates that the line is a comment and should be ignored by the parser.</summary>
    /// <returns>A string array that contains all of the comment tokens for the <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object.</returns>
    /// <exception cref="T:System.ArgumentException">A comment token includes white space.</exception>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string[] CommentTokens
    {
      get
      {
        return this.m_CommentTokens;
      }
      set
      {
        this.CheckCommentTokensForWhitespace(value);
        this.m_CommentTokens = value;
        this.m_NeedPropertyCheck = true;
      }
    }

    /// <summary>Returns <see langword="True" /> if there are no non-blank, non-comment lines between the current cursor position and the end of the file.</summary>
    /// <returns>
    /// <see langword="True" /> if there is no more data to read; otherwise, <see langword="False" />.</returns>
    public bool EndOfData
    {
      get
      {
        if (this.m_EndOfData)
          return this.m_EndOfData;
        if (this.m_Reader == null | this.m_Buffer == null)
        {
          this.m_EndOfData = true;
          return true;
        }
        if (this.PeekNextDataLine() != null)
          return false;
        this.m_EndOfData = true;
        return true;
      }
    }

    /// <summary>Returns the current line number, or returns -1 if no more characters are available in the stream.</summary>
    /// <returns>The current line number.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public long LineNumber
    {
      get
      {
        if (this.m_LineNumber != -1L && this.m_Reader.Peek() == -1 & this.m_Position == this.m_CharsRead)
          this.CloseReader();
        return this.m_LineNumber;
      }
    }

    /// <summary>Returns the line that caused the most recent <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> exception.</summary>
    /// <returns>The line that caused the most recent <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> exception.</returns>
    public string ErrorLine
    {
      get
      {
        return this.m_ErrorLine;
      }
    }

    /// <summary>Returns the number of the line that caused the most recent <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> exception.</summary>
    /// <returns>The number of the line that caused the most recent <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> exception.</returns>
    public long ErrorLineNumber
    {
      get
      {
        return this.m_ErrorLineNumber;
      }
    }

    /// <summary>Indicates whether the file to be parsed is delimited or fixed-width.</summary>
    /// <returns>A <see cref="P:Ported.VisualBasic.FileIO.TextFieldParser.TextFieldType" /> value that indicates whether the file to be parsed is delimited or fixed-width.</returns>
    public FieldType TextFieldType
    {
      get
      {
        return this.m_TextFieldType;
      }
      set
      {
        this.ValidateFieldTypeEnumValue(value, nameof (value));
        this.m_TextFieldType = value;
        this.m_NeedPropertyCheck = true;
      }
    }

    /// <summary>Denotes the width of each column in the text file being parsed.</summary>
    /// <returns>An integer array that contains the width of each column in the text file that is being parsed.</returns>
    /// <exception cref="T:System.ArgumentException">A width value in any location other than the last entry of the array is less than or equal to zero.</exception>
    public int[] FieldWidths
    {
      get
      {
        return this.m_FieldWidths;
      }
      set
      {
        if (value != null)
        {
          this.ValidateFieldWidthsOnInput(value);
          this.m_FieldWidthsCopy = (int[]) value.Clone();
        }
        else
          this.m_FieldWidthsCopy = (int[]) null;
        this.m_FieldWidths = value;
        this.m_NeedPropertyCheck = true;
      }
    }

    /// <summary>Defines the delimiters for a text file. </summary>
    /// <returns>A string array that contains all of the field delimiters for the <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object.</returns>
    /// <exception cref="T:System.ArgumentException">A delimiter value is set to a newline character, an empty string, or <see langword="Nothing" />.</exception>
    public string[] Delimiters
    {
      get
      {
        return this.m_Delimiters;
      }
      set
      {
        if (value != null)
        {
          this.ValidateDelimiters(value);
          this.m_DelimitersCopy = (string[]) value.Clone();
        }
        else
          this.m_DelimitersCopy = (string[]) null;
        this.m_Delimiters = value;
        this.m_NeedPropertyCheck = true;
        this.m_BeginQuotesRegex = (Regex) null;
      }
    }

    /// <summary>Sets the delimiters for the reader to the specified values, and sets the field type to <see langword="Delimited" />.</summary>
    /// <param name="delimiters">Array of type <see langword="String" />. </param>
    /// <exception cref="T:System.ArgumentException">A delimiter is zero-length.</exception>
    public void SetDelimiters(params string[] delimiters)
    {
      this.Delimiters = delimiters;
    }

    /// <summary>Sets the delimiters for the reader to the specified values.</summary>
    /// <param name="fieldWidths">Array of <see langword="Integer" />. </param>
    public void SetFieldWidths(params int[] fieldWidths)
    {
      this.FieldWidths = fieldWidths;
    }

    /// <summary>Indicates whether leading and trailing white space should be trimmed from field values.</summary>
    /// <returns>
    /// <see langword="True" /> if leading and trailing white space should be trimmed from field values; otherwise, <see langword="False" />.</returns>
    public bool TrimWhiteSpace
    {
      get
      {
        return this.m_TrimWhiteSpace;
      }
      set
      {
        this.m_TrimWhiteSpace = value;
      }
    }

    /// <summary>Returns the current line as a string and advances the cursor to the next line.</summary>
    /// <returns>The current line from the file or stream.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string ReadLine()
    {
      if (this.m_Reader == null | this.m_Buffer == null)
        return (string) null;
      string str = this.ReadNextLine(ref this.m_Position, new TextFieldParser.ChangeBufferFunction(this.ReadToBuffer));
      if (str == null)
      {
        this.FinishReading();
        return (string) null;
      }
      checked { ++this.m_LineNumber; }
      return str.TrimEnd('\r', '\n');
    }

    /// <summary>Reads all fields on the current line, returns them as an array of strings, and advances the cursor to the next line containing data.</summary>
    /// <returns>An array of strings that contains field values for the current line.</returns>
    /// <exception cref="T:Ported.VisualBasic.FileIO.MalformedLineException">A field cannot be parsed by using the specified format.</exception>
    public string[] ReadFields()
    {
      if (this.m_Reader == null | this.m_Buffer == null)
        return (string[]) null;
      this.ValidateReadyToRead();
      switch (this.m_TextFieldType)
      {
        case FieldType.Delimited:
          return this.ParseDelimitedLine();
        case FieldType.FixedWidth:
          return this.ParseFixedWidthLine();
        default:
          return (string[]) null;
      }
    }

    /// <summary>Reads the specified number of characters without advancing the cursor.</summary>
    /// <param name="numberOfChars">
    /// <see langword="Int32" />. Number of characters to read. Required. </param>
    /// <returns>A string that contains the specified number of characters read.</returns>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="numberOfChars" /> is less than 0.</exception>
    public string PeekChars(int numberOfChars)
    {
      if (numberOfChars <= 0)
        throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (numberOfChars), "TextFieldParser_NumberOfCharsMustBePositive", nameof (numberOfChars));
      if (this.m_Reader == null | this.m_Buffer == null)
        return (string) null;
      if (this.m_EndOfData)
        return (string) null;
      string str1 = this.PeekNextDataLine();
      if (str1 == null)
      {
        this.m_EndOfData = true;
        return (string) null;
      }
      string str2 = str1.TrimEnd('\r', '\n');
      if (str2.Length < numberOfChars)
        return str2;
      return new StringInfo(str2).SubstringByTextElements(0, numberOfChars);
    }

    /// <summary>Reads the remainder of the text file and returns it as a string.</summary>
    /// <returns>The remaining text from the file or stream.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public string ReadToEnd()
    {
      if (this.m_Reader == null | this.m_Buffer == null)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder(this.m_Buffer.Length);
      stringBuilder.Append(this.m_Buffer, this.m_Position, checked (this.m_CharsRead - this.m_Position));
      stringBuilder.Append(this.m_Reader.ReadToEnd());
      this.FinishReading();
      return stringBuilder.ToString();
    }

    /// <summary>Denotes whether fields are enclosed in quotation marks when a delimited file is being parsed.</summary>
    /// <returns>
    /// <see langword="True" /> if fields are enclosed in quotation marks; otherwise, <see langword="False" />.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool HasFieldsEnclosedInQuotes
    {
      get
      {
        return this.m_HasFieldsEnclosedInQuotes;
      }
      set
      {
        this.m_HasFieldsEnclosedInQuotes = value;
      }
    }

    /// <summary>Closes the current <see langword="TextFieldParser" /> object.</summary>
    public void Close()
    {
      this.CloseReader();
    }

    /// <summary>Releases resources used by the <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object.</summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>Releases resources used by the <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object.</summary>
    /// <param name="disposing">Boolean. <see langword="True" /> releases both managed and unmanaged resources; <see langword="False" /> releases only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (!this.m_Disposed)
        this.Close();
      this.m_Disposed = true;
    }

    private void ValidateFieldTypeEnumValue(FieldType value, string paramName)
    {
      switch (value)
      {
        case FieldType.Delimited:
        case FieldType.FixedWidth:
          break;
        default:
          throw new InvalidEnumArgumentException(paramName, (int) value, typeof (FieldType));
      }
    }

    /// <summary>Allows the <see cref="T:Ported.VisualBasic.FileIO.TextFieldParser" /> object to attempt to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
    ~TextFieldParser()
    {
      this.Dispose(false);
      // ISSUE: explicit finalizer call
      //base.Finalize();
    }

    private void CloseReader()
    {
      this.FinishReading();
      if (this.m_Reader == null)
        return;
      if (!this.m_LeaveOpen)
        this.m_Reader.Close();
      this.m_Reader = (TextReader) null;
    }

    private void FinishReading()
    {
      this.m_LineNumber = -1L;
      this.m_EndOfData = true;
      this.m_Buffer = (char[]) null;
      this.m_DelimiterRegex = (Regex) null;
      this.m_BeginQuotesRegex = (Regex) null;
    }

    private void InitializeFromPath(string path, Encoding defaultEncoding, bool detectEncoding)
    {
      if (Operators.CompareString(path, "", false) == 0)
        throw ExceptionUtils.GetArgumentNullException(nameof (path));
      if (defaultEncoding == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (defaultEncoding));
      this.m_Reader = (TextReader) new StreamReader((Stream) new FileStream(this.ValidatePath(path), FileMode.Open, FileAccess.Read, FileShare.ReadWrite), defaultEncoding, detectEncoding);
      this.ReadToBuffer();
    }

    private void InitializeFromStream(Stream stream, Encoding defaultEncoding, bool detectEncoding)
    {
      if (stream == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (stream));
      if (!stream.CanRead)
        throw ExceptionUtils.GetArgumentExceptionWithArgName(nameof (stream), "TextFieldParser_StreamNotReadable", nameof (stream));
      if (defaultEncoding == null)
        throw ExceptionUtils.GetArgumentNullException(nameof (defaultEncoding));
      this.m_Reader = (TextReader) new StreamReader(stream, defaultEncoding, detectEncoding);
      this.ReadToBuffer();
    }

    private string ValidatePath(string path)
    {
      string path1 = FileSystem.NormalizeFilePath(path, nameof (path));
      if (!File.Exists(path1))
        throw new FileNotFoundException(Utils.GetResourceString("IO_FileNotFound_Path", new string[1]
        {
          path1
        }));
      return path1;
    }

    private bool IgnoreLine(string line)
    {
      if (line == null)
        return false;
      string str = line.Trim();
      if (str.Length == 0)
        return true;
      if (this.m_CommentTokens != null)
      {
        string[] commentTokens = this.m_CommentTokens;
        int index = 0;
        while (index < commentTokens.Length)
        {
          string Left = commentTokens[index];
          if (Operators.CompareString(Left, "", false) != 0 && (str.StartsWith(Left, StringComparison.Ordinal) || line.StartsWith(Left, StringComparison.Ordinal)))
            return true;
          checked { ++index; }
        }
      }
      return false;
    }

    private int ReadToBuffer()
    {
      this.m_Position = 0;
      int count = this.m_Buffer.Length;
      if (count > 4096)
      {
        count = 4096;
        this.m_Buffer = new char[checked (count - 1 + 1)];
      }
      this.m_CharsRead = this.m_Reader.Read(this.m_Buffer, 0, count);
      return this.m_CharsRead;
    }

    private int SlideCursorToStartOfBuffer()
    {
      if (this.m_Position <= 0)
        return 0;
      int length = this.m_Buffer.Length;
      int num1 = checked (this.m_CharsRead - this.m_Position);
      char[] buffer = new char[checked (length - 1 + 1)];
      Array.Copy((Array) this.m_Buffer, this.m_Position, (Array) buffer, 0, num1);
      int num2 = this.m_Reader.Read(buffer, num1, checked (length - num1));
      this.m_CharsRead = checked (num1 + num2);
      this.m_Position = 0;
      this.m_Buffer = buffer;
      return num2;
    }

    private int IncreaseBufferSize()
    {
      this.m_PeekPosition = this.m_CharsRead;
      int num1 = checked (this.m_Buffer.Length + 4096);
      if (num1 > this.m_MaxBufferSize)
        throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_BufferExceededMaxSize");
      char[] buffer = new char[checked (num1 - 1 + 1)];
      Array.Copy((Array) this.m_Buffer, (Array) buffer, this.m_Buffer.Length);
      int num2 = this.m_Reader.Read(buffer, this.m_Buffer.Length, 4096);
      this.m_Buffer = buffer;
      checked { this.m_CharsRead += num2; }
      return num2;
    }

    private string ReadNextDataLine()
    {
      TextFieldParser.ChangeBufferFunction ChangeBuffer = new TextFieldParser.ChangeBufferFunction(this.ReadToBuffer);
      string line;
      do
      {
        line = this.ReadNextLine(ref this.m_Position, ChangeBuffer);
        checked { ++this.m_LineNumber; }
      }
      while (this.IgnoreLine(line));
      if (line == null)
        this.CloseReader();
      return line;
    }

    private string PeekNextDataLine()
    {
      TextFieldParser.ChangeBufferFunction ChangeBuffer = new TextFieldParser.ChangeBufferFunction(this.IncreaseBufferSize);
      this.SlideCursorToStartOfBuffer();
      this.m_PeekPosition = 0;
      string line;
      do
      {
        line = this.ReadNextLine(ref this.m_PeekPosition, ChangeBuffer);
      }
      while (this.IgnoreLine(line));
      return line;
    }

    private string ReadNextLine(ref int Cursor, TextFieldParser.ChangeBufferFunction ChangeBuffer)
    {
      if (Cursor == this.m_CharsRead && ChangeBuffer() == 0)
        return (string) null;
      StringBuilder stringBuilder = (StringBuilder) null;
      do
      {
        int num1 = Cursor;
        int num2 = checked (this.m_CharsRead - 1);
        int index = num1;
        while (index <= num2)
        {
          char ch = this.m_Buffer[index];
          if (Operators.CompareString(Conversions.ToString(ch), "\r", false) == 0 | Operators.CompareString(Conversions.ToString(ch), "\n", false) == 0)
          {
            if (stringBuilder != null)
            {
              stringBuilder.Append(this.m_Buffer, Cursor, checked (index - Cursor + 1));
            }
            else
            {
              stringBuilder = new StringBuilder(checked (index + 1));
              stringBuilder.Append(this.m_Buffer, Cursor, checked (index - Cursor + 1));
            }
            Cursor = checked (index + 1);
            if (Operators.CompareString(Conversions.ToString(ch), "\r", false) == 0)
            {
              if (Cursor < this.m_CharsRead)
              {
                if (Operators.CompareString(Conversions.ToString(this.m_Buffer[Cursor]), "\n", false) == 0)
                {
                  checked { ++Cursor; }
                  stringBuilder.Append("\n");
                }
              }
              else if (ChangeBuffer() > 0 && Operators.CompareString(Conversions.ToString(this.m_Buffer[Cursor]), "\n", false) == 0)
              {
                checked { ++Cursor; }
                stringBuilder.Append("\n");
              }
            }
            return stringBuilder.ToString();
          }
          checked { ++index; }
        }
        int charCount = checked (this.m_CharsRead - Cursor);
        if (stringBuilder == null)
          stringBuilder = new StringBuilder(checked (charCount + 10));
        stringBuilder.Append(this.m_Buffer, Cursor, charCount);
      }
      while (ChangeBuffer() > 0);
      return stringBuilder.ToString();
    }

    private string[] ParseDelimitedLine()
    {
      string str1 = this.ReadNextDataLine();
      if (str1 == null)
        return (string[]) null;
      long lineNumber = checked (this.m_LineNumber - 1L);
      int num = 0;
      List<string> stringList = new List<string>();
      int endOfLineIndex = this.GetEndOfLineIndex(str1);
      while (num <= endOfLineIndex)
      {
        Match match1 = (Match) null;
        bool flag = false;
        if (this.m_HasFieldsEnclosedInQuotes)
        {
          match1 = this.BeginQuotesRegex.Match(str1, num);
          flag = match1.Success;
        }
        if (flag)
        {
          int StartAt = checked (match1.Index + match1.Length);
          QuoteDelimitedFieldBuilder delimitedFieldBuilder = new QuoteDelimitedFieldBuilder(this.m_DelimiterWithEndCharsRegex, this.m_SpaceChars);
          delimitedFieldBuilder.BuildField(str1, StartAt);
          if (delimitedFieldBuilder.MalformedLine)
          {
            this.m_ErrorLine = str1.TrimEnd('\r', '\n');
            this.m_ErrorLineNumber = lineNumber;
            throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MalFormedDelimitedLine", new string[1]
            {
              lineNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)
            }), lineNumber);
          }
          string str2;
          if (delimitedFieldBuilder.FieldFinished)
          {
            str2 = delimitedFieldBuilder.Field;
            num = checked (delimitedFieldBuilder.Index + delimitedFieldBuilder.DelimiterLength);
          }
          else
          {
            do
            {
              int length = str1.Length;
              string str3 = this.ReadNextDataLine();
              if (str3 == null)
              {
                this.m_ErrorLine = str1.TrimEnd('\r', '\n');
                this.m_ErrorLineNumber = lineNumber;
                throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MalFormedDelimitedLine", new string[1]
                {
                  lineNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)
                }), lineNumber);
              }
              if (checked (str1.Length + str3.Length) > this.m_MaxLineSize)
              {
                this.m_ErrorLine = str1.TrimEnd('\r', '\n');
                this.m_ErrorLineNumber = lineNumber;
                throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MaxLineSizeExceeded", new string[1]
                {
                  lineNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)
                }), lineNumber);
              }
              str1 += str3;
              endOfLineIndex = this.GetEndOfLineIndex(str1);
              delimitedFieldBuilder.BuildField(str1, length);
              if (delimitedFieldBuilder.MalformedLine)
              {
                this.m_ErrorLine = str1.TrimEnd('\r', '\n');
                this.m_ErrorLineNumber = lineNumber;
                throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MalFormedDelimitedLine", new string[1]
                {
                  lineNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)
                }), lineNumber);
              }
            }
            while (!delimitedFieldBuilder.FieldFinished);
            str2 = delimitedFieldBuilder.Field;
            num = checked (delimitedFieldBuilder.Index + delimitedFieldBuilder.DelimiterLength);
          }
          if (this.m_TrimWhiteSpace)
            str2 = str2.Trim();
          stringList.Add(str2);
        }
        else
        {
          Match match2 = this.m_DelimiterRegex.Match(str1, num);
          if (match2.Success)
          {
            string str2 = str1.Substring(num, checked (match2.Index - num));
            if (this.m_TrimWhiteSpace)
              str2 = str2.Trim();
            stringList.Add(str2);
            num = checked (match2.Index + match2.Length);
          }
          else
          {
            string str2 = str1.Substring(num).TrimEnd('\r', '\n');
            if (this.m_TrimWhiteSpace)
              str2 = str2.Trim();
            stringList.Add(str2);
            break;
          }
        }
      }
      return stringList.ToArray();
    }

    private string[] ParseFixedWidthLine()
    {
      string str = this.ReadNextDataLine();
      if (str == null)
        return (string[]) null;
      StringInfo Line = new StringInfo(str.TrimEnd('\r', '\n'));
      this.ValidateFixedWidthLine(Line, checked (this.m_LineNumber - 1L));
      int Index = 0;
      int num1 = checked (this.m_FieldWidths.Length - 1);
      string[] strArray = new string[checked (num1 + 1)];
      int num2 = 0;
      int num3 = num1;
      int index = num2;
      while (index <= num3)
      {
        strArray[index] = this.GetFixedWidthField(Line, Index, this.m_FieldWidths[index]);
        checked { Index += this.m_FieldWidths[index]; }
        checked { ++index; }
      }
      return strArray;
    }

    private string GetFixedWidthField(StringInfo Line, int Index, int FieldLength)
    {
      string str;
      if (FieldLength > 0)
        str = Line.SubstringByTextElements(Index, FieldLength);
      else if (Index >= Line.LengthInTextElements)
        str = string.Empty;
      else
        str = Line.SubstringByTextElements(Index).TrimEnd('\r', '\n');
      if (this.m_TrimWhiteSpace)
        return str.Trim();
      return str;
    }

    private int GetEndOfLineIndex(string Line)
    {
      int length = Line.Length;
      if (length == 1)
        return length;
      if (Operators.CompareString(Conversions.ToString(Line[checked (length - 2)]), "\r", false) == 0 | Operators.CompareString(Conversions.ToString(Line[checked (length - 2)]), "\n", false) == 0)
        return checked (length - 2);
      if (Operators.CompareString(Conversions.ToString(Line[checked (length - 1)]), "\r", false) == 0 | Operators.CompareString(Conversions.ToString(Line[checked (length - 1)]), "\n", false) == 0)
        return checked (length - 1);
      return length;
    }

    private void ValidateFixedWidthLine(StringInfo Line, long LineNumber)
    {
      if (Line.LengthInTextElements < this.m_LineLength)
      {
        this.m_ErrorLine = Line.String;
        this.m_ErrorLineNumber = checked (this.m_LineNumber - 1L);
        throw new MalformedLineException(Utils.GetResourceString("TextFieldParser_MalFormedFixedWidthLine", new string[1]
        {
          LineNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)
        }), LineNumber);
      }
    }

    private void ValidateFieldWidths()
    {
      if (this.m_FieldWidths == null)
        throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_FieldWidthsNothing");
      if (this.m_FieldWidths.Length == 0)
        throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_FieldWidthsNothing");
      int index1 = checked (this.m_FieldWidths.Length - 1);
      this.m_LineLength = 0;
      int num1 = 0;
      int num2 = checked (index1 - 1);
      int index2 = num1;
      while (index2 <= num2)
      {
        checked { this.m_LineLength += this.m_FieldWidths[index2]; }
        checked { ++index2; }
      }
      if (this.m_FieldWidths[index1] <= 0)
        return;
      checked { this.m_LineLength += this.m_FieldWidths[index1]; }
    }

    private void ValidateFieldWidthsOnInput(int[] Widths)
    {
      int num1 = checked (Widths.Length - 1);
      int num2 = 0;
      int num3 = checked (num1 - 1);
      int index = num2;
      while (index <= num3)
      {
        if (Widths[index] < 1)
          throw ExceptionUtils.GetArgumentExceptionWithArgName("FieldWidths", "TextFieldParser_FieldWidthsMustPositive", "FieldWidths");
        checked { ++index; }
      }
    }

    private void ValidateAndEscapeDelimiters()
    {
      if (this.m_Delimiters == null)
        throw ExceptionUtils.GetArgumentExceptionWithArgName("Delimiters", "TextFieldParser_DelimitersNothing", "Delimiters");
      if (this.m_Delimiters.Length == 0)
        throw ExceptionUtils.GetArgumentExceptionWithArgName("Delimiters", "TextFieldParser_DelimitersNothing", "Delimiters");
      int length = this.m_Delimiters.Length;
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append(this.EndQuotePattern + "(");
      int num1 = 0;
      int num2 = checked (length - 1);
      int index = num1;
      while (index <= num2)
      {
        if (this.m_Delimiters[index] != null)
        {
          if (this.m_HasFieldsEnclosedInQuotes && this.m_Delimiters[index].IndexOf('"') > -1)
            throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_IllegalDelimiter");
          string str = Regex.Escape(this.m_Delimiters[index]);
          stringBuilder1.Append(str + "|");
          stringBuilder2.Append(str + "|");
        }
        checked { ++index; }
      }
      this.m_SpaceChars = this.WhitespaceCharacters;
      this.m_DelimiterRegex = new Regex(stringBuilder1.ToString(0, checked (stringBuilder1.Length - 1)), RegexOptions.CultureInvariant);
      stringBuilder1.Append("\r|\n");
      this.m_DelimiterWithEndCharsRegex = new Regex(stringBuilder1.ToString(), RegexOptions.CultureInvariant);
      stringBuilder2.Append("\r|\n)|\"$");
    }

    private void ValidateReadyToRead()
    {
      if (!(this.m_NeedPropertyCheck | this.ArrayHasChanged()))
        return;
      switch (this.m_TextFieldType)
      {
        case FieldType.Delimited:
          this.ValidateAndEscapeDelimiters();
          break;
        case FieldType.FixedWidth:
          this.ValidateFieldWidths();
          break;
      }
      if (this.m_CommentTokens != null)
      {
        string[] commentTokens = this.m_CommentTokens;
        int index = 0;
        while (index < commentTokens.Length)
        {
          string Left = commentTokens[index];
          if (Operators.CompareString(Left, "", false) != 0 && this.m_HasFieldsEnclosedInQuotes & this.m_TextFieldType == FieldType.Delimited && string.Compare(Left.Trim(), "\"", StringComparison.Ordinal) == 0)
            throw ExceptionUtils.GetInvalidOperationException("TextFieldParser_InvalidComment");
          checked { ++index; }
        }
      }
      this.m_NeedPropertyCheck = false;
    }

    private void ValidateDelimiters(string[] delimiterArray)
    {
      if (delimiterArray == null)
        return;
      string[] strArray = delimiterArray;
      int index = 0;
      while (index < strArray.Length)
      {
        string Left = strArray[index];
        if (Operators.CompareString(Left, "", false) == 0)
          throw ExceptionUtils.GetArgumentExceptionWithArgName("Delimiters", "TextFieldParser_DelimiterNothing", "Delimiters");
        if (Left.IndexOfAny(new char[2]{ '\r', '\n' }) > -1)
          throw ExceptionUtils.GetArgumentExceptionWithArgName("Delimiters", "TextFieldParser_EndCharsInDelimiter");
        checked { ++index; }
      }
    }

    private bool ArrayHasChanged()
    {
      switch (this.m_TextFieldType)
      {
        case FieldType.Delimited:
          if (this.m_Delimiters == null)
            return false;
          int lowerBound1 = this.m_DelimitersCopy.GetLowerBound(0);
          int upperBound1 = this.m_DelimitersCopy.GetUpperBound(0);
          int num1 = lowerBound1;
          int num2 = upperBound1;
          int index1 = num1;
          while (index1 <= num2)
          {
            if (Operators.CompareString(this.m_Delimiters[index1], this.m_DelimitersCopy[index1], false) != 0)
              return true;
            checked { ++index1; }
          }
          break;
        case FieldType.FixedWidth:
          if (this.m_FieldWidths == null)
            return false;
          int lowerBound2 = this.m_FieldWidthsCopy.GetLowerBound(0);
          int upperBound2 = this.m_FieldWidthsCopy.GetUpperBound(0);
          int num3 = lowerBound2;
          int num4 = upperBound2;
          int index2 = num3;
          while (index2 <= num4)
          {
            if (this.m_FieldWidths[index2] != this.m_FieldWidthsCopy[index2])
              return true;
            checked { ++index2; }
          }
          break;
      }
      return false;
    }

    private void CheckCommentTokensForWhitespace(string[] tokens)
    {
      if (tokens == null)
        return;
      string[] strArray = tokens;
      int index = 0;
      while (index < strArray.Length)
      {
        if (this.m_WhiteSpaceRegEx.IsMatch(strArray[index]))
          throw ExceptionUtils.GetArgumentExceptionWithArgName("CommentTokens", "TextFieldParser_WhitespaceInToken");
        checked { ++index; }
      }
    }

    private Regex BeginQuotesRegex
    {
      get
      {
        if (this.m_BeginQuotesRegex == null)
          this.m_BeginQuotesRegex = new Regex(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "\\G[{0}]*\"", new object[1]
          {
            (object) this.WhitespacePattern
          }), RegexOptions.CultureInvariant);
        return this.m_BeginQuotesRegex;
      }
    }

    private string EndQuotePattern
    {
      get
      {
        return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "\"[{0}]*", new object[1]
        {
          (object) this.WhitespacePattern
        });
      }
    }

    private string WhitespaceCharacters
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        int[] whitespaceCodes = this.m_WhitespaceCodes;
        int index = 0;
        while (index < whitespaceCodes.Length)
        {
          char testCharacter = Strings.ChrW(whitespaceCodes[index]);
          if (!this.CharacterIsInDelimiter(testCharacter))
            stringBuilder.Append(testCharacter);
          checked { ++index; }
        }
        return stringBuilder.ToString();
      }
    }

    private string WhitespacePattern
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        int[] whitespaceCodes = this.m_WhitespaceCodes;
        int index = 0;
        while (index < whitespaceCodes.Length)
        {
          int CharCode = whitespaceCodes[index];
          if (!this.CharacterIsInDelimiter(Strings.ChrW(CharCode)))
            stringBuilder.Append("\\u" + CharCode.ToString("X4", (IFormatProvider) CultureInfo.InvariantCulture));
          checked { ++index; }
        }
        return stringBuilder.ToString();
      }
    }

    private bool CharacterIsInDelimiter(char testCharacter)
    {
      string[] delimiters = this.m_Delimiters;
      int index = 0;
      while (index < delimiters.Length)
      {
        if (delimiters[index].IndexOf(testCharacter) > -1)
          return true;
        checked { ++index; }
      }
      return false;
    }

    private delegate int ChangeBufferFunction();
  }
}
