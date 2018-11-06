// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.ApplicationServices.NoStartupFormException
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using Ported.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ported.VisualBasic.ApplicationServices
{
  /// <summary>This exception is thrown by the Visual Basic Application Model when the <see cref="P:Ported.VisualBasic.ApplicationServices.WindowsFormsApplicationBase.MainForm" /> property has not been set.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Serializable]
  public class NoStartupFormException : Exception
  {
    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.NoStartupFormException" /> class.</summary>
    public NoStartupFormException()
      : base(Utils.GetResourceString("AppModel_NoStartupForm"))
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.NoStartupFormException" /> class with a specified error message.</summary>
    /// <param name="message">A message that describes the error.</param>
    public NoStartupFormException(string message)
      : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.NoStartupFormException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">A <see cref="T:System.String" /> object describing the error.</param>
    /// <param name="inner">The <see cref="T:System.Exception" /> object that is the cause of the current exception. If the <see cref="P:System.Exception.InnerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a catch block that handles the inner exception.</param>
    public NoStartupFormException(string message, Exception inner)
      : base(message, inner)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.ApplicationServices.NoStartupFormException" /> class with serialized data.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected NoStartupFormException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
