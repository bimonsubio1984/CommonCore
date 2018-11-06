// Decompiled with JetBrains decompiler
// Type: Ported.VisualBasic.CompilerServices.StaticLocalInitFlag
// Assembly: Ported.VisualBasic, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: E3216B21-51A1-4FFE-A3E7-D02127C24FF2
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\Ported.VisualBasic\8.0.0.0__b03f5f7f11d50a3a\Ported.VisualBasic.dll

using System;
using System.ComponentModel;

namespace Ported.VisualBasic.CompilerServices
{
  /// <summary>The Visual Basic compiler uses this class internally when initializing static local members; it is not meant to be called directly from your code.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Serializable]
  public sealed class StaticLocalInitFlag
  {
    /// <summary>Returns the state of the static local member's initialization flag (initialized or not).</summary>
    public short State;
  }
}
