// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.CantStartSingleInstanceException
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>This exception is thrown when a subsequent instance of a single-instance application is unable to connect to the first application instance.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Serializable]
  public class CantStartSingleInstanceException : Exception
  {
    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.CantStartSingleInstanceException" /> class.</summary>
    public CantStartSingleInstanceException()
      : base(Utils.GetResourceString("AppModel_SingleInstanceCantConnect"))
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.CantStartSingleInstanceException" /> class with a specified error message.</summary>
    /// <param name="message">A message that describes the error.</param>
    public CantStartSingleInstanceException(string message)
      : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.CantStartSingleInstanceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">A <see cref="T:System.String" /> object describing the error.</param>
    /// <param name="inner">The <see cref="T:System.Exception" /> object that is the cause of the current exception. If the <see cref="P:System.Exception.InnerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="Catch" /> block that handles the inner exception.</param>
    public CantStartSingleInstanceException(string message, Exception inner)
      : base(message, inner)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.CantStartSingleInstanceException" /> class with serialized data.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected CantStartSingleInstanceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
