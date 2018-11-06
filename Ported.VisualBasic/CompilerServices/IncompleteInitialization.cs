// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.IncompleteInitialization
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>The Visual Basic compiler uses this class during static local initialization; it is not meant to be called directly from your code. An exception of this type is thrown if a static local variable fails to initialize.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Serializable]
  public sealed class IncompleteInitialization : Exception
  {
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    private IncompleteInitialization(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.CompilerServices.IncompleteInitialization" /> class.</summary>
    /// <param name="message">A string representing the message to be sent.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public IncompleteInitialization(string message)
      : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.CompilerServices.IncompleteInitialization" /> class.</summary>
    /// <param name="message">A string representing the message to be sent.</param>
    /// <param name="innerException">An <see cref="T:System.Exception" /> object.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public IncompleteInitialization(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:Ported.VisualBasic.CompilerServices.IncompleteInitialization" /> class.</summary>
    public IncompleteInitialization()
    {
    }
  }
}
