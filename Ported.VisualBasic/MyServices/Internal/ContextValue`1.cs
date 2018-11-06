// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.MyServices.Internal.ContextValue`1
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

/*

using System;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Ported.VisualBasic.MyServices.Internal
{
  /// <summary>This class supports <see langword="My" /> in Visual Basic.</summary>
  /// <typeparam name="T">The type of the object to store.</typeparam>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class ContextValue<T>
  {
    private string m_ContextKey;

    /// <summary>This class supports <see langword="My" /> in Visual Basic.</summary>
    public ContextValue()
    {
      this.m_ContextKey = Guid.NewGuid().ToString();
    }

    /// <summary>This property supports <see langword="My" /> in Visual Basic.</summary>
    /// <returns>The value associated with this class.</returns>
    public T Value
    {
      get
      {
        HttpContext current = HttpContext.Current;
        if (current != null)
          return (T) current.Items[(object) this.m_ContextKey];
        return (T) CallContext.GetData(this.m_ContextKey);
      }
      set
      {
        HttpContext current = HttpContext.Current;
        if (current != null)
          current.Items[(object) this.m_ContextKey] = (object) value;
        else
          CallContext.SetData(this.m_ContextKey, (object) value);
      }
    }
  }
}

*/