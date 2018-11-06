// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.InternalErrorException
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>The exception thrown for internal Visual Basic compiler errors. </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Serializable]
  public sealed class InternalErrorException : Exception
  {
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    private InternalErrorException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.CompilerServices.InternalErrorException" /> class, specifying an error message. </summary>
    /// <param name="message">The message that describes the error.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public InternalErrorException(string message)
      : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.CompilerServices.InternalErrorException" /> class, specifying an error message and an inner exception.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current internal exception.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public InternalErrorException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.CompilerServices.InternalErrorException" /> class. </summary>
    public InternalErrorException()
      : base(Utils.GetResourceString("InternalError"))
    {
    }
  }
}
