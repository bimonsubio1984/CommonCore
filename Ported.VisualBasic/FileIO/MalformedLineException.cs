// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.FileIO.MalformedLineException
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Ported.VisualBasic.FileIO
{
  /// <summary>The exception that is thrown when the <see cref="M:Ported.VisualBasic.FileIO.TextFieldParser.ReadFields" /> method cannot parse a row using the specified format.</summary>
  [Serializable]
  public class MalformedLineException : Exception
  {
    private long m_LineNumber;
    private const string LINE_NUMBER_PROPERTY = "LineNumber";

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> class.</summary>
    public MalformedLineException()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> class with a specified error message and a line number.</summary>
    /// <param name="message">The message for the exception.</param>
    /// <param name="lineNumber">The line number of the malformed line.</param>
    public MalformedLineException(string message, long lineNumber)
      : base(message)
    {
      this.m_LineNumber = lineNumber;
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> class with a specified error message.</summary>
    /// <param name="message">A message that describes the error.</param>
    public MalformedLineException(string message)
      : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> class with a specified error message, a line number, and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The message for the exception.</param>
    /// <param name="lineNumber">The line number of the malformed line.</param>
    /// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception. If the <see cref="P:System.Exception.InnerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a catch block that handles the inner exception.</param>
    public MalformedLineException(string message, long lineNumber, Exception innerException)
      : base(message, innerException)
    {
      this.m_LineNumber = lineNumber;
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">A <see cref="T:System.String" /> describing the error.</param>
    /// <param name="innerException">The <see cref="T:System.Exception" /> object that is the cause of the current exception. If the <see cref="P:System.Exception.InnerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a catch block that handles the inner exception.</param>
    public MalformedLineException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.FileIO.MalformedLineException" /> class with serialized data.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains contextual information about the source or destination.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected MalformedLineException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info != null)
        this.m_LineNumber = (long) info.GetInt32(nameof (LineNumber));
      else
        this.m_LineNumber = -1L;
    }

    /// <summary>Gets the line number of the malformed line.</summary>
    /// <returns>The line number of the malformed line.</returns>
    [EditorBrowsable(EditorBrowsableState.Always)]
    public long LineNumber
    {
      get
      {
        return this.m_LineNumber;
      }
      set
      {
        this.m_LineNumber = value;
      }
    }

    /// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with information about the exception.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains contextual information about the source or destination.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info?.AddValue("LineNumber", (object) this.m_LineNumber, typeof (long));
      base.GetObjectData(info, context);
    }

    /// <summary>Creates and returns a string representation of the current exception. </summary>
    /// <returns>A string representation of the current exception.</returns>
    public override string ToString()
    {
      return base.ToString() + " " + Utils.GetResourceString("TextFieldParser_MalformedExtraData", new string[1]
      {
        this.LineNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture)
      });
    }
  }
}
